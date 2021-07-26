import 'reflect-metadata';
import { vueInversifyPlugin } from '@vanroeybe/vue-inversify-plugin';
import { Container } from 'inversify';
import Vue from 'vue';

import { ReceipeModule } from '@/modules/receipeModule';

export const registerContainer = () => {
    const container = new Container();

    container.load(ReceipeModule);

    Vue.use(vueInversifyPlugin(container));
};
