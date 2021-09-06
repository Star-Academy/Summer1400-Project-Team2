import {DebugElement} from '@angular/core';
import {ComponentFixture, TestBed} from '@angular/core/testing';
import {By} from '@angular/platform-browser';

import {OverviewComponent} from './overview.component';

describe('OverviewComponent', () => {
    let component: OverviewComponent;
    let fixture: ComponentFixture<OverviewComponent>;
    let parentDiv: DebugElement;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [OverviewComponent],
        }).compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(OverviewComponent);
        component = fixture.componentInstance;
        parentDiv = fixture.debugElement.query(By.css('div'));

        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call slideChangeHandler', async () => {
        let fixture = TestBed.createComponent(OverviewComponent);
        let app = fixture.debugElement.componentInstance;
        fixture.detectChanges();
        spyOn(app, 'slideChangeHandler');
        fixture.debugElement.query(By.css('#mat-slider')).triggerEventHandler('input', {});
        expect(app.slideChangeHandler).toHaveBeenCalled();
    });

    it('should render correct img', async () => {
        component.rateState = 'very-sad';
        fixture.detectChanges();
        let src = fixture.debugElement.query(By.css('#state-img')).nativeNode.src;
        let img = src.split('/')[src.split('/').length - 1];
        expect(img).toEqual('face--very-sad.svg');

        component.rateState = 'sad';
        fixture.detectChanges();
        src = fixture.debugElement.query(By.css('#state-img')).nativeNode.src;
        img = src.split('/')[src.split('/').length - 1];
        expect(img).toEqual('face--sad.svg');

        component.rateState = 'normal';
        fixture.detectChanges();
        src = fixture.debugElement.query(By.css('#state-img')).nativeNode.src;
        img = src.split('/')[src.split('/').length - 1];
        expect(img).toEqual('face--normal.svg');

        component.rateState = 'happy';
        fixture.detectChanges();
        src = fixture.debugElement.query(By.css('#state-img')).nativeNode.src;
        img = src.split('/')[src.split('/').length - 1];
        expect(img).toEqual('face--happy.svg');

        component.rateState = 'very-happy';
        fixture.detectChanges();
        src = fixture.debugElement.query(By.css('#state-img')).nativeNode.src;
        img = src.split('/')[src.split('/').length - 1];
        expect(img).toEqual('face--very-happy.svg');
    });

    it('should call infoChanger', async () => {
        let fixture = TestBed.createComponent(OverviewComponent);
        let app = fixture.debugElement.componentInstance;
        fixture.detectChanges();
        spyOn(app, 'infoChanger');
        fixture.debugElement.query(By.css('#next-btn')).triggerEventHandler('click', {});
        expect(app.infoChanger).toHaveBeenCalled();

        fixture.detectChanges();
        fixture.debugElement.query(By.css('#left-btn')).triggerEventHandler('click', {});
        expect(app.infoChanger).toHaveBeenCalled();
    });

    it('should render textArea', async () => {
        component.isEditing = true;
        fixture.detectChanges();
        expect(fixture.debugElement.query(By.css('.text-wrapper'))).not.toBeNull();

        component.isEditing = false;
        fixture.detectChanges();
        expect(fixture.debugElement.query(By.css('.text-wrapper'))).toBeNull();
        expect(fixture.debugElement.query(By.css('#des-txt'))).not.toBeNull();
    });

    it('should call save btn', async () => {
        let fixture = TestBed.createComponent(OverviewComponent);
        let app = fixture.debugElement.componentInstance;
        app.isEditing = true;
        fixture.detectChanges();
        spyOn(app, 'textAreaChangeSave');
        fixture.debugElement.query(By.css('#save-btn')).triggerEventHandler('click', {});
        expect(app.textAreaChangeSave).toHaveBeenCalled();
    });

    it('should call editToggler', async () => {
        let fixture = TestBed.createComponent(OverviewComponent);
        let app = fixture.debugElement.componentInstance;
        app.isEditing = true;
        fixture.detectChanges();
        spyOn(app, 'editToggler');
        fixture.debugElement.query(By.css('.back-btn')).triggerEventHandler('click', {});
        expect(app.editToggler).toHaveBeenCalled();
    });

    it('should call edittoggler btn', async () => {
        let fixture = TestBed.createComponent(OverviewComponent);
        let app = fixture.debugElement.componentInstance;
        app.isEditing = false;
        fixture.detectChanges();
        spyOn(app, 'editToggler');
        fixture.debugElement.query(By.css('.enable-edit')).triggerEventHandler('click', {});
        expect(app.editToggler).toHaveBeenCalled();
    });
});
