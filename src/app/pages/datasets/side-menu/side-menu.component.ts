import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
    selector: 'app-side-menu',
    templateUrl: './side-menu.component.html',
    styleUrls: ['./side-menu.component.scss'],
})
export class SideMenuComponent implements OnInit {
    @Output() layoutManager = new EventEmitter();

    constructor() {}

    ngOnInit(): void {}

    layoutChange(e: any) {
        this.layoutManager.emit(e.currentTarget.id);
    }
}
