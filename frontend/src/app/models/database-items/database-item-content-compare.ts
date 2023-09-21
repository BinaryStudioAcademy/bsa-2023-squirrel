import { InLineDiffResultDto } from '../text-pair/inline-diff-result-dto';
import { TextPairDifferenceDto } from '../text-pair/text-pair-difference-dto';

import { DatabaseItemType } from './database-item-type';

export interface DatabaseItemContentCompare {
    schemaName: string;
    itemName: string;
    type: DatabaseItemType;
    sideBySideDiffDto: TextPairDifferenceDto;
    inLineDiffResultDto: InLineDiffResultDto;
}
