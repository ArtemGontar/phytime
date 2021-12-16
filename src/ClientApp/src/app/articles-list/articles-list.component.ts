import { Component, OnInit } from '@angular/core';
import {FeedService} from "../feed.service";
import {Source} from "../models/sources";

@Component({
  selector: 'articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {
  sources: Object;

  constructor(private dataService: FeedService) {
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.sources = null;
    this.dataService.getRssList().subscribe((data: Object) => {
      console.log(data);
      this.sources = data
    });
  }

  openSourceFeed(url: string){
    this.dataService.getRssByUrl(url).subscribe((data: Object) => {
      console.log(data);
    })
  }
}
