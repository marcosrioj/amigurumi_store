import { apiClient } from "./client";
import { apiConfig } from "@amigurumi/config";

export type Product = {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  yarnType: string;
  difficulty: string;
  imageUrl: string;
  createdAtUtc: string;
};

export type CreateProduct = {
  name: string;
  description: string;
  price: number;
  stock: number;
  yarnType: string;
  difficulty: string;
  imageUrl: string;
};

export async function fetchProducts(): Promise<Product[]> {
  const res = await apiClient.get<Product[]>(`${apiConfig.catalog}/api/products`);
  return res.data;
}

export async function createProduct(payload: CreateProduct): Promise<Product> {
  const res = await apiClient.post<Product>(`${apiConfig.catalog}/api/products`, payload);
  return res.data;
}
