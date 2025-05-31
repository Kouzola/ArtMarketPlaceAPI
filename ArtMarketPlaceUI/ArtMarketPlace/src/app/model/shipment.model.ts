import { User } from "./user.model"

export type Shipment = {
    id: number,
    shippingDate: Date,
    estimatedArrivalDate: Date,
    arrivalDate: Date,
    trackingNumber: string,
    status: ShipmentStatus,
    createdAt: Date,
    updatedAt: Date,
    orderCode: string,
    deliveryPartner: User,
    products: number[]
}

export enum ShipmentStatus{
    PENDING_PICKUP,
    IN_TRANSIT,
    OUT_FOR_DELIVERY,
    DELIVERED,
    LOST
}