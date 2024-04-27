import React from "react";
import InputProps from "../component-props/InputProps";

const Input = ({name, setter, className="form-control", text, textarea=false, required=true,
                   textStyle="black", value, rows, type="text", minLength, maxLength, placeholder,
                    min, max
}: InputProps) => {
    return (
        <div className="my-4 row">
            <div className="col-4">
                <label>{name}</label>
            </div>
            <div className="col-8">
            {textarea ? <textarea
                        minLength={minLength}
                        maxLength={maxLength}
                        required={required}
                        id={name}
                        placeholder={placeholder}
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
                        placeholder={placeholder}
                        className={className}
                        onChange={(e) => {
                            if (className === "form-control")
                                setter(e.target.value)
                            else if (className === "form-check")
                                setter(e.target.checked)
                        }}
                        value={value}
                        type={type}
                        min={min}
                        max={max}
                    />}
                {text && <div className={`form-text text-${textStyle}`}>{text}</div>}
            </div>
        </div>
    )
}

export default Input;