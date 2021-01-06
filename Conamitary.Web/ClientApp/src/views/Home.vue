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
            <v-data-table
                :headers="headers"
                :items="receipes"
                :items-per-page="5"
            ></v-data-table>
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

@Component({
    components: {
        ReceipeComponent,
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
        await receipesModule.addReceipe(this.currentRecipe);
        this.receipeDialog = false;
    }
}
</script>

<style lang="scss" scoped>
.add-receipe-btn {
    margin-bottom: 30px;
}
</style>
