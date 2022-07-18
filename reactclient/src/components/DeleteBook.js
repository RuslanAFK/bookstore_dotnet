import React from "react";
import {Button, Form} from "react-bootstrap";

const server_url = 'https://localhost:7180/delete-book';

class DeleteBook extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
            name: '',
        };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleNameChange = (e) => {
        this.setState({
            name: e.target.value,
        });
    }

    ifValid(data) {
        return !data.error;
    }

    login(data) {
        alert("The book "+data.name+", is successfully deleted!");
    }

    raiseErr(data) {
        let errText;
        if(data.error.name){
            errText = data.error.name[0];
            document.getElementById('d_name').setCustomValidity(errText);
            document.getElementById('d_name').reportValidity();
        }
        console.log(data);
    }

    handleSubmit = (e) => {
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                name: this.state.name
            }),
        }
        fetch(server_url, requestOptions).then((response)=>
            response.json()
        ).then((data)=>this.ifValid(data)?this.login(data):this.raiseErr(data));
    }

    render(){
        return(
            <Form className="rl_form">
                <h1>Delete Book</h1>
                <Form.Label>Name</Form.Label>
                <Form.Control
                    onChange={this.handleNameChange}
                    placeHolder="Cinderella in the woods"
                    pattern=".{3,40}"
                    id="d_name"
                    required
                />
                <Form.Text className="text-warning">
                    One book must be with this name.
                </Form.Text>
                <br/>

                <Button className="one_but" onClick={this.handleSubmit}>Delete</Button>
            </Form>
        )
    }
}

export default DeleteBook;
