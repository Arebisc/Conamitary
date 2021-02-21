import { AddReceipeModel } from '@/models/addReceipeModel';
import { injectable } from 'inversify';
import { AddReceipeModelConverterInterface } from '../../abstract/receipes/AddReceipeModelConverterInterface';

@injectable()
export class AddReceipeModelConverter
    implements AddReceipeModelConverterInterface {
    public toFormData(model: AddReceipeModel): FormData {
        const result = new FormData();

        result.append('title', model.title as string);
        result.append('ingredients', model.ingredients as string);
        result.append('instructions', model.instructions as string);

        model.images.forEach(image => {
            result.append('images', image);
        });

        return result;
    }
}
