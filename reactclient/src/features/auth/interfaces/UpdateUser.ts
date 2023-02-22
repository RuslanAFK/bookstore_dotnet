interface UpdateUser {
    id: number,
    username: string,
    password: string,
    newPassword?: string
}

export default UpdateUser;