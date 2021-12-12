import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from './models/item';

@Injectable()
export class FeedService {

    private url = "/api/items";

    constructor(private http: HttpClient) {
    }

    getItems(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    getFeeds() {
        return this.http.get(this.url);
    }
}