import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {USER_URL} from "../../../store/urls";
import {handleError} from "../../../services/errorHandler";
import {getToken} from "../../../services/tokenManager";
import QueryObject from "../../../interfaces/QueryObject";
import PaginatedList from "../../../interfaces/PaginatedList";
import GetUser from "../interfaces/GetUser";
import UpdateUserRole from "../interfaces/UpdateUserRole";

export const getUsers = createAsyncThunk(
    "user/getUsers",
    async (input: QueryObject, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} =
                await axios.get<PaginatedList<GetUser>>(`${USER_URL}?page=${input.page}&search=${input.search}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const getSingleUser = createAsyncThunk(
    "user/getSingleUser",
    async (userId: number, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.get<GetUser>(`${USER_URL}/${userId}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)


export const updateUserRole = createAsyncThunk(
    "user/updateUserRole",
    async (userData: UpdateUserRole, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.put<void>(USER_URL, userData, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const deleteUser = createAsyncThunk(
    "user/deleteUser",
    async (id: number, thunkAPI) => {
        try {
            const config = getToken(thunkAPI);
            const {data} = await axios.delete<void>(`${USER_URL}/${id}`, config);
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)