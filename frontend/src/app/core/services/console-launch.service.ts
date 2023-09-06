import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class ConsoleLaunchService {
    launchConsole() {
        let timeout: number;
        const uri: string = 'squirrel:';

        const blurHandler = function () {
            window.clearTimeout(timeout);
            window.removeEventListener('blur', blurHandler);
        };
        const timeoutHandler = function () {
            window.removeEventListener('blur', blurHandler);
            //TODO: redirect to Download Page
            //window.open('main/pull-requests', '_self');
        };

        window.addEventListener('blur', blurHandler);
        timeout = window.setTimeout(timeoutHandler, 500);
        window.location.href = uri;
    }
}
