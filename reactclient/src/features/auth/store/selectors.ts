import {AuthState} from "./auth-slice";

export const isAdminOrCreator = ({user}: AuthState) => {
    if (!user)
        return false;
    return user.role === "Admin" || user.role === "Creator";
}

export const isCreator = ({user}: AuthState) => {
    if(!user)
        return false;
    return user.role === "Creator";
}

export const isLoading = ({loading}: AuthState) => loading;

export const  isAuthed = ({user}: AuthState) => user !== null;

