import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { saveAs } from 'file-saver';
import { Subject, takeUntil } from 'rxjs';

import { OperatingSystem } from 'src/app/models/downloads/operating-system';

import { HttpInternalService } from './http-internal.service';
import { NotificationService } from './notification.service';

@Injectable({
    providedIn: 'root',
})
export class FilesDownloaderService {
    protected unsubscribe$ = new Subject<void>();

    private readonly staticFilesRoutePrefix = '/api/staticfiles';

    constructor(private httpClient: HttpInternalService, private notificationService: NotificationService) { }

    public downloadSquirrelInstaller(os: OperatingSystem): void {
        this.httpClient
            .getFullBlobRequest(`${this.staticFilesRoutePrefix}/squirrel-installer/${os}`)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (event: HttpResponse<Blob>) => {
                    if (event.type === HttpEventType.Response) {
                        this.downloadFile(event);
                        this.notificationService.info('Downloaded successfully.');
                    }
                },
                error: () => {
                    this.notificationService.error('An error occurred while attempting to download the file.');
                },
            });
    }

    private downloadFile(response: HttpResponse<Blob>) {
        const contentDisposition = response.headers.get('content-disposition');

        const fileName = contentDisposition
            ? contentDisposition.split(';')[1].trim().split('=')[1]
            : 'downloaded-file.bin'; // Default filename if not provided

        const downloadedFile = new Blob([response.body!], { type: response.body!.type });

        saveAs(downloadedFile, fileName);
    }
}
