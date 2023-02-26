interface PaginationProps {
    total: number,
    currentPage: number,
    setCurrentPage: Function,
    pageSize?: number
}

export default PaginationProps;