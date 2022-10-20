
export const isBookDataValid = (bookData) => {
    const { name, genre, author, image, info } = bookData;

    if (name.length < 6 || name.length > 36) {
        alert("Name must be from 6 to 36 symbols.");
        return false;
    }
    else if (genre.length < 6 || genre.length > 36) {
        alert("Genres must be from 6 to 36 symbols.");
        return false;
    }
    else if (author.length < 6 || author.length > 36) {
        alert("Author name must be from 6 to 36 symbols.");
        return false;
    }
    else if (image.length < 10 || image.length > 1000) {
        alert("Image url must be from 10 to 1000 symbols.");
        return false;
    }
    else if (info.length < 10 || info.length > 400) {
        alert("Description must be from 10 to 400 symbols.");
        return false;
    }
    return true;
}

export const isUserDataValid = (userData) => {
    const {username, password} = userData;
    if (username.length < 6 || username.length > 16) {
        alert("Username must be from 6 to 16 symbols.");
        return false;
    }
    else if (password.length < 6 || password.length > 16) {
        alert("Password must be from 6 to 16 symbols.");
        return false;
    }
    return true;
}