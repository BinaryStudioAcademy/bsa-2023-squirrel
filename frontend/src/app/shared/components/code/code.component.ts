import { Component, ElementRef, Input, OnChanges, Renderer2, SimpleChanges } from '@angular/core';

import { ChangeTypeEnum } from 'src/app/models/text-pair/change-type-enum';
import { TextPairDifferenceDto } from 'src/app/models/text-pair/text-pair-difference-dto';

@Component({
    selector: 'app-code',
    templateUrl: './code.component.html',
    styleUrls: ['./code.component.sass'],
})
export class CodeComponent implements OnChanges {
    @Input() textPair: TextPairDifferenceDto;

    private insertColor: string = 'rgba(63,185,80,0.5)';

    private deleteColor: string = 'rgba(248,81,73,0.3)';

    // eslint-disable-next-line no-empty-function
    constructor(private renderer: Renderer2, private el: ElementRef) {}

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['textPair']) {
            for (let i = 0; i < this.textPair.newTextLines.length; i++) {
                const oldText = this.textPair.oldTextLines[i];
                const newText = this.textPair.newTextLines[i];

                if (newText.type === ChangeTypeEnum.unchanged) {
                    this.createLine(newText.text);
                    this.createLineNumber(newText.position);
                }
                if ((newText.type === ChangeTypeEnum.inserted || newText.type === ChangeTypeEnum.modified)
                && newText.position !== -1) {
                    this.createLine(newText.text, this.insertColor);
                    this.createLineNumber(newText.position, '+');
                }
                if (oldText.type === ChangeTypeEnum.deleted || newText.type === ChangeTypeEnum.modified) {
                    this.createLine(oldText.text, this.deleteColor);
                    this.createLineNumber(oldText.position, '-');
                }
            }
        }
    }

    private createLine(text: string, backgroundColor: string = 'transparent') {
        const element = this.renderer.createElement('li');

        this.renderer.setStyle(element, 'background-color', backgroundColor);

        const elText = this.renderer.createText(text);

        this.renderer.appendChild(element, elText);

        const container = this.el.nativeElement.querySelector('#code');

        this.renderer.appendChild(container, element);
    }

    private createLineNumber(number: number, symbol: string = '') {
        const li = this.renderer.createElement('li');
        const numberSpan = this.renderer.createElement('span');
        const symbolSpan = this.renderer.createElement('span');

        this.renderer.addClass(numberSpan, 'number');
        this.renderer.addClass(symbolSpan, 'symbol');
        this.renderer.addClass(li, 'number-container');

        const numberEl = this.renderer.createText(number.toString());
        const symbolEl = this.renderer.createText(symbol);

        this.renderer.appendChild(numberSpan, numberEl);
        this.renderer.appendChild(symbolSpan, symbolEl);

        this.renderer.appendChild(li, numberSpan);
        this.renderer.appendChild(li, symbolSpan);

        const container = this.el.nativeElement.querySelector('#lines');

        this.renderer.appendChild(container, li);
    }
}
