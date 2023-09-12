import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class SettingsService {
    activeTab: string = 'general';

    setActiveTab(tabName: string): void {
        this.activeTab = tabName;
    }
}
