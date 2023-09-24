import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { TreeNode } from './models/tree-node.model';

@Component({
    selector: 'app-tree',
    templateUrl: './tree.component.html',
    styleUrls: ['./tree.component.sass'],
})
export class TreeComponent implements OnInit {
    @Input() public asCheckList: boolean = false;

    @Input() public height: string = '100%';

    @Output() public selectionChange = new EventEmitter<{ selectedNodes: TreeNode[]; originalStructure: TreeNode[] }>();

    @Input() public treeData: TreeNode[] = [
        {
            name: 'Category 1',
            children: [{ name: 'Subcategory 1.1' }, { name: 'Subcategory 1.2' }],
        },
        {
            name: 'Category 2',
            children: [{ name: 'Subcategory 2.1' }, { name: 'Subcategory 2.2' }],
        },
    ];

    public filteredTreeData: TreeNode[] = [];

    public ngOnInit(): void {
        this.filteredTreeData = this.treeData;
    }

    public filterTree(event: Event): void {
        const searchTerm = (event.target as HTMLInputElement).value;

        if (!searchTerm) {
            this.filteredTreeData = this.treeData;

            return;
        }

        this.filteredTreeData = this.treeData
            .map((category) => ({
                name: category.name,
                children: category.children?.filter((subcategory) =>
                    subcategory.name.toLowerCase().includes(searchTerm.toLowerCase())),
            }))
            .filter((category) => category.children?.length && category.children?.length > 0);
    }

    public toggleSelect(node: TreeNode): void {
        if (node.children) {
            const allChildrenSelected = node.children.every((child) => child.selected);

            node.selected = !allChildrenSelected;

            node.children = node.children.map((child) => ({
                ...child,
                selected: node.selected,
            }));

            this.updateParentSelect(node);
        } else {
            node.selected = !node.selected;
            this.updateParentSelect(node);
        }

        const selectedNodes = this.getSelectedNodes(this.treeData);

        this.selectionChange.emit({ selectedNodes, originalStructure: this.treeData });
    }

    private getSelectedNodes(nodes: TreeNode[]): TreeNode[] {
        const selectedNodes = nodes
            .map((node) => {
                const selectedChildren = node.children ? this.getSelectedNodes(node.children) : [];

                return node.selected ? [node, ...selectedChildren] : selectedChildren;
            })
            .flat();

        return selectedNodes;
    }

    private updateParentSelect(node: TreeNode): void {
        const parent = this.getParentNode(node);

        if (parent) {
            const allChildrenSelected = parent.children?.every((child) => child.selected);

            parent.selected = allChildrenSelected;
            this.updateParentSelect(parent); // Recursively update parents
        }
    }

    private getParentNode(node: TreeNode): TreeNode | null {
        const parent = this.treeData.find((category) => category.children?.some((child) => child === node));

        return parent || null;
    }
}
