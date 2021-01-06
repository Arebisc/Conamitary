import { ReceipeDto } from '@/models/receipeDto';
import { injectable } from 'inversify';
import { EmptyReceipeGeneratorInterface } from '../../abstract/receipes/EmptyReceipeGeneratorInterface';

@injectable()
export class EmptyReceipeGenerator implements EmptyReceipeGeneratorInterface {
    public generate(): ReceipeDto {
        return {
            id: undefined,
            title: undefined,
            ingredients: undefined,
            instructions: undefined
        };
    }

}