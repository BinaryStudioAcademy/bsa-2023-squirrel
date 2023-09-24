import { Component } from '@angular/core';
import { FilesDownloaderService } from '@core/services/files-downloader.service';
import { faApple, faLinux, faWindows } from '@fortawesome/free-brands-svg-icons';

import { OperatingSystem } from 'src/app/models/downloads/operating-system';

@Component({
    selector: 'app-downloads',
    templateUrl: './downloads.component.html',
    styleUrls: ['./downloads.component.sass'],
})
export class DownloadsComponent {
    // eslint-disable-next-line no-empty-function
    constructor(private filesDownloader: FilesDownloaderService) {}

    public OS = OperatingSystem;

    public faWindows = faWindows;

    public faLinux = faLinux;

    public faApple = faApple;

    public downloadApp(os: OperatingSystem): void {
        this.filesDownloader.downloadSquirrelInstaller(os);
    }
}
