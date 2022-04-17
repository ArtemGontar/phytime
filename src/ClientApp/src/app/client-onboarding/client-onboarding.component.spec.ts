import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientOnboardingComponent } from './client-onboarding.component';

describe('ClientOnboardingComponent', () => {
  let component: ClientOnboardingComponent;
  let fixture: ComponentFixture<ClientOnboardingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientOnboardingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientOnboardingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
