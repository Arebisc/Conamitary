<template>
    <div class="home">
        <v-container>
            <v-pagination
                v-model="pageNumber"
                :length="maxPages"
            ></v-pagination>
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
                            currentRecipe.title
                        }}</v-toolbar-title>
                    </v-toolbar>
                    <v-container id="receipe-dialog-content">
                        <v-img
                            src="https://cdn.vuetifyjs.com/images/cards/house.jpg"
                            gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
                            max-height="600"
                            contain
                        ></v-img>
                        <v-card>
                            <v-card-title>
                                Składniki
                            </v-card-title>
                            <v-card-text>
                                {{ currentRecipe.ingredients }}
                            </v-card-text>
                        </v-card>
                        <v-card>
                            <v-card-title>
                                Instrukcja
                            </v-card-title>
                            <v-card-text>
                                {{ currentRecipe.instructions }}
                            </v-card-text>
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
                    :title="receipe.title"
                    imageSrc="https://cdn.vuetifyjs.com/images/cards/house.jpg"
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
import ReceipeComponent from '@/components/ReceipeComponent.vue';
import { ReceipeDto } from '@/models/receipeDto';
import { $inject } from '@vanroeybe/vue-inversify-plugin';
import { EmptyReceipeGeneratorInterface } from '@/abstract/receipes/EmptyReceipeGeneratorInterface';
import ReceipeCard from '@/components/ReceipeCard.vue';

@Component({
    components: {
        ReceipeComponent,
        ReceipeCard,
    },
})
export default class Home extends Vue {
    // eslint-disable-next-line no-undef
    @$inject(nameof<EmptyReceipeGeneratorInterface>())
    private emptyReceipeGenerator!: EmptyReceipeGeneratorInterface;

    private receipeDialog = false;
    private currentRecipe: ReceipeDto = this.emptyReceipeGenerator.generate();

    private receipesPerPage = 9;
    private pageNumber = 1;

    private get receipes() {
        return receipesModule.receipesGetter.slice(
            this.receipesPerPage * this.pageNumber - this.receipesPerPage,
            this.receipesPerPage * this.pageNumber - 1
        );
    }

    private get maxPages() {
        return Math.ceil(
            (receipesModule.receipesGetter.length - 1) / this.receipesPerPage
        );
    }

    private async created() {
        await receipesModule.loadReceipes();
        this.currentRecipe = this.emptyReceipeGenerator.generate();
    }

    private showReceipe(receipe: ReceipeDto) {
        this.currentRecipe = receipe;
        this.receipeDialog = true;
    }

    // private async saveChanges() {
    //     if (!this.currentRecipe.id) {
    //         await receipesModule.addReceipe(this.currentRecipe);
    //     } else {
    //         await receipesModule.editReceipe(this.currentRecipe);
    //     }

    //     this.receipeDialog = false;
    //     this.currentRecipe = this.emptyReceipeGenerator.generate();
    // }

    // private async editReceipe(selectedReceipe: ReceipeDto) {
    //     this.currentRecipe = selectedReceipe;
    //     this.receipeDialog = true;
    // }

    // private async deleteReceipe(receipe: ReceipeDto) {
    //     const confirmMessage = `Jesteś pewny/a, że chcesz usunąć przepis: ${receipe.title}`;

    //     if (confirm(confirmMessage)) {
    //         await receipesModule.deleteReceipe(receipe.id as string);
    //     }
    // }
}
</script>

<style lang="scss" scoped>
.home {
    #receipts-container div {
        width: 31.33333%;
        margin: 1%;
    }

    #receipe-dialog-content div {
        margin-bottom: 30px;
    }
}
</style>
