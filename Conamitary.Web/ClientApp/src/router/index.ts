import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';

import Home from '../views/Home.vue';
import AddReceipe from '../views/AddReceipe.vue';
import EditReceipe from '../views/EditReceipe.vue';
import Receipe from '../views/Receipe.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
    {
        path: '/',
        name: nameof<Home>(),
        component: Home,
    },
    {
        path: '/add-receipe',
        name: nameof<AddReceipe>(),
        component: AddReceipe,
    },
    {
        path: '/receipe',
        name: nameof<Receipe>(),
        component: Receipe,
        props: true,
    },
    {
        path: '/edit-receipe',
        name: nameof<EditReceipe>(),
        component: EditReceipe,
        props: true,
    },
];

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes,
});

export default router;
