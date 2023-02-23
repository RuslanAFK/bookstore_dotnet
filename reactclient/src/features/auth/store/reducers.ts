import {AuthState} from "./auth-slice";
import {PayloadAction} from "@reduxjs/toolkit";
import AuthResult from "../interfaces/AuthResult";

export const authRejected = (state: AuthState, {payload}: PayloadAction<string>) => {
    state.loading = false;
    state.error = payload;
}

export const loginPending = (state: AuthState) => {
    state.loading = true;
}

export const loginSuccessful = (state: AuthState, {payload}: PayloadAction<AuthResult>) => {
    state.loading = false;
    state.user = payload;
}

export const updateSuccessful = (state: AuthState) => {
    state.updated = true;
}