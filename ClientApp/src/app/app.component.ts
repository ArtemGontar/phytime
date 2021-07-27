import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Item } from './item';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    items: Item[];
    tableMode: boolean = true;         

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadItems();
    }

    loadItems() {
        this.dataService.getItems()
            .subscribe((data: Item[]) => this.items = data);
    }
}