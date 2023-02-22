import {AuthState} from "./authSlice";
import {AnyAction} from "@reduxjs/toolkit";

export const authRejected = (state: AuthState, {payload}: AnyAction) => {
    state.loading = false;
    state.error = payload;
}

export const loginPending = (state: AuthState) => {
    state.loading = true;
    state.error = null;
}

export const loginSuccessful = (state: AuthState, {payload}: AnyAction) => {
    state.loading = false;
    state.user = payload;
}
