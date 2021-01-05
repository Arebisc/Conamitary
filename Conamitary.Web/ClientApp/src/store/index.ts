import { ReceipeModule } from './modules/receipeModule';
import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

const store = new Vuex.Store({});
export default store;

export const receipesModule = new ReceipeModule(store);