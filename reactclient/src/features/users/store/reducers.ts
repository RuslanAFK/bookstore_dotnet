import {UserState} from "./user-slice";
import {AnyAction, PayloadAction} from "@reduxjs/toolkit";
import PaginatedList from "../../shared/interfaces/PaginatedList";
import GetUser from "../interfaces/GetUser";

export const getItemsSuccessful = (state: UserState, {payload}: PayloadAction<PaginatedList<GetUser>>) => {
    state.fetched = true;
    state.fetching = false;
    state.users = payload.items;
    state.count = payload.count;
}
export const getSingleItemSuccessful = (state: UserState, {payload}: PayloadAction<GetUser>) => {
    state.fetched = true;
    state.fetching = false;
    state.users = [payload];
}

