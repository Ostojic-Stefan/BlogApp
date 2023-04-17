import React from "react";
import Navbar from "./components/Navbar";
import ArticleList from "./ArticleList";
import { Outlet } from "react-router-dom";

function App() {
  return (
    <main>
      <Navbar />
      <Outlet />
    </main>
  );
}

export default App;
