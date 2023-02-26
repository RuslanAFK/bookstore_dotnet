import React from "react";
import InputProps from "../component-props/InputProps";

const Input = ({name, setter, className="form-control", text, textarea=false, required=true,
                   textStyle="warning", value, rows, type="text", minLength, maxLength}: InputProps) => {
    return (
        <div className="my-2">
            <div className="form-floating my-3">
                {textarea ? <textarea
                        minLength={minLength}
                        maxLength={maxLength}
                        required={required}
                        id={name}
                        placeholder={name}
                        className="form-control"
                        onChange={(e) => setter(e.target.value)}
                        value={value}
                        rows={rows}
                    /> :
                    <input
                        minLength={minLength}
                        maxLength={maxLength}
                        required={required}
                        id={name}
                        placeholder={name}
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
                <label htmlFor={name} className="floating-label">
                    {name}
                </label>
                {text && <div className={`form-text text-${textStyle}`}>{text}</div>}
            </div>
        </div>
    )
}

export default Input;