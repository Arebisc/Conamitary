import Vue from 'vue';
import Vuetify from 'vuetify/lib';

import VuetifyDialog from 'vuetify-dialog';
import 'vuetify-dialog/dist/vuetify-dialog.css';

Vue.use(Vuetify);

const vuetify = new Vuetify({
    theme: { dark: true },
});

Vue.use(VuetifyDialog, {
    context: {
        vuetify,
    },
});

export default vuetify;