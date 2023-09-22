import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';

export interface CanComponentDeactivate {
    canDeactivate: () => boolean;
}

@Injectable({
    providedIn: 'root',
})
export class UnsavedScriptGuard implements CanDeactivate<CanComponentDeactivate> {
    canDeactivate(component: CanComponentDeactivate): boolean {
        return component.canDeactivate();
    }
}
