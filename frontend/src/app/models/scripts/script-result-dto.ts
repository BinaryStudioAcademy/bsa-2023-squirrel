export interface ScriptResultDto {
    columnNames: string[];
    rows: string[][];
    rowCount: number;
    columnCount: number;
    date: Date | undefined;
}
