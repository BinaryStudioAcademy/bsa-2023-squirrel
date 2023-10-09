/* eslint-disable function-paren-newline */
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

import { TreeNode } from '../tree/models/tree-node.model';

@Component({
    selector: 'app-tree-one',
    templateUrl: './tree-one.component.html',
    styleUrls: ['./tree-one.component.sass'],
})
export class TreeOneComponent implements OnInit, OnChanges {
    @Input() height: string = '100%';

    @Output() selectionOne = new EventEmitter<TreeNode>();

    @Input() treeData: TreeNode[] = [];

    public filteredTreeData: TreeNode[] = [];

    private currentlySelectedNode: TreeNode | null = null;

    ngOnInit(): void {
        this.filteredTreeData = this.treeData;
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['treeData']) {
            this.filteredTreeData = this.treeData;
        }
    }

    public filterTree(event: Event): void {
        const searchTerm = (event.target as HTMLInputElement).value;

        if (!searchTerm) {
            this.filteredTreeData = this.treeData;

            return;
        }

        this.filteredTreeData = this.treeData
            .map((category) => ({
                ...category,
                children: category.children?.filter(
                    (subcategory) => subcategory.name.toLowerCase().includes(searchTerm.toLowerCase()),
                    // eslint-disable-next-line function-paren-newline
                ),
            }))
            .filter((category) => category.children?.length && category.children?.length > 0);
    }

    public toggleSelect(node: TreeNode): void {
        if (!node.children) {
            // Allow only child nodes to be selected
            if (this.currentlySelectedNode) {
                this.currentlySelectedNode.selected = false;
            }
            node.selected = true;
            this.currentlySelectedNode = node;

            this.selectionOne.emit(node);
        }
    }
}
