import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class FeedService {

    private itemsUrl = "/api/items/";
    private feedUrl = "/api/feed/"

    constructor(private http: HttpClient) {
    }

    getItems(id: number) {
        return this.http.get(this.itemsUrl + id);
    }

    getFeeds() {
        return this.http.get(this.feedUrl);
    }
}