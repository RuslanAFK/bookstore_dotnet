import {AxiosError} from "axios";

export const handleError = (e: any, rejectWithValue: any) => {

    if (e instanceof AxiosError && e.code === AxiosError.ERR_NETWORK)
        return rejectWithValue(e.message);

    let message = e.message ?? "Unknown error occurred.";
    let responseData = e?.response?.data;
    let code = responseData?.status ?? e?.response?.status;
    if (code === 404)
        message = "Not found.";
    else if (code === 500)
        message = "Unknown error occurred.";
    else if (typeof responseData === "string")
        message = responseData;
    else if (responseData.errors) {
        let errors = responseData.errors;
        let errorArr = [];
        for (let i in errors)
            errorArr.push(errors[i][0]);
        message = errorArr.join("\n");
    }

    if (message.includes("duplicate"))
        message = "Already exists."

    return rejectWithValue(message);
}
