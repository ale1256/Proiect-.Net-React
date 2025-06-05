import React, { useState } from "react";

const Login = () => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    rememberMe: false,
  });

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData({
      ...formData,
      [name]: type === "checkbox" ? checked : value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch("http://localhost:5168/api/auth/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        const data = await response.json();
        alert("Login successful!");
        console.log(data);
      } else {
        const error = await response.json();
        alert(error.message || "Login failed");
      }
    } catch (error) {
      console.error("Error logging in:", error);
    }
  };

  return (
    <div>
      {/* Hero Section */}
      <section className="bg-dark text-white text-center py-5">
        <div className="container">
          <h1 className="display-4 fw-bold">Welcome Back</h1>
          <p className="lead mt-3">Log in to your PrimeFit Gym account</p>
        </div>
      </section>

      {/* Login Form */}
      <section className="py-5 bg-light">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-6">
              <div className="card shadow-lg border-0">
                <div className="card-body p-4">
                  <h3 className="text-center mb-4">Login</h3>
                  <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                      <label htmlFor="email" className="form-label">
                        Email Address
                      </label>
                      <input
                        type="email"
                        id="email"
                        name="email"
                        className="form-control"
                        value={formData.email}
                        onChange={handleChange}
                        required
                      />
                    </div>

                    <div className="mb-3">
                      <label htmlFor="password" className="form-label">
                        Password
                      </label>
                      <input
                        type="password"
                        id="password"
                        name="password"
                        className="form-control"
                        value={formData.password}
                        onChange={handleChange}
                        required
                      />
                    </div>

                    <div className="mb-3 form-check">
                      <input
                        type="checkbox"
                        id="rememberMe"
                        name="rememberMe"
                        className="form-check-input"
                        checked={formData.rememberMe}
                        onChange={handleChange}
                      />
                      <label htmlFor="rememberMe" className="form-check-label">
                        Remember Me
                      </label>
                    </div>

                    <div className="d-grid">
                      <button type="submit" className="btn btn-primary btn-lg">
                        Login
                      </button>
                    </div>
                  </form>

                  <p className="mt-3 text-center">
                    Don't have an account? <a href="/register">Register</a>
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Contact Section */}
      <section className="py-5">
        <div className="container text-center">
          <h2 className="mb-4">Need Help?</h2>
          <p>
            Email:{" "}
            <a href="mailto:info@primefitgym.com">info@primefitgym.com</a> |
            Phone: (555) 123-4567
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

export default Login;