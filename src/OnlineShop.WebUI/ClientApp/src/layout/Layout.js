// Layout.js
import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu'; // Corrected import statement

export default class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div>
                <NavMenu />
                <Container
                    style={{
                        display: 'flex',
                        flexWrap: 'wrap',
                        justifyContent: 'center',
                    }}
                >
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
