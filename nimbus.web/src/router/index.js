import { createRouter, createWebHashHistory } from 'vue-router'
import { useAuth } from '@/stores/auth';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/views/StandardHome.vue')
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/UserSingIn&Up/LoginPage.vue')
  },
  {
    path: '/cadastro',
    name: 'Cadastro',
    component: () => import('@/views/UserSingIn&Up/CadastroPage.vue')
  },
  {
    path: '/homelogado',
    name: 'Home Logado',
    component: () => import('@/views/HomeLogado/HomeLogado.vue'),
    meta: { auth: true }
  }
];

const router = createRouter({
  history: createWebHashHistory(),
  routes
});

router.beforeEach(async(to, from, next) => {
  const auth = useAuth();
  const _isAuthenticated = await auth.isAuthenticated();
  if(to.meta?.auth) {
    if (auth.token && auth.user) {
      if (_isAuthenticated) next();
      else next('/');
    } else next('/');
  } else if (to.path == '/login' && _isAuthenticated) next('/homelogado');
    else next(); 
});

export default router
