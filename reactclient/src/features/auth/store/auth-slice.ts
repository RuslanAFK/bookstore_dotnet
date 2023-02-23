import {createSlice} from "@reduxjs/toolkit";
import {login, register, updateProfile} from "./effects";
import {authRejected, loginPending, loginSuccessful, updateSuccessful} from "./reducers";
import AuthResult from "../interfaces/AuthResult";

export type AuthState = {
    user: AuthResult | null,
    error: string | null,
    loading: boolean,
    updated: boolean
}

const initialState: AuthState = {
    user: null,
    error: null,
    loading: false,
    updated: false
}

const authSlice = createSlice({
    name: "auth",
    initialState: initialState,
    reducers: {
        logout(state) {
            state.user = null;
        },
        clearError(state) {
            state.error = null;
        },
        applyUpdate(state) {
            state.updated = false;
        }
    },
    extraReducers: {
        [register.rejected.toString()]: authRejected,
        [updateProfile.rejected.toString()]: authRejected,
        [updateProfile.fulfilled.toString()]: updateSuccessful,

        [login.rejected.toString()]: authRejected,
        [login.pending.toString()]: loginPending,
        [login.fulfilled.toString()]: loginSuccessful
    }
})

export const {logout, clearError, applyUpdate} = authSlice.actions;

export default authSlice;