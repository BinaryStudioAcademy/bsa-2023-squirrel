import { LineDifferenceDto } from './line-difference-dto';

export interface TextPairDifferenceDto {
    oldTextLines: LineDifferenceDto[];
    newTextLines: LineDifferenceDto[];
    hasDifferences: boolean;
}
