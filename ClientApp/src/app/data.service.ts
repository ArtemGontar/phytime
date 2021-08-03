import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { HttpClient } from '@angular/common/http';
import { Item } from './item';

@Injectable()
export class DataService {

    private url = "/api/items/";

    constructor(private http: HttpClient) {
    }

    getItems() {
        return this.http.get(this.url + 'get');
=======
import { HttpClient} from '@angular/common/http';
import { Item } from './item';
 
@Injectable()
export class DataService {
 
    private url = "/api/items";
 
    constructor(private http: HttpClient) {
    }
 
    getItems() {
        return this.http.get(this.url);
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
    }
}