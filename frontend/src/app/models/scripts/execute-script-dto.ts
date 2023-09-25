import { DbEngine } from '../projects/db-engine';

export interface ExecuteScriptDto {
    content: string;
    projectId: number;
    dbEngine: DbEngine;
    clientId: string | null;
}
