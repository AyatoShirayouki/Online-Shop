// index.js

/**
 * index.js is the entry point for the React application.
 * It sets up the React app, integrates the Redux store, and configures routing.
 * 
 * The app is wrapped in a Redux Provider and BrowserRouter to provide state management and routing capabilities.
 */

import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider, useDispatch } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import store from './redux/store';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';
import { loadCart } from './redux/actions/cartActions';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');
const root = createRoot(rootElement);

const AppWrapper = () => {
    const dispatch = useDispatch();

    React.useEffect(() => {
        dispatch(loadCart());
    }, [dispatch]);

    return <App />;
};

root.render(
    <Provider store={store}>
        <BrowserRouter basename={baseUrl}>
            <AppWrapper />
        </BrowserRouter>
    </Provider>
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals.console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
