<script setup>
  import { useRoute } from 'vue-router';
  import { watch } from 'vue';
  import NavBarStandardHome from './components/NavBarStandardHome.vue';
  import { useAuth } from './stores/auth';

  const seeRouter = useRoute();
  const auth = useAuth();
  const isLoggedIn = auth.isAuthenticated();
  const publicPages = ['/', '/login', '/cadastro'];
 
  watch(seeRouter, (newVal) => {
    document.title = `Nimbus Project - ${newVal.name}`;
  });
</script>

<template>
  <div v-if="!isLoggedIn || publicPages.includes(seeRouter.path)">
    <NavBarStandardHome />
  </div>
  <div :class="(!isLoggedIn || publicPages.includes(seeRouter.path)) ? 'h-[calc(100vh-94px)] mt-[94px] w-screen' : 'h-[100vh] w-screen' ">
    <RouterView />
  </div>
</template>

<style>
  
</style>
