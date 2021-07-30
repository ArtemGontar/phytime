import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Item } from './item';
 
@Component({
    templateUrl: './item-list.component.html'
})
export class ItemListComponent implements OnInit {
 
    items: Item[];
    constructor(private dataService: DataService) { }
 
    ngOnInit() {
        this.load();
    }

    load() {
        this.items = [];
        this.dataService.getItems().subscribe((data: Item[]) => this.items = data);
    }
}