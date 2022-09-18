import React, { useState } from "react";
import styles from "./WeatherForm.module.css";
import Button from "../UI/Button";
import api from "../../services/api/weather/weather";
import { validApiKey } from "../../utility/regex";

const WeatherForm = (props) => {
  const [apiKey, setApiKey] = useState("");
  const [isValidApiKey, setIsValidApiKey] = useState(true);
  const [city, setCity] = useState("");
  const [isValidCity, setIsValidCity] = useState(true);
  const [country, setCountry] = useState("");
  const [isValidCountry, setIsValidCountry] = useState(true);

  const ApiKeyInputChangeHandler = (event) => {
    const input = event.target.value.trim();

    setIsValidApiKey(validApiKey.test(input));

    setApiKey(input);
  };

  const CityInputChangeHandler = (event) => {
    const input = event.target.value.trim();

    setIsValidCity(input.length > 0);

    setCity(input);
  };

  const CountryInputChangeHandler = (event) => {
    const input = event.target.value.trim();

    setIsValidCountry(input.length > 0);

    setCountry(input);
  };

  const fetchWeather = async () => {
    try {
      const response = await api.get(
        `/weatherforecast?city=${city}&&country=${country}`,
        {
          headers: {
            "api-key": apiKey,
          },
        }
      );

      props.OnSubmit(response.data);
    } catch (err) {
      props.OnSubmit(err.response.data);
    }
  };

  const GetWeatherHandler = () => {
    fetchWeather();
  };

  return (
    <div>
      <div>
        <div
          className={`${styles["form-control"]} ${
            !isValidApiKey && styles.invalid
          }`}
        >
          <label>Api Key</label>
          <input type="text" onChange={ApiKeyInputChangeHandler} />
        </div>
        <div
          className={`${styles["form-control"]} ${
            !isValidCity && styles.invalid
          }`}
        >
          <label>City</label>
          <input type="text" onChange={CityInputChangeHandler} />
        </div>
        <div
          className={`${styles["form-control"]} ${
            !isValidCountry && styles.invalid
          }`}
        >
          <label>Country</label>
          <input type="text" onChange={CountryInputChangeHandler} />
        </div>
      </div>
      <Button
        type="submit"
        disabled={
          !validApiKey.test(apiKey) || !city.length > 0 || !country.length > 0
        }
        onClick={GetWeatherHandler}
      >
        Get Weather Details
      </Button>
    </div>
  );
};

export default WeatherForm;
