import {combineReducers, configureStore} from "@reduxjs/toolkit";
import authSlice from "../../auth/store/auth-slice";
import bookSlice from "../../books/store/book-slice";
import userSlice from "../../users/store/user-slice";


const rootReducer = combineReducers({
    auth: authSlice.reducer,
    book: bookSlice.reducer,
    user: userSlice.reducer,
});


export const store = configureStore({
    reducer: rootReducer
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch;
