import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {FeedService} from "../services/feed.service";
import {Source} from "../models/sources";

@Component({
  selector: 'sources',
  templateUrl: './sources.component.html',
  styleUrls: ['./sources.component.css']
})
export class SourcesComponent implements OnInit {
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
    this.router.navigate(['/sources/' + source.id], 
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
