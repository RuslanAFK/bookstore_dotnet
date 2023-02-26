import "../stylesheets/Pagination.css";
import React from "react";
import PaginationProps from "../component-props/PaginationProps";

const Pagination = ({total, currentPage, setCurrentPage, pageSize=4}: PaginationProps) => {

    let pages = [];
    const pageCount = Math.ceil(total/pageSize);

    // Logic for shortening of page count
    // ...

    for (let i = 1; i <= pageCount; i++)
        pages.push(i);

    const isCurrent = (page: number) => currentPage === page;


    return (
        <div className="pagination my-3">
            {
                pages.map((page, index) =>
                    (<button
                        className={`btn btn-${isCurrent(page) ? "primary" : "secondary"}`}
                        key={index}
                        onClick={() => setCurrentPage(page)}
                    >{page}</button>))
            }
        </div>
    )

}

export default Pagination;