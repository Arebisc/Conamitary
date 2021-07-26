<template>
    <div class="add-receipe">
        <v-form>
            <v-container>
                <v-card>
                    <v-card-title>
                        Dodaj nowy przepis
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

                        <v-file-input
                            accept="image/*"
                            label="Zdjęcia"
                            filled
                            show-size
                            prepend-icon="mdi-camera"
                            multiple
                            v-model="receipe.images"
                            chips
                        >
                        </v-file-input>
                    </v-card-text>
                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn @click="save">Zapisz przepis</v-btn>
                    </v-card-actions>
                </v-card>
            </v-container>
        </v-form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { imagesModule, receipesModule } from '@/store/index';
import { AddReceipeModel } from '@/models/addReceipeModel';
import Home from './Home.vue';

// eslint-disable-next-line @typescript-eslint/ban-ts-ignore
// @ts-ignore
import { TiptapVuetify } from 'tiptap-vuetify';
import {
    baseExtensionConfigurations,
    darkToolbarAttribute,
} from '@/configurations/tiptapVuetify';

@Component({
    beforeRouteLeave: async function(_to, _from, next) {
        if (_to.params.omitNavigationGuard === true.toString()) {
            return next();
        }
        this.$dialog
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
export default class AddReceipe extends Vue {
    private readonly tiptapExtensions = baseExtensionConfigurations;
    private readonly darkToolbarAttribute = darkToolbarAttribute;

    private saveButtonEnabled = true;

    private receipe: AddReceipeModel = {
        title: undefined,
        ingredients: undefined,
        instructions: undefined,
        images: [],
    };

    private async save() {
        this.saveButtonEnabled = false;

        const addedReceipe = await receipesModule.addReceipe(this.receipe);
        await imagesModule.addImagesToReceipe({
            receipeId: addedReceipe?.id as string,
            images: this.receipe.images,
        });

        this.saveButtonEnabled = true;

        this.$router.push({
            // eslint-disable-next-line no-undef
            name: nameof<Home>(),
            params: {
                omitNavigationGuard: true.toString(),
            },
        });
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
