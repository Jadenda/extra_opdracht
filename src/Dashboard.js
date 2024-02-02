import React, { useState, useEffect } from "react";
import axios from "axios";
import { apiPath } from "./Helper/Api";
import { useNavigate } from "react-router-dom";
import { SetAuthToken } from "./Helper/AuthToken";

const CountdownTimer = ({ duration }) => {
  const [remainingTime, setRemainingTime] = useState(duration);

  useEffect(() => {
    const timer = setInterval(() => {
      setRemainingTime((prevTime) => (prevTime > 0 ? prevTime - 1 : 0));
    }, 1000);

    return () => clearInterval(timer);
  }, [duration]);

  const formatDuration = (duration) => {
    const hours = Math.floor(duration / 3600);
    const minutes = Math.floor((duration % 3600) / 60);
    const seconds = duration % 60;
    return `${hours > 0 ? hours + "h " : ""}${minutes > 0 ? minutes + "m " : ""}${seconds}s`;
  };

  return <span>{formatDuration(remainingTime)}</span>;
};

  const Dashboard = () => {
  const [attractions, setAttractions] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
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

  const getTimerText = (attraction) => {
    if (attraction.duur) {
      return <CountdownTimer duration={attraction.duur} />;
    } else {
      return 'Not available';
    }
  };

  const neemDeel = async (attractieId) => {
    try {
        const response = await axios.post(apiPath + `api/Queue/join/${attractieId}`);
        console.log(response.data);
        // Refresh the list of attractions after joining the queue
        // getAttracties();
    } catch (error) {
        console.error("Error joining the queue", error);
    }
};

  return (
    <div>
      <h2>Dashboard</h2>
      <ul>
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
            <p>{`Duur: ${getTimerText(attraction)}`}</p>
            <button onClick={() => neemDeel(attraction.attractieId)}>Neem deel aan wachtrij</button>
          </li>
        ))}
      </ul>
      <button onClick={handleLogout}>Uitloggen</button>
    </div>
  );
};

export default Dashboard;
