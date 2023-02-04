import React from "react";
import "./Navbar.css";
import {useDispatch, useSelector} from "react-redux";
import {Link} from "react-router-dom";
import {logout} from "../features/auth/store/authSlice";

const Navbar = () => {

    const {loading, success, user} = useSelector(state => state.auth);
    const dispatch = useDispatch();

    const onLogout = () => {
        dispatch(logout());
    }

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary rounded">
                <Link className="navbar-brand" to="/">Navbar</Link>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav">
                        {success && <li className="nav-item active">
                            <Link to="/books" className="nav-link">Books</Link>
                        </li>}
                        {success && user.role === "Admin" &&
                            <li className="nav-item">
                                <Link className="nav-link" to="/load">Add Book</Link>
                            </li>
                        }
                    </ul>
                    <ul className="navbar-nav right">
                        {
                            loading ? (
                                <>
                                    <li className="nav-item">
                                        <span className="nav-link">Loading...</span>
                                    </li>
                                </>
                                ) :
                            success ? (
                                <>
                                    <li className="nav-item">
                                        <span className="nav-link">Hello {user.username}</span>
                                    </li>
                                    <li className="nav-item">
                                        <a onClick={onLogout} className="nav-link">Logout</a>
                                    </li>
                                </>
                            ) : (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/login">Login</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/register">Register</Link>
                                    </li>
                                </>
                            )
                        }
                    </ul>
                </div>
            </nav>
        </div>
    )
}

export default Navbar;