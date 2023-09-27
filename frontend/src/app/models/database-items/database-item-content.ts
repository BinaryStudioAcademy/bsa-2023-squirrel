import { DatabaseItemType } from './database-item-type';

export interface DatabaseItemContent<T> {
    name: string;
    type: DatabaseItemType;
    schema: string;
    content: T;
}
