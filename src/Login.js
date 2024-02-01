import React, { useRef, useState } from "react";
import axios from "axios";
import { SetAuthToken } from "./Helper/AuthToken";
import {apiPath} from "./Helper/Api";
import useLocalStorage from "use-local-storage";

import "./CSS/StichtingTheme.css"
import { useNavigate } from "react-router-dom";

 const Login = () => {
  const usernameRef = useRef(null);
  const passwordRef = useRef(null);
  const [error, setError] = useState(null);
  // const nav = useNavigate();
  // const response = await axios.post("http://localhost:5000/api/login", info);

//   useEffect(() => {
//     nav("/Dashboard");
//     }, [nav]);

const handleLogin = async () => {
    const info = {
        Gebruikersnaam: usernameRef.current.value,
        Wachtwoord: passwordRef.current.value
    };

    try {
        const response = await axios.post(apiPath + "api/auth/login", info);
        
        SetAuthToken(response.data);
        console.log("Inloggen gelukt", response.data);

       // nav("/Dashboard");
    } catch (err) {
        console.log(err);

        // Handel de fout af, bijvoorbeeld, toon een foutmelding
        setError("Gebruikersnaam of wachtwoord is onjuist");
    }
};


  // axios
  //     .post(apiPath + "Login", info)
  //     .then(response => {
  //         // const token = response.data.api_key;
  //         SetAuthToken(response.data);

      // }).catch((err) => {
      // console.log(err.toJSON());

      
  return (
    <div className="login-container">
      <h2>Login</h2>
      {error && <p className="error-message">{error}</p>}
      <label htmlFor="username">Gebruikersnaam</label>
      <br/>
      <input 
      type="text" 
      id="username" 
      ref={usernameRef} 
      aria-label="Invoerveld gebruikersnaam"
      className="inputFontSize"
      />

      <label htmlFor="password">Wachtwoord</label>
      <br/>
      <input 
      type="password" 
      id="password" 
      ref={passwordRef} 
      aria-label="Invoerveld wachtwoord"
      className="inputFontSize"
      />

      <button 
      aria-label="Log in"
      type="button" 
      onClick={handleLogin}>
        Log in
      </button>
    </div>
  );
}

export default Login;

