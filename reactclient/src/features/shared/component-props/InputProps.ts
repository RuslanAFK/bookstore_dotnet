import {HTMLInputTypeAttribute} from "react";

interface InputProps {
    // Input props
    name: string,
    setter: Function,
    className?: "form-control" | "form-check",
    value?: any,
    placeholder?: string,

    // Type of input
    type?: HTMLInputTypeAttribute,

    // Validation props
    minLength?: number,
    maxLength?: number,
    required?: boolean,
    min?: number,
    max?: number,

    // Text props
    textStyle?: "warning" | "danger" | "black",
    text?: string,

    // Textarea props
    textarea?: boolean,
    rows?: number,
}

export default InputProps;