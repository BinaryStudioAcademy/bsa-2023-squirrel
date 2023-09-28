import { LineDifferenceDto } from './line-difference-dto';

export interface InLineDiffResultDto {
    diffLinesResults: LineDifferenceDto[];
    hasDifferences: boolean;
}
