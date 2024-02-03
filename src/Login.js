import React, { useRef, useState } from "react";
import axios from "axios";
import { SetAuthToken } from "./Helper/AuthToken";
import {apiPath} from "./Helper/Api";
import "./CSS/style.css"

import "./CSS/StichtingTheme.css"
import { useNavigate } from "react-router-dom";

 const Login = () => {
  const usernameRef = useRef(null);
  const passwordRef = useRef(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

// const handleLogin = async () => {
//     const info = {
//         Gebruikersnaam: usernameRef.current.value,
//         Wachtwoord: passwordRef.current.value
//     };

//     try {
//         const response = await axios.post(apiPath + "api/Auth/login", info);
        
//         SetAuthToken(response.data);
//         console.log("Inloggen gelukt", response.data);

//        nav('/App');
//     } catch (err) {
//         console.log(err);

//         setError("Gebruikersnaam of wachtwoord is onjuist");
//     }
// };
      
  return (
    <div className="login-container">
      <h2>Login</h2>
      {/* {error && <p className="error-message">{error}</p>} */}
      <label htmlFor="username">Gebruikersnaam</label>
      <br/>
      <input 
      type="text" 
      id="username" 
      ref={usernameRef} 
      aria-label="Invoerveld gebruikersnaam"
      className="inputFontSize"
      />
<br/>
      <label htmlFor="password">Wachtwoord</label>
      <br/>
      <input 
      type="password" 
      id="password" 
      ref={passwordRef} 
      aria-label="Invoerveld wachtwoord"
      className="inputFontSize"
      />
      {error && <p className="error-message">{error}</p>}

      <button 
      aria-label="Log in"
      type="button" 
      onClick={async () => {
        const info = {
          Gebruikersnaam: usernameRef.current.value,
          Wachtwoord: passwordRef.current.value,
        };

        try {
          const response = await axios.post(apiPath + "api/Auth/login", info);

          SetAuthToken(response.data);

          console.log("Inloggen gelukt", response.data);

          navigate("/dashboard");
        } catch (err) {
          console.log(err);
          setError("Gebruikersnaam of wachtwoord is onjuist");
        }
      }}
        >
        Log in
      </button>
    </div>
  );
}

export default Login;

