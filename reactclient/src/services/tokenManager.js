export const getToken = (thunkAPI) => {
    const token = thunkAPI.getState()?.auth?.user?.token;
    if (!token)
        throw new Error("Register please.");
    return {headers: {'Authorization':`Bearer ${token}`}};
}