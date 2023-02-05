export const isAdmin = ({user}) => user && user.role === "Admin";

export const isLoading = ({loading}) => loading;

export const  isAuthed = ({user}) => user !== null;

export const hasError = ({error}) => error;