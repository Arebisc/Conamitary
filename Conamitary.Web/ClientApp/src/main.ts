import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import vuetify from './plugins/vuetify';

import { registerContainer } from './modules/diContainer';

Vue.config.productionTip = false;

registerContainer();

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App),
}).$mount('#app');
