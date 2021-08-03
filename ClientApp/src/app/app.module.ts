import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
<<<<<<< HEAD
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ItemListComponent } from './item-list.component';
import { NotFoundComponent } from './not-found.component';

import { DataService } from './data.service';

const appRoutes: Routes = [
    { path: 'angular', component: ItemListComponent },
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, ItemListComponent, NotFoundComponent],
    providers: [DataService], 
=======
import { AppComponent } from './app.component';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule],
    declarations: [AppComponent],
>>>>>>> 03436020527dea8a234f10a1cff9c93daa27112e
    bootstrap: [AppComponent]
})
export class AppModule { }