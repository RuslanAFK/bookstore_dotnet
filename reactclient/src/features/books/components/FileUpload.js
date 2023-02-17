import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {addFile, deleteFile} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {hasError, isChanged} from "../../../store/selectors";
import {applyChanges} from "../store/bookSlice";
import {notify} from "../../../services/toast-notifier";
import {ToastContainer} from "react-toastify";

const FileUpload = () => {

    const dispatch = useDispatch();
    const navigate = useNavigate();
    const params = useParams();
    const [file, setFile] = useState('');
    const [bookId, setId] = useState('');

    const bookState = useSelector(state => state.book);


    useEffect(() => {
        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId))
            navigate("/");
        setId(bookId);
    }, [bookId])

    useEffect(() => {
        if (isChanged(bookState)) {
            dispatch(applyChanges());
            navigate(`/book/${bookId}`);
        }
    }, [bookState.changed])

    useEffect(() => {
        if (hasError(bookState))
            notify(bookState.error, "error");
    }, [bookState.error])

    const uploadFile = (e) => {
        e.preventDefault();
        let fd = new FormData();
        fd.append('file', file);
        const input = {bookId, fd};
        dispatch(addFile(input))
    }

    const onChange = () => {
        const file = document.getElementById("file-upload")?.files[0] ?? '';
        setFile(file);
    }

    const onFileDelete = () => {
        if (window.confirm(`Do you really want to delete book file?`)) {
            dispatch(deleteFile(bookId));
        }
    }

    return (
        <div>
            <form onSubmit={uploadFile} className="mx-auto my-auto w-25">
                <h2 className="text-center my-3">Manage Book File</h2>
                <label htmlFor="file-upload" className="form-label">Upload .pdf file</label>
                <input type="file" className="form-control" onChange={onChange} id="file-upload" />
                <button className="btn btn-primary my-2 w-100">Upload</button>
                <button className="btn btn-secondary my-2 w-100" type='button' onClick={onFileDelete}>Delete</button>
            </form>
            <ToastContainer/>
        </div>
    );
}

export default FileUpload;
