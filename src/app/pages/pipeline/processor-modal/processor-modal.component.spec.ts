import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessorModalComponent } from './processor-modal.component';

describe('ProcessorModalComponent', () => {
  let component: ProcessorModalComponent;
  let fixture: ComponentFixture<ProcessorModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcessorModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcessorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
