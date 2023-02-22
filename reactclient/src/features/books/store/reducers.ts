import {BookState} from "./bookSlice";
import {PayloadAction} from "@reduxjs/toolkit";
import PaginatedList from "../../../interfaces/PaginatedList";
import GetBookDetails from "../interfaces/GetBookDetails";
import GetBook from "../interfaces/GetBook";

export const getItemsSuccessful = (state: BookState, {payload}: PayloadAction<PaginatedList<GetBook>>) => {
    state.fetched = true;
    state.books = payload.items;
    state.count = payload.count;
}
export const getSingleItemSuccessful = (state: BookState, {payload}: PayloadAction<GetBookDetails>) => {
    state.fetched = true;
    state.books = [payload];
}

