import { Component, OnInit } from '@angular/core';
import { Feed } from '../models/rss';
import {FeedService} from "../feed.service";
import { Router } from '@angular/router';
import { Source } from '../models/sources';

@Component({
  selector: 'articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.css']
})
export class ArticlesComponent implements OnInit {

  constructor(private router: Router, private dataService: FeedService) { }

  feed: Feed = {
    feedValue: {
      id: 1,
      title: "title",
      url: "www.google.com",
      itemsCount: 150,
      users: []
    },
    pageInfo: {
      pageNumber: 1,
      pageSize: 5
    },
    sortValue: null,
    syndicationItems: []
  }

  source: Source = {};
  page: number = 1
  sortValue: string = "Newest"

  ngOnInit(): void {
    this.source = history.state.source
    this.load();
  }

  load() {
    this.dataService.getRssByUrl(this.source.url, this.page, this.sortValue).subscribe((data: Feed) => {
      console.log(data);
      this.feed = data
    });
  }

  nextArticles(){
    this.dataService.getRssByUrl(this.source.url, 
      this.feed.pageInfo.pageNumber + this.feed.pageInfo.pageSize, 
      this.feed.sortValue).subscribe((data: Object) => {
        console.log(data);
        this.feed = data
    })
  }

  prevArticles(){
    this.dataService.getRssByUrl(this.source.url, 
      this.feed.pageInfo.pageNumber - this.feed.pageInfo.pageSize, 
      this.feed.sortValue).subscribe((data: Object) => {
        console.log(data);
        this.feed = data
    })
  }

  openArticle(url){
    console.log("article opened");
  }
}
