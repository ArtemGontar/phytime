import { Component, OnInit } from '@angular/core';
import { FeedViewModel } from '../models/rss';
@Component({
  selector: 'article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  constructor() { }

  feedViewModel: FeedViewModel = {
    FeedValue: null,
    PageInfo: null,
    SortValue: null,
    SyndicationItems: null
  }

  ngOnInit(): void {
  }

}
