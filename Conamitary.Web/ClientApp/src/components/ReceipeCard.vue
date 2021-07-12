<template>
    <v-card>
        <v-img
            :src="imageSrc"
            class="white--text align-end"
            gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
            height="200px"
        >
        </v-img>

        <v-card-actions>
            <v-card-title>{{ title }}</v-card-title>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { imagesModule } from '@/store/index';

@Component
export default class ReceipeCard extends Vue {
    @Prop()
    private imageId!: string;

    @Prop()
    private title!: string;

    @Prop()
    private id!: string;

    private get imageSrc() {
        return imagesModule.imageUrl(this.imageId);
    }
}
</script>
