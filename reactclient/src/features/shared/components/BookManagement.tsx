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
        <div className="clickableIconBox">
            <Link to={`/update/${book.id}`} className="mx-4">
                <img src="/icons/exchange.png" className="clickableIcon" alt="Update"/>
            </Link>
            <div onClick={() => onBookDelete(book)} className="mx-4">
                <img src="/icons/delete.png" className="clickableIcon" alt="Delete"/>
            </div>
            <Link to={`/upload-file/${book.id}`} className="mx-4">
                <img src="/icons/folder.png" className="clickableIcon" alt="Manage File"/>
            </Link>
        </div>
    )
}

export default BookManagement;