import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BranchService } from '@core/services/branch.service';
import { CommitService } from '@core/services/commit.service';
import { EventService } from '@core/services/event.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { TablesService } from '@core/services/tables.service';
import { TreeNode } from '@shared/components/tree/models/TreeNode.model';
import { Subject, takeUntil } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { DatabaseItemContent } from 'src/app/models/database-items/database-item-content';
import { DatabaseItemType } from 'src/app/models/database-items/database-item-type';
import { ItemCategory } from 'src/app/models/database-items/item-category';
import { QueryParameters } from 'src/app/models/sql-service/query-parameters';

@Component({
    selector: 'app-code',
    templateUrl: './code.component.html',
    styleUrls: ['./code.component.sass'],
})
export class CodeComponent implements OnInit, OnDestroy {
    public selectedItems: TreeNode[] = [];

    public selectedItem: DatabaseItemContent | undefined;

    public form: FormGroup;

    public guid: string;

    public items: TreeNode[];

    private unsubscribe$ = new Subject<void>();

    private currentProjectId: number;

    constructor(
        private eventService: EventService,
        private branchService: BranchService,
        private commitService: CommitService,
        private projectService: ProjectService,
        private spinner: SpinnerService,
        private formBuilder: FormBuilder,
        private tableService: TablesService,
    ) {
        this.eventService.changesLoadedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.items = this.mapDbItems(x);
            }
        });
        eventService.changesSavedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.guid = x;
            }
        });
    }

    public ngOnInit(): void {
        this.currentProjectId = this.projectService.currentProjectId;
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }

    public selectionChanged(event: { selectedNodes: TreeNode[]; originalStructure: TreeNode[] }) {
        this.selectedItems = event.originalStructure;
    }

    public onItemSelected(item: DatabaseItemContent): void {
        if (`${item.schema}.${item.name}` === `${this.selectedItem?.schema}.${this.selectedItem?.name}`) {
            return;
        }

        this.selectItem(item);
    }

    private updateEditorContent(content: string): void {
        this.form.patchValue({
            scriptContent: content,
        });
    }

    private selectItem(item: DatabaseItemContent): void {
        this.selectedItem = item;
        this.updateEditorContent(this.selectedItem.content);
        this.form.markAsPristine();
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

    public selectTable(selectedItem: DatabaseItemContent) {
        const query: QueryParameters = {
            clientId: this.guid,
            filterSchema: selectedItem.schema,
            filterName: selectedItem.name,
            filterRowsCount: 100,
        };

        this.tableService
            .getAllTablesNames(query)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (item) => {
                    this.selectItem = item;
                },
                error: () => {
                    this.notificationService.error('fail connect to db');
                },
            });
    }

    private initializeForm(): void {
        this.form = this.formBuilder.group({
            selectedItem: [this.selectedItem?.content],
        });
    }
}
