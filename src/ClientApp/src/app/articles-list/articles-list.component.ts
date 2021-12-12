import { Component, OnInit } from '@angular/core';
import {FeedService} from "../feed.service";
import {Article} from "../models/article";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {

  id: number;
  items: Article[];

  constructor(private dataService: FeedService) {
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.items = [];
    this.dataService.getFeeds().subscribe((data: Article[]) => this.items = data);
  }
}
