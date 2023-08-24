import { ErrorDetailsDto } from '../error/error-details-dto';

export interface WebApiResponse<T> {
    success: boolean;
    data: T;
    error: ErrorDetailsDto | null;
}
