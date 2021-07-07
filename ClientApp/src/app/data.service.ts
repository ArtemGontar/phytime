import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Feed } from './feed';
 
@Injectable()
export class DataService {
 
    private url = "/api/feeds";
 
    constructor(private http: HttpClient) {
    }
 
    getFeeds() {
        return this.http.get(this.url);
    }
}