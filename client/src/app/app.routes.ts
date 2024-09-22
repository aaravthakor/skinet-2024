import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProdcutDetailsComponent } from './features/shop/prodcut-details/prodcut-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'shop',component:ShopComponent},
    {path:'shop/:id',component:ProdcutDetailsComponent},
    {path:'cart',component:CartComponent},
    {path:'checkout',component:CheckoutComponent, canActivate:[authGuard]},
    {path:'account/login',component:LoginComponent},
    {path:'account/register',component:RegisterComponent},
    {path:'checkout',component:CheckoutComponent},
    {path:'not-found',component:NotFoundComponent},
    {path:'server-error',component:ServerErrorComponent},
    {path:'**',redirectTo:'not-found',pathMatch:'full'},
];
