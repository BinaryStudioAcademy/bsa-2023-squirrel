export interface TableColumnInfo {
    ColumnName: string;
    ColumnOrder: number;
    DataType: string;
    IsUserDefined: boolean;
    Default: string;
    Precision: number;
    Scale: number;
    MaxLength: number | null;
    IsAllowNulls: boolean;
    IsIdentity: boolean;
    IsPrimaryKey: boolean;
    IsForeignKey: boolean;
    RelatedTable: string;
    RelatedTableColumn: string;
    RelatedTableSchema: string;
    Description: string;
}
