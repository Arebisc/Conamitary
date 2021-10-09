import { RemoveImageFromReceipeModel } from './../../models/removeImageFromReceipeModel';
import { ReceipeDto } from '../../dtos/receipeDto';
import { Store } from 'vuex';
import { VuexModule, Module, Action, Mutation } from 'vuex-class-modules';
import axios from 'axios';
import { AddReceipeModel } from '@/models/addReceipeModel';
import { AddImageToReceipeDto } from '@/dtos/addImagesToReceipeDto';

@Module
export class ReceipeModule extends VuexModule {
    private readonly receipeUrl = 'api/receipes';

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public constructor(store: Store<any>) {
        super({
            name: nameof<ReceipeModule>(),
            store,
        });
    }

    private receipes: ReceipeDto[] = [];

    public get receipeGetter() {
        return (id: string) => {
            if (!id) {
                return null;
            }
            return this.receipes.find(x => x.id === id);
        };
    }

    public get receipesGetter() {
        return this.receipes;
    }

    @Action
    public async loadReceipes() {
        const response = await axios.get<ReceipeDto[]>(this.receipeUrl);

        if (response.status === 200) {
            this.setReceipes(response.data);
        } else {
            console.error('Cannot load receipts');
        }
    }

    @Action
    public async addReceipe(receipe: AddReceipeModel) {
        const response = await axios.post<ReceipeDto>(this.receipeUrl, receipe);

        if (response.status !== 200) {
            console.error('Cannot save receipt');
            return;
        }

        this.addReceipeToStore(response.data);
        return response.data;
    }

    @Action
    public async editReceipe(receipe: ReceipeDto) {
        const url = `${this.receipeUrl}/${receipe.id}`;
        const response = await axios.put<ReceipeDto>(url, receipe);

        if (response.status === 200) {
            this.changeReceipeInStore(response.data);
        } else {
            console.error('Cannot save receipt');
        }
    }

    @Action
    public async deleteReceipe(receipeId: string) {
        const url = `${this.receipeUrl}/${receipeId}`;
        const response = await axios.delete<ReceipeDto>(url);

        if (response.status === 200) {
            this.deleteReceipeInStore(response.data.id as string);
        } else {
            console.error('Cannot delete receipt');
        }
    }

    @Mutation
    private setReceipes(receipes: ReceipeDto[]) {
        this.receipes = receipes;
    }

    @Mutation
    private addReceipeToStore(receipe: ReceipeDto) {
        this.receipes.push(receipe);
    }

    @Mutation
    private changeReceipeInStore(receipe: ReceipeDto) {
        const receipeInStore = this.receipes.find(x => x.id === receipe.id);
        Object.assign(receipeInStore, receipe);
    }

    @Mutation
    private deleteReceipeInStore(receipeId: string) {
        const receipeIndex = this.receipes.findIndex(x => x.id === receipeId);
        this.receipes.splice(receipeIndex, 1);
    }

    @Mutation
    public removeImageFromReceipe(removeModel: RemoveImageFromReceipeModel) {
        const receipe = this.receipes.find(x => x.id === removeModel.receipeId);
        const fileIndex = receipe?.imagesIds?.findIndex(
            x => x === removeModel.imageId
        );
        if (fileIndex !== undefined && fileIndex > -1) {
            receipe?.imagesIds?.splice(fileIndex, 1);
        }
    }

    @Mutation
    public insertImageToReceipe(addModel: AddImageToReceipeDto) {
        const receipe = this.receipes.find(x => x.id === addModel.receipeId);
        receipe?.imagesIds?.push(addModel.imageId);
    }
}
