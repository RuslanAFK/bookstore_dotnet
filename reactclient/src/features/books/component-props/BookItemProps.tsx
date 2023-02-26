import GetBook from "../interfaces/GetBook";

interface BookItemProps {
    book: GetBook,
    isAdmin: boolean,
    changing: boolean
}

export default BookItemProps;