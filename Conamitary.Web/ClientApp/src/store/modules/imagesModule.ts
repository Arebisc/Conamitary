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
    private imagesEndpointUrl: string | null = null;
    private readonly configurationUrl = 'api/configuration/fileApiUrl';

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

    public get baseUrl() {
        return `${this.imagesEndpointUrl}/api/images`;
    }

    public get imageUrl() {
        return (imageId: string | undefined) => {
            if (!imageId || imageId === undefined) {
                return require('@/assets/no-image.png');
            }
            return `${this.baseUrl}/${imageId}`;
        };
    }

    @Action
    public async initializeModule() {
        const result = await axios.get(this.configurationUrl);

        if (result.status === 200) {
            this.setFileEndpointUrl(result.data);
        } else {
            throw new Error('Cannot initialize images module!');
        }
    }

    @Action
    public async removeImageFromReceipe(
        removeModel: RemoveImageFromReceipeModel
    ) {
        const url = `${this.baseUrl}/${removeModel.imageId}/${removeModel.receipeId}`;
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

        const response = await axios.post(this.baseUrl, formData, {
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

    @Mutation
    private setFileEndpointUrl(endpointUrl: string) {
        this.imagesEndpointUrl = endpointUrl;
    }
}
