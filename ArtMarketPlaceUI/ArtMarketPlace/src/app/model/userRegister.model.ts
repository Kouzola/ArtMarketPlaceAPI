export interface userRegister{
    userName: string,
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    street: string,
    city: string,
    postalCode: string,
    country: string,
    role: Role,
}

enum Role {
    Customer,
    Artisan,
    Delivery,
    Admin
}