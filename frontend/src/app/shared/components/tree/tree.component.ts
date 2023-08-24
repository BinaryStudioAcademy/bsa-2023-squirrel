import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeModule } from '@angular/material/tree';

import { ChecklistDatabase } from './auxiliary/ChecklistDatabase';
import { TreeDataObject } from './auxiliary/TreeDataObject';
import { TreeItemFlatNode } from './auxiliary/TreeItemFlatNode';
import { TreeItemNode } from './auxiliary/TreeItemNode';

@Component({
    selector: 'app-tree',
    templateUrl: 'tree.component.html',
    styleUrls: ['tree.component.sass'],
    providers: [ChecklistDatabase],
    standalone: true,
    imports: [
        MatTreeModule,
        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        CommonModule,
    ],
})
export class TreeComponent implements OnInit {
    @Input() asCheckList: boolean = false;

    @Input() treeData: TreeDataObject[];

    @Input() minHeightValue: number = 25;

    @Output() selectionChanged = new EventEmitter<string[]>();

    flatNodeMap = new Map<TreeItemFlatNode, TreeItemNode>();

    /** Map from nested node to flattened node. This helps us to keep the same object for selection */
    nestedNodeMap = new Map<TreeItemNode, TreeItemFlatNode>();

    treeControl: FlatTreeControl<TreeItemFlatNode>;

    treeFlattener: MatTreeFlattener<TreeItemNode, TreeItemFlatNode>;

    dataSource: MatTreeFlatDataSource<TreeItemNode, TreeItemFlatNode>;

    /** The selection for checklist */
    checklistSelection = new SelectionModel<TreeItemFlatNode>(true /* multiple */);

    constructor(private database: ChecklistDatabase) {
        this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel, this.isExpandable, this.getChildren);
        this.treeControl = new FlatTreeControl<TreeItemFlatNode>(this.getLevel, this.isExpandable);
        this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    }

    ngOnInit(): void {
        this.database.setData(this.treeData);
        this.database.dataChange.subscribe((data) => {
            this.dataSource.data = data;
        });

        this.checklistSelection.changed.subscribe((s) => {
            this.selectionChanged.emit(s.source.selected.filter((n) => n.level > 0).map((n) => n.item));
        });
    }

    getLevel = (node: TreeItemFlatNode) => node.level;

    isExpandable = (node: TreeItemFlatNode) => node.expandable;

    getChildren = (node: TreeItemNode): TreeItemNode[] => node.children;

    hasChild = (_: number, _nodeData: TreeItemFlatNode) => _nodeData.expandable;

    /**
     * Transformer to convert nested node to flat node. Record the nodes in maps for later use.
     */
    transformer = (node: TreeItemNode, level: number) => {
        const existingNode = this.nestedNodeMap.get(node);
        const flatNode = existingNode && existingNode.item === node.item ? existingNode : new TreeItemFlatNode();

        flatNode.item = node.item;
        flatNode.level = level;
        flatNode.code = node.code;
        flatNode.expandable = node.children && node.children.length > 0;
        this.flatNodeMap.set(flatNode, node);
        this.nestedNodeMap.set(node, flatNode);

        return flatNode;
    };

    /** Whether all the descendants of the node are selected */
    descendantsAllSelected(node: TreeItemFlatNode): boolean {
        const descendants = this.treeControl.getDescendants(node);

        return descendants.every((child) => this.checklistSelection.isSelected(child));
    }

    /** Whether part of the descendants are selected */
    descendantsPartiallySelected(node: TreeItemFlatNode): boolean {
        const descendants = this.treeControl.getDescendants(node);
        const result = descendants.some((child) => this.checklistSelection.isSelected(child));

        return result && !this.descendantsAllSelected(node);
    }

    /** Toggle the to-do item selection. Select/deselect all the descendants node */
    itemSelectionToggle(node: TreeItemFlatNode): void {
        this.checklistSelection.toggle(node);
        const descendants = this.treeControl.getDescendants(node);

        if (this.checklistSelection.isSelected(node)) {
            this.checklistSelection.select(...descendants);
        } else {
            this.checklistSelection.deselect(...descendants);
        }
    }

    onFilterChange(event: Event) {
        const filterText = (event.target as HTMLInputElement).value;

        this.database.filter(filterText);
        if (filterText) {
            this.treeControl.expandAll();
        } else {
            this.treeControl.collapseAll();
        }
    }

    getChildNodeStyles(): any {
        return {
            'min-height.px': this.minHeightValue,
        };
    }

    getParentNodeStyles() {
        return {
            'font-weight': 600,
            'font-size.px': 16,
        };
    }
}
