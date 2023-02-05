import {createSlice} from "@reduxjs/toolkit";
import {createBook, deleteBook, getBook, getBooks, updateBook} from "./effects";
import {
    changePending,
    changeRejected,
    changeSuccessful,
    getBooksSuccessful,
    getPending,
    getRejected,
    getSingleBookSuccessful
} from "./reducers";

const initialState = {
    books: [],
    error: null,
    fetching: false,
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
        [getBooks.pending]: getPending,
        [getBook.pending]: getPending,
        [createBook.pending]: changePending,
        [deleteBook.pending]: changePending,
        [updateBook.pending]: changePending,

        [getBooks.rejected]: getRejected,
        [getBook.rejected]: getRejected,
        [createBook.rejected]: changeRejected,
        [deleteBook.rejected]: changeRejected,
        [updateBook.rejected]: changeRejected,

        [getBooks.fulfilled]: getBooksSuccessful,
        [getBook.fulfilled]: getSingleBookSuccessful,
        [createBook.fulfilled]: changeSuccessful,
        [deleteBook.fulfilled]: changeSuccessful,
        [updateBook.fulfilled]: changeSuccessful
    }
})

export const {applyChanges} = bookSlice.actions;

export default bookSlice;