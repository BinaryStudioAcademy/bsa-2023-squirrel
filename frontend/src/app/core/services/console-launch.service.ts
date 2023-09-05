import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class ConsoleLaunchService {
    launchConsole() {
        window.open('squirrel:', '_self');
    }
}
