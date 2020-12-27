import { FooInterface } from '@/abstract/fooInterface';
import { $inject } from '@vanroeybe/vue-inversify-plugin';
import { Store } from 'vuex';
import { Action, Module, Mutation, VuexModule } from 'vuex-class-modules';


@Module()
export class FooModule extends VuexModule {

    public constructor(store: Store<any>) {
        super({
            store,
            name: nameof<FooModule>()
        });
    }

    private testText = 'test text';

    @$inject(nameof<FooInterface>())
    private foo!: FooInterface;

    public get testTextGetter() {
        return this.testText;
    }

    @Action
    public loadText(newText: string) {
        this.foo.bar();
        this.setText(this.testText + newText);
    }

    @Mutation
    private setText(newText: string) {
        this.testText = newText;
    }
}