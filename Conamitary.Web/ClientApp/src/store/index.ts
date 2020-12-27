import Vue from 'vue';
import Vuex from 'vuex';
import { FooModule } from './modules/fooModule';

Vue.use(Vuex);

const store = new Vuex.Store({});

export default store;
export const fooModule = new FooModule(store);

