import { ReceipeDto } from './../../dtos/receipeDto';
export interface ReceipesPaginationServiceInterface {
    getMaxPageNumber(elementsPerPage: number, elementsCount: number): number;
    getPageNumberForReceipe(
        elementsPerPage: number,
        receipeId: string,
        receipesCollection: ReceipeDto[]
    ): number;
}
