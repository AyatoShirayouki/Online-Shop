//CartCount.jsx

/**
 * CartCount component displays the number of items in the shopping cart.
 * It utilizes React-Redux to access the state of cart items.
 * 
 * @returns {JSX.Element} The count of items in the cart.
 */

import { useSelector } from 'react-redux';

const CartCount = () => {
    // Accessing cart items from Redux store
    const cartItems = useSelector((state) => state.cart.cartItems);
    const itemCount = cartItems.length;
    return <>{itemCount}</>;
};

export default CartCount;
