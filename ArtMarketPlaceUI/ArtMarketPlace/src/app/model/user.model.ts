export interface User{
    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    email: string,
    role?: string,
    fullName: string,
    address: Address,
    password?:string,
}

interface Address{
    street: string,
    city: string,
    postalCode: string,
    country: string,
}