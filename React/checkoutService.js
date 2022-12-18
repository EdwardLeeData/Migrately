import axios from "axios";
import { API_HOST_PREFIX } from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}`;

const CreateCheckoutSession = (priceIdObj) => {
  const config = {
    method: "POST",
    url: endpoint + "/api/checkout/session",
    withCredentials: true,
    data: priceIdObj,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

const OnboardStripeAccount = () => {
  const config = {
    method: "POST",
    url: endpoint + "/api/checkout/account",
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

const GetSessionDetails = (sessionId) => {
  const config = {
    method: "GET",
    url: endpoint + `/api/checkout/order?session_id=${sessionId}`,
    withCredentials: true,
    data: sessionId,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

export { CreateCheckoutSession, OnboardStripeAccount, GetSessionDetails };
