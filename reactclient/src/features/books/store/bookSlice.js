import {createSlice} from "@reduxjs/toolkit";
import {createBook, deleteBook, getBook, getBooks, updateBook} from "./effects";
import {
    getItemsSuccessful,
    getSingleItemSuccessful
} from "./reducers";
import {changePending, changeRejected, changeSuccessful, getPending, getRejected} from "../../../store/reducers";

const initialState = {
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
        [getBook.pending]: getPending,
        [getBook.rejected]: getRejected,
        [getBook.fulfilled]: getSingleItemSuccessful,

        [getBooks.pending]: getPending,
        [getBooks.rejected]: getRejected,
        [getBooks.fulfilled]: getItemsSuccessful,

        [createBook.pending]: changePending,
        [createBook.rejected]: changeRejected,
        [createBook.fulfilled]: changeSuccessful,

        [deleteBook.pending]: changePending,
        [deleteBook.rejected]: changeRejected,
        [deleteBook.fulfilled]: changeSuccessful,

        [updateBook.pending]: changePending,
        [updateBook.rejected]: changeRejected,
        [updateBook.fulfilled]: changeSuccessful
    }
})

export const {applyChanges} = bookSlice.actions;

export default bookSlice;