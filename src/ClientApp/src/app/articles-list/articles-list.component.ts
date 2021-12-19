import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {FeedService} from "../feed.service";
import {Source} from "../models/sources";

@Component({
  selector: 'articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {
  sources: Source[];

  constructor(private router: Router, private dataService: FeedService) {
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

  openArticles(source: Source){
    this.router.navigate(['/articles'], 
    {
      state: {
        source: {
          title: source.title,
          url: source.url
        }
      },
    });
  }
}
