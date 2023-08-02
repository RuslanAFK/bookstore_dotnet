import {AxiosError} from "axios";

export const handleError = (e: any, rejectWithValue: any) => {
    let responseData = e.response.data
    let message = responseData.error;
    if (!message) {
        message = getValidationErrorMessage(e);
    }
    return rejectWithValue(message);
}

const getValidationErrorMessage = (e: any) : string => {
    let validationErrorObject = e.response.data.errors;
    let errors: any = Object.values(validationErrorObject)[0];
    return errors[0];
}
