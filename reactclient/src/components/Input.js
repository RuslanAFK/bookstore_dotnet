import React from "react";

const Input = ({name, setter, className="form-control", text, textarea=false, ...other}) => {
    return (
        <div>
            <label className="form-label">
                {name}
            </label>
            {textarea ? <textarea
                className={className}
                onChange={(e) => {
                    setter(e.target.value)
                }}
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
            {text && <div className="form-text text-warning">{text}</div>}
        </div>
    )
}

export default Input;