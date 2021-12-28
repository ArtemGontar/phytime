import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Feed } from './models/rss';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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

    getRss(id: number, page: number, sortValue:  string): Observable<Feed> {
        return this.http.get(this.feedUrl + id
            + "?page=" + page
            + "&sortValue=" + sortValue)
            .pipe(map((data) => data));
    }
}