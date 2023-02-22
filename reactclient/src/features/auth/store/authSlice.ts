import {createSlice} from "@reduxjs/toolkit";
import {login, register, updateProfile} from "./effects";
import {authRejected, loginPending, loginSuccessful} from "./reducers";
import AuthResult from "../interfaces/AuthResult";

export type AuthState = {
    user: AuthResult | null,
    error: string | null,
    loading: boolean
}

const initialState: AuthState = {
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
        [register.rejected.toString()]: authRejected,
        [updateProfile.rejected.toString()]: authRejected,

        [login.rejected.toString()]: authRejected,
        [login.pending.toString()]: loginPending,
        [login.fulfilled.toString()]: loginSuccessful
    }
})

export const {logout} = authSlice.actions;

export default authSlice;