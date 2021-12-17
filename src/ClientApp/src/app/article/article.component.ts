import { Component, OnInit } from '@angular/core';
import { Feed } from '../models/rss';
import {FeedService} from "../feed.service";

@Component({
  selector: 'article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  constructor(private dataService: FeedService) { }

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
  ngOnInit(): void {
  }
  nextArticles(url: string, page: number, sortValue: string){
    this.dataService.getRssByUrl(url, page, sortValue).subscribe((data: Object) => {
      console.log(data);
    })
  }
}
