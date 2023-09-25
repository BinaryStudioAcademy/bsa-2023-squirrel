import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { isObservable, Observable, of } from 'rxjs';

export interface CanComponentDeactivate {
    canDeactivate: () => boolean | Observable<boolean>;
}

@Injectable({
    providedIn: 'root',
})
export class UnsavedScriptGuard implements CanDeactivate<CanComponentDeactivate> {
    canDeactivate(component: CanComponentDeactivate): boolean | Observable<boolean> {
        const result = component.canDeactivate();

        return isObservable(result) ? result : of(result);
    }
}
