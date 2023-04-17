import { useState } from "react";
import { apiHandler } from "../api/apiHandler";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    try {
      await apiHandler.account.login({ email, password });
      const response = await apiHandler.account.me();
      console.log(response);
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

        <button>Login</button>
      </form>
    </div>
  );
}

export default Login;
