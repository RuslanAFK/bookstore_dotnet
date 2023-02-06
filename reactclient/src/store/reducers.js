export const getPending = (state) => {
    state.fetched = false;
    state.error = null;
}
export const getRejected = (state, {payload}) => {
    state.fetched = false;
    state.error = payload;
}
export const changePending = (state) => {
    state.changed = false;
    state.error = null;
}
export const changeRejected = (state, {payload}) => {
    state.changed = false;
    state.error = payload;
}
export const changeSuccessful = (state) => {
    state.changed = true;
}