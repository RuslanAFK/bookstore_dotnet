import {AnyAction} from "@reduxjs/toolkit";

export interface CommonState {
    fetched: boolean,
    error: string | null,
    changed: boolean
}

export const getPending = (state: CommonState) => {
    state.fetched = false;
    state.error = null;
}
export const getRejected = (state: CommonState, {payload}: AnyAction) => {
    state.fetched = false;
    state.error = payload;
}
export const changePending = (state: CommonState) => {
    state.changed = false;
    state.error = null;
}
export const changeRejected = (state: CommonState, {payload}: AnyAction) => {
    state.changed = false;
    state.error = payload;
}
export const changeSuccessful = (state: CommonState) => {
    state.changed = true;
}
