import { TagDto } from '../tags/tag-dto';

import { DbEngine } from './db-engine';

export interface ProjectResponseDto {
    id: number;
    name: string;
    description: string | null;
    dbEngine: DbEngine;
    createdAt: Date;

    tags: TagDto[];
}
