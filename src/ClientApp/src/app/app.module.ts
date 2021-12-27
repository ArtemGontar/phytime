import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './not-found.component';
import { FeedService } from './feed.service';
import { CitationComponent } from './citation/citation.component';
import { SourcesComponent } from './sources/sources.component';
import { HomeComponent } from './home/home.component';;
import { ArticlesComponent } from './articles/articles.component'

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'articles', component: ArticlesComponent },
    { path: 'sources', component: SourcesComponent },
    { path: 'recommendations', component: RecommendationsComponent },
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, NotFoundComponent, CitationComponent, SourcesComponent, HomeComponent, ArticleComponent, NavbarComponent, RecommendationsComponent, FooterComponent],
    providers: [FeedService], 
    bootstrap: [AppComponent]
})
export class AppModule { }