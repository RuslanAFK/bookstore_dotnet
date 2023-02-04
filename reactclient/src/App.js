import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./features/books/routes/LoadBook";
import Login from "./features/auth/routes/Login";
import Signup from "./features/auth/routes/Signup";
import BookView from "./features/books/routes/BookView";
import AllBooks from "./features/books/routes/AllBooks";
import Navbar from "./components/Navbar";

const App = () => {
    return (
        <div className="center">
            <Router>
                <Navbar/>
                <Routes>
                    <Route exact path="/" element={<div>Home page</div>} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Signup />} />

                    <Route exact path="/books" element={<AllBooks />} />
                    <Route path="/book/:id" element={<BookView />} />
                    <Route exact path="/load" element={<LoadBook isUpdate={false} />} />
                    <Route path="/load/:id" element={<LoadBook isUpdate={true} />} />

                </Routes>
            </Router>
        </div>
    )
}

export default App;
