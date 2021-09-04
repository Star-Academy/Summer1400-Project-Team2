import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PipelinePage } from './pipeline.component';

describe('PipelineComponent', () => {
  let component: PipelinePage;
  let fixture: ComponentFixture<PipelinePage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PipelinePage]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PipelinePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
