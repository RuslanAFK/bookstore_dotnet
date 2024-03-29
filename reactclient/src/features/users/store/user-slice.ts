import {createSlice} from "@reduxjs/toolkit";
import {deleteUser, getUsers, updateUserRole} from "./effects";
import {getSingleItemSuccessful, getItemsSuccessful} from "./reducers";
import {
    changePending,
    changeRejected,
    changeSuccessful,
    CommonState,
    getPending,
    getRejected
} from "../../shared/store/reducers";
import GetUser from "../interfaces/GetUser";

export interface UserState extends CommonState {
    users: GetUser[],
    count: number,
}

const initialState: UserState = {
    users: [],
    count: 0,
    error: null,
    fetched: false,
    changed: false,
    fetching: false,
    changing: false
}

const userSlice = createSlice({
    name: "user",
    initialState: initialState,
    reducers: {
        clearError(state) {
            state.error = null;
        }
    },
    extraReducers: {


        [getUsers.pending.toString()]: getPending,
        [getUsers.rejected.toString()]: getRejected,
        [getUsers.fulfilled.toString()]: getItemsSuccessful,

        [updateUserRole.pending.toString()]: changePending,
        [updateUserRole.rejected.toString()]: changeRejected,
        [updateUserRole.fulfilled.toString()]: changeSuccessful,

        [deleteUser.pending.toString()]: changePending,
        [deleteUser.rejected.toString()]: changeRejected,
        [deleteUser.fulfilled.toString()]: changeSuccessful,
    }
})

export const {clearError} = userSlice.actions;

export default userSlice;