import { ReceipeDto } from '@/dtos/receipeDto';

export interface EmptyReceipeGeneratorInterface {
    generate(): ReceipeDto;
}