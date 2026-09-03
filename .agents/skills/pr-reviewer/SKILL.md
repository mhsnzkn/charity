---
name: pr-reviewer
description: Conducts multi-axis code review. Use before merging any change. Use when reviewing code written by yourself, another agent, or a human. Use when you need to assess code quality across multiple dimensions before it enters the main branch.
argument-hint: "[optional acceptance criteria or task/ticket description]"
---

# Code Review and Quality

## Overview

Multi-dimensional review with quality gates — every change gets reviewed before merge, no exceptions. Five axes: correctness, readability, architecture, security, performance.

**The approval standard:** approve when the change definitely improves overall code health, even if it isn't perfect. Perfect code doesn't exist; the goal is continuous improvement. Don't block a change because it isn't exactly how you would have written it — if it improves the codebase and follows the project's conventions, approve it.

**Stack-agnostic by design.** Nothing here assumes a language, framework, package manager, or hosting platform. Where a check names a concept (resolved-dependency file, interpreter boundary, audit tooling), translate it into the project's own idiom and conventions before applying it — and judge the code against the conventions already in the repository, not against a preferred stack.

**Optional input — acceptance criteria or task description:** pass the ticket's acceptance criteria, spec, or a short task description alongside the change (e.g. `/pr-reviewer <acceptance criteria or ticket link>`). When given, treat it as the authoritative definition of "correct" for Step 1 and the Correctness axis instead of inferring intent from the diff alone. Without it, fall back to the PR/commit description and surrounding code.

## When to Use

- Before merging any PR or change
- After completing a feature implementation
- When another agent or model produced code you need to evaluate
- When refactoring existing code
- After any bug fix (review the fix *and* the regression test)

## The Five-Axis Review

### 1. Correctness — does the code do what it claims?

- Matches the spec or task requirements
- Edge cases handled (null, empty, boundary values)
- Error paths handled, not just the happy path
- Tests pass — and are actually testing the right things
- No off-by-one errors, race conditions, or state inconsistencies

### 2. Readability & Simplicity — can another engineer or agent understand it unaided?

- Names descriptive and consistent with project conventions (no bare `temp`, `data`, `result`)
- Control flow straightforward (no nested ternaries, deep callbacks)
- Code organized logically — related code grouped, clear module boundaries
- No "clever" tricks that should be simplified
- **Could this be done in fewer lines?** 1000 lines where 100 suffice is a failure
- **Are abstractions earning their complexity?** Don't generalize until the third use case
- Comments clarify non-obvious intent only — don't comment obvious code
- No dead-code artifacts: no-op variables (`_unused`), backwards-compat shims, `// removed` comments
- **Is a new conditional bolted onto an unrelated flow?** A design smell, not a nit — push the logic into its own helper, state, or policy instead of tangling an existing path
- **Do repeated conditionals on the same shape appear?** They signal a missing model or dispatcher; a "temporary" branch is usually permanent debt

### 3. Architecture — does the change fit the system's design?

- Follows existing patterns, or justifies the new one it introduces
- Maintains clean module boundaries; dependencies flow in the right direction (no circular dependencies)
- No code duplication that should be shared
- Abstraction level appropriate — not over-engineered, not too coupled
- **Does this refactor reduce complexity or just relocate it?** Count the concepts a reader must hold to follow the change; if a "cleaner" version leaves that count unchanged, it isn't cleaner. Prefer restructuring that makes whole branches, modes, or layers disappear over one that re-centralizes the same logic, and prefer deleting an abstraction to polishing it.
- **Is feature-specific logic leaking into a shared or general-purpose module?** Keep logic in its owning layer, reuse the existing canonical helper instead of a near-duplicate, and don't normalize architectural drift.
- **Are type and data boundaries explicit?** Question escape hatches that dodge the language's own guarantees — permissive or dynamic catch-all types, unchecked casts, everything-optional signatures, untyped maps — and silent fallbacks that paper over an unclear invariant. Making the boundary explicit often makes the surrounding control flow simpler.

### 4. Security — does the change introduce vulnerabilities?

- User input validated and sanitized
- Secrets kept out of code, logs, and version control
- Authentication/authorization checked where needed
- Queries and commands sent to any interpreter (database, shell, template engine, LDAP) built from parameters or a safe API — never string concatenation
- Output encoded for the context it lands in (markup, URL, log, serialized payload) so data can't be read as code
- Dependencies from trusted sources with no known vulnerabilities
- Data from external sources (APIs, logs, user content, config files) treated as untrusted, and validated at system boundaries before use in logic or rendering

### 5. Performance — does the change introduce performance problems?

- N+1 query patterns
- Unbounded loops or unconstrained data fetching
- Synchronous operations that should be async
- Redundant repeated work on every update — re-renders, re-fetches, recomputation that could be cached or memoized
- Missing pagination on list endpoints
- Large objects created in hot paths

## Structural Remedies

When you flag a structural problem, propose the move — a review that only says "this is complex" leaves the author guessing. Reach for a named restructuring:

- **Replace a chain of conditionals** with a typed model or an explicit dispatcher
- **Collapse duplicate branches** into a single clearer flow
- **Separate orchestration from business logic** so each reads on its own
- **Move feature-specific logic** out of a shared module into the package that owns the concept
- **Reuse the canonical helper** instead of a bespoke near-duplicate
- **Make a type boundary explicit** so downstream branching disappears
- **Delete a pass-through wrapper** that adds indirection without clarifying the API
- **Extract a helper, or split a large file** into focused modules

Prefer the remedy that removes moving pieces over one that spreads the same complexity around.

## Change Sizing

Small, focused changes are easier to review, faster to merge, and safer to deploy.

```
~100 lines changed   → Good. Reviewable in one sitting.
~300 lines changed   → Acceptable if it's a single logical change.
~1000 lines changed  → Too large. Split it.
```

**Watch file size, not just diff size.** A small diff can still push a file past a healthy boundary — around 1000 *total* lines in a single file (distinct from the ~1000 *changed*-lines threshold above) is a common inspection signal, not a hard cap. When a change materially grows an already-large file, ask whether to extract helpers, subcomponents, or modules *first*, before piling more on. Decompose, then add.

**What counts as "one change":** a single self-contained modification that addresses one thing, includes related tests, and keeps the system functional after submission. One part of a feature — not the whole feature.

| Splitting strategy | How | When |
|---|---|---|
| **Stack** | Submit a small change, start the next one based on it | Sequential dependencies |
| **By file group** | Separate changes for groups needing different reviewers | Cross-cutting concerns |
| **Horizontal** | Create shared code/stubs first, then consumers | Layered architecture |
| **Vertical** | Break into smaller full-stack slices of the feature | Feature work |

**When large changes are acceptable:** complete file deletions and automated refactoring, where the reviewer only needs to verify intent, not every line.

**Separate refactoring from feature work.** A change that refactors existing code *and* adds new behavior is two changes — submit them separately. Small cleanups (variable renaming) can ride along at reviewer discretion.

## Change Descriptions

Every change needs a description that stands alone in version control history.

- **First line:** short, imperative, standalone — "Delete the FizzBuzz RPC", not "Deleting the FizzBuzz RPC". Informative enough that someone searching history understands the change without reading the diff.
- **Body:** what is changing and why — context, decisions, and reasoning not visible in the code itself. Link to bug numbers, benchmark results, or design docs where relevant. Acknowledge approach shortcomings when they exist.
- **Anti-patterns:** "Fix bug," "Fix build," "Add patch," "Moving code from A to B," "Phase 1," "Add convenience functions."

## Review Process

### Step 1: Understand the context

Before looking at code, establish intent — use the acceptance criteria or task description supplied at invocation as the spec if there is one, otherwise infer it from the PR/commit description and surrounding code:

```
- What is this change trying to accomplish?
- What spec or task does it implement?
- What is the expected behavior change?
```

### Step 2: Review the tests first

Tests reveal intent and coverage:

```
- Do tests exist for the change?
- Do they test behavior (not implementation details)?
- Are edge cases covered?
- Do tests have descriptive names?
- Would the tests catch a regression if the code changed?
```

### Step 3: Review the implementation

For each changed file, walk every bullet of the five axes above (§ The Five-Axis Review) in
order — not a general impression. Anchor correctness to what Step 2 established: does this
code do what the tests say it should?

### Step 4: Categorize findings

Label every comment with one of three severity levels, each marked with a colored button so the author can triage at a glance:

| Button | Label | Meaning | Author Action |
|--------|-------|---------|---------------|
| 🔴 | **Critical** | Blocks merge | Security vulnerability, data loss, broken functionality, correctness bug |
| 🟡 | **Major** | Should be addressed before merge | Design/architecture problems, missing or inadequate test coverage, significant readability or maintainability issues |
| 🔵 | **Minor** | Optional | Style preferences, nits, suggestions, FYI context — author may ignore |

Without labels, authors treat all feedback as mandatory and waste time on optional suggestions.

**Lead with what matters.** Order findings by leverage: correctness and security first, then structural regressions and missed simplifications, then everything else. Don't bury a real issue under cosmetic nits — a few high-conviction comments beat a long list. If you have one structural problem and ten nits, the structural problem *is* the review.

**Publishing findings:** posting to a PR or merge request is visible to others and hard to walk back. Draft the comment locally, show it to the user first, and publish via the review platform's CLI or API only after they explicitly approve the content.

### Step 5: Verify the verification

Check the author's verification story:

```
- What tests were run?
- Did the build pass?
- Was the change tested manually?
- Are there screenshots for UI changes?
- Is there a before/after comparison?
```

## Multi-Model Review Pattern

Use different models for different review perspectives — Model A writes the code → Model B reviews for correctness and architecture → Model A addresses the feedback → a human makes the final call. Different models have different blind spots, so this catches issues a single model might miss.

**Example prompt for a review agent:**

```
Review this code change for correctness, security, and adherence to
our project conventions. The spec says [X]. The change should [Y].
Flag any issues as 🔴 Critical, 🟡 Major, or 🔵 Minor.
```

## Dead Code Hygiene

After any refactoring or implementation change, identify code that is now unreachable or unused, list it explicitly, and **ask before deleting**. Don't leave dead code lying around — it confuses future readers and agents — but don't silently delete things you're not sure about. When in doubt, ask.

```
DEAD CODE IDENTIFIED:
- formatLegacyDate() in the date utilities — replaced by formatDate()
- OldTaskCard component — replaced by TaskCard
- LEGACY_API_URL constant in the config module — no remaining references
→ Safe to remove these?
```

## Review Speed

Slow reviews block entire teams. The cost of context-switching to review is less than the waiting cost imposed on others.

- **Respond within one business day** — this is the maximum, not the target. Ideally respond shortly after a review request arrives, unless deep in focused coding; a typical change should complete multiple review rounds in a single day.
- **Prioritize fast individual responses** over quick final approval. Quick feedback reduces frustration even if multiple rounds are needed.
- **Large changes:** ask the author to split them rather than reviewing one massive changeset.

## Handling Disagreements

1. **Technical facts and data** override opinions and preferences
2. **Style guides** are the absolute authority on style matters
3. **Software design** must be evaluated on engineering principles, not personal preference
4. **Codebase consistency** is acceptable if it doesn't degrade overall health

**Don't accept "I'll clean it up later."** Experience shows deferred cleanup rarely happens. Require cleanup before submission unless it's a genuine emergency. If surrounding issues can't be addressed in this change, require filing a bug with self-assignment.

## Honesty in Review

Whether the code was written by you, another agent, or a human:

- **Don't rubber-stamp.** "LGTM" without evidence of review helps no one.
- **Don't soften real issues.** "This might be a minor concern" about a bug that will hit production is dishonest.
- **Quantify problems when possible.** "This N+1 query will add ~50ms per item in the list" beats "this could be slow."
- **Push back on approaches with clear problems.** Sycophancy is a failure mode in reviews — if the implementation has issues, say so directly and propose alternatives.
- **Accept override gracefully.** If the author has full context and disagrees, defer to their judgment. Comment on code, not people — reframe personal critiques to focus on the code itself.

## Dependency Discipline

Part of code review is dependency review.

**Before adding any dependency:** does the existing stack solve this (often it does)? How large is it, and what does it cost at build or ship time (artifact size, transitive tree)? Is it actively maintained (last commit, open issues)? Any known vulnerabilities, per the ecosystem's audit or advisory tooling? Is the license compatible with the project?

**Rule:** prefer the standard library and existing utilities over new dependencies. Every dependency is a liability.

**Upgrading an existing dependency** is a code change like any other, and the riskiest upgrades are the ones merged in bulk with a message like "bump deps":

1. **Read the changelog, not just the version number.** Semver is a promise the maintainer may not have kept — a "patch" can carry a behavioral change. For a major bump, read the migration notes and find what breaks.
2. **One dependency per change.** Upgrade and merge them individually (or in small related groups). When a bulk bump breaks the build, you've lost which package did it; a single-package change makes the cause obvious and the revert clean.
3. **Let the tests decide.** The upgrade is verified by a green suite before *and* after, not by "it installed." If coverage around the dependency's behavior is thin, that gap is the real finding — add a test first.
4. **Mind the transitive graph.** Most installed packages are ones nobody chose directly. Review the resolved-dependency (lock) file's diff, not just the manifest that declares direct dependencies; a single direct bump can pull in dozens of indirect changes.
5. **Keep the resolved-dependency file honest.** Whatever the ecosystem calls it, commit it, review its diff, and never hand-edit it — it is the thing that actually pins what ships.

This section covers the upgrade *workflow* only. Triaging advisory findings and supply-chain risk (typosquatting, compromised maintainers, malicious install scripts) is a security verdict — hand it to a dedicated security review pass rather than settling it here.

## The Review Checklist

```markdown
## Review: [PR/Change title]

### Context
- [ ] I understand what this change does and why

### Correctness
- [ ] Change matches spec/task requirements
- [ ] Edge cases handled
- [ ] Error paths handled
- [ ] Tests cover the change adequately

### Readability
- [ ] Names are clear and consistent
- [ ] Logic is straightforward
- [ ] No unnecessary complexity

### Architecture
- [ ] Follows existing patterns
- [ ] No unnecessary coupling or dependencies
- [ ] Appropriate abstraction level
- [ ] Refactors reduce complexity rather than relocate it
- [ ] No feature logic in shared modules; file stays within a healthy size

### Security
- [ ] No secrets in code
- [ ] Input validated at boundaries
- [ ] No injection vulnerabilities
- [ ] Auth checks in place
- [ ] External data sources treated as untrusted

### Performance
- [ ] No N+1 patterns
- [ ] No unbounded operations
- [ ] Pagination on list endpoints

### Verification
- [ ] Tests pass
- [ ] Build succeeds
- [ ] Manual verification done (if applicable)

### Verdict
- [ ] **Approve** — Ready to merge
- [ ] **Request changes** — Issues must be addressed
```

## Common Rationalizations

| Rationalization | Reality |
|---|---|
| "It works, that's good enough" | Working code that's unreadable, insecure, or architecturally wrong creates debt that compounds. |
| "I wrote it, so I know it's correct" | Authors are blind to their own assumptions. Every change benefits from another set of eyes. |
| "We'll clean it up later" | Later never comes. The review is the quality gate — require cleanup before merge, not after (§ Handling Disagreements). |
| "AI-generated code is probably fine" | AI code needs more scrutiny, not less. It's confident and plausible, even when wrong. |
| "The tests pass, so it's good" | Tests are necessary but not sufficient — they don't catch architecture problems, security issues, or readability concerns. |
| "The refactor makes it cleaner" | Relocating complexity isn't reducing it — if the reader still holds the same number of concepts, the structure didn't improve (§ Architecture). |
| "It's only a small addition to this file" | Judge the resulting structure, not the diff size (§ Change Sizing). |
| "It's just a version bump" | A bump is a behavior change you didn't write (§ Dependency Discipline). |
| "I'll upgrade everything in one PR to save time" | A bulk bump that breaks the build hides which package did it (§ Dependency Discipline). |

## Red Flags

- PRs merged without any review, or "LGTM" without evidence of actual review (§ Honesty in Review)
- Review that only checks whether tests pass, ignoring the other axes
- Security-sensitive changes without security-focused review
- No regression test with a bug-fix PR
- Review comments without severity labels — required vs optional becomes unclear (§ Step 4)
- Large PRs that are "too big to review properly" — split them (§ Change Sizing)
- Accepting "I'll fix it later" (§ Handling Disagreements)
- A refactor that relocates complexity, or a change that grows an already-large file (§ Architecture, § Change Sizing)
- New conditionals scattered into unrelated code paths — a missing abstraction (§ Readability & Simplicity)
- A bespoke near-duplicate helper, or feature logic placed in a shared module (§ Architecture)
- A bulk "bump dependencies" PR, or a hand-edited / unreviewed resolved-dependency file (§ Dependency Discipline)

## Verification

After review is complete:

- [ ] All 🔴 Critical issues are resolved
- [ ] All 🟡 Major issues are resolved or explicitly deferred with justification
- [ ] Tests pass
- [ ] Build succeeds
- [ ] The verification story is documented (what changed, how it was verified)
- [ ] Dependency upgrades followed § Dependency Discipline (changelog read, one package per change, green suite, resolved-dependency diff reviewed)

**Presumptive blockers** — relocated complexity, undecomposed file growth, feature logic in a shared module, a near-duplicate canonical helper, a silent fallback over an unclear invariant (all stated in full under § Architecture and § Change Sizing). Surface each one with the simpler design proposed (§ Structural Remedies), and escalate to 🟡 Major only when the change actively makes structure worse.
