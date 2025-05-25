export type Cart = {
    id: number,
    items: CartItem[],
    userId: number,
    createdAt: Date,
    updateAt: Date
}

export type CartItem = {
    id: number,
    productName: string,
    productId: number,
    productPrice: number,
    quantity: number
}