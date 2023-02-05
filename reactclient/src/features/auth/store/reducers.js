export const authRejected = (state, {payload}) => {
    state.loading = false;
    state.error = payload;
}

export const loginPending = (state) => {
    state.loading = true;
    state.error = null;
}

export const loginSuccessful = (state, {payload}) => {
    state.loading = false;
    state.user = payload;
}
