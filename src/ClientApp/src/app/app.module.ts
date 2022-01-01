import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from '../material.module'
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
import { FooterComponent } from './footer/footer.component';
import {PortalModule} from '@angular/cdk/portal';
import {MatNativeDateModule} from '@angular/material/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'sources', component: SourcesComponent },
    { path: 'recommendations', component: RecommendationsComponent },
    { path: 'sources/:id', component: ArticlesComponent }
];

@NgModule({
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        FormsModule,
        MatNativeDateModule,
        MaterialModule,
        PortalModule,
        ReactiveFormsModule,
        HttpClientModule, 
        RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, NotFoundComponent, CitationComponent, SourcesComponent, HomeComponent, ArticlesComponent, NavbarComponent, RecommendationsComponent, FooterComponent],
    providers: [FeedService, RecommendationService], 
    bootstrap: [AppComponent]
})
export class AppModule { }