//ProductDetails.jsx

/**
 * ProductDetails component displays detailed information about a single product.
 * It fetches product data using the product ID from the URL parameters and provides the option to add the product to the cart.
 * 
 * This component uses React-Redux for dispatching actions to the store.
 */

import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { getProduct } from './ProductsApi';
import {
    Card,
    CardBody,
    CardTitle,
    CardText,
    CardSubtitle,
    Button,
    CardImg,
} from 'reactstrap';
import { addToCart } from '../redux/actions/cartActions';

const ProductDetails = () => {
    let { id } = useParams();
    const [product, setProduct] = useState(null);
    const [loaded, setLoaded] = useState(false);
    const dispatch = useDispatch();

    // Fetch product details on component mount or when ID changes
    useEffect(() => {
        getProduct(id).then((product) => {
            setProduct(product);
            setLoaded(true);
        });
    }, [id]);

    // Handler to add product to the cart
    const handleAddToCart = () => {
        if (product) {
            dispatch(addToCart(product));
        }
    };

    return loaded ? (
        <Card style={{ width: '100%' }}>
            <CardBody>
                <CardTitle tag="h1">{product.name}</CardTitle>
                <CardImg
                    alt="p"
                    src={product.imageUrl}
                    style={{
                        height: 180,
                    }}
                    width="100%"
                />
                <CardSubtitle>Price: ${product.price}</CardSubtitle>
                <CardText>{product.description}</CardText>
                <Button color="primary" onClick={handleAddToCart}>
                    Add to Cart
                </Button>
                <Link className="btn btn-secondary" to="/">
                    Back
                </Link>
            </CardBody>
        </Card>
    ) : (
        <div>Loading</div>
    );
};

export default ProductDetails;
