import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import vuetify from './plugins/vuetify';

import wysiwyg from 'vue-wysiwyg';
import 'vue-wysiwyg/dist/vueWysiwyg.css';

import VuetifyDialog from 'vuetify-dialog';
import 'vuetify-dialog/dist/vuetify-dialog.css';

import { registerContainer } from './modules/diContainer';

Vue.use(wysiwyg, {});

Vue.use(VuetifyDialog, {
    context: {
        vuetify,
    },
});

Vue.config.productionTip = false;

registerContainer();

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App),
}).$mount('#app');
