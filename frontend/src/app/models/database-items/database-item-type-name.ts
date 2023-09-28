import { DatabaseItemType } from './database-item-type';

export const DatabaseItemTypeName = {
    [DatabaseItemType.Table]: 'Table',
    [DatabaseItemType.Type]: 'Type',
    [DatabaseItemType.Constraint]: 'Constraint',
    [DatabaseItemType.StoredProcedure]: 'StoredProcedure',
    [DatabaseItemType.Function]: 'Function',
    [DatabaseItemType.View]: 'View',
};
