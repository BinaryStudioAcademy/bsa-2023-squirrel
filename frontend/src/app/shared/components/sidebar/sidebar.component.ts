import { Component } from '@angular/core';
import { FilesDownloaderService } from '@core/services/files-downloader.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.sass'],
})
export class SidebarComponent {
    constructor(private filesDownloader: FilesDownloaderService) { }

    public downloadApp() {
        this.filesDownloader.downloadSquirrelInstaller();
    }
}
