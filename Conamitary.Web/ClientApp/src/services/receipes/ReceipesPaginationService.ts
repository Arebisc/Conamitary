import { ReceipeDto } from './../../dtos/receipeDto';
import { injectable } from 'inversify';
import { ReceipesPaginationServiceInterface } from './../../abstract/receipes/ReceipesPaginationServiceInterface';

@injectable()
export class ReceipesPaginationService
    implements ReceipesPaginationServiceInterface {
    public getMaxPageNumber(
        elementsPerPage: number,
        elementsCount: number
    ): number {
        return Math.ceil(elementsCount / elementsPerPage);
    }
    public getPageNumberForReceipe(
        elementsPerPage: number,
        receipeId: string,
        receipesCollection: ReceipeDto[]
    ): number {
        const receipeIndex = receipesCollection.findIndex(
            x => x.id === receipeId
        );

        return Math.ceil((receipeIndex + 1) / elementsPerPage);
    }
}
