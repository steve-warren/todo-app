import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

import AllTodoItems from "./pages/ViewTodos";

const routes = [
  { path: '/tasks', name: 'view-all', component: AllTodoItems },
  { path: '/lists/:listId', name: 'view-list', component: AllTodoItems }
];

const router = new VueRouter({
  routes
});

export default router;
