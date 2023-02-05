const handleError = (e=new Error("Unknown error occurred."), rejectWithValue) => {
    let message = e.message;
    let responseData = e?.response?.data;
    let code = responseData?.status;
    if (code === 404)
        message = "Not found.";
    else if (typeof responseData === "string")
        message = responseData;
    else if (responseData.errors) {
        let errors = responseData.errors;
        for (let i in errors) {
            message = errors[i][0] ?? message;
            break;
        }
    }
    return rejectWithValue(message);
}
export default handleError;