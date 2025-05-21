import { Category } from "./category.model";
import { Customization } from "./customization.model";
import { Review } from "./review.model";
import { User } from "./user.model";

export interface Product{
    id: number,
    name: string,
    description: string,
    reference?: string,
    price: number,
    stock: number,
    image?: string,
    available: boolean,
    createdAt: Date,
    updated: Date,
    artisan: User | number,
    category: Category,
    reviews: Review[],
    customization?: Customization[],
}