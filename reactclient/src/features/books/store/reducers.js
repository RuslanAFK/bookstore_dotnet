export const getItemsSuccessful = (state, {payload}) => {
    state.fetched = true;
    state.books = payload.items;
    state.count = payload.count;
}
export const getSingleItemSuccessful = (state, {payload}) => {
    state.fetched = true;
    state.books = [payload];
}

