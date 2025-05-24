export type Review = {
    id: number,
    title: string,
    description: string,
    score: number,
    artisanAnswer?: string,
    productId?: number,
    createdAt: Date,
    updateAt: Date,
}