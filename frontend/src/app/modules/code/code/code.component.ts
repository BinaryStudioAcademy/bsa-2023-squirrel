import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BaseComponent } from '@core/base/base.component';
import { CommitChangesService } from '@core/services/commit-changes.service';
import { EventService } from '@core/services/event.service';
import { TreeNode } from '@shared/components/tree/models/TreeNode.model';
import { takeUntil } from 'rxjs';

import { DatabaseItem } from 'src/app/models/database-items/database-item';
import { DatabaseItemType } from 'src/app/models/database-items/database-item-type';
import { DatabaseItemTypeName } from 'src/app/models/database-items/database-item-type-name';
import { ItemCategory } from 'src/app/models/database-items/item-category';
import { LineDifferenceDto } from 'src/app/models/text-pair/line-difference-dto';

import { DatabaseItemContentCompare } from '../../../models/database-items/database-item-content-compare';

@Component({
    selector: 'app-changes',
    templateUrl: './code.component.html',
    styleUrls: ['./code.component.sass'],
})
export class CodeComponent extends BaseComponent implements OnInit, OnDestroy {
    public allContentChanges: DatabaseItemContentCompare[] = [];

    public selectedContent: DatabaseItemContentCompare | null = null;

    public items: TreeNode[];

    public selectedItem: TreeNode | null = null;

    public form: FormGroup;

    public DatabaseItemTypeName = DatabaseItemTypeName;

    public currentChangesGuid: string;

    constructor(
        private eventService: EventService,
        private formBuilder: FormBuilder,
        private commitChangesService: CommitChangesService,
    ) {
        super();
    }

    public ngOnInit(): void {
        this.loadChanges();
        this.commitChangesService.contentChanges$.pipe(takeUntil(this.unsubscribe$)).subscribe((changes) => {
            this.allContentChanges = changes;
        });
        this.initializeForm();
    }

    public selectionChanged(selectedOne: TreeNode) {
        this.selectedItem = selectedOne;
        this.showSelectedContent();
    }

    private showSelectedContent(): void {
        if (this.selectedItem) {
            const matchingContentChange = this.allContentChanges.find(
                (contentChange) => contentChange.itemName === this.selectedItem?.name,
            );

            this.selectedContent = matchingContentChange || null;
        } else {
            this.selectedContent = null;
        }

        this.initEditorContent();
    }

    private initEditorContent(): void {
        if (this.selectedContent) {
            const content = this.formatContent(this.selectedContent.sideBySideDiff.newTextLines);

            this.form = this.formBuilder.group({
                scriptContent: [content],
            });
        } else {
            this.form = this.formBuilder.group({
                scriptContent: ['Selected Content not found'],
            });
        }
    }

    private formatContent(contentLines: LineDifferenceDto[]): string {
        return contentLines.map((line) => line.text).join('\n');
    }

    private initializeForm(): void {
        this.form = this.formBuilder.group({
            scriptContent: [''],
        });
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

    private loadChanges(): void {
        this.eventService.changesLoadedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.items = this.mapDbItems(x);
            }
        });
        this.eventService.changesSavedEvent$.pipe(takeUntil(this.unsubscribe$)).subscribe((x) => {
            if (x !== undefined) {
                this.currentChangesGuid = x;
            }
        });
    }
}
