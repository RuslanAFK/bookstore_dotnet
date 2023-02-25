import {createSlice} from "@reduxjs/toolkit";
import {deleteAccount, login, register, updateProfile} from "./effects";
import {
    authRejected,
    changePending, deleteSuccessful,
    loginPending,
    loginSuccessful,
    registerSuccessful,
    updateSuccessful
} from "./reducers";
import AuthResult from "../interfaces/AuthResult";

export type AuthState = {
    user: AuthResult | null,
    error: string | null,
    fetching: boolean,
    changing: boolean,
    updated: boolean
}

const initialState: AuthState = {
    user: null,
    error: null,
    fetching: false,
    updated: false,
    changing: false
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
        [updateProfile.pending.toString()]: changePending,
        [updateProfile.rejected.toString()]: authRejected,
        [updateProfile.fulfilled.toString()]: updateSuccessful,

        [login.pending.toString()]: loginPending,
        [login.rejected.toString()]: authRejected,
        [login.fulfilled.toString()]: loginSuccessful,

        [register.pending.toString()]: changePending,
        [register.fulfilled.toString()]: registerSuccessful,
        [register.rejected.toString()]: authRejected,

        [deleteAccount.pending.toString()]: changePending,
        [deleteAccount.rejected.toString()]: authRejected,
        [deleteAccount.fulfilled.toString()]: deleteSuccessful

    }
})

export const {logout, clearError, applyUpdate} = authSlice.actions;

export default authSlice;