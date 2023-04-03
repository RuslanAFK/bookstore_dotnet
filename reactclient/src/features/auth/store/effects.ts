import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {AUTH_URL} from "../../shared/store/urls";
import {handleError} from "../../shared/services/error-handler";
import {addBearerToken, getToken} from "../../shared/services/token-manager";
import AuthUser from "../interfaces/AuthUser";
import UpdateUser from "../interfaces/UpdateUser";
import AuthResult from "../interfaces/AuthResult";
import {logout} from "./auth-slice";
import DeleteUser from "../interfaces/DeleteUser";

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
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            await axios.patch<void>(AUTH_URL, userData, {headers: headers});
            const toLogin = {username: userData.username, password: userData.newPassword ?? userData.password};
            return thunkAPI.dispatch(login(toLogin));
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue)
        }
    }
)

export const deleteAccount = createAsyncThunk(
    "auth/deleteAccount",
    async (user: DeleteUser, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.delete<void>(AUTH_URL, {
                headers: headers,
                data: user
            });
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue)
        }
    }
)
