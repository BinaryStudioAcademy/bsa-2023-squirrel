import { Component, Input, OnInit } from '@angular/core';

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
            maxWidth: '100%',
            borderRadius: '50%',
            width: `${this.size}px`,
            height: `${this.size}px`,
            ...this.style,
        };
    }

    private getInitialsStyle(avatarValue: string): Style {
        return {
            textAlign: 'center',
            borderRadius: '100%',
            textTransform: 'uppercase',
            color: '#FFFFFF',
            backgroundColor: this.generateHSLString(avatarValue),
            font: `${Math.floor(+this.size / this.textSizeRatio)}px Helvetica, Arial, sans-serif`,
            lineHeight: `${this.size}px`,
            ...this.style,
        };
    }

    private calculateHash(value: string): number {
        let hash = 0;

        for (let i = 0; i < value.length; i++) {
            hash = value.charCodeAt(i) + ((hash << 5) - hash);
        }
        hash = Math.abs(hash);

        return hash;
    }

    private normalizeHash(hash: number, min: number, max: number): number {
        return Math.floor((hash % (max - min)) + min);
    }

    private generateHSLString(name: string): string {
        const hRange = [0, 360];
        const sRange = [50, 75];
        const lRange = [25, 60];

        const hash = this.calculateHash(name);
        const h = this.normalizeHash(hash, hRange[0], hRange[1]);
        const s = this.normalizeHash(hash, sRange[0], sRange[1]);
        const l = this.normalizeHash(hash, lRange[0], lRange[1]);

        return `hsl(${h}, ${s}%, ${l}%)`;
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

    // eslint-disable-next-line @typescript-eslint/no-unused-vars, no-unused-vars
    handleImgError(event: Event) {
        this.imgLink = '';
        this.initializeAvatar();
    }
}
