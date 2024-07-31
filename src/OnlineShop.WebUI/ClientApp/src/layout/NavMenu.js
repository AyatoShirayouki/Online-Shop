//NavMenu.js
import React from 'react';
import {
    Collapse,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
    NavbarText,
} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import CartCount from '../cart/CartCount';

const NavMenu = () => {
    const [collapsed, setCollapsed] = React.useState(true);

    const toggleNavbar = () => setCollapsed(!collapsed);

    return (
        <header>
            <Navbar
                className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
                container
                light
            >
                <NavbarBrand tag={Link} to="/">
                    OnlineShop
                </NavbarBrand>
                <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                <Collapse
                    className="d-sm-inline-flex flex-sm-row-reverse"
                    isOpen={!collapsed}
                    navbar
                >
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/">
                                Home
                            </NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/cart">
                                Cart
                            </NavLink>
                        </NavItem>
                        <NavbarText>
                            Items in Cart: <CartCount />
                        </NavbarText>
                    </ul>
                </Collapse>
            </Navbar>
        </header>
    );
};

export default NavMenu;
