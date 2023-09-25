import { Component, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-base',
    template: '',
})
export class BaseComponent implements OnDestroy {
    protected unsubscribe$ = new Subject<void>();

    public ngOnDestroy() {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
