import {AuthState} from "./auth-slice";
import {PayloadAction} from "@reduxjs/toolkit";
import AuthResult from "../interfaces/AuthResult";

export const authRejected = (state: AuthState, {payload}: PayloadAction<string>) => {
    state.fetching = false;
    state.changing = false;
    state.error = payload;
}

export const loginPending = (state: AuthState) => {
    state.fetching = true;
}

export const loginSuccessful = (state: AuthState, {payload}: PayloadAction<AuthResult>) => {
    state.fetching = false;
    state.user = payload;
}

export const updateSuccessful = (state: AuthState) => {
    state.updated = true;
    state.changing = false;
}

export const deleteSuccessful = (state: AuthState) => {
    state.updated = true;
    state.changing = false;
    state.user = null;
}

export const registerSuccessful = (state: AuthState) => {
    state.changing = false;
}

export const changePending = (state: AuthState) => {
    state.changing = true;
}
