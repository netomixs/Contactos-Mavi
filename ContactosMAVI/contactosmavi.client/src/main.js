import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import Tabla from './components/Tabla.vue'
import { createMemoryHistory, createRouter } from 'vue-router'
import Formulario from './components/Formulario.vue'

const routes = [
    { path: '/', component: Tabla  },
    { path: '/form/:id/:view', component:Formulario},
    { path: '/form', component:Formulario}
  ]
const router = createRouter({
    history: createMemoryHistory(),
    routes,
  })
createApp(App).use(router)
.mount('#app')
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
 