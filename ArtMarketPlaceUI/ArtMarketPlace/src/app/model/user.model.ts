export interface User{
    name: string,
    id: number,
    role: 'Customer' | 'Artisan' | 'Delivery' | 'Admin',
}