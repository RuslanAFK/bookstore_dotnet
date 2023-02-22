interface PaginatedList<T> {
    count: number,
    items: T[]
}

export default PaginatedList;