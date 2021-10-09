<template>
    <div class="edit-receipe">
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

                        <label>Składniki:</label>
                        <tiptap-vuetify
                            class="receipe-wysiwyg"
                            v-model="receipe.ingredients"
                            :extensions="tiptapExtensions"
                            :toolbar-attributes="darkToolbarAttribute"
                            placeholder="Składniki..."
                        />

                        <label>Instrukcja:</label>
                        <tiptap-vuetify
                            class="receipe-wysiwyg"
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

                <v-card style="margin-top: 30px;">
                    <v-card-title>
                        Zdjęcia
                    </v-card-title>
                    <v-card-text>
                        <v-file-input
                            accept="image/*"
                            label="Dodaj nowe zdjęcia"
                            filled
                            show-size
                            prepend-icon="mdi-camera"
                            multiple
                            chips
                            v-model="newImages"
                        >
                        </v-file-input>
                    </v-card-text>
                    <v-card-actions>
                        <v-btn @click="saveNewFiles">Prześlij</v-btn>
                    </v-card-actions>
                    <v-card-text v-if="receipe.imagesIds.length > 0">
                        <v-container>
                            <v-row>
                                <v-col
                                    cols="4"
                                    v-for="imageId in receipe.imagesIds"
                                    :key="imageId"
                                >
                                    <v-card>
                                        <v-btn
                                            color="error"
                                            @click="
                                                removeImage(imageId, receipe.id)
                                            "
                                            icon
                                        >
                                            <v-icon>mdi-delete</v-icon>
                                        </v-btn>

                                        <v-card-text>
                                            <receipe-image
                                                :imageId="imageId"
                                                :maxHeight="200"
                                            ></receipe-image>
                                        </v-card-text>
                                    </v-card>
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-card-text>
                </v-card>
            </v-container>
        </v-form>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { receipesModule } from '@/store/index';
import Home from './Home.vue';
import ReceipeImage from './../components/ReceipeImage.vue';

import { ReceipeDto } from '@/dtos/receipeDto';

// eslint-disable-next-line @typescript-eslint/ban-ts-ignore
// @ts-ignore
import { TiptapVuetify } from 'tiptap-vuetify';
import {
    baseExtensionConfigurations,
    darkToolbarAttribute,
} from '@/configurations/tiptapVuetify';
import { imagesModule } from '@/store/index';

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
        ReceipeImage,
    },
})
export default class EditReceipe extends Vue {
    private receipe!: ReceipeDto;

    private readonly tiptapExtensions = baseExtensionConfigurations;
    private readonly darkToolbarAttribute = darkToolbarAttribute;

    private newImages: File[] = [];

    private async created() {
        const receipeId = this.$route.params.id;
        const receipe = receipesModule.receipeGetter(receipeId);

        if (!receipe) {
            return await this.$router.push({
                // eslint-disable-next-line no-undef
                name: nameof<Home>(),
                params: {
                    omitNavigationGuard: true.toString(),
                },
            });
        }
        this.receipe = receipe;
    }

    private async save() {
        await receipesModule.editReceipe(this.receipe);

        this.$router.push({
            // eslint-disable-next-line no-undef
            name: nameof<Home>(),
            params: {
                omitNavigationGuard: true.toString(),
            },
        });
    }

    private async saveNewFiles() {
        await imagesModule.addImagesToReceipe({
            receipeId: this.receipe.id as string,
            images: this.newImages,
        });
    }

    private removeImage(imageId: string, receipeId: string) {
        this.$dialog
            .confirm({
                text: 'Czy na pewno chcesz usunąć to zdjęcie?',
                title: 'Uwaga!',
            })
            .then(async userResponse => {
                if (userResponse as boolean) {
                    await imagesModule.removeImageFromReceipe({
                        imageId,
                        receipeId,
                    });
                }
            });
    }
}
</script>

<style lang="scss" scoped>
.edit-receipe {
    .receipe-wysiwyg {
        margin-bottom: 22px;
    }
}
</style>
