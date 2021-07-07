import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Feed } from './feed';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    feed: Feed = new Feed();   
    feeds: Feed[];                
    tableMode: boolean = true;         

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.loadFeeds();    // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadFeeds() {
        this.dataService.getFeeds()
            .subscribe((data: Feed[]) => this.feeds = data);
    }
}