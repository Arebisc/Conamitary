<template>
    <v-card>
        <v-toolbar>
            <v-btn icon dark @click="goBack">
                <v-icon>mdi-arrow-left</v-icon>
            </v-btn>
            <v-toolbar-title>{{ receipe.title }}</v-toolbar-title>

            <v-spacer></v-spacer>

            <v-btn color="warning" @click="editReceipe(receipe)" icon>
                <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn color="error" @click="removeReceipe(receipe)" icon>
                <v-icon>mdi-delete</v-icon>
            </v-btn>
        </v-toolbar>
        <v-container id="receipe-dialog-content">
            <v-carousel
                v-model="carouselIndex"
                :show-arrows="carouselNavigationArrows"
                :hide-delimiters="!carouselNavigationArrows"
            >
                <v-carousel-item
                    v-for="imageId in receipe.imagesIds"
                    :key="imageId"
                >
                    <receipe-image
                        :imageId="imageId"
                        :maxHeight="600"
                    ></receipe-image>
                </v-carousel-item>

                <!-- When item does not exist, show placeholder -->
                <v-carousel-item
                    v-if="
                        receipe.imagesIds === undefined ||
                            receipe.imagesIds.length === 0
                    "
                >
                    <receipe-image
                        :imageId="undefined"
                        :maxHeight="600"
                    ></receipe-image>
                </v-carousel-item>
            </v-carousel>

            <v-card class="receipe-dialog-item">
                <v-card-title>
                    Składniki
                </v-card-title>
                <v-card-text v-html="receipe.ingredients"></v-card-text>
            </v-card>
            <v-card class="receipe-dialog-item">
                <v-card-title>
                    Instrukcja
                </v-card-title>
                <v-card-text v-html="receipe.instructions"></v-card-text>
            </v-card>
        </v-container>
    </v-card>
</template>

<script lang="ts">
import { receipesModule } from '@/store';
import { Component, Vue } from 'vue-property-decorator';
import { $inject } from '@vanroeybe/vue-inversify-plugin';
import { EmptyReceipeGeneratorInterface } from '@/abstract/receipes/EmptyReceipeGeneratorInterface';
import { ReceipeDto } from '@/dtos/receipeDto';

import EditReceipe from './EditReceipe.vue';
import ReceipeImage from '@/components/ReceipeImage.vue';

@Component({
    components: {
        ReceipeImage,
    },
})
export default class Home extends Vue {
    // eslint-disable-next-line no-undef
    @$inject(nameof<EmptyReceipeGeneratorInterface>())
    private emptyReceipeGenerator!: EmptyReceipeGeneratorInterface;

    private receipe!: ReceipeDto;

    private carouselIndex = 0;
    private get carouselNavigationArrows() {
        return !!this.receipe.imagesIds && this.receipe.imagesIds.length > 1;
    }

    private async created() {
        const id = this.$route.params.receipeId;
        const receipe = receipesModule.receipeGetter(id);

        if (!receipe) {
            // eslint-disable-next-line no-undef
            this.$router.push({ name: nameof<Home>() });
            return;
        }

        this.receipe = receipe;
    }

    private async removeReceipe(receipe: ReceipeDto) {
        this.$dialog
            .confirm({
                text: `Jesteś pewny/a, że chcesz usunąć przepis: ${receipe.title}`,
                title: 'Uwaga!',
            })
            .then(async userResponse => {
                if (userResponse as boolean) {
                    await receipesModule.deleteReceipe(receipe.id as string);
                }
            });
    }

    private editReceipe(receipeToEdit: ReceipeDto) {
        this.$router.push({
            // eslint-disable-next-line no-undef
            name: nameof<EditReceipe>(),
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            params: { receipe: receipeToEdit as any },
        });
    }

    private goBack() {
        // eslint-disable-next-line no-undef
        this.$router.push({ name: nameof<Home>() });
    }
}
</script>
