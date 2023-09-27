import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { CommitService } from '@core/services/commit.service';
import { CommitChangesService } from '@core/services/commit-changes.service';
import { EventService } from '@core/services/event.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { TreeNode } from '@shared/components/tree/models/TreeNode.model';
import { finalize, takeUntil } from 'rxjs';

import { CreateCommitDto } from 'src/app/models/commit/create-commit-dto';
import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { DatabaseItemType } from 'src/app/models/database-items/database-item-type';
import { ItemCategory } from 'src/app/models/database-items/item-category';
import { TextPairDifferenceDto } from 'src/app/models/text-pair/text-pair-difference-dto';

import { DatabaseItemContentCompare } from '../../../models/database-items/database-item-content-compare';

@Component({
    selector: 'app-changes',
    templateUrl: './changes.component.html',
    styleUrls: ['./changes.component.sass'],
})
export class ChangesComponent extends BaseComponent implements OnInit, OnDestroy {
    public textPair: TextPairDifferenceDto;

    contentChanges: DatabaseItemContentCompare[] = [];

    public guid: string;

    public items: TreeNode[];

    public selectedItems: TreeNode[] = [];

    public message: string = '';

    private currentProjectId: number;

    constructor(
        private eventService: EventService,
        private branchService: BranchService,
        private commitService: CommitService,
        private projectService: ProjectService,
        private spinner: SpinnerService,
        private commitChangesService: CommitChangesService,
    ) {
        super();
        this.eventService.changesLoadedEvent$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((x) => {
                if (x !== undefined) {
                    this.items = this.mapDbItems(x);
                }
            });
        eventService.changesSavedEvent$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((x) => {
                if (x !== undefined) {
                    this.guid = x;
                }
            });
    }

    public ngOnInit(): void {
        this.currentProjectId = this.projectService.currentProjectId;
        this.commitChangesService.contentChanges$
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe((changes) => {
                this.contentChanges = changes;
            });
    }

    public validateCommit() {
        if (!this.guid) {
            return false;
        }
        if (!(this.message.length > 0 && this.message.length <= 300)) {
            return false;
        }
        if (!this.selectedItems.some(x => x.children?.some(y => y.selected))) {
            return false;
        }

        return true;
    }

    public commit() {
        const branchId = this.branchService.getCurrentBranch(this.currentProjectId);
        const commit = {
            branchId,
            postScript: '',
            preScript: '',
            selectedItems: this.selectedItems,
            changesGuid: this.guid,
            message: this.message,
        } as CreateCommitDto;

        this.spinner.show();
        this.commitService.commit(commit)
            .pipe(takeUntil(this.unsubscribe$), finalize(this.spinner.hide))
            .subscribe(x => {
                // eslint-disable-next-line no-console
                console.log(x.body);
                this.items.forEach(parent => {
                    if (parent.children) {
                        parent.children = parent.children.filter(item => !item.selected);
                    }
                });
                this.items = this.items.filter(item => !item.selected && item.children && item.children?.length > 0);
            });
    }

    public selectionChanged(event: { selectedNodes: TreeNode[]; originalStructure: TreeNode[]; }) {
        this.selectedItems = event.originalStructure;
    }

    public messageChanged(message: string) {
        this.message = message;
    }

    private mapDbItems(items: DatabaseItem[]): TreeNode[] {
        const typeMap: { [type: string]: TreeNode } = {};

        items.forEach((item) => {
            const { name, type } = item;

            if (!typeMap[type]) {
                typeMap[type] = {
                    name: this.getSectionName(type),
                    children: [],
                };
            }
            typeMap[type].children?.push({
                name,
                selected: false,
            });
        });
        const tree = [] as TreeNode[];

        Object.values(typeMap).forEach(x => {
            tree.push(x);
        });

        return tree;
    }

    private getSectionName(type: DatabaseItemType): string {
        switch (type) {
            case DatabaseItemType.Function:
                return ItemCategory.Function;
            case DatabaseItemType.StoredProcedure:
                return ItemCategory.StoredProcedure;
            case DatabaseItemType.Table:
                return ItemCategory.Table;
            case DatabaseItemType.View:
                return ItemCategory.View;
            case DatabaseItemType.Constraint:
                return ItemCategory.Constraint;
            case DatabaseItemType.Type:
                return ItemCategory.Type;
            default:
                return 'Unknown category';
        }
    }
}
