export interface UserTokenInfo{
    name: string,
    id: number,
    role: 'Customer' | 'Artisan' | 'Delivery' | 'Admin',
}