export type LoginRequest = {
    email: string;
    password: string;
}

export type RegisterRequest = {
    email: string;
    username: string;
    password: string;
}

export type UserResponse = {
    username: string;
    email: string;
}