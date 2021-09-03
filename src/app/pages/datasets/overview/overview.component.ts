import {Component, OnInit} from '@angular/core';

@Component({
    selector: 'app-overview',
    templateUrl: './overview.component.html',
    styleUrls: ['./overview.component.scss'],
})
export class OverviewComponent implements OnInit {
    constructor() {}

    radarInfoDescriber = [
        {
            title: 'اعتبار',
            text: 'توضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبارتوضیحات بخش اعتبار',
        },
        {
            title: 'محبوبیت',
            text: 'توضیحات بخش محبوبیتتوضیحات بخش محبوبیتتوضیحات بخش محبوبیتتوضیحات بخش محبوبیتتوضیحات بخش محبوبیتتوضیحات بخش محبوبیت',
        },
        {
            title: 'کامل بودن',
            text: 'توضیحات بخش کامل بودنتوضیحات بخش کامل بودنتوضیحات بخش کامل بودنتوضیحات بخش کامل بودنتوضیحات بخش کامل بودنتوضیحات بخش کامل بودنتوضیحات بخش کامل بودن',
        },
        {
            title: 'قابل کشف',
            text: 'توضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشفتوضیحات بخش قابل کشف',
        },
        {
            title: 'میزان کارکرد',
            text: 'توضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکردتوضیحات بخش میزان کارکرد',
        },
    ];

    index: number = 0;

    isEditing: boolean = false;
    description: string = 'توضیحات';
    currentTextAreaValue: string = this.description;
    rateSlideValue: number = 1;
    rateState!: string;

    doughnutColor = {
        type1: {
            data: [45.3, 54.7],
            color: ['hsl(0, 0%, 82%)', 'hsl(89, 49%, 50%)'],
            label: ['معتبر', 'نامعتبر'],
        },
        type2: {
            data: [55.1, 44.9],
            color: ['hsl(0, 0%, 82%)', 'hsl(0, 0%, 30%)'],
            label: ['خالی', 'پر'],
        },
        type3: {
            data: [99.6, 0.4],
            color: ['hsl(0, 0%, 82%)', 'hsl(358, 76%, 65%)'],
            label: ['نامعتبر', 'معتبر'],
        },
    };

    ngOnInit(): void {
        this.moodChanger();
    }

    moodChanger() {
        if (this.rateSlideValue < 25) {
            this.rateState = 'very-sad';
        } else if (this.rateSlideValue < 50) {
            this.rateState = 'sad';
        } else if (this.rateSlideValue < 75) {
            this.rateState = 'normal';
        } else if (this.rateSlideValue < 100) {
            this.rateState = 'happy';
        } else if (this.rateSlideValue === 100) {
            this.rateState = 'very-happy';
        }
    }

    infoChanger(dir: string) {
        if (dir === 'next') {
            this.index++;
            if (this.index === 5) {
                this.index = 0;
            }
        } else if (dir === 'pre') {
            this.index--;
            if (this.index === -1) {
                this.index = 4;
            }
        }
    }

    formatLabel(value: number) {
        if (value >= 1000) {
            return Math.round(value / 1000) + '%';
        }

        return value;
    }

    textAreaChangeSave() {
        this.description = this.currentTextAreaValue;
        this.editToggler();
    }

    editToggler() {
        this.isEditing = !this.isEditing;
        this.currentTextAreaValue = this.description;
    }

    slideChangeHandler(e: any) {
        this.rateSlideValue = e.value;
        this.moodChanger();
    }
}
