<template>
    <v-card>
        <receipe-image :imageId="imageId" :maxHeight="200"></receipe-image>
        <v-card-actions>
            <v-card-title class="title">{{ title }}</v-card-title>
        </v-card-actions>
    </v-card>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { imagesModule } from '@/store/index';
import ReceipeImage from '@/components/ReceipeImage.vue';

@Component({
    components: {
        ReceipeImage,
    },
})
export default class ReceipeCard extends Vue {
    @Prop()
    private imageId!: string;

    @Prop()
    private title!: string;

    private get imageSrc() {
        return imagesModule.imageUrl(this.imageId);
    }
}
</script>

<style lang="scss" scoped>
.title {
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    display: block;
}
</style>
