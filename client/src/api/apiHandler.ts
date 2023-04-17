import axios, { AxiosResponse } from "axios";
import { PostRequest, PostResponse } from "./postDtos";
import { LoginRequest, RegisterRequest, UserResponse } from "./userDtos";

axios.defaults.baseURL = "http://localhost:5031/api/";
axios.defaults.withCredentials = true;

const responseBody = <TResponse>(res: AxiosResponse<TResponse>) => res.data;

const posts = {
    getAllPosts: () => axios.get<PostResponse[]>("posts")
        .then(responseBody<PostResponse[]>),

    getAllPostsByUser: (userId: number) => axios.get<PostResponse[]>(`posts?userId=${userId}`)
        .then(responseBody<PostResponse[]>),

    getPost: (postId: number) => axios.get<PostResponse>(`posts/${postId}`)
        .then(responseBody<PostResponse>),

    addPost: (data: PostRequest) => axios.post<PostResponse>("posts", data)
        .then(responseBody<PostResponse>),

    deletePost: (postId: number) => axios.delete(`posts/${postId}`)
        .then(responseBody<void>)
};

const account = {
    login: (data: LoginRequest) => axios.post("account/login", data).then(responseBody<void>),
    register: (data: RegisterRequest) => axios.post("account/register", data).then(responseBody<void>),
    logout: () => axios.post("account/logout").then(responseBody<void>),
    me: () => axios.get<UserResponse>("account/me").then(responseBody<UserResponse>)
}

export const apiHandler = {
    posts,
    account
}