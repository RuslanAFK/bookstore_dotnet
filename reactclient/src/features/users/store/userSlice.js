import {createSlice} from "@reduxjs/toolkit";
import {deleteUser, getSingleUser, getUsers, updateUserRole} from "./effects";
import {getSingleItemSuccessful, getItemsSuccessful} from "./reducers";
import {changePending, changeRejected, changeSuccessful, getPending, getRejected} from "../../../store/reducers";

const initialState = {
    users: [],
    count: 0,
    error: null,
    fetching: false,
    fetched: false,
    changed: false,
}

const userSlice = createSlice({
    name: "user",
    initialState: initialState,
    reducers: {
        applyChanges(state) {
            state.changed = false;
        }
    },
    extraReducers: {
        [getSingleUser.pending]: getPending,
        [getSingleUser.rejected]: getRejected,
        [getSingleUser.fulfilled]: getSingleItemSuccessful,

        [getUsers.pending]: getPending,
        [getUsers.rejected]: getRejected,
        [getUsers.fulfilled]: getItemsSuccessful,

        [updateUserRole.pending]: changePending,
        [updateUserRole.rejected]: changeRejected,
        [updateUserRole.fulfilled]: changeSuccessful,

        [deleteUser.pending]: changePending,
        [deleteUser.rejected]: changeRejected,
        [deleteUser.fulfilled]: changeSuccessful,
    }
})

export const {applyChanges} = userSlice.actions;

export default userSlice;