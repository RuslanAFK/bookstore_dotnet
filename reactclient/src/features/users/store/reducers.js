export const getItemsSuccessful = (state, {payload}) => {
    state.fetching = false;
    state.fetched = true;
    state.users = payload.items;
    state.count = payload.count;
}
export const getSingleItemSuccessful = (state, {payload}) => {
    state.fetching = false;
    state.fetched = true;
    state.users = [payload];
}
