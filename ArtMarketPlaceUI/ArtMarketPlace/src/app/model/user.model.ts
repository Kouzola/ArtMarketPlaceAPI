export interface User{
    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    email?: string,
    role: number,
    active?: boolean,
    fullName: string,
    address?: Address,
    password?:string,
}

interface Address{
    street: string,
    city: string,
    postalCode: string,
    country: string,
}

export enum RoleUser {
    Customer = 0,
    Artisan = 1,
    Delivery = 2,
    Admin = 3
}