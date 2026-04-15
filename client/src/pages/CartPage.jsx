import React from 'react';
import { useCart } from '../context/CartContext';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

const CartPage = () => {
    const { cart, removeFromCart, clearCart } = useCart();
    const navigate = useNavigate();

    const total = cart.reduce((sum, item) => sum + item.price, 0);

    const handleCheckout = async () => {
        // 1. Check if user is logged in
        const token = localStorage.getItem('userToken');
        if (!token) {
            alert("Please login to complete your purchase.");
            navigate('/login');
            return;
        }

        try {
            // 2. Prepare data for the backend OrderItemDto
            const orderData = cart.map(item => ({
                productId: item.id,
                productTitle: item.title,
                price: item.price
            }));

            // 3. Send to backend (Interceptor handles the Token)
            const response = await axios.post('http://localhost:5000/api/orders', orderData);

            if (response.status === 200) {
                alert(`Order Placed Successfully! Order ID: ${response.data.orderId}`);
                
                // 4. Clear the cart (Rubric Requirement)
                clearCart();
                
                // 5. Send home
                navigate('/');
            }
        } catch (err) {
            console.error("Checkout Error:", err);
            alert("There was an issue processing your order. Please try again.");
        }
    };

    return (
        <div className="cart-container">
            <h2>Your Buckeye Shopping Cart</h2>
            
            {cart.length === 0 ? (
                <div className="empty-cart">
                    <p>Your cart is empty. Go find some deals!</p>
                    <Link to="/" className="back-btn">Back to Store</Link>
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
                            {/* Updated Button */}
                            <button className="checkout-btn" onClick={handleCheckout}>
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