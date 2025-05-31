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
    role: 'Customer' | 'Artisan' | 'Delivery' | 'Admin',
}

enum Role {
    Customer = 'Customer',
    Artisan = 'Artisan',
    Delivery = 'Delivery',
    Admin = 'Admin'
}