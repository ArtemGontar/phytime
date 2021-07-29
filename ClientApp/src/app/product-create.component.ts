import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from './data.service';
import { Product } from './item';
 
@Component({
    templateUrl: './product-create.component.html'
})
export class ProductCreateComponent {
 
    product: Product = new Product();    // добавляемый объект
    constructor(private dataService: DataService, private router: Router) { }
    save() {
        this.dataService.createProduct(this.product).subscribe(data => this.router.navigateByUrl("/"));
    }
}