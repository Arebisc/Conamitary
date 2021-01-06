<template>
    <div class="receipe">
        <v-form v-model="valid">
            <v-container>
                <v-row>
                    <v-text-field
                        v-model="receipe.title"
                        label="Tytuł"
                        required
                    ></v-text-field>

                    <v-textarea
                        v-model="receipe.ingredients"
                        label="Składniki"
                        required
                    ></v-textarea>

                    <v-textarea
                        v-model="receipe.instructions"
                        label="Instrukcje"
                        required
                    ></v-textarea>
                </v-row>
            </v-container>
        </v-form>
    </div>
</template>

<script lang="ts">
import { ReceipeDto } from '@/models/receipeDto';
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class ReceipeComponent extends Vue {
    @Prop()
    private receipe!: ReceipeDto;

    private valid = false;
}
</script>
