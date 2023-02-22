import GetBook from "./GetBook";

interface GetBookDetails extends GetBook {
    info: string,
    genre: string,
    author: string,
    bookFile?: string
}

export default GetBookDetails;