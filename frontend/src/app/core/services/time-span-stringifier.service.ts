import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class TimeSpanStringifierService {
    private avgMonthLength = 30.436875;

    public stringify(date: Date): string {
        const timePassed = Date.now() - date.getTime();

        const seconds = Math.floor(timePassed / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);
        const months = Math.floor(days / this.avgMonthLength);
        const years = Math.floor(months / 12);

        if (years > 0) {
            return years + (years > 1 ? ' years' : ' year');
        }
        if (months > 0) {
            return months + (months > 1 ? ' months' : ' month');
        }
        if (days > 0) {
            return days + (days > 1 ? ' days' : ' day');
        }
        if (hours > 0) {
            return hours + (hours > 1 ? ' hours' : ' hour');
        }
        if (minutes > 0) {
            return minutes + (minutes > 1 ? ' minutes' : ' minute');
        }

        return seconds + (seconds > 1 ? ' seconds' : ' second');
    }
}
