import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./features/books/routes/LoadBook";
import Auth from "./features/auth/routes/Auth";
import BookView from "./features/books/routes/BookView";
import AllBooks from "./features/books/routes/AllBooks";
import Navbar from "./components/Navbar";

import 'react-toastify/dist/ReactToastify.css'
import Home from "./components/Home";

const App = () => {
    return (
        <div className="center">
            <Router>
                <Navbar/>
                <Routes>
                    <Route exact path="/" element={<Home />} />

                    <Route path="/login" element={<Auth />} />
                    <Route path="/register" element={<Auth isRegisterPage />} />

                    <Route exact path="/books" element={<AllBooks />} />
                    <Route path="/book/:id" element={<BookView />} />
                    <Route exact path="/load" element={<LoadBook />} />
                    <Route path="/update/:id" element={<LoadBook isUpdatePage />} />
                </Routes>
            </Router>
        </div>
    )
}

export default App;
