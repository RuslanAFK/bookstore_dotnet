export const throwHttpError = async (response) => {
    if (!response.ok){
        const errorMessage = await response.json();
        throw new Error(errorMessage);
    }
    else
        return response;
}