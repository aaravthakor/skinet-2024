import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from './product-item/product-item.component';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { ShopParams } from '../../shared/sortParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatIcon,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatMenu,
    MatPaginator,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);

  private diloagService = inject(MatDialog);

  shopParams  = new ShopParams();
  
  title = 'Skinet';
  products? : Pagination<Product>;
  pageSizeOptions = [5,10,15,20];

  sortOptions = [
    {name:'Alphabetical',value:'name'},
    {name:'Price: Low-High', value:'priceAsc'},
    {name:'Price: High-Low', value:'priceDesc'}
  ]

  ngOnInit(): void {
      this.initiallizeShop();
  }

  initiallizeShop(){
    this.shopService.getBrands();
    this.shopService.getTypes(); 
    this.getProduducts();   
  }

  getProduducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.log(error)        
   });
  }

  onSearchChange(){
    this.shopParams.pageNumber = 1;
    this.getProduducts();
  }

  handlePageEvent(event : PageEvent){
    this.shopParams.pageNumber = event.pageIndex +1 ;
    this.shopParams.pageSize = event.pageSize;
    this.getProduducts();
  }

  onSortChange(event : MatSelectionListChange){
    const selectedOption = event.options[0];
    if(selectedOption){
      this.shopParams.sort = selectedOption.value;  
      this.shopParams.pageNumber = 1;  
      this.getProduducts();
    }
  } 

  openFilterDialog(){
    const dilogRef = this.diloagService.open(FiltersDialogComponent,{
      minWidth :'500px',
      data:{
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types 
      }
    });

    dilogRef.afterClosed().subscribe({
      next : result => {
        if(result){
          console.log(result);
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber =1;  
         this.getProduducts();
        }
      }
    });  
  }

}
