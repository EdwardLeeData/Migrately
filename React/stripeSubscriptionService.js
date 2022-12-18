import axios from "axios";
import { API_HOST_PREFIX } from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}`;

const PostSubscriptionData = (subscriptionObj) => {
  const config = {
    method: "POST",
    url: endpoint + "/api/subscription",
    withCredentials: true,
    data: subscriptionObj,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};
const CreateInvoicePdf = (subscriptionId) => {
  const config = {
    method: "POST",
    url:
      endpoint + `/api/subscription/invoice?subscriptionId=${subscriptionId}`,
    withCredentials: true,
    data: subscriptionId,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

const GetSubscriptionDetails = (subscriptionId) => {
  const config = {
    method: "GET",
    url: endpoint + `/api/subscription?subscriptionId=${subscriptionId}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

const GetPayloads = (sessionId) => {
  const config = {
    method: "GET",
    url: endpoint + `/api/subscription/session?sessionId=${sessionId}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

export {
  PostSubscriptionData,
  CreateInvoicePdf,
  GetSubscriptionDetails,
  GetPayloads,
};
