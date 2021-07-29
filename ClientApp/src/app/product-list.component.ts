import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Product } from './item';
 
@Component({
    templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
 
    products: Product[]; 
    constructor(private dataService: DataService) { }
 
    ngOnInit() {
        this.load();
    }
    load() {
        this.dataService.getProducts().subscribe((data: Product[]) => this.products = data);
    }
    delete(id: number) {
        this.dataService.deleteProduct(id).subscribe(data => this.load());
    }
}