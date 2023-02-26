import MainLabelProps from "../component-props/MainLabelProps";
import React from "react";

const MainLabel = ({text}: MainLabelProps) => {
  return (
      <div className="text-center my-3">
          <h1>{text}</h1>
      </div>)
}

export default MainLabel;