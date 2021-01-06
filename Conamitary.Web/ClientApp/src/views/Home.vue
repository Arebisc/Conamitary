<template>
    <div class="home">
        <v-container>
            <v-dialog
                v-model="receipeDialog"
                fullscreen
                hide-overlay
                transition="dialog-bottom-transition"
            >
                <template v-slot:activator="{ on, attrs }">
                    <v-btn
                        color="primary"
                        v-bind="attrs"
                        v-on="on"
                        class="add-receipe-btn"
                    >
                        Dodaj przepis
                    </v-btn>
                </template>
                <v-card>
                    <v-toolbar dark color="primary">
                        <v-btn icon dark @click="receipeDialog = false">
                            <v-icon>mdi-close</v-icon>
                        </v-btn>
                        <v-toolbar-title>Przepis</v-toolbar-title>
                        <v-spacer></v-spacer>
                        <v-toolbar-items>
                            <v-btn dark text @click="saveChanges">
                                Zapisz zmiany
                            </v-btn>
                        </v-toolbar-items>
                    </v-toolbar>
                    <receipe-component
                        :receipe="currentRecipe"
                    ></receipe-component>
                </v-card>
            </v-dialog>

            <v-container fluid class="d-flex justify-start mb-3 flex-wrap" id="receipts-container">
                <receipe-card v-for="n in 20" :key="n" title="Ciasto czekoladowe" imageSrc="https://cdn.vuetifyjs.com/images/cards/house.jpg"></receipe-card>
            </v-container>
            
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
        ReceipeCard
    },
})
export default class Home extends Vue {
    // eslint-disable-next-line no-undef
    @$inject(nameof<EmptyReceipeGeneratorInterface>())
    private emptyReceipeGenerator!: EmptyReceipeGeneratorInterface;

    private headers = [
        { text: 'Tytuł', value: 'title' },
        {
            text: 'Składniki',
            value: 'ingredients',
            sortable: false,
            filterable: false,
        },
        {
            text: 'Instrukcja',
            value: 'instructions',
            sortable: false,
            filterable: false,
        },
        {
            text: 'Akcje',
            value: 'actions',
            sortable: false,
            filterable: false,
        },
    ];

    private receipeDialog = false;
    private currentRecipe: ReceipeDto = this.emptyReceipeGenerator.generate();

    private get receipes() {
        return receipesModule.receipesGetter;
    }

    private async created() {
        await receipesModule.loadReceipes();
        this.currentRecipe = this.emptyReceipeGenerator.generate();
    }

    private async saveChanges() {
        if (!this.currentRecipe.id) {
            await receipesModule.addReceipe(this.currentRecipe);
        } else {
            await receipesModule.editReceipe(this.currentRecipe);
        }

        this.receipeDialog = false;
        this.currentRecipe = this.emptyReceipeGenerator.generate();
    }

    private async editReceipe(selectedReceipe: ReceipeDto) {
        this.currentRecipe = selectedReceipe;
        this.receipeDialog = true;
    }

    private async deleteReceipe(receipe: ReceipeDto) {
        const confirmMessage = `Jesteś pewny/a, że chcesz usunąć przepis: ${receipe.title}`;

        if (confirm(confirmMessage)) {
            await receipesModule.deleteReceipe(receipe.id as string);
        }
    }
}
</script>

<style lang="scss" scoped>

.home {
    .add-receipe-btn {
        margin-bottom: 30px;
    }

    #receipts-container div {
        width: 31.33333%;
        margin: 1%;
    }
}

</style>
