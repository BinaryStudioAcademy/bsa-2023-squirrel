import { Component, OnInit } from '@angular/core';
import { SettingsService } from '@core/services/settings.service';

@Component({
    selector: 'app-settings-menu',
    templateUrl: './settings-menu.component.html',
    styleUrls: ['./settings-menu.component.sass'],
})
export class SettingsMenuComponent implements OnInit {
    activeTab: string = 'general';

    constructor(private settingsService: SettingsService) {
        // Intentionally left empty for dependency injection purposes only
    }

    setActiveTab(tabName: string): void {
        this.settingsService.setActiveTab(tabName);
        this.activeTab = tabName;
    }

    ngOnInit(): void {
        this.activeTab = this.settingsService.activeTab;
    }
}
