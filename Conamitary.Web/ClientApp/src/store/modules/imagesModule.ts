import { AddImagesToReceipeModel } from '../../models/addImagesToReceipeModel';
import { RemoveImageFromReceipeModel } from './../../models/removeImageFromReceipeModel';
import { Store } from 'vuex';
import { VuexModule, Module, Action, Mutation } from 'vuex-class-modules';
import axios from 'axios';
import { ReceipeModule } from './receipeModule';

@Module
export class ImagesModule extends VuexModule {
    private imagesEndpointUrl: string | null = null;
    private readonly configurationEndpointUrl = '/api/configuration/fileApiUrl';

    public constructor(
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
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
        const result = await axios.get(this.configurationEndpointUrl);

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
        const url = `${this.baseUrl}/${removeModel.receipeId}/${removeModel.imageId}`;
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
        for (const image of addImagesToReceipeModel.images) {
            const formData = new FormData();
            formData.append('image', image);
            formData.append('receipeId', addImagesToReceipeModel.receipeId);

            const response = await axios.post<string>(this.baseUrl, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            if (response.status === 200) {
                this.receipesModule.insertImageToReceipe({
                    receipeId: addImagesToReceipeModel.receipeId,
                    imageId: response.data,
                });
            } else {
                console.error('Cannot add images to receipe.');
            }
        }
    }

    @Mutation
    private setFileEndpointUrl(endpointUrl: string) {
        this.imagesEndpointUrl = endpointUrl;
    }
}
