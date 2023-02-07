import React from "react";

const Input = ({name, setter, className="form-control", text, textarea=false,
                   textStyle="warning", ...other}) => {
    return (
        <div className="my-2">
            <label className="form-label">
                {name}
            </label>
            {textarea ? <textarea
                className={className}
                onChange={(e) => setter(e.target.value)}
                {...other}
            /> :
            <input
                className={className}
                onChange={(e) => {
                    if (className === "form-control")
                        setter(e.target.value)
                    else if (className === "form-check")
                        setter(e.target.checked)
                }}
                {...other}
            />}
            {text && <div className={`form-text text-${textStyle}`}>{text}</div>}
        </div>
    )
}

export default Input;