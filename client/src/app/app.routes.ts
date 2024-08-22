import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProdcutDetailsComponent } from './features/shop/prodcut-details/prodcut-details.component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'shop',component:ShopComponent},
    {path:'shop/:id',component:ProdcutDetailsComponent},
    {path:'**',redirectTo:'',pathMatch:'full'},
];
