import Vue from 'vue';
import Vuetify from 'vuetify/lib';
import pl from 'vuetify/src/locale/pl';
import en from 'vuetify/src/locale/en';

import VuetifyDialog from 'vuetify-dialog';
import 'vuetify-dialog/dist/vuetify-dialog.css';

import { TiptapVuetifyPlugin } from 'tiptap-vuetify';
import 'tiptap-vuetify/dist/main.css';

Vue.use(Vuetify);
const vuetify = new Vuetify({
    theme: { dark: true },
    lang: {
        locales: { en, pl },
        current: 'pl'
    }
});

Vue.use(VuetifyDialog, {
    context: {
        vuetify,
    },
});

Vue.use(TiptapVuetifyPlugin, {
    vuetify,
    iconsGroup: 'mdi'
});

export default vuetify;