import React, { useState, useEffect } from "react";
import axios from "axios";
import { apiPath } from "./Helper/Api";
import { useNavigate } from "react-router-dom";
import { SetAuthToken } from "./Helper/AuthToken";
// import "./CSS/style.css";
import "./CSS/Theme.css";

  const Dashboard = () => {
  const [attractions, setAttractions] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    const storedUserId = localStorage.getItem("userId");
  console.log("Stored User Id:", storedUserId);
    getAttracties();
  }, []);

  const getAttracties = async () => {
    axios.get(apiPath + "api/Attraction").then((response) => {
      setAttractions(response.data);
    });
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
        return "Images/attractie2.jpg";
      case 3:
        return "Images/attractie3.jpg";
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
      // const attractieId = attractions.attractieId;

      console.log("Join queue met AttractieId:", attractieId);
      console.log("Join met User Id:", userId);

      const response = await axios.post(
        apiPath + "api/Queue/join",
        { AttractieId: attractieId, UserId: userId },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      );

      console.log(response.data);
      // Refresh the list of attractions after joining the queue
      getAttracties();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div>
      <h2>Dashboard</h2>
      <ul className="attracties">
        {attractions.map((attraction) => (
          <li key={attraction.attractieId}>
            <h3>{attraction.Naam}</h3>
            <img
              src={getAttractionImage(attraction.attractieId)}
              alt={`Afbeelding van ${attraction.Naam}`}
              style={{ maxWidth: "200px", maxHeight: "200px" }}
            />
            <p>{`id: ${attraction.attractieId || "Not available"}`}</p>
            <p>{`Virtuele rij: ${attraction.VirtualQueue || "Not available"}`}</p>
            <p>{`Capaciteit: ${attraction.capaciteit || "Not available"}`}</p>
            <p>{`Duur: ${(attraction.duration)}`}</p>
            <button onClick={() => neemDeel(attraction.attractieId)}>Neem deel aan wachtrij</button>
          </li>
        ))}
      </ul>
      <button onClick={handleLogout}>Uitloggen</button>
    </div>
  );
};

export default Dashboard;
