import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './layout/home/home.component';
import { AuthGuard } from './guard/auth.guard';
import { loggedInGuard } from './guard/logged-in.guard';
import { ProductListComponent } from './pages/product/product-list/product-list.component';
import { OrderComponent } from './pages/order/order.component';
import { InquiryComponent } from './pages/inquiry/inquiry.component';
import { ShipmentsListComponent } from './pages/shipment/shipments-list/shipments-list.component';
import { InquiriesListComponent } from './pages/inquiry/inquiries-list/inquiries-list.component';
import { CartComponent } from './pages/cart/cart.component';
import { UserManageComponent } from './pages/user-manage/user-manage.component';
import { ProductComponent } from './pages/product/product.component';
import { PaymentComponent } from './pages/payment/payment.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { MyProductComponent } from './pages/product/my-product/my-product.component';
import { artisanGuard } from './guard/artisan.guard';
import { mainPageGuard } from './guard/main-page.guard';
import { ProductAddFormComponent } from './pages/product/product-add-form/product-add-form.component';
import { ProductEditFormComponent } from './pages/product/product-edit-form/product-edit-form.component';
import { CategoryAddFormComponent } from './pages/category/category-add-form/category-add-form.component';
import { CustomizationComponent } from './pages/customization/customization.component';
import { CustomizationAddFormComponent } from './pages/customization/customization-add-form/customization-add-form.component';
import { CustomizationEditFormComponent } from './pages/customization/customization-edit-form/customization-edit-form.component';
import { OrderShipmentComponent } from './pages/order/order-shipment/order-shipment.component';
import { deliveryPartnerGuard } from './guard/delivery-partner.guard';
import { dashboardGuard } from './guard/dashboard.guard';
import { ShipmentComponent } from './pages/shipment/shipment.component';

export const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [loggedInGuard]
    },
    {
        path: 'register',
        component: RegisterComponent,
        canActivate: [loggedInGuard]
    },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [AuthGuard],
        children:[
            {path: '', redirectTo: 'products', pathMatch: 'full'},
            {path: 'products', component: ProductListComponent, canActivate: [mainPageGuard]},
            {path: 'products/view/:productId', component: ProductComponent},
            {path: 'orders', component: OrderComponent},
            {path: 'orders/shipment/:orderId', component: OrderShipmentComponent},
            {path: 'shipments', component: ShipmentComponent},
            {path: 'inquiries', component: InquiriesListComponent},
            {path: 'cart', component: CartComponent},
            {path: 'payment/:orderId',component: PaymentComponent},
            {path: 'profile', component: UserManageComponent},
            {path: 'dashboard', component: DashboardComponent, canActivate: [dashboardGuard]},
            {path: 'myProducts', component: MyProductComponent, canActivate: [artisanGuard]},
            {path: 'myProducts/edit/:productId', component: ProductEditFormComponent, canActivate: [artisanGuard]},
            {path: 'myProducts/add', component: ProductAddFormComponent, canActivate: [artisanGuard]},
            {path: 'categories/add', component: CategoryAddFormComponent, canActivate: [artisanGuard]},
            {path: 'customizations/:productId', component: CustomizationComponent, canActivate: [artisanGuard]},
            {path: 'customizations/:productId/add', component: CustomizationAddFormComponent, canActivate: [artisanGuard]},
            {path: 'customizations/:productId/edit/:customizationId', component: CustomizationEditFormComponent, canActivate: [artisanGuard]},
        ]
    },
    {
        path: '**',
        redirectTo: 'home/products',
    }
];
