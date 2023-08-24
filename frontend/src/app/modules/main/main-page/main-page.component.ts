import { Component, OnDestroy, OnInit } from '@angular/core';
import { BroadcastHubService } from '@core/hubs/broadcast-hub.service';
import { TreeDataObject } from '@shared/components/tree/auxiliary/TreeDataObject';

@Component({
    selector: 'app-home',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.sass'],
})
export class MainComponent implements OnInit, OnDestroy {
    treeData: TreeDataObject[] = [
        { text: 'Tables', code: '0.1' },
        { text: 'Table', code: '0.1.1' },
        { text: 'Table', code: '0.1.1' },
        { text: 'Table', code: '0.1.1' },
        { text: 'Table', code: '0.1.1' },
        { text: 'Table', code: '0.1.1' },
        { text: 'Table', code: '0.1.1' },

        { text: 'Procedures', code: '0.2' },
        { text: 'Procedure 1', code: '0.2.2' },
        { text: 'Procedure 2', code: '0.2.2' },

        { text: 'Data', code: '0.3' },
        { text: 'Data 1', code: '0.3.1' },
        { text: 'Data 2', code: '0.3.2' },
        { text: 'Data 3', code: '0.3.3' },
    ];

    // eslint-disable-next-line no-empty-function
    constructor(private broadcastHub: BroadcastHubService) {}

    async ngOnInit() {
        await this.broadcastHub.start();
        this.broadcastHub.listenMessages((msg) => {
            console.info(`The next broadcast message was received: ${msg}`);
        });
    }

    ngOnDestroy() {
        this.broadcastHub.stop();
    }
}
