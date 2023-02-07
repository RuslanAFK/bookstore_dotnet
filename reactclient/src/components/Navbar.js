import React, {useEffect} from "react";
import "./Navbar.css";
import {useDispatch, useSelector} from "react-redux";
import {Link, useNavigate} from "react-router-dom";
import {isAdminOrCreator, isAuthed, isCreator, isLoading} from "../features/auth/store/selectors";
import {logout} from "../features/auth/store/authSlice";

const Navbar = () => {

    const logoText = "BookStore Inc.";

    const authState = useSelector(state => state.auth);
    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (!isAuthed(authState))
            navigate("/login");
    }, [authState.user])


    const onLogout = () => {
        dispatch(logout());
    }

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary rounded">
                <Link className="navbar-brand" to="/">{logoText}</Link>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav">
                        <li className="nav-item active">
                            <Link to="/" className="nav-link">Home</Link>
                        </li>
                        {isAuthed(authState) && <li className="nav-item active">
                            <Link to="/books" className="nav-link">Books</Link>
                        </li>}
                        {isAdminOrCreator(authState) &&
                            <li className="nav-item">
                                <Link className="nav-link" to="/load">Add Book</Link>
                            </li>
                        }
                        {isCreator(authState) &&
                            <li className="nav-item">
                                <Link className="nav-link" to="/users">Users</Link>
                            </li>
                        }
                    </ul>
                    <ul className="navbar-nav right">
                        {
                            isLoading(authState) ? (
                                <>
                                    <li className="nav-item">
                                        <span className="nav-link">Loading...</span>
                                    </li>
                                </>
                                ) :
                            isAuthed(authState) ? (
                                <>
                                    <li className="nav-item">
                                        <Link to="/profile" className="nav-link">
                                            Hello {authState.user.username}
                                        </Link>
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