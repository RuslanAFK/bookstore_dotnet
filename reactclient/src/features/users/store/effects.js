import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {USER_URL} from "../../../store/urls";
import {handleError} from "../../../store/errorHandler";
import {getToken} from "../../../store/tokenManager";

export const getUsers = createAsyncThunk(
    "user/getUser",
    async (input, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get(`${USER_URL}?page=${input}`, config);
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