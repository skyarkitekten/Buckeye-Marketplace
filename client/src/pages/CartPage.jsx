import React from 'react';
import { useCart } from '../context/CartContext';
import { Link } from 'react-router-dom';

const CartPage = () => {
    const { cart, removeFromCart, clearCart } = useCart();

    const total = cart.reduce((sum, item) => sum + item.price, 0);

    return (
        <div className="cart-container">
            <h2>Your Buckeye Shopping Cart</h2>
            
            {cart.length === 0 ? (
                <div className="empty-cart">
                    <p>Your cart is empty. Go find some deals!</p>
                    <Link TO="/" className="back-btn">Back to Store</Link>
                </div>
            ) : (
                <>
                    <table className="cart-table">
                        <thead>
                            <tr>
                                <th>Product</th>
                                <th>Price</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {cart.map((item, index) => (
                                <tr key={`${item.id}-${index}`}>
                                    <td>{item.title}</td>
                                    <td>${item.price.toFixed(2)}</td>
                                    <td>
                                        <button 
                                            className="remove-btn" 
                                            onClick={() => removeFromCart(item.id)}
                                        >
                                            Remove
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>

                    <div className="cart-summary">
                        <h3>Total: ${total.toFixed(2)}</h3>
                        <div className="cart-actions">
                            <button onClick={clearCart} className="clear-btn">Clear Cart</button>
                            <button className="checkout-btn" onClick={() => alert('Checkout not implemented yet!')}>
                                Proceed to Checkout
                            </button>
                        </div>
                    </div>
                </>
            )}
        </div>
    );
};

export default CartPage;