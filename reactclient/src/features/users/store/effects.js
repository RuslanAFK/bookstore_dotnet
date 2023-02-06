import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {USER_URL} from "../../../store/urls";
import {handleError} from "../../../store/errorHandler";
import {getToken} from "../../../store/tokenManager";

export const getUsers = createAsyncThunk(
    "user/getUsers",
    async (input, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get(`${USER_URL}?page=${input.page}&search=${input.search}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const getSingleUser = createAsyncThunk(
    "user/getSingleUser",
    async (userId, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get(`${USER_URL}/${userId}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)


export const updateUserRole = createAsyncThunk(
    "user/updateUserRole",
    async (userData, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.put(USER_URL, userData, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const deleteUser = createAsyncThunk(
    "user/deleteUser",
    async (id, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.delete(`${USER_URL}/${id}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)