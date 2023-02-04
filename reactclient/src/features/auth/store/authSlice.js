import {createSlice} from "@reduxjs/toolkit";
import {login, register} from "./thunks";

const initialState = {
    user: null,
    error: null,
    success: false,
    loading: false
}

const authSlice = createSlice({
    name: "auth",
    initialState: initialState,
    reducers: {
        logout(state) {
            state.user = null;
            state.success = false;
        }
    },
    extraReducers: {
        [login.pending || register.pending]: (state) => {
            state.loading = true;
        },
        [login.rejected || register.rejected]: (state, {payload}) => {
            state.loading = false;
            state.success = false;
            state.error = payload;
        },
        [login.fulfilled]: (state, {payload}) => {
            state.loading = false;
            state.success = true;
            state.user = payload;
        }
    }
})

export const {logout} = authSlice.actions;

export default authSlice;