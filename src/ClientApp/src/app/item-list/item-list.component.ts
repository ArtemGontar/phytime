import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FeedService } from '../feed.service';
import { Item } from '../models/item';
 
@Component({
    templateUrl: './item-list.component.html'
})
export class ItemListComponent implements OnInit {

    id: number;
    items: Item[];
    constructor(private dataService: FeedService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }
 
    ngOnInit() {
        this.load();
    }

    load() {
        this.items = [];
        this.dataService.getItems(this.id).subscribe((data: Item[]) => this.items = data);
    }
}