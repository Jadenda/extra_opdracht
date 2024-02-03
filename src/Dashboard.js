import React, { useEffect, useState } from "react";
import axios from "axios";
import { apiPath } from "./Helper/Api";
import { useNavigate } from "react-router-dom";
import { SetAuthToken } from "./Helper/AuthToken";
import "./CSS/style.css";
// import "./CSS/Theme.css";

  const Dashboard = () => {
  const [attractions, setAttractions] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    const storedUserId = localStorage.getItem("userId");
  console.log("Stored User Id:", storedUserId);
    getAttracties();
  }, []);

  const getAttracties = async () => {
    try {
      const response = await axios.get(apiPath + "api/Attraction/attractions");
      console.log("API Response:", response.data);
      setAttractions(response.data);
    } catch (error) {
      console.error("API Error:", error);
    }
  };

  const handleLogout = () => {
    SetAuthToken(null);
    navigate("/");
  };

  const getAttractionImage = (attractieId) => {
    switch (attractieId) {
      case 1:
        return "Images/reuzenrad.jpg";
      case 2:
        return "Images/LostGravity.jpg";
      case 3:
        return "Images/baron.jpg";
      default:
        return "Images/default.jpg";
    }
  };

  // const getTimerText = (attraction) => {
  //   if (attraction.duur) {
  //     return <CountdownTimer duration={attraction.duur} />;
  //   } else {
  //     return 'Not available';
  //   }
  // };

  const neemDeel = async (attractieId) => {
    try {
      const userId = parseInt(localStorage.getItem("userId"), 10);
  
      console.log("Join queue met AttractieId:", attractieId);
      console.log("Join met User Id:", userId);
  
      const response = await axios.post(
        apiPath + "api/Queue/join",
        { AttractionId: attractieId, UserId: userId },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );
  
      console.log(response.data);
      getAttracties();
    } catch (error) {
      console.error(error);
      alert(error.response.data);
    }
  };

  const verlaatRij = async (attractieId) => {
    try {
      const userId = parseInt(localStorage.getItem("userId"), 10);

      console.log("Verlaat queue met AttractieId:", attractieId);
      console.log("Verlaat met User Id:", userId);

      const response = await axios.delete(
        apiPath + "api/Queue/leave",
        {
          data: { AttractionId: attractieId, UserId: userId },
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      console.log(response.data);
      getAttracties();
    } catch (error) {
      console.error(error);
      alert(error.response.data);
    }
  };

  return (
    <div id="Attracties">
      <h3>Dashboard</h3>
      <ul className="attracties">
        {attractions.map((attraction) => (
          <li key={attraction.attractieId} className="attractie-box">
            <h3>{attraction.Naam}</h3>
            <img
              src={getAttractionImage(attraction.attractieId)}
              alt={`Afbeelding van ${attraction.Naam}`}
              style={{ maxWidth: "200px", maxHeight: "200px" }}
            />
            <p>{`Naam: ${attraction.naam || "Not available"}`}</p>
            <p>{`Aantal wachtende: ${attraction.virtualQueueCount || "Er is geen rij"}`}</p>
            <p>{`Capaciteit: ${attraction.capaciteit || "Not available"}`}</p>
            <p>{`Duur: ${(attraction.duration)}`}</p>
            <p>{`${(attraction.beschrijving)}`}</p>
            <button onClick={() => neemDeel(attraction.attractieId)}>Deelnemen aan wachtrij</button>
            <button onClick={() => verlaatRij(attraction.attractieId)}>
              Verlaat wachtrij</button>
          </li>
        ))}
      </ul>
      <button onClick={handleLogout}>Uitloggen</button>
    </div>
  );
};

export default Dashboard; 