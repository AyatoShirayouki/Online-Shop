// App.js

/**
 * App component serves as the root component for the application.
 * It uses React Router for navigation and renders different components based on the current route.
 * 
 * The layout and structure of the app are defined here, integrating with the routing system.
 */

import React from 'react';
import Layout from './layout/Layout';
import './custom.css';
import { Routes, Route } from 'react-router-dom';
import AppRoutes from './AppRoutes';

const App = () => (
    <Layout>
        <Routes>
            {AppRoutes.map((route, index) => {
                const { element, ...rest } = route;
                return <Route key={index} {...rest} element={element} />;
            })}
        </Routes>
    </Layout>
);

export default App;
