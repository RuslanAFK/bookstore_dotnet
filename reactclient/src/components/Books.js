import React, { Component } from 'react';
import { Link } from "react-router-dom";
import { Button, Image } from "react-bootstrap"

function onChangeClicked (bookId) {
    //window.location.href = `change?bookId=${bookId}`;
}

export default class Books extends Component {
    constructor(props) {
        super(props);
    }
    
    onDeleteClicked = () => {

    }
    render = () => {
        return (
            <ul className="list-group list-group-horizontal-md">
                {
                    this.props.books.map((book, i) => (
                        <li className="list-group-item">
                            <div>
                                <Link to={'/book?bookId=' + book.id}>
                                    <Image src={book.image} alt={book.name} height={500} width={350} />
                                </Link>
                                {this.props.isAdmin &&
                                    <div>
                                        <Button onClick={onChangeClicked(book.id)}>Change</Button>
                                        <Button onClick={this.onDeleteClicked}>Delete</Button>
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

