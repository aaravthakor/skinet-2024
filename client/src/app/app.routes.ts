import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProdcutDetailsComponent } from './features/shop/prodcut-details/prodcut-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'shop',component:ShopComponent},
    {path:'shop/:id',component:ProdcutDetailsComponent},
    {path:'not-found',component:NotFoundComponent},
    {path:'server-error',component:ServerErrorComponent},
    {path:'**',redirectTo:'not-found',pathMatch:'full'},
];
