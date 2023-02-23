import React from "react";

type Params = {
    name: string,
    setter: Function,
    className?: "form-control" | "form-check",
    text?: string,
    textarea?: boolean,
    textStyle?: "warning" | "danger",
    value?: any,
    rows?: number,
    type?: "password" | "text"
}

const Input = ({name, setter, className="form-control", text, textarea=false,
                   textStyle="warning", value, rows, type="text"}: Params) => {
    return (
        <div className="my-2">
            <label className="form-label">
                {name}
            </label>
            {textarea ? <textarea
                className={className}
                onChange={(e) => setter(e.target.value)}
                value={value}
                rows={rows}
            /> :
            <input
                className={className}
                onChange={(e) => {
                    if (className === "form-control")
                        setter(e.target.value)
                    else if (className === "form-check")
                        setter(e.target.checked)
                }}
                value={value}
                type={type}
            />}
            {text && <div className={`form-text text-${textStyle}`}>{text}</div>}
        </div>
    )
}

export default Input;