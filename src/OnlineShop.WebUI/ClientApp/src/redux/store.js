// store.js

/**
 * store.js sets up the Redux store for the application.
 * It combines reducers, applies middleware (Redux Thunk), and integrates Redux DevTools.
 * 
 * The store is configured to manage the application state and handle asynchronous actions.
 */

import { createStore, combineReducers, applyMiddleware} from 'redux';
import { thunk } from 'redux-thunk';
import cartReducer from './reducers/cartReducer';
import { composeWithDevTools } from '@redux-devtools/extension';

const rootReducer = combineReducers({
    cart: cartReducer,
});

const composeEnhancers = composeWithDevTools({});

const store = createStore(
    rootReducer,
    composeEnhancers(applyMiddleware(thunk))
);

export default store;
