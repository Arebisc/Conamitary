import { ReceipeDto } from '../../models/receipeDto';

export interface EmptyReceipeGeneratorInterface {
    generate(): ReceipeDto;
}