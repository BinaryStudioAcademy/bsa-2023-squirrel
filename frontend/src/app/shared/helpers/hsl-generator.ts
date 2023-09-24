import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class HSLGenerator {
    public generateHSLString(name: string): string {
        const hRange = [0, 360];
        const sRange = [50, 75];
        const lRange = [25, 60];

        const hash = this.calculateHash(name);
        const h = this.normalizeHash(hash, hRange[0], hRange[1]);
        const s = this.normalizeHash(hash, sRange[0], sRange[1]);
        const l = this.normalizeHash(hash, lRange[0], lRange[1]);

        return `hsl(${h}, ${s}%, ${l}%)`;
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
}
