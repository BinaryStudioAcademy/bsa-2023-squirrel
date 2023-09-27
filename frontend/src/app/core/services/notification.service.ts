import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root',
})
export class NotificationService {
    constructor(private toastr: ToastrService) { }

    public info(content: string, title?: string) {
        this.toastr.info(content, title);
    }

    public error(content: string, title?: string) {
        this.toastr.error(content, title);
    }
}
