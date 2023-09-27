import { DatabaseItemType } from './database-item-type';

export interface DatabaseItemContent {
    name: string;
    type: DatabaseItemType;
    schema: string;
    content: string;
}
