import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    //懒加载
    path: '/new',
    name: 'New',
    component: () => import(/* webpackChunkName: "New" */ '../views/New.vue')
  },
  {
    //懒加载
    path: '/init',
    name: 'Init',
    component: () => import(/* webpackChunkName: "Init" */ '../views/Init.vue')
  },
  {
    //懒加载
    path: '/set',
    name: 'Set',
    component: () => import(/* webpackChunkName: "Set" */ '../views/Set.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router
