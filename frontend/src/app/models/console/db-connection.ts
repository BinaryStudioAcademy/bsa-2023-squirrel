import { DbEngine } from '../projects/db-engine';

export interface DbConnection {
    serverName: string
    port: number
    username: string
    password: string
    dbEngine: DbEngine
}
