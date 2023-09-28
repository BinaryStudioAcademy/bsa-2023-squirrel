import { DatabaseItemType } from './database-item-type';

export interface DatabaseItemContent {
    schemaName: string;
    itemName: string;
    itemType: DatabaseItemType;
    content: string;
}
