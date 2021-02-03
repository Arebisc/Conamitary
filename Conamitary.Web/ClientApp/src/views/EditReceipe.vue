<template>
    <div class="add-receipe">
        <v-form>
            <v-container>
                <v-card>
                    <v-card-title>
                        Edytuj przepis
                    </v-card-title>
                    <v-card-text>
                        <v-text-field
                            v-model="receipe.title"
                            label="Tytuł"
                            required
                        ></v-text-field>

                        <tiptap-vuetify
                            v-model="receipe.ingredients"
                            :extensions="tiptapExtensions"
                            :toolbar-attributes="darkToolbarAttribute"
                            placeholder="Składniki..."
                        />

                        <tiptap-vuetify
                            v-model="receipe.instructions"
                            :extensions="tiptapExtensions"
                            :toolbar-attributes="darkToolbarAttribute"
                            placeholder="Instrukcja..."
                        />
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="save">Zapisz</v-btn>
                    </v-card-actions>
                </v-card>
            </v-container>
        </v-form>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { receipesModule } from '@/store/index';
import Home from './Home.vue';
import { ReceipeDto } from '@/dtos/receipeDto';

import { TiptapVuetify } from 'tiptap-vuetify';
import {
    baseExtensionConfigurations,
    darkToolbarAttribute,
} from '@/configurations/tiptapVuetify';

@Component({
    beforeRouteLeave: async function(_to, _from, next) {
        await this.$dialog
            .confirm({
                text: 'Na pewno chcesz opuścić podstronę?',
                title: 'Uwaga!',
            })
            .then(result => next(result));
    },
    components: {
        TiptapVuetify,
    },
})
export default class EditReceipe extends Vue {
    @Prop()
    private receipe!: ReceipeDto;

    private readonly tiptapExtensions = baseExtensionConfigurations;
    private readonly darkToolbarAttribute = darkToolbarAttribute;

    private async save() {
        await receipesModule.editReceipe(this.receipe);
        // eslint-disable-next-line no-undef
        this.$router.push({ name: nameof<Home>() });
    }
}
</script>

<style lang="scss" scoped>
.add-receipe {
    .v-card__text {
        div {
            margin-bottom: 30px;
        }
    }
}
</style>
