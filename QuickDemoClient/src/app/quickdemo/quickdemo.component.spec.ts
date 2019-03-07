import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuickdemoComponent } from './quickdemo.component';

describe('QuickdemoComponent', () => {
  let component: QuickdemoComponent;
  let fixture: ComponentFixture<QuickdemoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuickdemoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuickdemoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
