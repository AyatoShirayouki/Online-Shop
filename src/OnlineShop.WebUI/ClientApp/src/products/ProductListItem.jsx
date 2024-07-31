//ProductListItem.jsx

/**
 * ProductListItem component represents a single product in the product list.
 * It provides functionality to view product details and add the product to the cart.
 * 
 * This component uses React-Redux for dispatching actions to the store.
 */

import React from 'react';
import { Card, CardBody, CardTitle, Button, CardSubtitle } from 'reactstrap';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { addToCart } from '../redux/actions/cartActions';
import './ProductListItem.css';

const ProductListItem = ({ product }) => {
    const dispatch = useDispatch();

    // Handler to add product to the cart
    const handleAddToCart = () => {
        dispatch(addToCart(product));
    };

    return (
        <Card>
            <img alt="product" src={product.imageUrl} />
            <CardBody>
                <CardTitle tag="h5">{product.name}</CardTitle>
                <CardSubtitle>Price: ${product.price}</CardSubtitle>
                <div className="button-wrapper">
                    <Button color="primary" onClick={handleAddToCart}>
                        Add to Cart
                    </Button>
                    <Link className="btn btn-secondary" to={`/product/${product.id}`}>
                        Details
                    </Link>
                </div>
            </CardBody>
        </Card>
    );
};

export default ProductListItem;
