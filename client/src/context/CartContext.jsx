import React, { createContext, useState, useContext } from 'react';

const CartContext = createContext();

export const CartProvider = ({ children }) => {
    const [cart, setCart] = useState([]);

    // We'll handle the "Add" logic locally for now so it's instant!
    const addToCart = (product) => {
        setCart((prevCart) => {
            const newCart = [...prevCart, product];
            console.log("Cart Updated:", newCart); // This helps us debug in Inspect > Console
            return newCart;
        });
    };

    const removeFromCart = (productId) => {
        setCart((prevCart) => prevCart.filter(item => item.id !== productId));
    };

    const clearCart = () => setCart([]);

    return (
        <CartContext.Provider value={{ cart, addToCart, removeFromCart, clearCart }}>
            {children}
        </CartContext.Provider>
    );
};

export const useCart = () => useContext(CartContext);