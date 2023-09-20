import { DbEngine } from '../projects/db-engine';

export interface RunScriptDto {
    content: string;
    projectId: number;
    dbEngine: DbEngine;
}
