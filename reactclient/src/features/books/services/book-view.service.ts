import {NavigateFunction, Params} from "react-router-dom";

export const BookViewService = {
    getBookIdFromParams: (params: Readonly<Params>, navigate: NavigateFunction) => {
        const bookId: number = parseInt(params.id ?? "");
        if (bookId === undefined || isNaN(bookId)) {
            navigate("/");
        }
        return bookId;
    }
}

