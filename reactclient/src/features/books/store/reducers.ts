import {BookState} from "./book-slice";
import {PayloadAction} from "@reduxjs/toolkit";
import PaginatedList from "../../shared/interfaces/PaginatedList";
import GetBookDetails from "../interfaces/GetBookDetails";
import GetBook from "../interfaces/GetBook";

export const getItemsSuccessful = (state: BookState, {payload}: PayloadAction<PaginatedList<GetBook>>) => {
    state.fetched = true;
    state.fetching = false;
    state.books = payload.items;
    state.count = payload.count;
}
export const getSingleItemSuccessful = (state: BookState, {payload}: PayloadAction<GetBookDetails>) => {
    state.fetched = true;
    state.fetching = false;
    state.books = [payload];
}

