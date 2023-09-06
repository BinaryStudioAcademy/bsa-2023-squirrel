import { Component } from '@angular/core';
import { SettingsService } from '@core/services/settings.service';

@Component({
    selector: 'app-settings-menu',
    templateUrl: './settings-menu.component.html',
    styleUrls: ['./settings-menu.component.sass'],
})
export class SettingsMenuComponent {
    activeTab: string = 'general';

    constructor(private settingsService: SettingsService) {}

    setActiveTab(tabName: string): void {
        this.settingsService.setActiveTab(tabName);
        this.activeTab = tabName;
    }
}
