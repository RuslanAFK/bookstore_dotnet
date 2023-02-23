import {PayloadAction} from "@reduxjs/toolkit";

export interface CommonState {
    fetched: boolean,
    changed: boolean,
    fetching: boolean,
    changing: boolean,
    error: string | null,
}

export const getPending = (state: CommonState) => {
    state.fetched = false;
    state.fetching = true;
}
export const getRejected = (state: CommonState, {payload}: PayloadAction<string>) => {
    state.fetched = false;
    state.fetching = false;
    state.error = payload;
}
export const changePending = (state: CommonState) => {
    state.changed = false;
    state.changing = true;
}
export const changeRejected = (state: CommonState, {payload}: PayloadAction<string>) => {
    state.changed = false;
    state.changing = false;
    state.error = payload;
}
export const changeSuccessful = (state: CommonState) => {
    state.changed = true;
    state.changing = false;
}
