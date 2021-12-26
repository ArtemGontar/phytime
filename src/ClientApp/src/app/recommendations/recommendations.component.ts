import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'recommendations',
  templateUrl: './recommendations.component.html',
  styleUrls: ['./recommendations.component.css']
})
export class RecommendationsComponent implements OnInit {
  recommendationPost = {
    recommendations: [{
      title: "игра «Хорошее поведение»: история и современные тренды",
      summary: "игра «Хорошее поведение» — поведенческая процедура, направленная на изменение поведения всех членов учебной группы посредством взаимозависимого группового договора. С середины 60-х гг. XX в. игра применяется в различных культурных контекстах в начальной школе и дошкольных группах, а также на этапе средней и высшей школы, в том числе в условиях инклюзивного образования.",  
      link: "https://psyjournals.ru/autism/2021/n2/Statnikov.shtml"
    },
    {
      title: "игра «Хорошее поведение»: история и современные тренды",
      summary: "игра «Хорошее поведение» — поведенческая процедура, направленная на изменение поведения всех членов учебной группы посредством взаимозависимого группового договора. С середины 60-х гг. XX в. игра применяется в различных культурных контекстах в начальной школе и дошкольных группах, а также на этапе средней и высшей школы, в том числе в условиях инклюзивного образования.",  
      link: "https://psyjournals.ru/autism/2021/n2/Statnikov.shtml"
    }],
    author: "юлия литвинович",
    level: "продвинутый",
    publishDate: "20/20/21",
    tags: ["гештальт-терапия", "поведенческая терапия"]
  }
  constructor() { }

  ngOnInit(): void {
  }

  nextRecommendation(){
    
  }

}
