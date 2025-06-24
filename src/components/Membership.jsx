import React, { useEffect, useState } from "react";
import "../Membership.css";

const Membership = () => {
  const [memberships, setMemberships] = useState([]);
  const [message, setMessage] = useState(null);

  useEffect(() => {
    fetch("http://localhost:5168/api/membership")
      .then((response) => {
        if (!response.ok) throw new Error("Failed to fetch memberships");
        return response.json();
      })
      .then((data) => {
        setMemberships(data);
      })
      .catch((error) => {
        console.error("Error fetching memberships:", error);
      });
  }, []);
  const handleBuy = async (membershipId) => {
    const clientId = localStorage.getItem("clientId");
    if (!clientId) {
      setMessage("You must be logged in to buy a membership.");
      return;
    }
  
    try {
      const response = await fetch("http://localhost:5168/api/membership/buy", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ clientId: parseInt(clientId), membershipId }),
      });
  
      if (response.ok) {
        const result = await response.json();
        setMessage(result.message);
      } else {
        const error = await response.json();
        setMessage(error.message || "Failed to purchase membership.");
      }
    } catch (error) {
      console.error("Error purchasing membership:", error);
      setMessage("An error occurred. Please try again.");
    }
  };
  
  return (
    <section className="py-5 membership-section">
      <div className="container">
        <h2 className="mb-4 text-center text-primary">Choose Your Membership Plan</h2>

        {message && <div className="alert alert-success text-center">{message}</div>}

        <div className="row g-4 justify-content-center">
          {memberships.map((option) => (
            <div className="col-md-6 col-lg-4" key={option.id}>
              <div className="card membership-card shadow-sm">
                <div className="card-body text-center">
                  <h4 className="card-title">{option.planName}</h4>
                  <p className="card-text text-muted">{option.description}</p>
                  <p className="membership-price">${option.pricePerMonth} / month</p>
                  <button
                    className="btn btn-success mt-3 px-4"
                    onClick={() => handleBuy(option.id)}
                  >
                    Buy Now
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default Membership;
