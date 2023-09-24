import { Component } from '@angular/core';
import { SettingsService } from '@core/services/settings.service';

@Component({
    selector: 'app-settings-page',
    templateUrl: './settings-page.component.html',
    styleUrls: ['./settings-page.component.sass'],
})
export class SettingsPageComponent {
    constructor(private settingsService: SettingsService) {
        // Intentionally left empty for dependency injection purposes only
    }

    get activeTab(): string {
        return this.settingsService.activeTab;
    }
}
