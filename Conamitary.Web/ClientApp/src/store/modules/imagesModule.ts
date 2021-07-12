import { Store } from 'vuex';
import { VuexModule, Module } from 'vuex-class-modules';

@Module
export class ImagesModule extends VuexModule {
    private readonly imagesUrl = 'api/images';

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public constructor(store: Store<any>) {
        super({
            name: nameof<ImagesModule>(),
            store,
        });
    }

    public get url() {
        return this.imagesUrl;
    }

    public get imageUrl() {
        return (imageId: string) => {
            if (!imageId) {
                return require('@/assets/no-image.png');
            }
            return `${this.imagesUrl}/${imageId}`;
        };
    }
}
