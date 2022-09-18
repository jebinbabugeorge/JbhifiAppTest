import React from "react";
import styles from "./Button.module.css";

const Button = (props) => {
  return (
    <div>
      <button
        type={props.type}
        disabled={props.disabled}
        className={`${styles.button} ${props.disabled && styles.invalid}`}
        onClick={props.onClick}
      >
        {props.children}
      </button>
    </div>
  );
};

export default Button;