import { InLineDiffResultDto } from '../text-pair/inline-diff-result-dto';
import { TextPairDifferenceDto } from '../text-pair/text-pair-difference-dto';

import { DatabaseItemType } from './database-item-type';

export interface DatabaseItemContentCompare {
    schemaName: string;
    itemName: string;
    itemType: DatabaseItemType;
    sideBySideDiff: TextPairDifferenceDto;
    inLineDiff: InLineDiffResultDto;
}
