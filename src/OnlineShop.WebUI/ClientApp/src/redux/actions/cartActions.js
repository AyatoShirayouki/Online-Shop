// cartActions.js

/**
 * cartActions defines Redux actions for managing the shopping cart state.
 * These actions include adding to cart, removing from cart, adjusting quantity, and loading the cart.
 * 
 * API interactions are performed using axios, and the base URL is configured using an environment variable.
 */

import axios from 'axios';
export const ADD_TO_CART = 'ADD_TO_CART';
export const REMOVE_FROM_CART = 'REMOVE_FROM_CART';
export const INCREASE_QUANTITY = 'INCREASE_QUANTITY';
export const DECREASE_QUANTITY = 'DECREASE_QUANTITY';
export const LOAD_CART = 'LOAD_CART';

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL;
const userId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';

/**
 * Adds a product to the cart.
 * Dispatches a LOAD_CART action upon success to refresh the cart state.
 * @param {Object} product - The product to add to the cart.
 * @returns {Function} A thunk action to be handled by Redux Thunk middleware.
 */

export const addToCart = (product) => async (dispatch) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/ShoppingCart/add`, {
            userId: userId,
            productId: product.id,
            quantity: 1,
        });
        if (response.status === 200) {
            dispatch(loadCart());
        } else {
            console.error('Failed to add to cart', response);
        }
    } catch (error) {
        console.error('Failed to add to cart', error);
    }
};

/**
 * Removes a product from the cart.
 * Dispatches a LOAD_CART action upon success to refresh the cart state.
 * @param {string} productId - The ID of the product to remove from the cart.
 * @returns {Function} A thunk action to be handled by Redux Thunk middleware.
 */

export const removeFromCart = (productId) => async (dispatch) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/ShoppingCart/remove`, {
            userId: userId,
            productId: productId,
        });
        if (response.status === 200) {
            dispatch(loadCart());
        } else {
            console.error('Failed to remove from cart', response);
        }
    } catch (error) {
        console.error('Failed to remove from cart', error);
    }
};

/**
 * Increases the quantity of a product in the cart.
 * Dispatches a LOAD_CART action upon success to refresh the cart state.
 * @param {string} productId - The ID of the product.
 * @returns {Function} A thunk action to be handled by Redux Thunk middleware.
 */

export const increaseQuantity = (productId) => async (dispatch) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/ShoppingCart/add`, {
            userId: userId,
            productId: productId,
            quantity: 1,
        });
        if (response.status === 200) {
            dispatch(loadCart());
        } else {
            console.error('Failed to increase quantity', response);
        }
    } catch (error) {
        console.error('Failed to increase quantity', error);
    }
};

/**
 * Decreases the quantity of a product in the cart.
 * If the quantity reaches zero, the product is removed from the cart.
 * Dispatches a LOAD_CART action upon success to refresh the cart state.
 * @param {string} productId - The ID of the product.
 * @returns {Function} A thunk action to be handled by Redux Thunk middleware.
 */

export const decreaseQuantity = (productId) => async (dispatch) => {
    try {
        const response = await axios.post(`${API_BASE_URL}/ShoppingCart/add`, {
            userId: userId,
            productId: productId,
            quantity: -1,
        });
        if (response.status === 200) {
            dispatch(loadCart());
        } else {
            console.error('Failed to decrease quantity', response);
        }
    } catch (error) {
        console.error('Failed to decrease quantity', error);
    }
};

/**
 * Loads the cart items from the server.
 * Dispatches a LOAD_CART action to update the Redux store with the cart items.
 * @returns {Function} A thunk action to be handled by Redux Thunk middleware.
 */

export const loadCart = () => async (dispatch) => {
    try {
        const response = await axios.get(`${API_BASE_URL}/ShoppingCart?userId=${userId}`);
        dispatch({
            type: LOAD_CART,
            payload: response.data,
        });
    } catch (error) {
        console.error('Failed to load cart', error);
    }
};
