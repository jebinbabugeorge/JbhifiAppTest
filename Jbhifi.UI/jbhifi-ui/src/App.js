import React, { useState } from "react";
import "./App.css";
import WeatherForm from "./components/WeatherForm/WeatherForm";

function App() {
  const [content, setContent] = useState("");

  const WeatherFormHandler = (description) => {
    setContent(description);
  };

  return (
    <div>
      <section id="weather-form">
        <WeatherForm OnSubmit={WeatherFormHandler} />
      </section>
      <section id="weather">
        <p style={{ textAlign: "center" }}>{content}</p>
      </section>
    </div>
  );
}

export default App;
