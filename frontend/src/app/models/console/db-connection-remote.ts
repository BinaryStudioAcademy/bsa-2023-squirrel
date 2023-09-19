import { DbConnection } from './db-connection';

export interface DbConnectionRemote {
    dbConnection: DbConnection;
    clientId: string;
}
