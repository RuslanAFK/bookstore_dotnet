import {HTMLInputTypeAttribute} from "react";

interface InputProps {
    // Input props
    name: string,
    setter: Function,
    className?: "form-control" | "form-check",
    value?: any,

    // Type of input
    type?: HTMLInputTypeAttribute,

    // Validation props
    minLength?: number,
    maxLength?: number,
    required?: boolean,

    // Text props
    textStyle?: "warning" | "danger",
    text?: string,

    // Textarea props
    textarea?: boolean,
    rows?: number,
}

export default InputProps;