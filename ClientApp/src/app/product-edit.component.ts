import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { Product } from './item';
 
@Component({
    templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {
 
    id: number;
    product: Product;    // изменяемый объект
    loaded: boolean = false;
 
    constructor(private dataService: DataService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }
 
    ngOnInit() {
        if (this.id)
            this.dataService.getProduct(this.id)
                .subscribe((data: Product) => {
                    this.product = data;
                    if (this.product != null) this.loaded = true;
                });
    }
 
    save() {
        this.dataService.updateProduct(this.product).subscribe(data => this.router.navigateByUrl("/"));
    }
}