import { ErrorType } from './error-type';

export interface ErrorDetailsDto {
    message: string;
    errorType: ErrorType;
}
