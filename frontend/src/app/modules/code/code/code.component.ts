import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { BranchService } from '@core/services/branch.service';
import { CommitService } from '@core/services/commit.service';
import { CommitChangesService } from '@core/services/commit-changes.service';
import { EventService } from '@core/services/event.service';
import { ProjectService } from '@core/services/project.service';
import { SpinnerService } from '@core/services/spinner.service';
import { TreeNode } from '@shared/components/tree/models/TreeNode.model';
import { takeUntil } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { DatabaseItemType } from 'src/app/models/database-items/database-item-type';
import { DatabaseItemTypeName } from 'src/app/models/database-items/database-item-type-name';
import { ItemCategory } from 'src/app/models/database-items/item-category';
import { TableColumnInfo } from 'src/app/models/table-structure/table-columns';
import { TableStructureDto } from 'src/app/models/table-structure/table-structure-dto';
import { LineDifferenceDto } from 'src/app/models/text-pair/line-difference-dto';

import { DatabaseItemContentCompare } from '../../../models/database-items/database-item-content-compare';

@Component({
    selector: 'app-changes',
    templateUrl: './code.component.html',
    styleUrls: ['./code.component.sass'],
})
export class CodeComponent extends BaseComponent implements OnInit, OnDestroy {
    public allContentChanges: DatabaseItemContentCompare[] = [];

    public form: FormGroup;

    public DatabaseItemTypeName = DatabaseItemTypeName;

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
        this.eventService.changesLoadedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.items = this.mapDbItems(x);
            }
        });
        eventService.changesSavedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.currentChangesGuid = x;
            }
        });
    }

    public ngOnInit(): void {
        this.currentProjectId = this.projectService.currentProjectId;
        this.commitChangesService.contentChanges$.pipe(takeUntil(this.unsubscribe$)).subscribe((changes) => {
            this.allContentChanges = changes;
        });
    }

    public formatContent(itemType: DatabaseItemType, contentLines: LineDifferenceDto[]): string {
        const contentText = contentLines.map((line) => line.text).join('\n');

        switch (itemType) {
            case DatabaseItemType.Table: {
                const table: TableStructureDto = JSON.parse(contentText);

                return this.formatTable(table);
            }

            case DatabaseItemType.Constraint:
                return this.formatConstraint(JSON.parse(contentText));
            case DatabaseItemType.Type:
            case DatabaseItemType.Function:
            case DatabaseItemType.StoredProcedure:
            case DatabaseItemType.View:
                return this.formatDefinition(JSON.parse(contentText));
            default:
                return this.cleanUpText(contentText);
        }
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

    private cleanUpText(text: string): string {
        return text.replace(/\\"/g, '"').replace(/\\n/g, '\n');
    }

    private formatTable(table: TableStructureDto): string {
        return table.Columns.map(this.formatColumn).join('\n\n');
    }

    private formatColumn(column: TableColumnInfo): string {
        const columnDetails: string[] = [];

        columnDetails.push(`Column Name: ${column.ColumnName}`);
        columnDetails.push(`Column Order: ${column.ColumnOrder}`);
        columnDetails.push(`Data Type: ${column.DataType}`);
        columnDetails.push(`User Defined: ${column.IsUserDefined}`);
        columnDetails.push(`Default: ${column.Default}`);
        columnDetails.push(`Precision: ${column.Precision}`);
        columnDetails.push(`Scale: ${column.Scale}`);
        columnDetails.push(`Max Length: ${column.MaxLength}`);
        columnDetails.push(`Allow Nulls: ${column.IsAllowNulls}`);
        columnDetails.push(`Identity: ${column.IsIdentity}`);
        columnDetails.push(`Primary Key: ${column.IsPrimaryKey}`);
        columnDetails.push(`Foreign Key: ${column.IsForeignKey}`);
        columnDetails.push(`Related Table Schema: ${column.RelatedTableSchema}`);
        columnDetails.push(`Related Table: ${column.RelatedTable}`);
        columnDetails.push(`Related Table Column: ${column.RelatedTableColumn}`);
        columnDetails.push(`Description: ${column.Description}`);

        return columnDetails.join('\n');
    }

    private formatConstraint(content: any): string {
        return `ConstraintName: ${content.ConstraintName}\nColumns: ${content.Columns}\nCheckClause: ${content.CheckClause}`;
    }

    private formatDefinition(content: any): string {
        return content.Definition;
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
}
