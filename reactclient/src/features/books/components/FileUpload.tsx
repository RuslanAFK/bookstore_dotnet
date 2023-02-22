import React, {FormEvent, useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {addFile, deleteFile} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {isChanged} from "../../../store/selectors";
import {applyChanges} from "../store/bookSlice";
import {notify} from "../../../services/toast-notifier";
import {ToastContainer} from "react-toastify";
import {AppDispatch, RootState} from "../../../store/store";
import FileObject from "../../../interfaces/FileObject";

const FileUpload = () => {

    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const params = useParams();
    const [file, setFile] = useState<File | undefined>();
    const [bookId, setId] = useState(0);

    const bookState = useSelector((state: RootState) => state.book);


    useEffect(() => {
        if (params.id) {
            const bookId = parseInt(params.id);
            if (bookId === null || isNaN(bookId))
                navigate("/");
            setId(bookId);
        }
    }, [bookId])

    useEffect(() => {
        if (isChanged(bookState)) {
            dispatch(applyChanges());
            navigate(`/book/${bookId}`);
        }
    }, [bookState.changed])

    useEffect(() => {
        if (bookState.error)
            notify(bookState.error, "error");
    }, [bookState.error])

    const uploadFile = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        let fd = new FormData();
        if (file) {
            fd.append('file', file);
            const input: FileObject = {bookId, fileData: fd };
            dispatch(addFile(input))
        }
    }

    const onChange = (e: FormEvent<HTMLInputElement>) => {
        const files = (e.target as HTMLInputElement).files;
        if (files) {
            const file = files[0] ?? '';
            setFile(file);
        }
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
