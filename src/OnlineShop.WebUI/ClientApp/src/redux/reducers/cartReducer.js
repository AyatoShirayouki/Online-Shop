// cartReducer.js

/**
 * cartReducer handles actions related to the shopping cart in the Redux store.
 * It manages the state of cart items, including adding, removing, and updating quantities.
 * 
 * The initialState includes an empty array for cartItems and properties for discounts.
 */

import {
    ADD_TO_CART,
    REMOVE_FROM_CART,
    INCREASE_QUANTITY,
    DECREASE_QUANTITY,
    LOAD_CART,
} from '../actions/cartActions';

const initialState = {
    cartItems: [],
};

const cartReducer = (state = initialState, action) => {
    switch (action.type) {
        case LOAD_CART:
            return {
                ...state,
                cartItems: Array.isArray(action.payload.items) ? action.payload.items : [],
                discount: action.payload.discount || 0,
                discountDescription: action.payload.discountDescription || '',
            };
        case ADD_TO_CART:
            const existingProduct = state.cartItems.find(
                (item) => item.id === action.payload.id
            );
            if (existingProduct) {
                return {
                    ...state,
                    cartItems: state.cartItems.map((item) =>
                        item.id === action.payload.id
                            ? { ...item, quantity: item.quantity + 1 }
                            : item
                    ),
                };
            } else {
                return {
                    ...state,
                    cartItems: [...state.cartItems, { ...action.payload, quantity: 1 }],
                };
            }
        case REMOVE_FROM_CART:
            return {
                ...state,
                cartItems: state.cartItems.filter((item) => item.id !== action.payload.id),
            };
        case INCREASE_QUANTITY:
            return {
                ...state,
                cartItems: state.cartItems.map((item) =>
                    item.id === action.payload.id
                        ? { ...item, quantity: item.quantity + 1 }
                        : item
                ),
            };
        case DECREASE_QUANTITY:
            return {
                ...state,
                cartItems: state.cartItems.map((item) =>
                    item.id === action.payload.id
                        ? { ...item, quantity: item.quantity - 1 }
                        : item
                ),
            };
        default:
            return state;
    }
};

export default cartReducer;
