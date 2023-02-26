import {Link} from "react-router-dom";
import React from "react";
import SpinnerButton from "../../shared/components/SpinnerButton";

import "../stylesheets/BookItem.css";
import BookManagement from "../../shared/components/BookManagement";
import BookItemProps from "../component-props/BookItemProps";

const BookItem = ({book, isAdmin, changing}: BookItemProps) => {

    return (
        <li className="list-group-item">
            <Link to={`/book/${book.id}`}>
                <img src={book.image} alt={book.name} className="imageOfBook"/>
            </Link>
            <p className="text-center text-secondary">{book.name}</p>
            {isAdmin &&
                <div className="my-1">
                    {changing ?
                        <div className="w-100">
                            <SpinnerButton/>
                        </div> : <BookManagement book={book}/>
                    }
                </div>}
        </li>
    )
}

export default BookItem;