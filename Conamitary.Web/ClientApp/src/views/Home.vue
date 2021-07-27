<template>
    <div class="home">
        <v-container>
            <h1>Moje przepisy</h1>
            <v-container
                fluid
                class="d-flex justify-start mb-3 flex-wrap"
                id="receipts-container"
            >
                <h1 v-if="receipes.lenght === 0">
                    Nie masz jeszcze żadnych przepisów.
                </h1>
                <receipe-card
                    v-else
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

import ReceipeCard from '@/components/ReceipeCard.vue';
import Receipe from '@/views/Receipe.vue';
import { ReceipeDto } from '@/dtos/receipeDto';

@Component({
    components: {
        ReceipeCard,
    },
})
export default class Home extends Vue {
    private readonly receipesPerPage = 9;
    private pageNumber = 1;

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
    }

    private async showReceipe(receipe: ReceipeDto) {
        await this.$router.push({
            // eslint-disable-next-line no-undef
            name: nameof<Receipe>(),
            params: {
                receipeId: receipe.id as string,
            },
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
