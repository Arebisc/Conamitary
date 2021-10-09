import { ImagesModule } from './modules/imagesModule';
import { ReceipeModule } from './modules/receipeModule';
import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

const store = new Vuex.Store({});
export default store;

export const receipesModule = new ReceipeModule(store);
export const imagesModule = new ImagesModule(store, receipesModule);

export const initializeStore = async () => {
    await receipesModule.loadReceipes();
    await imagesModule.initializeModule();
};
