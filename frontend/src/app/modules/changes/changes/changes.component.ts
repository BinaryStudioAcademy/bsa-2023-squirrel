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
import { DatabaseItemType } from 'src/app/models/database-items/database-item-type';
import { ItemCategory } from 'src/app/models/database-items/item-category';

import { DatabaseItemContentCompare } from '../../../models/database-items/database-item-content-compare';

@Component({
    selector: 'app-changes',
    templateUrl: './changes.component.html',
    styleUrls: ['./changes.component.sass'],
})
export class ChangesComponent extends BaseComponent implements OnInit, OnDestroy {
    public allContentChanges: DatabaseItemContentCompare[] = [];

    public selectedContentChanges: DatabaseItemContentCompare[] = [];

    public currentChangesGuid: string;

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
        eventService.changesSavedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.currentChangesGuid = x;
            }
        });
    }

    public ngOnInit(): void {
        this.currentProjectId = this.projectService.currentProjectId;
        this.commitChangesService.contentChanges$.pipe(takeUntil(this.unsubscribe$)).subscribe((changes) => {
            this.allContentChanges = changes.filter(
                (x) => x.sideBySideDiff.hasDifferences || x.inLineDiff.hasDifferences,
            );
            this.items = this.mapDbItems(this.allContentChanges);
        });
    }

    public validateCommit() {
        if (!this.currentChangesGuid) {
            return false;
        }
        if (!(this.message.length > 0 && this.message.length <= 300)) {
            return false;
        }
        if (!this.selectedItems.some((x) => x.children?.some((y) => y.selected))) {
            return false;
        }

        return true;
    }

    public commit() {
        this.spinner.show();

        const branchId = this.branchService.getCurrentBranch(this.currentProjectId);
        const commit = {
            branchId,
            postScript: '',
            preScript: '',
            selectedItems: this.selectedItems,
            changesGuid: this.currentChangesGuid,
            message: this.message,
        } as CreateCommitDto;

        this.commitService
            .commit(commit)
            .pipe(takeUntil(this.unsubscribe$), finalize(this.spinner.hide))
            .subscribe(() => {
                this.items.forEach((parent) => {
                    if (parent.children) {
                        parent.children = parent.children.filter((item) => !item.selected);
                    }
                });
                this.items = this.items.filter((item) => !item.selected && item.children && item.children?.length > 0);
            });
    }

    public selectionChanged(event: { selectedNodes: TreeNode[]; originalStructure: TreeNode[] }) {
        this.addSelectedChanges(event.selectedNodes);
        this.selectedItems = event.originalStructure;
    }

    public addSelectedChanges(selectedChanges: TreeNode[]) {
        this.selectedContentChanges = [];
        for (let i = 0; i < selectedChanges.length; i++) {
            const selectedName = selectedChanges[i].name;
            const matchingContentChange = this.allContentChanges.find(
                (contentChange) => contentChange.itemName === selectedName,
            );

            if (matchingContentChange) {
                this.selectedContentChanges.push(matchingContentChange);
            }
        }
    }

    public messageChanged(message: string) {
        this.message = message;
    }

    private mapDbItems(items: DatabaseItemContentCompare[]): TreeNode[] {
        const typeMap: { [type: string]: TreeNode } = {};

        items
            .filter((x) => x.inLineDiff || x.sideBySideDiff)
            .forEach((item) => {
                const { itemName, itemType } = item;

                if (!typeMap[itemType]) {
                    typeMap[itemType] = {
                        name: this.getSectionName(itemType),
                        children: [],
                    };
                }
                typeMap[itemType].children?.push({
                    name: itemName,
                    selected: false,
                });
            });
        const tree = [] as TreeNode[];

        Object.values(typeMap).forEach((x) => {
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
