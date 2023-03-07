import './App.css';
import LoginForm from './LoginForm';
import Chatscreen from './ChatscreenComponents/Chatscreen';
import { BrowserRouter, Route, Routes, Link, Router, useNavigate } from 'react-router-dom';
import Registerform from './Registerform';
import React from 'react';






function App() {
  return (
    <div>
      <Routes>
        <Route path="/" element={<LoginForm />} />
        <Route path="/chat" element={<Chatscreen />} />
        <Route path="/register" element={<Registerform />} />
      </Routes>
    </div>
  );
}

export default App;