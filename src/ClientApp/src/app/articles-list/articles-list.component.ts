import { Component, OnInit } from '@angular/core';
import {FeedService} from "../feed.service";
import {Source} from "../models/sources";

@Component({
  selector: 'articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {
  sources: Source[];

  constructor(private dataService: FeedService) {
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.sources = null;
    this.dataService.getRssList().subscribe((data: Source[]) => {
      console.log(data);
      this.sources = data
    });
  }

  openSourceFeed(url: string, page: number, sortValue: string){
    this.dataService.getRssByUrl(url, page, sortValue).subscribe((data: Object) => {
      console.log(data);
    })
  }
}
