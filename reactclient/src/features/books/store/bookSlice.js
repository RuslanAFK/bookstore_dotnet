import {createSlice} from "@reduxjs/toolkit";
import {createBook, deleteBook, getBook, getBooks, updateBook} from "./thunks";

const initialState = {
    books: [],
    error: null,
    loading: false,
    success: false
}

const bookSlice = createSlice({
    name: "book",
    initialState: initialState,
    extraReducers: {
        [getBooks.pending || getBook.pending || createBook.pending || deleteBook.pending || updateBook.pending]: (state) => {
            state.loading = true;
        },
        [getBooks.rejected || getBook.rejected || createBook.rejected || deleteBook.rejected || updateBook.rejected]: (state, {payload}) => {
            state.loading = false;
            state.success = false;
            state.error = payload;
        },
        [getBooks.fulfilled]: (state, {payload}) => {
            state.loading = false;
            state.success = true;
            state.books = payload;
        },
        [getBook.fulfilled]: (state, {payload}) => {
            state.loading = false;
            state.success = true;
            state.books = [payload];
        },
        [createBook.fulfilled || deleteBook.fulfilled || updateBook.fulfilled]: (state) => {
            state.loading = false;
            state.success = true;
        }
    }
})

export default bookSlice;