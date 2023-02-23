import {Link} from "react-router-dom";
import React from "react";
import {deleteBook} from "../store/effects";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../../shared/store/store";
import GetBook from "../interfaces/GetBook";
import SpinnerButton from "../../shared/components/spinners/SpinnerButton";

type Params = {
    book: GetBook,
    isAdmin: boolean,
    changing: boolean
}

const BookItem = ({book, isAdmin, changing}: Params) => {
    const dispatch = useDispatch<AppDispatch>();

    const onBookDelete = (params: {id: number, name: string}) => {
        const {id, name} = params;
        if (window.confirm(`Do you really want to delete ${name}?`)) {
            dispatch(deleteBook(id));
        }
    }
    return (
        <li key={book.id} className="list-group-item">
            <Link to={`/book/${book.id}`}>
                <img src={book.image} alt={book.name} height={500} width={350} />
            </Link>
            <p className="text-center text-secondary">{book.name}</p>
            {isAdmin &&
                <div>
                    {changing ?
                        <div className="w-100 my-2">
                        <SpinnerButton/>
                    </div> :
                        <div>
                            <Link to={`/update/${book.id}`}>
                                <button className='w-50 my-2 btn btn-secondary'>Change</button>
                            </Link>
                            <button className='w-50 my-2 btn btn-secondary'
                                    onClick={() => onBookDelete(book)}>
                                Delete</button>
                            <Link to={`/upload-file/${book.id}`}>
                                <div className="text-center">Update Book File</div>
                            </Link>
                    </div>}
                </div>}
        </li>
    )
}

export default BookItem;