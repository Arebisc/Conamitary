import 'reflect-metadata';
import { vueInversifyPlugin } from '@vanroeybe/vue-inversify-plugin';
import { Container } from 'inversify';
import Vue from 'vue';

export const registerContainer = () => {
    const container = new Container();

    Vue.use(vueInversifyPlugin(container));
};
