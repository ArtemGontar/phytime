import { Component, OnInit } from '@angular/core';

const surveys = [{
  id: 1,
  questions: [{
    question: 'On the scale of 1 to 10, what would you rate this course?',
    options: [
      {value: 10, label: 10},
      {value: 9, label: 9},
      {value: 8, label: 8},
      {value: 7, label: 7},
      {value: 6, label: 6},
      {value: 5, label: 5},
      {value: 4, label: 4},
      {value: 3, label: 3},
      {value: 2, label: 2},
      {value: 1, label: 1},
    ],
    type: 'radio',
  }, {
    question: 'What do you like the most about this course?',
    type: 'text'
  }, {
    question: 'What did you learn from this course that you didn\'t know before?',
    options: [
      {value: "angular", label: "Angular"},
      {value: "cli", label: "Angular CLI"},
      {value: "pwa", label: "Progressive Web Apps"},
      {value: "spa", label: "Single Page Application using Anuglar"},
      {value: "ssr", lable: "Server Side Rendering using Angular Universal"},
      {value: "monorepo", label: "Creating Mono Repo App using Nx"}
    ]
  }, {
    question: 'Whats your feedback about the course?',
    type: 'text'
  }]
}]

@Component({
  selector: 'client-onboarding',
  templateUrl: './client-onboarding.component.html',
  styleUrls: ['./client-onboarding.component.css']
})
export class ClientOnboardingComponent implements OnInit {

  seasons: number[] = [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
  questions = surveys[0].questions;
  
  constructor() { }

  ngOnInit(): void {
  }

}
