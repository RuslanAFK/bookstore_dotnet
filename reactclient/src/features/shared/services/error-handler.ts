import {AxiosError} from "axios";

export const handleError = (e: any, rejectWithValue: any) => {
    let message = e.response.data.error;
    return rejectWithValue(message);
}
