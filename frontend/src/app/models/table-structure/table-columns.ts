export interface TableColumnInfo {
    columnName: string;
    columnOrder?: number;
    dataType: string;
    isUserDefined?: boolean;
    default?: string;
    precision?: number;
    scale?: number;
    maxLength?: number;
    isAllowNulls?: boolean;
    isIdentity?: boolean;
    isPrimaryKey?: boolean;
    isForeignKey?: boolean;
    relatedTable?: string;
    relatedTableColumn?: string;
    relatedTableSchema?: string;
    description?: string;
}
