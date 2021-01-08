<template>
    <div id="app">
        <v-app>
            <v-navigation-drawer
                app
                src="@/assets/register.jpg"
                v-model="navigationDrawer"
            >
                <v-list-item>
                    <v-list-item-content>
                        <v-list-item-title class="title">
                            Conamitary
                        </v-list-item-title>
                        <v-list-item-subtitle>
                            Cooking receipts
                        </v-list-item-subtitle>
                    </v-list-item-content>
                </v-list-item>

                <v-divider></v-divider>

                <v-list dense nav>
                    <v-list-item
                        v-for="item in menuItems"
                        :key="item.title"
                        link
                        :to="item.to"
                    >
                        <v-list-item-icon>
                            <v-icon>{{ item.icon }}</v-icon>
                        </v-list-item-icon>

                        <v-list-item-content>
                            <v-list-item-title>{{
                                item.title
                            }}</v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                </v-list>
            </v-navigation-drawer>

            <v-app-bar app>
                <v-app-bar-nav-icon
                    @click.stop="navigationDrawer = !navigationDrawer"
                ></v-app-bar-nav-icon>
            </v-app-bar>

            <v-main>
                <v-container fluid>
                    <router-view></router-view>
                </v-container>
            </v-main>

            <v-footer app></v-footer>
        </v-app>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class App extends Vue {
    private navigationDrawer = true;
    private menuItems = [
        { title: 'Przepisy', icon: 'mdi-view-dashboard', to: '/' },
        {
            title: 'Dodaj przepis',
            icon: 'mdi-view-dashboard',
            to: '/add-receipe',
        },
    ];
}
</script>
