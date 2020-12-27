import 'reflect-metadata';
import { MainModule } from './mainModule';
import { vueInversifyPlugin } from '@vanroeybe/vue-inversify-plugin';
import { Container } from 'inversify';
import Vue from 'vue';

export const registerContainer = () => {
    const container = new Container();

    container.load(MainModule);

    Vue.use(vueInversifyPlugin(container));
};
