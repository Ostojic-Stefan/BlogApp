import React from "react";
import { Article } from "./types";

interface Props {
  article: Article;
}

function SingleArticle({ article }: Props) {
  return (
    <div className="single-article">
      <img src={article.imgUrl} alt="" />
      <div className="text-content">
        <h2>{article.title}</h2>
        <p>{article.body}</p>
        <button>Read More</button>
      </div>
    </div>
  );
}

export default SingleArticle;
