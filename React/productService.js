import axios from "axios";
import { API_HOST_PREFIX } from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}`;

const GetProductById = (id) => {
  const config = {
    method: "GET",
    url: endpoint + `/api/product/${id}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

const GetCurrentSubscription = () => {
  const config = {
    method: "GET",
    url: endpoint + "/api/product/current",
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config);
};

export { GetProductById, GetCurrentSubscription };
