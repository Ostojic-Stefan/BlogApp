import React from "react";
import SingleArticle from "./components/SingleArticle";
import { Article } from "./types";

const tmpArticles: Article[] = [
  {
    id: 1,
    imgUrl:
      "https://images.unsplash.com/photo-1661956602153-23384936a1d3?ixlib=rb-4.0.3&ixid=MnwxMjA3fDF8MHxlZGl0b3JpYWwtZmVlZHw2fHx8ZW58MHx8fHw%3D&auto=format&fit=crop&w=500&q=60",
    body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus, quasi? Adipisci cum est eos tempora veritatis quia ab necessitatibus. Praesentium.",
    title: "Lorem ipsum dolor sit amet",
    username: "Dude",
    createdAt: new Date(),
  },
  {
    id: 2,
    imgUrl:
      "https://images.unsplash.com/photo-1661956602153-23384936a1d3?ixlib=rb-4.0.3&ixid=MnwxMjA3fDF8MHxlZGl0b3JpYWwtZmVlZHw2fHx8ZW58MHx8fHw%3D&auto=format&fit=crop&w=500&q=60",
    body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus, quasi? Adipisci cum est eos tempora veritatis quia ab necessitatibus. Praesentium.",
    title: "Lorem ipsum dolor sit amet",
    username: "Dude",
    createdAt: new Date(),
  },
  {
    id: 3,
    imgUrl:
      "https://images.unsplash.com/photo-1661956602153-23384936a1d3?ixlib=rb-4.0.3&ixid=MnwxMjA3fDF8MHxlZGl0b3JpYWwtZmVlZHw2fHx8ZW58MHx8fHw%3D&auto=format&fit=crop&w=500&q=60",
    body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellendus, quasi? Adipisci cum est eos tempora veritatis quia ab necessitatibus. Praesentium.",
    title: "Lorem ipsum dolor sit amet",
    username: "Dude",
    createdAt: new Date(),
  },
];

const renderedArticles = tmpArticles.map((article) => (
  <SingleArticle article={article} />
));

function ArticleList() {
  return <article>{renderedArticles}</article>;
}

export default ArticleList;
