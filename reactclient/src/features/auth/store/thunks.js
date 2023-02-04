import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {LOGIN_URL, REGISTER_URL} from "../../../helpers/Urls";

export const login = createAsyncThunk(
    "auth/login",
    async (userData, {rejectWithValue}) => {
        try {
            const {data} = await axios.post(LOGIN_URL, userData);
            return data;
        } catch (e) {
            return rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)

export const register = createAsyncThunk(
    "auth/register",
    async (userData, {dispatch, rejectWithValue}) => {
        try {
            await axios.post(REGISTER_URL, userData);
            const toLogin = {username: userData.username, password: userData.password}
            return dispatch(login(toLogin))
        } catch (e) {
            return rejectWithValue(e?.response?.data ?? e.message)
        }
    }
)
