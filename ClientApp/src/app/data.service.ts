import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from './item';

@Injectable()
export class DataService {

    private url = "/api/items/";

    constructor(private http: HttpClient) {
    }

    getProducts() {
        return this.http.get(this.url + 'get');
    }

    getProduct(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    createProduct(product: Product) {
        return this.http.post(this.url, product);
    }

    updateProduct(product: Product) {
        return this.http.put(this.url, product);
    }

    deleteProduct(id: number) {
        return this.http.delete(this.url + '/' + id);
    }
}