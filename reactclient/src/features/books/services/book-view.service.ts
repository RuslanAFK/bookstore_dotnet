export const BookViewService = {
    getBookIdFromParams: (params: any, navigate: any) => {
        const bookId: number = parseInt(params.id);
        if (bookId === undefined || isNaN(bookId)) {
            navigate("/");
        }
        return bookId;
    }
}

