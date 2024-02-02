import axios from "axios";

export const SetAuthToken = (data) => {
  console.log(data);
  if (data && data.token && data.userId) { // Updated to use lowercase properties
    const token = data.token;
    const userId = data.userId; // Updated to use lowercase properties
    localStorage.setItem("token", token);
    localStorage.setItem("userId", userId);
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete axios.defaults.headers.common['Authorization'];
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
  }
};


export const GetAuthTokenUser = () => {
  const token = localStorage.getItem("token");
  const userId = localStorage.getItem("userId");

  if (token === null || userId === null) return false;

  return { token, userId };
};
