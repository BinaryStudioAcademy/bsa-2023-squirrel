import { HttpEventType, HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { HttpInternalService } from '@core/services/http-internal.service';
import { NotificationService } from '@core/services/notification.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent {
    private readonly staticFilesRoutePrefix = '/api/staticfiles';

    // eslint-disable-next-line no-empty-function
    constructor(private httpClient: HttpInternalService, private notificationService: NotificationService) {}

    downloadApp() {
        this.httpClient.getFullBlobRequest(`${this.staticFilesRoutePrefix}/downloadconsole`)
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
        const a = document.createElement('a');

        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        a.download = fileName;
        a.href = URL.createObjectURL(downloadedFile);
        a.target = '_blank';
        a.click();
        document.body.removeChild(a);
    }
}
