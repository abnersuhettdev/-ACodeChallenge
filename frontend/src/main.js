import { createApp } from 'vue';
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import router from './router';
import App from './App.vue';
import { mask } from 'vue-the-mask'; 

const vuetify = createVuetify({
    components,
    directives,
});

createApp(App)
    .use(vuetify)
    .use(router)
    .directive('mask', mask) 
    .mount('#app');
