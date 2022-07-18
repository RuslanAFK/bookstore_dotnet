import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { Button, Image } from "react-bootstrap"


export default class Books extends Component {
    constructor(props) {
        super(props);
    }
    

    render = () => {
        return (
            <ul className="list-group list-group-horizontal-md">
                {
                    this.props.books.map((book, i) => 
                    (
                        <li key={book.id} className="list-group-item">
                            <div>
                                <Link to={'/view?bookId=' + book.id}>
                                    <Image src={book.image} alt={book.name} height={500} width={350} />
                                </Link>
                                {this.props.isAdmin &&
                                    <div>
                                        <Button href={"change?bookId=" + book.id}>Change</Button>
                                        <Button href={"delete?bookId=" + book.id}>Delete</Button>
                                    </div>
                                }
                            </div>
                        </li>
                    ))
                }

            </ul>
        )
    }
}

