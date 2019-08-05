import Vue from 'vue';
import Router from 'vue-router';
import List from './views/List.vue';
import Add from './views/Add.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
        path: '/',
        name: 'home',
        component: List,
    },
    {
        path: '/add',
        name: 'add',
        component: Add
    },
  ],
});
