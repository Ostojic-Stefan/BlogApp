import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import "./style.scss";
import { RouterProvider } from "react-router-dom";
import { router } from "./routes";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
