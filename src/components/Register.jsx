import "../Register.css";
import React, { useState } from "react";

const Register = () => {
  const [formData, setFormData] = useState({
    fullName: "",
    email: "",
    password: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    console.log("handleSubmit fired");
    e.preventDefault();
  
    try {
      const response = await fetch("http://localhost:5168/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          FullName: formData.fullName,
          Email: formData.email,
          Password: formData.password,
        }),
      });
  
      if (response.ok) {
        const data = await response.json();
        alert(data.message);
      } else {
        const error = await response.json();
        alert(error.message || "Registration failed");
      }
    } catch (error) {
      console.error("Error registering:", error);
    }
  };
  

  return (
    <div>
      <section className="bg-dark text-white text-center py-5">
        <div className="container">
          <h1 className="display-4 fw-bold">Join PrimeFit</h1>
          <p className="lead mt-3">
            Create your PrimeFit Gym account and start your fitness journey
          </p>
        </div>
      </section>

      <section className="py-5 bg-light">
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-6">
              <div className="card shadow-lg border-0">
                <div className="card-body p-4">
                  <h3 className="text-center mb-4">Create Account</h3>
                  <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                      <label htmlFor="fullName" className="form-label">
                        Full Name
                      </label>
                      <input
                        type="text"
                        id="fullName"
                        name="fullName"
                        className="form-control"
                        value={formData.fullName}
                        onChange={handleChange}
                        required
                      />
                    </div>

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

                    <div className="d-grid">
                      <button type="submit" className="btn btn-primary btn-lg">
                        Create Account
                      </button>
                    </div>
                  </form>

                  <p className="mt-3 text-center">
                    Already have an account? <a href="/login">Login</a>
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section className="py-5">
        <div className="container text-center">
          <h2 className="mb-4">Need Help?</h2>
          <p>
            Email: <a href="mailto:info@primefitgym.com">info@primefitgym.com</a> | Phone: (555) 123-4567
          </p>
          <p>123 Fitness Lane, Muscle City, Fitland</p>
        </div>
      </section>

      <footer className="text-center text-muted py-3 bg-light">
        © {new Date().getFullYear()} PrimeFit Gym. All rights reserved.
      </footer>
    </div>
  );
};

export default Register;
