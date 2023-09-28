export enum DatabaseItemType {
    Table,
    Type,
    Constraint,
    StoredProcedure,
    Function,
    View,
}

export const DatabaseItemTypeName = {
    [DatabaseItemType.Table]: 'Table',
    [DatabaseItemType.Type]: 'Type',
    [DatabaseItemType.Constraint]: 'Constraint',
    [DatabaseItemType.StoredProcedure]: 'StoredProcedure',
    [DatabaseItemType.Function]: 'Function',
    [DatabaseItemType.View]: 'View',
};
