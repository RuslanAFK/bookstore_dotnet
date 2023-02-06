export const isAdminOrCreator = ({user}) => {
    if (!user)
        return false;
    return user.role === "Admin" || user.role === "Creator";
}

export const isCreator = ({user}) => {
    if(!user)
        return false;
    return user.role === "Creator";
}

export const isLoading = ({loading}) => loading;

export const  isAuthed = ({user}) => user !== null;

export const hasError = ({error}) => error !== null;