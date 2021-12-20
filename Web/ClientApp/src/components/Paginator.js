import React from 'react';

export default function Paginator({ totalItems, pageIndex, pageSize, pageChange }) {
    const pageNeighbours = 3; // can be added with props
    const totalPage = Math.ceil(totalItems / pageSize);
    const prevDisabled = totalPage > 1 && pageIndex !== 1 ? "" : "disabled";
    const nextDisabled = totalPage > 1 && pageIndex !== totalPage ? "" : "disabled";

    const startPage = Math.max(1, pageIndex - pageNeighbours);
    const endPage = Math.min(totalPage, pageIndex + pageNeighbours);

    let pageContent = [];
    for (let i = startPage; i <= endPage; i++) {
        pageContent.push(
            <li key={i} className={`page-item ${i === pageIndex ? "active" : ""}`}>
                <button className="page-link shadow-none" onClick={() => pageChange(i)} disabled={i === pageIndex}>{i}</button>
            </li>)
    }

    return (
        <div className='d-flex justify-content-between'>
            <div>
                {totalItems} Items/ {totalPage} pages
            </div>
            <div>
                <nav aria-label="Page navigation example">
                    <ul className="pagination">
                        <li className={`page-item ${prevDisabled}`}>
                            <button className="page-link" aria-label="Previous" onClick={() => pageChange(pageIndex - 1)}>
                                <span aria-hidden="true">&laquo;</span>
                            </button>
                        </li>

                        {pageContent}

                        <li className={`page-item ${nextDisabled}`}>
                            <button className="page-link" aria-label="Next" onClick={() => pageChange(pageIndex + 1)}>
                                <span aria-hidden="true">&raquo;</span>
                            </button>
                        </li>
                    </ul>
                </nav>

            </div>
        </div>
    );
}
