import { AddImagesToReceipeModel } from '../../models/addImagesToReceipeModel';
import { RemoveImageFromReceipeModel } from './../../models/removeImageFromReceipeModel';
import { Store } from 'vuex';
import { VuexModule, Module, Action, Mutation } from 'vuex-class-modules';
import axios from 'axios';
import { ReceipeModule } from './receipeModule';
import { $inject } from '@vanroeybe/vue-inversify-plugin';
import { AddImagesToReceipeModelConverterInterface } from '@/abstract/images/AddImagesToReceipeModelConverterInterface';

@Module
export class ImagesModule extends VuexModule {
    private readonly imagesUrl = 'api/images';

    // eslint-disable-next-line no-undef
    @$inject(nameof<AddImagesToReceipeModelConverterInterface>())
    private modelConverter!: AddImagesToReceipeModelConverterInterface;

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public constructor(
        store: Store<any>,
        private readonly receipesModule: ReceipeModule
    ) {
        super({
            name: nameof<ImagesModule>(),
            store,
        });
    }

    public get url() {
        return this.imagesUrl;
    }

    public get imageUrl() {
        return (imageId: string | undefined) => {
            if (!imageId || imageId === undefined) {
                return require('@/assets/no-image.png');
            }
            return `${this.imagesUrl}/${imageId}`;
        };
    }

    @Action
    public async removeImageFromReceipe(
        removeModel: RemoveImageFromReceipeModel
    ) {
        const url = `${this.imagesUrl}/${removeModel.imageId}/${removeModel.receipeId}`;
        const response = await axios.delete(url);

        if (response.status === 200) {
            this.receipesModule.removeImageFromReceipe(removeModel);
        } else {
            console.error('Cannot delete image from receipe');
        }
    }

    @Action
    public async addImagesToReceipe(
        addImagesToReceipeModel: AddImagesToReceipeModel
    ) {
        const formData = this.modelConverter.toFormData(
            addImagesToReceipeModel
        );

        const response = await axios.post(this.imagesUrl, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
        if (response.status === 200) {
            return;
        } else {
            console.error('Cannot add images to receipe.');
        }
    }
}
