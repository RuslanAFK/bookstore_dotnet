import React, {useEffect, useState} from "react";
import "../stylesheets/Navbar.css";
import {useDispatch, useSelector} from "react-redux";
import {Link, useLocation, useNavigate} from "react-router-dom";
import {isAdminOrCreator, isAuthed, isCreator} from "../../auth/store/selectors";
import {logout} from "../../auth/store/auth-slice";
import {AppDispatch, RootState} from "../store/store";

const Navbar = () => {

    const logoText = "BookStore Inc.";

    const authState = useSelector((state: RootState) => state.auth);
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const location = useLocation();
    const [linkSelectedCssClasses, setLinkSelectedCssClasses] = useState({
        auth: "nav-link-disabled",
        books: "nav-link-disabled",
        users: "nav-link-disabled",
        profile: "nav-link-disabled",
        docs: "nav-link-disabled",
        add: "nav-link-disabled",
    });

    const linkSelectedCssClass = (pathName: string): string => {
        let linkSelected = location.pathname === pathName;
        return linkSelected ? "nav-link-active" : "nav-link-disabled";
    }

    useEffect(() => {
        setLinkSelectedCssClasses({
            auth: linkSelectedCssClass("/login"),
            books: linkSelectedCssClass("/books"),
            users: linkSelectedCssClass("/users"),
            profile: linkSelectedCssClass("/profile"),
            docs: linkSelectedCssClass("/docs"),
            add: linkSelectedCssClass("/load"),
        });
    }, [location.pathname])

    useEffect(() => {
        if (!isAuthed(authState)) {
            navigate("/login");
        }
    }, [authState.user])


    const onLogout = () => {
        dispatch(logout());
    }

    return (
        <div>
            <nav className="navbar navbar-expand-lg navbar-dark bg-primary rounded">
                <Link className="navbar-brand mx-2" to="/">{logoText}</Link>
                <div className="collapse navbar-collapse">
                    <ul className="navbar-nav">
                            {isAuthed(authState) && <li className="nav-item">
                            <Link to="/books" className={"nav-link " + linkSelectedCssClasses.books}>Books Catalog</Link>
                        </li>}
                        {isAdminOrCreator(authState) &&
                            <li className="nav-item">
                                <Link className={"nav-link " + linkSelectedCssClasses.add} to="/load">Add Book</Link>
                            </li>
                        }
                        {isCreator(authState) &&
                            <li className="nav-item">
                                <Link className={"nav-link " + linkSelectedCssClasses.users} to="/users">Users</Link>
                            </li>
                        }
                    </ul>
                    <ul className="navbar-nav right">
                        {
                            authState.fetching ? (
                                    <>
                                        <li className="nav-item">
                                            <span className="nav-link">Loading...</span>
                                        </li>
                                    </>
                                ) :
                                authState.user ? (
                                    <>
                                        <li>
                                            <Link to="/profile" className="nav-link">
                                                <img className={"nav-item " + linkSelectedCssClasses.profile}
                                                     src="/icons/edit.png" alt="Profile"
                                                     width={30} height={30}/>
                                            </Link>
                                        </li>
                                        <li>
                                            <a onClick={onLogout} className="nav-link">
                                                <img className={"nav-item nav-link-disabled"}
                                                     src="/icons/logout.png" alt="Logout"
                                                     width={30} height={30}/>
                                            </a>
                                        </li>
                                    </>
                                ) : (
                                    <>
                                    <li>
                                        <Link className="nav-link" to="/login">
                                            <img className={"nav-item " + linkSelectedCssClasses.auth}
                                                 src="/icons/user.png" alt="Login"
                                                 width={25} height={30}/>
                                        </Link>
                                    </li>
                                    </>
                                )
                        }
                        <li>
                            <Link className="nav-link" to="/docs">
                                <img src="/icons/info.png" alt="Documentation" className={"nav-item " + linkSelectedCssClasses.docs}
                                     width={25} height={25}/>
                            </Link>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
)
}

export default Navbar;