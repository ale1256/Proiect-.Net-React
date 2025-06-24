import "../Home.css";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const [userName, setUserName] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      const user = JSON.parse(storedUser);
      setUserName(user.name); // fallback: user.email
    }
  }, []);

  const handleSignOut = () => {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <div>
      {/* Hero Section with embedded login/sign-out */}
      <section className="bg-dark text-white text-center py-5 position-relative">
        <div className="container">
          {/* Greeting + Auth Button */}
          <div className="auth-section position-absolute top-0 end-0 p-3">
            {userName ? (
              <>
                <span className="me-2 text-white">Hello, {userName}</span>
                <button onClick={handleSignOut} className="btn btn-outline-light btn-sm">
                  Sign Out
                </button>
              </>
            ) : (
              <a href="/login" className="btn btn-light btn-sm">
                Login
              </a>
            )}
          </div>

          {/* Hero content */}
          <h1 className="display-4 fw-bold mt-4">Welcome to PrimeFit Gym</h1>
          <p className="lead mt-3">Train harder. Get stronger. Be your best self.</p>
          <a href="/register" className="btn btn-primary btn-lg mt-4">
            Join Now
          </a>
        </div>
      </section>

      {/* Why Choose PrimeFit Section */}
      <section className="py-5 bg-light">
        <div className="container text-center">
          <h2 className="mb-4">Why Choose PrimeFit?</h2>
          <div className="row g-4">
            <div className="col-md-4">
              <h5>💪 Modern Equipment</h5>
              <p>We provide state-of-the-art equipment for strength, cardio, and functional training.</p>
            </div>
            <div className="col-md-4">
              <h5>🧘 Certified Trainers</h5>
              <p>Our expert trainers help you achieve your fitness goals with personalized plans.</p>
            </div>
            <div className="col-md-4">
              <h5>⌛ 24/7 Access</h5>
              <p>Workout whenever it suits you — we’re open around the clock.</p>
            </div>
          </div>
        </div>
      </section>

      {/* Membership CTA */}
      <section id="membership" className="py-5 text-white bg-primary text-center">
        <div className="container">
          <h2 className="mb-3">Become a Member Today</h2>
          <p>Flexible plans starting at just $29/month. No long-term contracts.</p>
          <a href="/membership" className="btn btn-light btn-lg mt-2">
            See Membership Options
          </a>
          <a href="/class" className="btn btn-outline-light btn-lg mt-2">
      Schedule a Class
    </a>
        </div>
      </section>

      {/* Contact Section */}
      <section className="py-5">
        <div className="container text-center">
          <h2 className="mb-4">Contact Us</h2>
          <p>
            Email: <a href="mailto:info@primefitgym.com">info@primefitgym.com</a> | Phone: (555) 123-4567
          </p>
          <p>123 Fitness Lane, Muscle City, Fitland</p>
        </div>
      </section>

      {/* Footer */}
      <footer className="text-center text-muted py-3 bg-light">
        © {new Date().getFullYear()} PrimeFit Gym. All rights reserved.
      </footer>
    </div>
  );
};

export default Home;
