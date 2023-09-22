import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseComponent } from '@core/base/base.component';
import { BroadcastHubService } from '@core/hubs/broadcast-hub.service';
import { NotificationService } from '@core/services/notification.service';
import { ProjectService } from '@core/services/project.service';
import { SharedProjectService } from '@core/services/shared-project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { finalize, takeUntil } from 'rxjs';

import { ProjectResponseDto } from 'src/app/models/projects/project-response-dto';

@Component({
    selector: 'app-home',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.sass'],
})
export class MainComponent extends BaseComponent implements OnInit, OnDestroy {
    public project: ProjectResponseDto;

    constructor(
        private broadcastHub: BroadcastHubService,
        private route: ActivatedRoute,
        private router: Router,
        private projectService: ProjectService,
        private notificationService: NotificationService,
        private spinner: SpinnerService,
        private sharedProject: SharedProjectService,
    ) {
        super();
    }

    async ngOnInit() {
        this.loadProject();
        await this.broadcastHub.start();
        this.broadcastHub.listenMessages((msg) => {
            console.info(`The next broadcast message was received: ${msg}`);
        });
    }

    override ngOnDestroy() {
        this.broadcastHub.stop();
        this.sharedProject.setProject(null);
        this.sharedProject.setCurrentDb(null);
        super.ngOnDestroy();
    }

    private loadProject() {
        this.spinner.show();
        const projectId = this.route.snapshot.paramMap.get('id');

        if (!projectId) {
            this.notificationService.error('wrong route');
            this.router.navigateByUrl('/projects');
            this.projectService.setCurrentProjectId(0);

            return;
        }

        this.projectService.setCurrentProjectId(Number(projectId));

        this.projectService.getProject(projectId)
            .pipe(
                takeUntil(this.unsubscribe$),
                finalize(() => this.spinner.hide()),
            )
            .subscribe({
                next: project => {
                    this.project = project;
                    this.sharedProject.setProject(project);
                },
                error: err => {
                    this.notificationService.error(err.message);
                    this.router.navigateByUrl('projects');
                },
            });
    }
}
