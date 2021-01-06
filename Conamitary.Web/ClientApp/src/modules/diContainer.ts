import 'reflect-metadata';
import { ReceipeModule } from '@/modules/receipeModule';
import { vueInversifyPlugin } from '@vanroeybe/vue-inversify-plugin';
import { Container } from 'inversify';
import Vue from 'vue';

export const registerContainer = () => {
    const container = new Container();

    container.load(ReceipeModule);

    Vue.use(vueInversifyPlugin(container));
};
