import { DbEngine } from '../projects/db-engine';

export interface ApplyChangesDto {
    clientId: string
    dbEngine: DbEngine
}
