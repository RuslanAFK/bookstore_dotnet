import {createSlice} from "@reduxjs/toolkit";
import {getSingleUser, getUsers} from "./effects";
import {getPending, getRejected, getSingleUserSuccessful, getUsersSuccessful} from "./reducers";

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
    extraReducers: {
        [getSingleUser.pending]: getPending,
        [getSingleUser.rejected]: getRejected,
        [getSingleUser.fulfilled]: getSingleUserSuccessful,

        [getUsers.pending]: getPending,
        [getUsers.rejected]: getRejected,
        [getUsers.fulfilled]: getUsersSuccessful
    }
})

export default userSlice;