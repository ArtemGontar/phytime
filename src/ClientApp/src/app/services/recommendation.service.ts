import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Recommendation } from '../models/recommendation';

@Injectable()
export class RecommendationService {

    private recommendationUrl = "/api/recommendations/";

    constructor(private http: HttpClient) {
    }

    getRecommendations(): Observable<Recommendation[]> {
        return this.http.get<Recommendation[]>(this.recommendationUrl)
        .pipe(map((data) => data));
    }

    getRecommendation(id: number): Observable<Recommendation> {
        return this.http.get<Recommendation>(this.recommendationUrl + id)
        .pipe(map((data) => data));
    }
}