import { ChangeTypeEnum } from './change-type-enum';

export interface LineDifferenceDto {
    type: ChangeTypeEnum;
    position: number;
    text: string;
}
