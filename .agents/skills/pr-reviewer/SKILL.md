---
name: reviewer
description: Language- and framework-agnostic pull request reviewer. Reviews any codebase (any language, framework, or stack) against the ecosystem's own official documentation and widely accepted best practices rather than a fixed set of technologies. Analyzes a diff — provided directly, or generated locally with `git diff` — for requirements coverage, code quality, security, performance, logic/bug risks, and test coverage. Always produces a detailed markdown report of problems and concrete fixes as output. Use this whenever the user asks to review a branch, PR, or diff and does not want a stack-specific reviewer — e.g. "review my branch", "do a PR review", "check this diff", "review against best practices / the official docs", regardless of what language or framework the code is in.
compatibility: Any language or framework; requires a git repository, or a diff supplied directly, to review. No `gh` CLI or GitHub API access is used. Web access optional, used only to confirm version-specific claims.
---

# Stack-Agnostic PR Reviewer Skill

Reviews a diff for requirements coverage, quality, security, performance, logic/bugs, and test coverage — judged against the detected technology's own docs/conventions, not fixed per-language rules.

## Inputs

- **Diff** — preferably provided directly by the caller (a file path, or pasted diff/patch text). If not provided, generate it locally (§1).
- **Requirements** — ticket/acceptance criteria, referred to as `{requirements_context}`.
- **Branch** — only needed when no diff is provided and one must be generated locally; defaults to current branch.

Placeholders: `{repos_root}` parent dir of repos, `{repo}` repo dir name, `{requirements_context}` ticket text, `{TICKET_N}` ticket id.

## 1. Diff Acquisition (run once, at start)

If the caller already supplied a diff (file or pasted text), use it as-is — skip straight to §2.

Otherwise, generate it locally with plain `git`:

**Resolve base branch** — never hardcode `main`:

```bash
cd {repos_root}/{repo}
BASE_BRANCH=$(git symbolic-ref --quiet --short refs/remotes/origin/HEAD 2>/dev/null | sed 's@^origin/@@')
BASE_BRANCH=${BASE_BRANCH:-main}
```

**Generate the diff** — three-dot (`BASE...HEAD`, from merge-base) not two-dot (includes unrelated base commits). Append branch-only commit log for context:

```bash
cd {repos_root}/{repo}
git diff "$BASE_BRANCH...HEAD" -- . > /tmp/review-diff.txt
git log "$BASE_BRANCH...HEAD" --oneline >> /tmp/review-diff.txt
```

**Clean up** when done: `rm -f /tmp/review-diff.txt`

## 2. Standards: how to judge each change

1. **Detect the technology** per changed file — extensions, imports, manifests/lockfiles (`package.json`, `go.mod`, `Cargo.toml`, `pom.xml`, `requirements.txt`, `*.csproj`, `Gemfile`), config, syntax. A PR may span several; assess each on its own terms.
2. **Default to built-in knowledge** for stable norms — language idioms, documented framework patterns, OWASP-style security guidance, general engineering principles (naming, single responsibility, error handling). Don't fetch docs to confirm what's already stable and well-known.
3. **Fetch official docs only when a claim is version/API-specific AND you're not confident** — and getting it wrong would mislead. Cite the official/primary source (not blogs/forums) in the finding. If you can't verify, downgrade to a question or drop it — never assert an unverified version-specific claim as fact.

**Anti-vagueness rule** — every finding must be specific and falsifiable:
- Cite exact file:line (see Output Format); no prose-only findings.
- State the concrete consequence (what input/state triggers it), not "could be improved."
- Recommend a concrete change, not a category ("sanitize `id` before interpolation" not "improve input handling").
- Don't invent rules — a style preference with no behavioral/security/maintainability impact is omitted or marked minor.
- Name the standard you cite ("violates PEP 8 naming", "counter to React's rules of hooks", a doc URL) — "violates best practices" alone is not acceptable.

## 3. Review Areas

Apply all six through the lens of each changed file's detected technology.

**1. Requirements coverage** — changes match ticket/acceptance criteria; no scope creep; bug fixes resolve the stated issue; features satisfy the spec; breaking changes are flagged; requirement-stated edge cases are handled. Map changes to requirements, flag gaps and over-engineering.

**2. Code quality** — idiomatic for the language/framework; function complexity & single-responsibility; ecosystem naming conventions; dead code/unused imports; duplication; comment/doc quality; type safety where supported; idiomatic error handling.

**3. Security** — input validation/sanitization; injection (SQL, command, template, LDAP, ...); XSS/output-encoding for client-rendered content; hardcoded secrets; unsafe deserialization; auth flaws; known-vulnerable/outdated deps; CSRF/CORS on web endpoints; unsafe dynamic execution (eval/exec); race conditions with security impact.

**4. Performance** — algorithmic complexity; query efficiency/indexing; memory leaks/inefficient allocation; blocking calls on latency-sensitive/async paths; unnecessary loops/repeated work; missed caching; load on hot paths; N+1 patterns (queries, network, file I/O).

**5. Logic consistency & bugs** — read changed functions in full (not just hunks), trace data entry-to-exit, cross-reference interacting files in the diff. Look for:
- State/data-flow inconsistencies (assumed state, unexpected state left for callers).
- Contract violations (signatures, return shapes, event payloads, API structures).
- Conditional gaps — missing branches, off-by-one, wrong boolean logic.
- Null/undefined/empty handling inconsistent with the rest of the codebase.
- Concurrency/ordering assumptions — races, missing locks.
- Asymmetric changes — one side of a pair changed without the other (serialize/deserialize, create/delete, open/close, acquire/release).
- Renamed/restructured entities not updated everywhere in the diff.
- Dead/unreachable code introduced.
- Silent failures — swallowed errors, false success.
- Cross-module consistency — all diff-visible consumers of a changed interface updated.

**6. Test coverage** — use the repo's existing framework/conventions, don't impose a new one. Test public APIs/integrated behavior, not internals:
- New public functions/entry points and bug fixes need unit tests (skip bug-fix tests if an existing test already covers it).
- Multi-component feature changes need integration tests; user-facing features need the project's existing E2E coverage.
- Skip private/internal helpers (covered via public-surface tests) and tests redundant with existing integration coverage.
- Check mock/stub usage (external deps and isolated units only), edge/error-case coverage on critical paths, assertion quality, and refactors-with-no-behavior-change requiring no new tests.

## 4. Severity Rubric

- 🔴 **critical** — incorrect behavior, data loss, security breach, or outage in prod. Blocks merge. E.g. injection vuln, hardcoded secret, unhandled null on a critical path, broken downstream contract.
- 🟡 **major** — likely to cause bugs/tech debt or missing required tests, not an immediate prod break. Fix before merge. E.g. missing error handling, hot-path N+1, unimplemented acceptance criterion.
- 🔵 **minor** — style, readability, naming, small redundancies, nice-to-haves. Doesn't block merge.

When unsure between two tiers, pick the lower one and explain why.

## 5. Review Process

1. Use the supplied diff, or generate one locally via `git diff` if none was given (§1).
2. Gather `{requirements_context}` if not provided.
3. Parse the diff — additions/deletions/modifications, commit messages for intent.
4. Detect technologies per file (§2.1); group by technology and logical component/domain (use the repo's own structure, not a fixed list).
5. Establish standards per technology (§2.2–2.3).
6. Map changes to requirements; identify gaps.
7. Scan every changed file against all six Review Areas.
8. Classify each finding: **severity** (critical/major/minor), **domain** (backend/frontend/infra/tests/cross-cutting — inferred from the repo), **category** (quality/security/performance/logic/tests/requirements).
9. **Self-critique pass** — drop a finding if: already handled elsewhere in the diff/codebase; rests on an unsupported assumption; is a style preference with no behavioral impact; the cited file/line doesn't actually show it on re-read; or it's an unverifiable version-specific claim. Track drop count + one-line reason each, for the "Dropped findings" section.
10. Prioritize surviving findings by severity/impact, keeping domain grouping.
11. Generate the report (§6).
12. Deliver the report directly to the user (§7) — never post it to GitHub.

## 6. Output Format

Single markdown report, organized by domain, problems/fixes only. If a domain has no issues, say so explicitly rather than omitting it.

Every finding: severity dot + full file path + line(s) — `` `path/to/file.ext:LINE` `` or `` `path/to/file.ext:START-END` ``. Never describe a finding without a file location. Append the URL in parens when a finding rests on a fetched doc.

Domain headings name the actual technologies present (e.g. "Backend findings (Go / net/http)"); omit sections for technologies not present.

```
## PR Review — {TICKET_N}

### {Domain} findings ({technologies detected in this domain})
- 🔴 [critical] `path/to/file.ext:47` — {concrete consequence and fix} (source URL if version-specific)
- 🟡 [major]    `path/to/file.ext:112-118` — {description and fix}
- 🔵 [minor]    `path/to/file.ext:23` — {description and fix}

### {Another domain} findings ({technologies})
- 🟡 [major]    `path/to/file.ext:56` — {description and fix}

### Testing findings
- 🟡 [major]    `path/to/test_file.ext` — {coverage gap and recommended test}

### Cross-cutting findings
- 🔴 [critical] `path/a.ext:12` / `path/b.ext:7` — {description, primary ownership identified}

### Dropped findings (self-critique pass)
- {dropped count}: {one-line reason per dropped candidate, or "none"}

### No-action items
{informational observations requiring no code change}
```

Per-domain depth scales with the diff: multi-technology PRs get one focused section each; single-technology PRs go deep on that one domain.

## 7. Delivering the Review

Always return the report directly in the conversation.

## 8. Completion Summary (when invoked by an orchestrator)

Return this alongside the full report, with sub-headings from the domains actually detected:

```
PR review complete.

## Findings Summary

🔴 Critical findings: {critical_count}
🟡 Major findings: {major_count}
🔵 Minor findings: {minor_count}
Dropped in self-critique pass: {drop_count}

## Findings by Domain

### {Domain detected}
- 🔴 Critical: {list}
- 🟡 Major: {list}
- 🔵 Minor: {list}

Job complete — returning full report to orchestrator.
```

## Tips

- Provide the diff directly (file path or pasted text) when you have one — it avoids a local `git diff` regeneration and is required when there's no local checkout of the branch.
- Provide ticket requirements/acceptance criteria and relevant commit/PR context for the best assessment.
- Keep the base branch current if a local diff must be generated.
- No need to declare the stack — detection handles it; do mention unusual internal conventions so findings respect them.
- Mention target framework/runtime version if version-specific behavior matters.
