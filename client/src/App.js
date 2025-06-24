import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import Home from "./components/Home";
import Membership from "./components/Membership";
import Class from "./components/GymClass";

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/membership" element={<Membership />} />
        <Route path="/class" element={<Class />} />
      </Routes>
    </Router>
  );
};

export default App;
