import {createSlice} from "@reduxjs/toolkit";
import {addFile, createBook, deleteBook, getBook, getBooks, updateBook} from "./effects";
import {
    getItemsSuccessful,
    getSingleItemSuccessful
} from "./reducers";
import {
    changePending,
    changeRejected,
    changeSuccessful,
    CommonState,
    getPending,
    getRejected
} from "../../../store/reducers";
import GetBook from "../interfaces/GetBook";
import GetBookDetails from "../interfaces/GetBookDetails";

export interface BookState extends CommonState {
    books: Array<GetBook | GetBookDetails>,
    count: number,
}

const initialState: BookState = {
    books: [],
    count: 0,
    error: null,
    fetched: false,
    changed: false,
}

const bookSlice = createSlice({
    name: "book",
    initialState: initialState,
    reducers: {
        applyChanges(state) {
            state.changed = false;
        }
    },
    extraReducers: {
        [getBook.pending.toString()]: getPending,
        [getBook.rejected.toString()]: getRejected,
        [getBook.fulfilled.toString()]: getSingleItemSuccessful,

        [getBooks.pending.toString()]: getPending,
        [getBooks.rejected.toString()]: getRejected,
        [getBooks.fulfilled.toString()]: getItemsSuccessful,

        [createBook.pending.toString()]: changePending,
        [createBook.rejected.toString()]: changeRejected,
        [createBook.fulfilled.toString()]: changeSuccessful,

        [deleteBook.pending.toString()]: changePending,
        [deleteBook.rejected.toString()]: changeRejected,
        [deleteBook.fulfilled.toString()]: changeSuccessful,

        [updateBook.pending.toString()]: changePending,
        [updateBook.rejected.toString()]: changeRejected,
        [updateBook.fulfilled.toString()]: changeSuccessful,

        [addFile.pending.toString()]: changePending,
        [addFile.rejected.toString()]: changeRejected,
        [addFile.fulfilled.toString()]: changeSuccessful,

        [deleteBook.pending.toString()]: changePending,
        [deleteBook.rejected.toString()]: changeRejected,
        [deleteBook.fulfilled.toString()]: changeSuccessful
    }
})

export const {applyChanges} = bookSlice.actions;

export default bookSlice;