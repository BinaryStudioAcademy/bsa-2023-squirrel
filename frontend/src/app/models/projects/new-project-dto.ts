import { DbEngine } from './db-engine';

export interface NewProjectDto {
    name: string;
    engine: DbEngine;
}
