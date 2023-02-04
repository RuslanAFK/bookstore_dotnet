import {combineReducers, configureStore} from "@reduxjs/toolkit";
import authSlice from "./features/auth/store/authSlice";
import bookSlice from "./features/books/store/bookSlice";


const rootReducer = combineReducers({
    auth: authSlice.reducer,
    book: bookSlice.reducer
})


export const store = configureStore({
    reducer: rootReducer
})
