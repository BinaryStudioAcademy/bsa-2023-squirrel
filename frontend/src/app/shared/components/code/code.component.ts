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

    @Input() height: string = '100%';

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
                    const line = this.createLine(newText.position);

                    this.createCodeLine(line, newText.text);
                }
                if ((newText.type === ChangeTypeEnum.inserted || newText.type === ChangeTypeEnum.modified)
                && newText.position !== -1) {
                    const line = this.createLine(newText.position, '+');

                    this.createCodeLine(line, newText.text, this.insertColor);
                }
                if (oldText.type === ChangeTypeEnum.deleted || newText.type === ChangeTypeEnum.modified) {
                    const line = this.createLine(oldText.position, '-');

                    this.createCodeLine(line, oldText.text, this.deleteColor);
                }
            }
        }
    }

    private createCodeLine(parent: any, text: string, backgroundColor: string = 'transparent') {
        const td = this.renderer.createElement('td');

        this.renderer.addClass(td, 'line-content');

        this.renderer.setStyle(parent, 'background-color', backgroundColor);

        const elText = this.renderer.createText(text);

        this.renderer.appendChild(td, elText);

        this.renderer.appendChild(parent, td);
    }

    private createLine(number: number, symbol: string = '') {
        const tr = this.renderer.createElement('tr');

        this.renderer.addClass(tr, 'line');

        const numberTd = this.renderer.createElement('td');
        const symbolTd = this.renderer.createElement('td');

        this.renderer.addClass(numberTd, 'line-number');
        this.renderer.addClass(symbolTd, 'line-symbol');

        const numberEl = this.renderer.createText(number.toString());
        const symbolEl = this.renderer.createText(symbol);

        this.renderer.appendChild(numberTd, numberEl);
        this.renderer.appendChild(symbolTd, symbolEl);

        this.renderer.appendChild(tr, numberTd);
        this.renderer.appendChild(tr, symbolTd);

        const container = this.el.nativeElement.querySelector('#code-table');

        this.renderer.appendChild(container, tr);

        return tr;
    }
}
