import { AddImagesToReceipeModel } from '@/models/addImagesToReceipeModel';
import { injectable } from 'inversify';
import { AddImagesToReceipeModelConverterInterface } from '../../abstract/images/AddImagesToReceipeModelConverterInterface';

@injectable()
export class AddImagesToReceipeModelConverter
    implements AddImagesToReceipeModelConverterInterface {
    public toFormData(model: AddImagesToReceipeModel): FormData {
        const result = new FormData();

        result.append('receipeId', model.receipeId);
        model.images.forEach(image => {
            result.append('images', image);
        });

        return result;
    }
}
