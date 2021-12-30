import { Component, OnInit } from '@angular/core';
import { Feed, SyndicationItem } from '../models/rss';
import {FeedService} from "../services/feed.service";
import { ActivatedRoute, Router } from '@angular/router';
import { Source } from '../models/sources';
import { map } from "rxjs/operators";
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private dataService: FeedService) { }
  source: Source;
  syndicationItems: Observable<SyndicationItem[]>
  page: number = 1
  sortValue: string = "Newest"
  id: number;
  private routeSub: Subscription;

  ngOnInit(): void {
    this.source = history.state.source
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params['id'])
      this.id = parseInt(params['id'])
      this.load();
    });
  }
  
  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }
  
  load() {
    this.syndicationItems = this.dataService.getRss(this.id, this.page, this.sortValue)
      .pipe(map((data) => {
        console.log(data)
        let newData: SyndicationItem[] = [];
        data.syndicationItems.forEach(x => newData.push(new SyndicationItem(x.title, x.summary, x.publishDate, x.links)));
        console.log(newData)
        return newData;
      }));
  }
  
  prevArticles(){
    this.page--;
    this.syndicationItems = this.dataService.getRss(this.id, this.page, this.sortValue)
      .pipe(map((data) => {
        let newData: SyndicationItem[] = [];
        data.syndicationItems.forEach(x => newData.push(new SyndicationItem(x.title, x.summary, x.publishDate, x.links)));
        return newData;
      }));
  }

  nextArticles(){
    this.page++
    this.syndicationItems = this.dataService.getRss(this.id, this.page, this.sortValue)
      .pipe(map((data) => {
        let newData: SyndicationItem[] = [];
        data.syndicationItems.forEach(x => newData.push(new SyndicationItem(x.title, x.summary, x.publishDate, x.links)));
        return newData;
      }))
  }

  openArticle(url){
    window.open(url, "_blank");
  }
}
