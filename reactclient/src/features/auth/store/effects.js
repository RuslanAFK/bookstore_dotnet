import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {AUTH_URL} from "../../../store/urls";
import {handleError} from "../../../store/errorHandler";
import {getToken} from "../../../store/tokenManager";

export const login = createAsyncThunk(
    "auth/login",
    async (userData, {rejectWithValue}) => {
        try {
            const {data} = await axios.post(`${AUTH_URL}/Login`, userData);
            return data;
        } catch (e) {
            return handleError(e, rejectWithValue);
        }
    }
)

export const register = createAsyncThunk(
    "auth/register",
    async (userData, {dispatch, rejectWithValue}) => {
        try {
            await axios.post(`${AUTH_URL}/Register`, userData);
            const toLogin = {username: userData.username, password: userData.password}
            return dispatch(login(toLogin))
        } catch (e) {
            return handleError(e, rejectWithValue)
        }
    }
)

export const updateProfile = createAsyncThunk(
    "auth/profile",
    async (userData, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            await axios.put(AUTH_URL, userData, config);
            const toLogin = {username: userData.username, password: userData.newPassword ?? userData.password};
            return thunkAPI.dispatch(login(toLogin));
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue)
        }
    }
)
