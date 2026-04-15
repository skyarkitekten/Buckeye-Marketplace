import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import './App.css'
import { useCart } from './context/CartContext.jsx' // Added .jsx
import CartPage from './pages/CartPage.jsx' // Added .jsx
import Auth from './components/Auth.jsx' // Added .jsx

function ProductList({ products, addToCart }) {
  if (!products) return <div>Loading products...</div>;
  return (
    <main className="product-grid">
      {products.map(product => (
        <div key={product.id} className="product-card">
          <h3>{product.title}</h3>
          <p className="price">${product.price}</p>
          <p className="condition">{product.condition}</p>
          <button onClick={() => addToCart(product)}>
            Add to Cart
          </button>
        </div>
      ))}
    </main>
  );
}
import axios from 'axios';

// Attach the JWT token to every request automatically
axios.interceptors.request.use(
  config => {
    const token = localStorage.getItem('userToken');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  error => Promise.reject(error)
);





function App() {
  const [products, setProducts] = useState([]);
  const cartContext = useCart(); 

  useEffect(() => {
    fetch('http://localhost:5000/api/products')
      .then(res => res.json())
      .then(data => setProducts(data))
      .catch(err => console.error("Error fetching products:", err));
  }, []);

  // Safety check: If context is missing, don't crash the whole page
  if (!cartContext) return <div style={{padding: '50px', textAlign: 'center'}}><h2>Loading Buckeye Marketplace...</h2><p>If this persists, check your CartProvider in main.jsx</p></div>;

  const { cart, addToCart } = cartContext;

  return (
    <Router>
      <div className="app-container">
        <header>
          <h1>Buckeye Marketplace</h1>
          <nav className="navbar">
            <Link to="/" className="nav-link">Store</Link>
            <Link to="/cart" className="nav-link">View Cart ({cart?.length || 0})</Link>
            <Link to="/login" className="nav-link">Login/Register</Link> 
          </nav>
        </header>

        <Routes>
          <Route path="/" element={<ProductList products={products} addToCart={addToCart} />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/login" element={<Auth />} /> 
        </Routes>
      </div>
    </Router>
  )
}

export default App