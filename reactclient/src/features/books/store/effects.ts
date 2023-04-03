import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {BOOK_FILE_URL, BOOK_URL} from "../../shared/store/urls";
import {handleError} from "../../shared/services/error-handler";
import {addBearerToken, getToken} from "../../shared/services/token-manager";
import GetBookDetails from "../interfaces/GetBookDetails";
import PaginatedList from "../../shared/interfaces/PaginatedList";
import CreateBook from "../interfaces/CreateBook";
import UpdateBook from "../interfaces/UpdateBook";
import QueryObject from "../../shared/interfaces/QueryObject";
import FileObject from "../../shared/interfaces/FileObject";
import GetBook from "../interfaces/GetBook";

export const getBooks = createAsyncThunk(
    "book/getBooks",
    async (input: QueryObject, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} =
                await axios.get<PaginatedList<GetBook>>(`${BOOK_URL}?page=${input.page}&search=${input.search}`,
                    {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const getBook = createAsyncThunk(
    "book/getBook",
    async (bookId: number, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.get<GetBookDetails>(`${BOOK_URL}/${bookId}`, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const createBook = createAsyncThunk(
    "book/createBook",
    async (bookData: CreateBook, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.post<void>(BOOK_URL, bookData, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const updateBook = createAsyncThunk(
    "book/updateBook",
    async (bookData: UpdateBook, thunkAPI) => {
        try {
            const book: CreateBook = {...bookData};
            const bookId = bookData.id;
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.patch<void>(`${BOOK_URL}/${bookId}`, book, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const deleteBook = createAsyncThunk(
    "book/deleteBook",
    async (id: number, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.delete<void>(`${BOOK_URL}/${id}`, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const addFile = createAsyncThunk(
    "book/addFile",
    async ({bookId, fileData}: FileObject, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.post<void>(`${BOOK_FILE_URL}?bookId=${bookId}`, fileData, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const deleteFile = createAsyncThunk(
    "book/deleteFile",
    async (bookId: number, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.delete(`${BOOK_FILE_URL}?bookId=${bookId}`, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)