import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Item } from './item';
 
@Injectable()
export class DataService {
 
    private url = "/api/items";
 
    constructor(private http: HttpClient) {
    }
 
    getItems() {
        return this.http.get(this.url);
    }
}