import { Component } from '@angular/core';
import { FilesDownloaderService } from '@core/services/files-downloader.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent {
    // eslint-disable-next-line no-empty-function
    constructor(private filesDownloader: FilesDownloaderService) {}

    downloadApp() {
        this.filesDownloader.downloadSquirrelInstaller();
    }
}
