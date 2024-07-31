// AppRoutes.js

/**
 * AppRoutes defines the routes for the application.
 * Each route is associated with a specific component, allowing navigation between different parts of the app.
 * 
 * This file centralizes the routing configuration, making it easier to manage and update.
 */

import ProductDetails from './products/ProductDetails';
import ProductList from './products/ProductList';
import ShoppingCart from './cart/ShoppingCart';

const AppRoutes = [
    {
        index: true,
        element: <ProductList />,
    },
    {
        path: '/product/:id',
        element: <ProductDetails />,
    },
    {
        path: '/cart',
        element: <ShoppingCart />,
    },
];

export default AppRoutes;
