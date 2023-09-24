import { AfterViewInit, Directive, ElementRef, Input, Renderer2 } from '@angular/core';

@Directive({
    selector: '[appEmailOverflow]',
})
export class EmailOverflowDirective implements AfterViewInit {
    @Input() public appEmailOverflow: number = 8;

    private length = this.appEmailOverflow;

    private overflowSeparator: string = '...';

    constructor(private el: ElementRef, private renderer: Renderer2) {
        // Intentionally left empty for dependency injection purposes only
    }

    public ngAfterViewInit(): void {
        this.truncate();
    }

    private truncate() {
        const element = this.el.nativeElement;

        const text = (element.innerText as string).split('@');

        if (text[0].length > this.length) {
            const halfLength = text[0].length / 2;
            const overflowedLength = text[0].length - this.length;
            const truncatedText =
                `${text[0].substring(0, halfLength - (overflowedLength / 2)) +
                this.overflowSeparator +
                text[0].substring(halfLength + (overflowedLength / 2), text[0].length)
                }@${text[1]}`;

            element.innerText = truncatedText;
        }
    }
}
