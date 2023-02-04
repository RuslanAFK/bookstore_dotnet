import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {BOOK_URL} from "../../../helpers/Urls";

const getToken = (thunkAPI) => {
    const token = thunkAPI.getState()?.auth?.user?.token;
    if (!token)
        throw new Error("Register please.");
    return {headers: {'Authorization':`Bearer ${token}`}};
}

export const getBooks = createAsyncThunk(
    "book/getBooks",
    async (input, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get(BOOK_URL, config);
            return data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)

export const getBook = createAsyncThunk(
    "book/getBook",
    async (bookId, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get(`${BOOK_URL}/${bookId}`, config);
            return data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)

export const createBook = createAsyncThunk(
    "book/createBook",
    async (bookData, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.post(BOOK_URL, bookData, config);
            return data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)

export const updateBook = createAsyncThunk(
    "book/updateBook",
    async (bookData, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.put(BOOK_URL, bookData, config);
            return data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)

export const deleteBook = createAsyncThunk(
    "book/deleteBook",
    async (id, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.delete(`${BOOK_URL}/${id}`, config);
            return data;
        } catch (e) {
            return thunkAPI.rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)