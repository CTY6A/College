import Vue from 'vue'
import VueResource from 'vue-resource'
import App from './App.vue'
import 'materialize-css/dist/js/materialize.min'
import './registerServiceWorker'

import router from './router'
import store from './store'

Vue.use(VueResource)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
