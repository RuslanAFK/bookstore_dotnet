import {Link} from "react-router-dom";
import React from "react";
import {deleteBook} from "../../books/store/effects";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../store/store";
import GetBook from "../../books/interfaces/GetBook";

import "../stylesheets/BookManagement.css";

interface Props {
    book: GetBook
}

const BookManagement = ({book}: Props) => {

    const dispatch = useDispatch<AppDispatch>();

    const onBookDelete = (params: {id: number, name: string}) => {
        const {id, name} = params;
        if (window.confirm(`Do you really want to delete ${name}?`)) {
            dispatch(deleteBook(id));
        }
    }

    return (
        <div>
            <hr/>
            <div className="row">
                <div className="col">
                    <Link to={`/update/${book.id}`}>
                        <div className="clickableIcon">Edit</div>
                    </Link>
                </div>
                <div className="col">
                    <a onClick={() => onBookDelete(book)} className="link-primary">
                        <div className="clickableIcon">Delete</div>
                    </a>
                </div>
            </div>
            <div className="row">
                <div className="col">
                    <Link to={`/upload-file/${book.id}`}>
                        <div className="clickableIcon">Manage file</div>
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default BookManagement;