import { Component, OnInit } from '@angular/core';
import { Feed } from '../models/rss';
import {FeedService} from "../feed.service";
import { Router } from '@angular/router';
import { Source } from '../models/sources';

@Component({
  selector: 'article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  constructor(private router: Router, private dataService: FeedService) { }

  feed: Feed = {
    feedValue: {
      id: 1,
      title: "title",
      url: "www.google.com",
      itemsCount: 150,
      users: []
    },
    pageInfo: 1,
    sortValue: null,
    syndicationItems: [{
      title: {
        text: "text"
      },
      publishDate: "22222",
      summary: {
        text: "summary"
      }
    }]
  }

  url: string = "https://psyjournals.ru/rss/allnews.rss";
  page: number = 1
  sortValue: string = "Newest"

  ngOnInit(): void {
    this.load(history.state.source);
  }

  load(source: Source) {
    console.log(source.url)
    this.feed = null;
    this.dataService.getRssByUrl(source.url, this.page, this.sortValue).subscribe((data: Feed) => {
      console.log(data);
      this.feed = data
    });
  }

  nextArticles(url: string, page: number, sortValue: string){
    this.dataService.getRssByUrl(url, page, sortValue).subscribe((data: Object) => {
      console.log(data);
    })
  }

  openArticle(url){
    window.open(url, "_blank");
  }
}
