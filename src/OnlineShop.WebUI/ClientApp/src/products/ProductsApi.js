//ProductsApi.js

/**
 * ProductsApi provides functions to interact with the products API.
 * It includes methods to fetch all products and fetch details of a single product.
 * 
 * The base URL for the API is configured using an environment variable.
 */

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;
const GET_PRODUCTS_API_URL = `${API_BASE_URL}/Products`;

/**
 * Fetches all products from the API.
 * @returns {Promise<Array>} A promise that resolves to an array of products.
 */

const getProducts = async () => {
  const response = await fetch(GET_PRODUCTS_API_URL);
  return await response.json();
};

/**
 * Fetches details of a single product by ID.
 * @param {string} id - The ID of the product.
 * @returns {Promise<Object>} A promise that resolves to the product details.
 */

const getProduct = async (id) => {
  const response = await fetch(`${GET_PRODUCTS_API_URL}/${id}`);
  return await response.json();
};

export { getProducts, getProduct };
