import { DbEngine } from './db-engine';

export interface ProjectDto {
    name: string;
    engine: DbEngine;
    description: string;
    date: Date;
}
