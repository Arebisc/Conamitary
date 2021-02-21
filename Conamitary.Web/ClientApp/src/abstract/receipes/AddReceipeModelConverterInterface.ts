import { AddReceipeModel } from '../../models/addReceipeModel';
export interface AddReceipeModelConverterInterface {
    toFormData(model: AddReceipeModel): FormData;
}
