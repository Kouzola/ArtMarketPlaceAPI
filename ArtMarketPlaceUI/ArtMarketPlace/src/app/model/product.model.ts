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
    image?: string | File,
    available: boolean,
    createdAt: Date,
    updatedAt: Date,
    artisan: User | number,
    category: string,
    reviews: Review[],
    customization?: Customization[],
}