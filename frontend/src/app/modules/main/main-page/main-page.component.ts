import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { BroadcastHubService } from '@core/hubs/broadcast-hub.service';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { takeUntil } from 'rxjs';

import { ProjectDto } from '../../../models/projects/project-dto';

@Component({
    selector: 'app-home',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.sass'],
})
export class MainComponent extends BaseComponent implements OnInit, OnDestroy {
    public project: ProjectDto;

    constructor(
        private broadcastHub: BroadcastHubService,
        private route: ActivatedRoute,
        private router: Router,
        private projectService: ProjectService,
        private notificationService: NotificationService,
    ) {
        super();
    }

    async ngOnInit() {
        await this.broadcastHub.start();
        this.broadcastHub.listenMessages((msg) => {
            console.info(`The next broadcast message was received: ${msg}`);
        });
        this.loadProject();
    }

    override ngOnDestroy() {
        this.broadcastHub.stop();
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    private loadProject() {
        const projectId = this.route.snapshot.paramMap.get('id');

        if (!projectId) {
            this.notificationService.error('wrong route');

            return;
        }

        this.projectService.getProject(projectId)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: project => {
                    this.project = project;
                },
                error: err => {
                    this.notificationService.error(err.message);
                    this.router.navigateByUrl('projects');
                },
            });
    }
}
