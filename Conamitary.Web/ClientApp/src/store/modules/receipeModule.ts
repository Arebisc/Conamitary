import { ReceipeDto } from '../../models/receipeDto';
import { Store } from 'vuex';
import { VuexModule, Module, Action, Mutation } from 'vuex-class-modules';
import axios from 'axios';

@Module
export class ReceipeModule extends VuexModule {
    private receipeUrl = 'api/receipe';

    public constructor(store: Store<any>) {
        super({
            name: nameof<ReceipeModule>(),
            store
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
        }
        else {
            console.error('Cannot load receipts');
        }
    }

    @Mutation
    private setReceipes(receipes: ReceipeDto[]) {
        this.receipes = receipes;
    }
}