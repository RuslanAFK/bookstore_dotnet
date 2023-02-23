import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {AUTH_URL} from "../../shared/store/urls";
import {handleError} from "../../shared/services/errorHandler";
import {getToken} from "../../shared/services/tokenManager";
import AuthUser from "../interfaces/AuthUser";
import UpdateUser from "../interfaces/UpdateUser";
import AuthResult from "../interfaces/AuthResult";

export const login = createAsyncThunk(
    "auth/login",
    async (userData: AuthUser, {rejectWithValue}) => {
        try {
            const {data} = await axios.post<AuthResult>(`${AUTH_URL}/Login`, userData);
            return data;
        } catch (e) {
            return handleError(e, rejectWithValue);
        }
    }
)

export const register = createAsyncThunk(
    "auth/register",
    async (userData: AuthUser, {dispatch, rejectWithValue}) => {
        try {
            await axios.post<void>(`${AUTH_URL}/Register`, userData);
            const toLogin = {username: userData.username, password: userData.password}
            return dispatch(login(toLogin))
        } catch (e) {
            return handleError(e, rejectWithValue)
        }
    }
)

export const updateProfile = createAsyncThunk(
    "auth/profile",
    async (userData: UpdateUser, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            await axios.put<void>(AUTH_URL, userData, config);
            const toLogin = {username: userData.username, password: userData.newPassword ?? userData.password};
            return thunkAPI.dispatch(login(toLogin));
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue)
        }
    }
)
