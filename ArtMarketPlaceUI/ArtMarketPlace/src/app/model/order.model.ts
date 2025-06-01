import { PaymentDetail } from "./paymentDetail.model";
import { User } from "./user.model";

export type Order = {
    id: number,
    code: string,
    orderDate?: Date,
    status?: Status,
    shippingOption: ShippingOption,
    createdAt: Date,
    updatedAt: Date,
    paymentDetail?: PaymentDetail,
    shipments?: number[],
    customer: User,
    productsOrderedInfo: ProductsOrderedInfo[],
    orderStatusPerArtisans?: OrderStatusPerArtisan[]
}

export type ProductsOrderedInfo = {
    name: string,
    id: number,
    reference: string,
    quantity: number,
}

export type OrderStatusPerArtisan = {
    orderId: number,
    artisanId: number,
    status: number,
}

export enum Status {
    NOT_PAYED,
    PENDING,
    CONFIRM,
    SHIPPED,
    DELIVERED,
    CANCEL
}

export enum ShippingOption{
    NORMAL,
    FAST
}