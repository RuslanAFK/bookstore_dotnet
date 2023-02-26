import {AxiosError} from "axios";

export const handleError = (e: any, rejectWithValue: any) => {

    // Network Error
    if (e.code === AxiosError.ERR_NETWORK)
        return rejectWithValue(e.message);

    // Assign default
    let message = e.message;
    let responseData: any = e?.response?.data;
    let code = e?.response?.status;
    // 404 Error
    if (code === 404)
        message = "Not found.";
    // Validation error
    else if (responseData.errors) {
        let errors = responseData.errors;
        let errorArr = [];
        for (let i in errors)
            errorArr.push(errors[i][0]);
        message = errorArr.join("\n");
    }
    // Filtered error
    else if (responseData.title)
        message = responseData.title;


    // Duplicate error (of Filtered)
    if (message.includes("duplicate"))
        message = "Already exists."

    return rejectWithValue(message);
}
