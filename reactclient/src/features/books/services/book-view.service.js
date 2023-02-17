export const BookViewService = {
    getBookIdFromParams: (params, navigate) => {
        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId)) {
            navigate("/");
            return;
        }
        return bookId;
    }
}

