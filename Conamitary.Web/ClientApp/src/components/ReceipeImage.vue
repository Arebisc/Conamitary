<template>
    <v-img
        :src="imageSrc"
        class="white--text align-end"
        gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
        :max-height="maxHeightGetter"
        :lazy-src="require('@/assets/no-image.png')"
        contain
    >
    </v-img>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { imagesModule } from '@/store/index';

@Component
export default class ReceipeImage extends Vue {
    @Prop()
    private imageId!: string;

    @Prop()
    private maxHeight!: number;

    private get maxHeightGetter() {
        const defaultHeightInPx = 200;

        if (!this.maxHeight) {
            return defaultHeightInPx;
        }
        return this.maxHeight;
    }

    private get imageSrc() {
        return imagesModule.imageUrl(this.imageId);
    }
}
</script>
