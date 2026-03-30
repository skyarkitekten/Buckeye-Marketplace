import { useState, useEffect } from 'react'
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom'
import './App.css'
import { useCart } from './context/CartContext'
import CartPage from './pages/CartPage'

// This component handles the Home/Store view
function ProductList({ products, addToCart }) {
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

function App() {
  const [products, setProducts] = useState([]);
  const { cart, addToCart } = useCart(); // Access 'cart' to show the count

  useEffect(() => {
    fetch('http://localhost:5000/api/products')
      .then(res => res.json())
      .then(data => setProducts(data))
      .catch(err => console.error("Error fetching products:", err));
  }, []);

  return (
    <Router>
      <div className="app-container">
        <header>
          <h1>Buckeye Marketplace</h1>
          <nav className="navbar">
            <Link to="/" className="nav-link">Store</Link>
            <Link to="/cart" className="nav-link">View Cart ({cart.length})</Link>
          </nav>
          <div className="cart-status">
            <span>🛒 Status: {products.length > 0 ? "Products Loaded" : "Connecting to API..."}</span>
          </div>
        </header>

        {/* This "Routes" block is the switcher that swaps the page content */}
        <Routes>
          <Route path="/" element={<ProductList products={products} addToCart={addToCart} />} />
          <Route path="/cart" element={<CartPage />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App