import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PipelineHeaderComponent } from './pipeline-header.component';

describe('PipelineHeaderComponent', () => {
  let component: PipelineHeaderComponent;
  let fixture: ComponentFixture<PipelineHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PipelineHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PipelineHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
