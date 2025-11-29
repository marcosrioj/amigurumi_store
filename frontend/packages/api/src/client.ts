import axios from "axios";

export const apiClient = axios.create({
  timeout: 8000
});

apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    const message = error?.response?.data?.detail ?? error?.message ?? "Unexpected error";
    return Promise.reject(new Error(message));
  }
);
