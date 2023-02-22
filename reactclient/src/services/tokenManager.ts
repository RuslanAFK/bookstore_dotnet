import {AxiosRequestConfig} from "axios";

export const getToken = (thunkAPI: any) => {
    const token = thunkAPI.getState()?.auth?.user?.token;
    if (!token)
        throw new Error("Register please.");
    const config: AxiosRequestConfig = {}
    config.headers = {'Authorization':`Bearer ${token}`};
    return config;
}