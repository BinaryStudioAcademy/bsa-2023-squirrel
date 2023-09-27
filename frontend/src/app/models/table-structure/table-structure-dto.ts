import { BaseDbItem } from '../database-items/base-db-item';

import { TableColumnInfo } from './table-columns';

export interface TableStructureDto extends BaseDbItem {
    columns: TableColumnInfo[];
}
