import {toast} from "react-toastify";

const notify = (message, type) => {
    toast(message, {
        type: type
    })
}

export default notify;