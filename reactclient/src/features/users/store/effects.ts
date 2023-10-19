import {createAsyncThunk} from "@reduxjs/toolkit";
import axios from "axios";
import {USER_URL} from "../../shared/store/urls";
import {handleError} from "../../shared/services/error-handler";
import {addBearerToken, getToken} from "../../shared/services/token-manager";
import QueryObject from "../../shared/interfaces/QueryObject";
import PaginatedList from "../../shared/interfaces/PaginatedList";
import GetUser from "../interfaces/GetUser";
import UpdateUserRole from "../interfaces/UpdateUserRole";

export const getUsers = createAsyncThunk(
    "user/getUsers",
    async (input: QueryObject, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} =
                await axios.get<PaginatedList<GetUser>>(`${USER_URL}?page=${input.page}&search=${input.search}`,
                    {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)



export const updateUserRole = createAsyncThunk(
    "user/updateUserRole",
    async (userData: GetUser, thunkAPI) => {
        try {
            const updateUserData: UpdateUserRole = {...userData};
            const id = userData.id;
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.patch<void>(`${USER_URL}/${id}`, updateUserData, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)

export const deleteUser = createAsyncThunk(
    "user/deleteUser",
    async (userData: GetUser, thunkAPI) => {
        try {
            const token = getToken(thunkAPI);
            const headers = addBearerToken(token);
            const {data} = await axios.delete<void>(`${USER_URL}/${userData.id}`, {headers: headers});
            return data;
        } catch (e) {
            return handleError(e, thunkAPI.rejectWithValue);
        }
    }
)