import { Component, Input, OnInit } from '@angular/core';
import { HSLGenerator } from '@shared/helpers/hsl-generator';

type Style = Partial<CSSStyleDeclaration>;

@Component({
    selector: 'app-avatar',
    templateUrl: './avatar.component.html',
    styleUrls: ['./avatar.component.sass'],
})
export class AvatarComponent implements OnInit {
    @Input()
    public userName: string = '';

    @Input()
    public imgLink: string = '';

    @Input()
    public size: string | number = 50;

    @Input()
    public textSizeRatio = 3;

    @Input()
    public style: Style = {};

    public avatarSrc: string | null = null;

    public avatarText: string | null = null;

    public avatarStyle: Style = {};

    public hostStyle: Style = {};

    ngOnInit(): void {
        this.initializeAvatar();
    }

    // eslint-disable-next-line no-empty-function
    constructor(private hslGenerator: HSLGenerator) {}

    private initializeAvatar(): void {
        this.hostStyle = {
            width: `${this.size}px`,
            height: `${this.size}px`,
        };

        if (this.imgLink) {
            this.avatarStyle = this.getImageStyle();
            this.avatarSrc = this.imgLink;
        } else {
            this.avatarStyle = this.getInitialsStyle(this.userName);
            this.avatarText = this.getAvatarInitials(this.userName, 2);
        }
    }

    private getImageStyle(): Style {
        return {
            width: `${this.size}px`,
            height: `${this.size}px`,
            ...this.style,
        };
    }

    private getInitialsStyle(avatarValue: string): Style {
        return {
            backgroundColor: this.hslGenerator.generateHSLString(avatarValue),
            font: `${Math.floor(+this.size / this.textSizeRatio)}px montserrat-variable-font`,
            lineHeight: `${this.size}px`,
            ...this.style,
        };
    }

    private constructInitials(elements: string[]): string {
        if (!elements || !elements.length) {
            return '';
        }

        return elements
            .filter((element) => element && element.length > 0)
            .map((element) => element[0].toUpperCase())
            .join('');
    }

    private getAvatarInitials(name: string, size: number): string {
        const nameTrim = name.trim();

        if (!nameTrim) {
            return '';
        }

        const initials = nameTrim.split(' ');

        if (size && size < initials.length) {
            return this.constructInitials(initials.slice(0, size));
        }

        return this.constructInitials(initials);
    }

    handleImgError() {
        this.imgLink = '';
        this.initializeAvatar();
    }
}
