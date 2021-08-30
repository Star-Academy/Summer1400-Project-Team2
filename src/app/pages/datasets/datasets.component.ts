import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-datasets',
    templateUrl: './datasets.component.html',
    styleUrls: ['./datasets.component.scss'],
})
export class DatasetsComponent implements OnInit {
    layout: string = 'overview';

    constructor() {}

    ngOnInit(): void {}

    layoutChanger(layout: any) {
        this.layout = layout;
    }
}
