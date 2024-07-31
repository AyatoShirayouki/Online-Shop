//ShoppingCart.jsx

/**
 * ShoppingCart component manages the display and interaction with the user's shopping cart.
 * It includes functionality for increasing, decreasing, and removing items, as well as displaying discounts.
 * 
 * This component uses React-Redux for state management and Redux actions for interacting with the cart.
 */

import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
    increaseQuantity,
    decreaseQuantity,
    removeFromCart,
    loadCart,
} from '../redux/actions/cartActions';
import { Container, Row, Col, Button, ListGroup, ListGroupItem } from 'reactstrap';

// Retrieve cart state from Redux store
const ShoppingCart = () => {
    const cartItems = useSelector((state) => state.cart.cartItems);
    const discount = useSelector((state) => state.cart.discount);
    const discountDescription = useSelector((state) => state.cart.discountDescription);
    const dispatch = useDispatch();

    // Load cart items on component mount
    useEffect(() => {
        dispatch(loadCart());
    }, [dispatch]);

    // Handler to increase quantity of a product
    const handleIncreaseQuantity = (productId) => {
        dispatch(increaseQuantity(productId));
    };

    // Handler to decrease quantity or remove item from cart
    const handleDecreaseQuantity = (productId, quantity) => {
        if (quantity === 1) {
            dispatch(removeFromCart(productId));
        } else {
            dispatch(decreaseQuantity(productId));
        }
    };

    // Handler to remove an item from the cart
    const handleRemoveFromCart = (productId) => {
        dispatch(removeFromCart(productId));
    };

    return (
        <Container>
            <h2>Shopping Cart</h2>
            <ListGroup>
                {cartItems.map((item) => (
                    <ListGroupItem key={item.productId}>
                        <Row>
                            <Col>{item.productName}</Col>
                            <Col>${item.price}</Col>
                            <Col>
                                <Button onClick={() => handleDecreaseQuantity(item.productId, item.quantity)}>-</Button>
                                <span>{item.quantity}</span>
                                <Button onClick={() => handleIncreaseQuantity(item.productId)}>+</Button>
                            </Col>
                            <Col>
                                <Button color="danger" onClick={() => handleRemoveFromCart(item.productId)}>
                                    Remove
                                </Button>
                            </Col>
                        </Row>
                    </ListGroupItem>
                ))}
            </ListGroup>
            <h3>Total: ${Math.round((cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0) - discount) * 100) / 100}</h3>
            {discount > 0 && (
                <div>
                    <h4>Discount: ${discount}</h4>
                    <h4>Old Price: ${Math.round(cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0) * 100) / 100}</h4>
                    <p>{discountDescription}</p>
                </div>
            )}
        </Container>
    );
};

export default ShoppingCart;
