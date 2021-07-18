import 'reflect-metadata';
import { vueInversifyPlugin } from '@vanroeybe/vue-inversify-plugin';
import { Container } from 'inversify';
import Vue from 'vue';

import { ReceipeModule } from '@/modules/receipeModule';
import { ImageModule } from './imageModule';

export const registerContainer = () => {
    const container = new Container();

    container.load(ReceipeModule);
    container.load(ImageModule);

    Vue.use(vueInversifyPlugin(container));
};
