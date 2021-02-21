import { AddReceipeModelConverterInterface } from '../../abstract/receipes/AddReceipeModelConverterInterface';
import { ReceipeDto } from '../../dtos/receipeDto';
import { Store } from 'vuex';
import { VuexModule, Module, Action, Mutation } from 'vuex-class-modules';
import axios from 'axios';
import { AddReceipeModel } from '@/models/addReceipeModel';
import { $inject } from '@vanroeybe/vue-inversify-plugin';

@Module
export class ReceipeModule extends VuexModule {
    private readonly receipeUrl = 'api/receipe';

    @$inject(nameof<AddReceipeModelConverterInterface>())
    private readonly addReceipeModelConverter!: AddReceipeModelConverterInterface;

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public constructor(store: Store<any>) {
        super({
            name: nameof<ReceipeModule>(),
            store,
        });
    }

    private receipes: ReceipeDto[] = [];

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
        const formData = this.addReceipeModelConverter.toFormData(receipe);
        const response = await axios.post<ReceipeDto>(
            this.receipeUrl,
            formData,
            {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            }
        );
        debugger;

        if (response.status !== 200) {
            console.error('Cannot save receipt');
        }

        this.addReceipeToStore(response.data);
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
}
