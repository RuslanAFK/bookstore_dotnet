import React from 'react';
import { Link } from "react-router-dom";
import { Button, Image } from "react-bootstrap"

const Books = ({ books, isAdmin }) => {
    return (
        <ul className="list-group list-group-horizontal-md">
            {
                books.map((book, i) =>
                (
                    <li key={book.id} className="list-group-item">
                        <div>
                            <Link to={'/view?bookId=' + book.id}>
                                <Image src={book.image} alt={book.name} height={500} width={350} />
                            </Link>
                            {isAdmin &&
                                <div>
                                    <Button className='w-50 my-2' href={"change?bookId=" + book.id}>Change</Button>
                                    <Button className='w-50 my-2' href={"delete?bookId=" + book.id}>Delete</Button>
                                </div>
                            }
                        </div>
                    </li>
                ))
            }

        </ul>
    )
}

export default Books;
