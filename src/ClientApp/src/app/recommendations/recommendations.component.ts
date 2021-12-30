import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Recommendation } from '../models/recommendation';
import { RecommendationService } from '../services/recommendation.service';

@Component({
  selector: 'recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.css']
})
export class RecommendationsComponent implements OnInit {
  recommendations: Observable<Recommendation[]>
  constructor(private recommendationService: RecommendationService) {

   }

  ngOnInit(): void {
    this.recommendations = this.recommendationService.getRecommendations()
    .pipe(map(data => data));
  }

  nextRecommendations(){
    
  }

  prevRecommendations(){
    
  }
}
