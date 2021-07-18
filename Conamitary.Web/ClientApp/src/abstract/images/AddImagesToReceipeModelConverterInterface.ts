import { AddImagesToReceipeModel } from '@/models/addImagesToReceipeModel';

export interface AddImagesToReceipeModelConverterInterface {
    toFormData(addImagesToReceipeModel: AddImagesToReceipeModel): FormData;
}
