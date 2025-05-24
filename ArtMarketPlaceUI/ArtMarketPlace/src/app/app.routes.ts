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
            { path: '', redirectTo: 'products', pathMatch: 'full' },
            {path: 'products', component: ProductListComponent},
            {path: 'products/view/:productId', component: ProductComponent},
            {path: 'orders', component: OrderComponent},
            {path: 'shipments', component: ShipmentsListComponent},
            {path: 'inquiries', component: InquiriesListComponent},
            {path: 'cart', component: CartComponent},
            {path: 'profile', component: UserManageComponent}
        ]
    },
    {
        path: '**',
        redirectTo: 'home/products'
    }
];
