import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class FeedService {

    private itemsUrl = "/api/items/";
    private feedUrl = "/api/rss/"

    constructor(private http: HttpClient) {
    }

    getItems(id: number) {
        return this.http.get(this.itemsUrl + id);
    }

    getRssList() {
        return this.http.get(this.feedUrl);
    }

    getRssByUrl(url: string) {
        return this.http.get(this.feedUrl + "some?url=" + url);
    }
}