import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './assets/tailwind.css'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faBars, faX, faArrowRight, faTv, faMicrochip, faArrowLeft} from '@fortawesome/free-solid-svg-icons'
import { faLinkedin, faTwitter } from '@fortawesome/free-brands-svg-icons'
import { createPinia } from 'pinia'

const pinia = createPinia();

library.add(faBars, faX, faTwitter, faLinkedin, faArrowRight, faTv, faMicrochip, faArrowLeft);

createApp(App)
    .component('font-awesome-icon', FontAwesomeIcon)
    .use(pinia)
    .use(router)
    .mount('#app')
