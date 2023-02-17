import {combineReducers, configureStore} from "@reduxjs/toolkit";
import authSlice from "../features/auth/store/authSlice";
import bookSlice from "../features/books/store/bookSlice";
import userSlice from "../features/users/store/userSlice";

const rootReducer = combineReducers({
    auth: authSlice.reducer,
    book: bookSlice.reducer,
    user: userSlice.reducer,
})

export const store = configureStore({
    reducer: rootReducer
})
