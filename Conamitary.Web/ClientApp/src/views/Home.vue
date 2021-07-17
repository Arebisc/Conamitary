<template>
    <div class="home">
        <v-container>
            <h1>Moje przepisy</h1>
            <v-dialog
                v-model="receipeDialog"
                fullscreen
                hide-overlay
                transition="dialog-bottom-transition"
            >
                <v-card>
                    <v-toolbar dark color="primary">
                        <v-btn icon dark @click="receipeDialog = false">
                            <v-icon>mdi-close</v-icon>
                        </v-btn>
                        <v-toolbar-title>{{
                            currentReceipe.title
                        }}</v-toolbar-title>

                        <v-spacer></v-spacer>

                        <v-btn
                            color="warning"
                            @click="editReceipe(currentReceipe)"
                            icon
                        >
                            <v-icon>mdi-pencil</v-icon>
                        </v-btn>
                        <v-btn
                            color="error"
                            @click="removeReceipe(currentReceipe)"
                            icon
                        >
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
                                v-for="imageId in currentReceipe.imagesIds"
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
                                    currentReceipe.imagesIds === undefined ||
                                        currentReceipe.imagesIds.length === 0
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
                            <v-card-text
                                v-html="currentReceipe.ingredients"
                            ></v-card-text>
                        </v-card>
                        <v-card class="receipe-dialog-item">
                            <v-card-title>
                                Instrukcja
                            </v-card-title>
                            <v-card-text
                                v-html="currentReceipe.instructions"
                            ></v-card-text>
                        </v-card>
                    </v-container>
                </v-card>
            </v-dialog>

            <v-container
                fluid
                class="d-flex justify-start mb-3 flex-wrap"
                id="receipts-container"
            >
                <receipe-card
                    v-for="receipe in receipes"
                    :key="receipe.id"
                    :id="receipe.id"
                    :title="receipe.title"
                    :imageId="receipe.imagesIds[0]"
                    @click.native="showReceipe(receipe)"
                ></receipe-card>
            </v-container>
            <v-pagination
                v-model="pageNumber"
                :length="maxPages"
            ></v-pagination>
        </v-container>
    </div>
</template>

<script lang="ts">
import { receipesModule } from '@/store';
import { Component, Vue } from 'vue-property-decorator';
import { $inject } from '@vanroeybe/vue-inversify-plugin';
import { EmptyReceipeGeneratorInterface } from '@/abstract/receipes/EmptyReceipeGeneratorInterface';
import { ReceipeDto } from '@/dtos/receipeDto';

import ReceipeCard from '@/components/ReceipeCard.vue';
import EditReceipe from './EditReceipe.vue';
import ReceipeImage from '@/components/ReceipeImage.vue';

@Component({
    components: {
        ReceipeCard,
        ReceipeImage,
    },
})
export default class Home extends Vue {
    // eslint-disable-next-line no-undef
    @$inject(nameof<EmptyReceipeGeneratorInterface>())
    private emptyReceipeGenerator!: EmptyReceipeGeneratorInterface;

    private receipeDialog = false;
    private currentReceipe: ReceipeDto = this.emptyReceipeGenerator.generate();

    private readonly receipesPerPage = 9;
    private pageNumber = 1;

    private carouselIndex = 0;
    private get carouselNavigationArrows() {
        debugger;
        return (
            !!this.currentReceipe.imagesIds &&
            this.currentReceipe.imagesIds.length > 1
        );
    }

    private get receipes() {
        return receipesModule.receipesGetter.slice(
            this.receipesPerPage * this.pageNumber - this.receipesPerPage,
            this.receipesPerPage * this.pageNumber
        );
    }

    private get maxPages() {
        return Math.ceil(
            receipesModule.receipesGetter.length / this.receipesPerPage
        );
    }

    private async created() {
        await receipesModule.loadReceipes();
        this.currentReceipe = this.emptyReceipeGenerator.generate();
    }

    private showReceipe(receipe: ReceipeDto) {
        this.currentReceipe = receipe;
        this.receipeDialog = true;
    }

    private async removeReceipe(receipe: ReceipeDto) {
        const confirmMessage = `Jesteś pewny/a, że chcesz usunąć przepis: ${receipe.title}`;

        if (confirm(confirmMessage)) {
            await receipesModule.deleteReceipe(receipe.id as string);
        }
        this.receipeDialog = false;
    }

    private editReceipe(receipeToEdit: ReceipeDto) {
        this.$router.push({
            // eslint-disable-next-line no-undef
            name: nameof<EditReceipe>(),
            // eslint-disable-next-line @typescript-eslint/no-explicit-any
            params: { receipe: receipeToEdit as any },
        });
    }
}
</script>

<style lang="scss" scoped>
.home {
    h1 {
        text-align: center;
    }

    #receipts-container div {
        width: 31.33333%;
        margin: 1%;
    }
}
</style>
