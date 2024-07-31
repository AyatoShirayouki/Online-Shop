//ProductList.jsx

/**
 * ProductList component fetches and displays a list of products.
 * Each product is displayed using the ProductListItem component.
 * 
 * This component fetches product data from an API and manages loading state.
 */

import { useEffect, useState } from 'react';
import ProductListItem from './ProductListItem';
import { getProducts } from './ProductsApi';

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const [loaded, setLoaded] = useState(false);

    // Fetch list of products on component mount
    useEffect(() => {
        getProducts().then((products) => {
            setProducts(products);
            setLoaded(true);
        });
    }, []);

    const productList = products.map((product) => (
        <ProductListItem key={product.id} product={product}></ProductListItem>
    ));

    return loaded ? (
        products?.length > 0 ? (
            productList
        ) : (
            <div>No products</div>
        )
    ) : (
        <div>Loading</div>
    );
};

export default ProductList;
