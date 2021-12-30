import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './not-found.component';
import { FeedService } from './services/feed.service';
import { RecommendationService } from './services/recommendation.service';
import { CitationComponent } from './citation/citation.component';
import { SourcesComponent } from './sources/sources.component';
import { HomeComponent } from './home/home.component';;
import { ArticlesComponent } from './articles/articles.component';
import { NavbarComponent } from './navbar/navbar.component';
import { RecommendationsComponent } from './recommendations/recommendations.component';
import { MatSliderModule } from '@angular/material/slider';
import { FooterComponent } from './footer/footer.component';
const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'sources', component: SourcesComponent },
    { path: 'recommendations', component: RecommendationsComponent },
    { path: 'sources/:id', component: ArticlesComponent }
];

@NgModule({
    imports: [MatSliderModule, BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, NotFoundComponent, CitationComponent, SourcesComponent, HomeComponent, ArticlesComponent, NavbarComponent, RecommendationsComponent, FooterComponent],
    providers: [FeedService, RecommendationService], 
    bootstrap: [AppComponent]
})
export class AppModule { }