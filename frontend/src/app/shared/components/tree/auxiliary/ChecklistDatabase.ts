/* eslint-disable @typescript-eslint/no-loop-func */
/* eslint-disable no-loop-func */
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { TreeDataObject } from './TreeDataObject';
import { TreeItemNode } from './TreeItemNode';

@Injectable()
export class ChecklistDatabase {
    dataChange = new BehaviorSubject<TreeItemNode[]>([]);

    treeData: TreeDataObject[];

    setData(treeData: TreeDataObject[]) {
        this.treeData = treeData;

        this.initialize();
    }

    get data(): TreeItemNode[] {
        return this.dataChange.value;
    }

    initialize() {
        const data = this.buildFileTree(this.treeData, '0');

        this.dataChange.next(data);
    }

    buildFileTree(obj: TreeDataObject[], level: string): TreeItemNode[] {
        return obj
            .filter(
                (o) =>
                    (<string>o.code).startsWith(`${level}.`) &&
                    (o.code.match(/\./g) || []).length === (level.match(/\./g) || []).length + 1,
            )
            .map((o) => {
                const node = new TreeItemNode();

                node.item = o.text;
                node.code = o.code;
                const children = obj.filter((so) => (<string>so.code).startsWith(`${level}.`));

                if (children && children.length > 0) {
                    node.children = this.buildFileTree(children, o.code);
                }

                return node;
            });
    }

    public filter(filterText: string) {
        let filteredTreeData: TreeDataObject[];

        if (filterText) {
            filteredTreeData = this.treeData.filter(
                (d) => d.text.toLocaleLowerCase().indexOf(filterText.toLocaleLowerCase()) > -1,
            );
            Object.assign([], filteredTreeData).forEach((ftd: TreeDataObject) => {
                let str = <string>ftd.code;

                while (str.lastIndexOf('.') > -1) {
                    const index = str.lastIndexOf('.');

                    str = str.substring(0, index);
                    if (filteredTreeData.findIndex((t) => t.code === str) === -1) {
                        const obj = this.treeData.find((d) => d.code === str);

                        if (obj) {
                            filteredTreeData.push(obj);
                        }
                    }
                }
            });
        } else {
            filteredTreeData = this.treeData;
        }

        const data = this.buildFileTree(filteredTreeData, '0');

        this.dataChange.next(data);
    }
}
