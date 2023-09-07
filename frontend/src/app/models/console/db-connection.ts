import { DbEngine } from '../projects/db-engine';

export interface DbConnection {
    dbName: string
    serverName: string
    port: number
    username: string
    password: string
    dbEngine: DbEngine
    isLocalhost: boolean
}
