import { TreeNode } from '@shared/components/tree/models/tree-node.model';

export interface CreateCommitDto {
    message: string;
    branchId: number;
    changesGuid: string;
    preScript: string;
    postScript: string;
    selectedItems: TreeNode[];
}
