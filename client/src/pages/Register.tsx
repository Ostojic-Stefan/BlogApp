import { useState, useEffect } from "react";
import { apiHandler } from "../api/apiHandler";

function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    try {
      await apiHandler.account.register({ username, email, password });
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div className="auth">
      <form onSubmit={handleSubmit}>
        <div className="form-item">
          <input
            type="text"
            placeholder="UserName"
            onChange={(e) => setUsername(e.target.value)}
            value={username}
          />
        </div>

        <div className="form-item">
          <input
            type="text"
            placeholder="Email"
            onChange={(e) => setEmail(e.target.value)}
            value={email}
          />
        </div>

        <div className="form-item">
          <input
            type="password"
            placeholder="Password"
            onChange={(e) => setPassword(e.target.value)}
            value={password}
          />
        </div>

        <button>Register</button>
      </form>
    </div>
  );
}

export default Register;
