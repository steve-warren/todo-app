//import router from "./router";
import App from "./App.vue";
import Vue from 'vue';
import VueMaterial from 'vue-material';
import 'vue-material/dist/vue-material.min.css';
import 'vue-material/dist/theme/default.css';

Vue.use(VueMaterial);

new Vue({ render: createElement => createElement(App) }).$mount('#app');