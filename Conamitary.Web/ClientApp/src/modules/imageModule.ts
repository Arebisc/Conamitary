import { AddImagesToReceipeModelConverterInterface } from '@/abstract/images/AddImagesToReceipeModelConverterInterface';
import { AddImagesToReceipeModelConverter } from '@/services/images/AddImagesToReceipeModelConverter';
import { ContainerModule, interfaces } from 'inversify';

export const ImageModule = new ContainerModule((bind: interfaces.Bind) => {
    bind(nameof<AddImagesToReceipeModelConverterInterface>()).to(
        AddImagesToReceipeModelConverter
    );
});
