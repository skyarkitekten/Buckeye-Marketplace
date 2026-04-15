import React, { useState } from 'react';

const Auth = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const endpoint = isLogin ? 'login' : 'register';
    
    try {
      const response = await fetch(`http://localhost:5000/api/auth/${endpoint}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
      });

      const data = await response.json();
      
      // --- DEBUG LOGS START ---
      console.log("!!! DEBUG: Response Status:", response.status);
      console.log("!!! DEBUG: Full data from backend:", data);
      // --- DEBUG LOGS END ---

      if (response.ok) {
        if (isLogin) {
          // Check if token exists in the data before saving
          if (data.token) {
            localStorage.setItem('userToken', data.token); 
            console.log("!!! DEBUG: Token saved to localStorage successfully.");
          } else {
            console.error("!!! DEBUG: Login was successful, but NO TOKEN was found in the response.");
          }

          alert('Login Successful! Check your console and Application tab before clicking OK.');
          
          // Redirect is commented out for testing. Uncomment when token is verified!
          window.location.href = "/"; 
        } else {
          alert('Registration Successful! You can now login.');
          setIsLogin(true);
        }
      } else {
        alert('Error: ' + JSON.stringify(data));
      }
    } catch (error) {
      console.error("!!! DEBUG: Fetch Error:", error);
      alert("Could not connect to the backend. Is it running?");
    }
  };

  return (
    <div style={{ padding: '20px', maxWidth: '400px', margin: 'auto' }}>
      <h2>{isLogin ? 'Login' : 'Register'}</h2>
      <form onSubmit={handleSubmit}>
        <input 
          type="email" 
          placeholder="OSU Email" 
          value={email} 
          onChange={(e) => setEmail(e.target.value)} 
          style={{ display: 'block', width: '100%', marginBottom: '10px', padding: '8px' }}
        />
        <input 
          type="password" 
          placeholder="Password" 
          value={password} 
          onChange={(e) => setPassword(e.target.value)} 
          style={{ display: 'block', width: '100%', marginBottom: '10px', padding: '8px' }}
        />
        <button type="submit" style={{ backgroundColor: '#ba0c2f', color: 'white', padding: '10px', width: '100%', cursor: 'pointer' }}>
          {isLogin ? 'Login' : 'Sign Up'}
        </button>
      </form>
      <button 
        onClick={() => setIsLogin(!isLogin)} 
        style={{ marginTop: '10px', background: 'none', border: 'none', color: 'blue', cursor: 'pointer' }}
      >
        {isLogin ? 'Need an account? Register' : 'Already have an account? Login'}
      </button>
    </div>
  );
};

export default Auth;