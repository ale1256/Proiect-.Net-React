import React, { useEffect, useState } from "react";
import "../GymClass.css";

export default function GymClass() {
  const [classes, setClasses] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch("http://localhost:5168/api/gymclasses")
      .then((res) => res.json())
      .then((data) => {
        setClasses(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Failed to fetch classes", err);
        setLoading(false);
      });
  }, []);

  const handleBook = (classId) => {
    const token = localStorage.getItem("token");

    fetch(`http://localhost:5168/api/gymclasses/${classId}/book`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`,
      },
    })
      .then((res) => {
        if (!res.ok) throw new Error("Booking failed");
        return res.json();
      })
      .then((data) => {
        alert(`${data.user}, you booked this class!`);

        // Update the spotsLeft in local state
        setClasses((prev) =>
          prev.map((c) =>
            c.id === classId ? { ...c, spotsLeft: data.spotsLeft } : c
          )
        );
      })
      .catch((err) => {
        console.error(err);
        alert("Booking failed. Please try again.");
      });
  };

  if (loading) return <div className="loading">Loading classes...</div>;

  return (
    <div className="gym-class-container">
      <h2>Class Schedule</h2>
      <div className="class-grid">
        {classes.map((c) => (
          <div key={c.id} className="class-card">
            <h3 className="class-name">{c.name}</h3>
            <p><strong>Start Time:</strong> {new Date(c.startTime).toLocaleString()}</p>
            <p><strong>Trainer:</strong> {c.trainerName}</p>
            <p><strong>Spots Left:</strong> {c.spotsLeft}</p>
            <button
              className="book-btn"
              onClick={() => handleBook(c.id)}
              disabled={c.spotsLeft <= 0}
            >
              {c.spotsLeft > 0 ? "Book Class" : "Full"}
            </button>
          </div>
        ))}
      </div>
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
}
