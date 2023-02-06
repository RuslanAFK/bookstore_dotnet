export const getPending = (state) => {
    state.fetched = false;
    state.fetching = true;
    state.error = null;
}

export const getRejected = (state, {payload}) => {
    state.fetching = false;
    state.fetched = false;
    state.error = payload;
}

export const getUsersSuccessful = (state, {payload}) => {
    state.fetching = false;
    state.fetched = true;
    state.users = payload.items;
    state.count = payload.count;
}

export const getSingleUserSuccessful = (state, {payload}) => {
    state.fetching = false;
    state.fetched = true;
    state.users = [payload];
}
