import {createSlice} from "@reduxjs/toolkit";
import {login, register} from "./effects";
import {authRejected, loginPending, loginSuccessful} from "./reducers";

const initialState = {
    user: null,
    error: null,
    loading: false
}

const authSlice = createSlice({
    name: "auth",
    initialState: initialState,
    reducers: {
        logout(state) {
            state.user = null;
        }
    },
    extraReducers: {
        [register.rejected]: authRejected,
        [login.rejected]: authRejected,
        [login.pending]: loginPending,
        [login.fulfilled]: loginSuccessful
    }
})

export const {logout} = authSlice.actions;

export default authSlice;