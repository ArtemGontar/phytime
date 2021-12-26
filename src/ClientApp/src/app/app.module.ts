import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './not-found.component';
import { FeedService } from './feed.service';
import { CitationComponent } from './citation/citation.component';
import { ArticlesListComponent } from './articles-list/articles-list.component';
import { HomeComponent } from './home/home.component';;
import { ArticleComponent } from './article/article.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RecommendationsComponent } from './recommendations/recommendations.component'
;
import { FooterComponent } from './footer/footer.component'
const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'sources', component: ArticlesListComponent },
    { path: 'recommendations', component: RecommendationsComponent },
    { path: 'articles', component: ArticleComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, NotFoundComponent, CitationComponent, ArticlesListComponent, HomeComponent, ArticleComponent, NavbarComponent, RecommendationsComponent, FooterComponent],
    providers: [FeedService], 
    bootstrap: [AppComponent]
})
export class AppModule { }