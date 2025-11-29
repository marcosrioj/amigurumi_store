type ApiConfig = {
  identity: string;
  catalog: string;
  ordering: string;
};

const required = (value: string | undefined, key: string) => {
  if (!value) {
    throw new Error(`Missing env var ${key}`);
  }
  return value;
};

export const apiConfig: ApiConfig = {
  identity: required(import.meta.env.VITE_IDENTITY_API_URL, "VITE_IDENTITY_API_URL"),
  catalog: required(import.meta.env.VITE_CATALOG_API_URL, "VITE_CATALOG_API_URL"),
  ordering: required(import.meta.env.VITE_ORDERING_API_URL, "VITE_ORDERING_API_URL")
};
