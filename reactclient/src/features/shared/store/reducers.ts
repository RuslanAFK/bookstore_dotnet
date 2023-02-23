import {PayloadAction} from "@reduxjs/toolkit";

export interface CommonState {
    fetched: boolean,
    error: string | null,
    changed: boolean
}

export const getPending = (state: CommonState) => {
    state.fetched = false;
}
export const getRejected = (state: CommonState, {payload}: PayloadAction<string>) => {
    state.fetched = false;
    state.error = payload;
}
export const changePending = (state: CommonState) => {
    state.changed = false;
}
export const changeRejected = (state: CommonState, {payload}: PayloadAction<string>) => {
    state.changed = false;
    state.error = payload;
}
export const changeSuccessful = (state: CommonState) => {
    state.changed = true;
}
