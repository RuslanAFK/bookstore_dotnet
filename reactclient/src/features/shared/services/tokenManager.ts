import {AxiosRequestHeaders} from "axios";

export const getToken = (thunkAPI: any) => {
    const token = thunkAPI.getState()?.auth?.user?.token;
    if (!token)
        throw new Error("Register please.");
    return token;
}

export const addBearerToken = (token: string, headers?: AxiosRequestHeaders) : AxiosRequestHeaders => {
    if (!headers)
        headers = {};
    headers["Authorization"] = `Bearer ${token}`
    return headers;
}