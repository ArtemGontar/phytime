import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ItemListComponent } from './item-list/item-list.component';
import { NotFoundComponent } from './not-found.component';

import { FeedService } from './feed.service';;
import { CitationComponent } from './citation/citation.component'
;
import { ArticlesListComponent } from './articles-list/articles-list.component'
const appRoutes: Routes = [
    { path: '', component: ArticlesListComponent },
    { path: 'angular/:id', component: ItemListComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, ItemListComponent, NotFoundComponent, CitationComponent, ArticlesListComponent],
    providers: [FeedService], 
    bootstrap: [AppComponent]
})
export class AppModule { }