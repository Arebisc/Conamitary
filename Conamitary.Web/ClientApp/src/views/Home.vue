<template>
    <div class="home">
        <img alt="Vue logo" src="../assets/logo.png" />
        <HelloWorld msg="Welcome to Your Vue.js + TypeScript App" />

        <h1>Store: {{ textGetter }}</h1>
        <button @click="callAction">Change</button>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import HelloWorld from '@/components/HelloWorld.vue'; // @ is an alias to /src
import { fooModule } from '@/store';

@Component({
    components: {
        HelloWorld,
    },
})
export default class Home extends Vue {
    private get textGetter() {
        return fooModule.testTextGetter;
    }

    private callAction() {
        fooModule.loadText('new text');
    }
}
</script>
