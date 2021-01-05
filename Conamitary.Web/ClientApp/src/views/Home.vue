<template>
    <div class="home">
        <v-data-table
            :headers="headers"
            :items="receipes"
            :items-per-page="5"
        ></v-data-table>
    </div>
</template>

<script lang="ts">
import { receipesModule } from '@/store';
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Home extends Vue {
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

    private get receipes() {
        return receipesModule.receipesGetter;
    }

    private async created() {
        await receipesModule.loadReceipes();
    }
}
</script>
