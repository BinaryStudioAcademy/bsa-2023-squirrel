import { DbConnection } from './db-connection';

export interface DbConnectionRemote extends DbConnection {
    guid: string;
}
