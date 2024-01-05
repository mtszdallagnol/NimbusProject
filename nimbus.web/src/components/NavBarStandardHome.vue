<script setup>
    import { ref, computed, onMounted } from 'vue'
    import { useRouter } from 'vue-router';

    const NavBarItemsList = ref([
        {name: 'Home', path: '/'},
        {name: 'Produto', path: '/#produto'},
        {name: 'Projeto', path: '/#projeto'},
        {name: 'Contato', path: '/#contato'}
    ])  

    const isOpen = ref(false)
    const windowWidth = ref(window.innerWidth)

    const scrollPosition = ref(window.scrollY)

    onMounted(() => {
        window.addEventListener('resize', () => {
            windowWidth.value = window.innerWidth
        })

        window.addEventListener('scroll', () => {
            if (window.scrollY == 0) {
                document.activeElement?.blur()
            }
            scrollPosition.value = window.scrollY
        })
    })

    const isMobile = computed(() => {
        return windowWidth.value < 1280 ? true : false
    })

    const Router = useRouter()

    function scrolltoFunction() {
        setTimeout(() => {
            if (Router.currentRoute.value.hash) {
                let scrollDiv = document.querySelector(Router.currentRoute.value.hash).offsetTop 
                window.scrollTo({top: scrollDiv - 164, behavior: 'smooth'})
            }
            else {
                window.scrollTo({top: 0, behavior: 'smooth'})
            }
        }, 50);
    }
</script>

<template>
    <header class="fixed w-full z-50 top-0 transition-all rounded-lg" :class="{ 'headerTopScrollNot0': ((scrollPosition > 0 && !isMobile) || (isMobile && (isOpen || scrollPosition > 0))) }">
        <nav class="flex relative justify-between xl:container xl:mx-auto mx-10 py-3">
            <div class="flex items-center">
                <RouterLink to="/"><img src="../assets/img/Logo.svg"></RouterLink>
                <RouterLink to="/" class="font-bold text-lg" v-if="!isMobile">Nimbus</RouterLink>
            </div>
            <div class="xl:grid flex grid-cols-4 gap-10 items-center">
                <RouterLink v-for="item in NavBarItemsList" :to="item.path" class="font-semibold transition-colors hover:text-blue-400 focus:text-blue-400" v-if="!isMobile" @click="scrolltoFunction">{{ item.name }}</RouterLink>
                <RouterLink to="" class="font-bold text-lg" v-else>Nimbus</RouterLink>
            </div>
            <div class="hidden xl:flex items-center gap-10">
                <RouterLink to="/login" class="font-semibold p-2 hover:text-blue-400" id="navBarItemMobile">Login</RouterLink>
                <RouterLink to="/cadastro" class="font-semibold p-2 hover:text-blue-400" id="navBarItemMobile">Cadastre-se</RouterLink>
            </div>
            <div class="xl:hidden flex items-center w-[20px] ">
                <font-awesome-icon :icon="isOpen ? 'fa-solid fa-x' : 'fa-solid fa-bars'" size="lg" @click="isOpen = !isOpen"/>
            </div>
        </nav>
        <div class="grid grid-rows-4 gap-5 mx-10 h-auto transition-all" :class="{'max-h-0 invisible opacity-0': !isOpen || !isMobile, 'max-h-[257px] py-5': isOpen && isMobile}">
            <RouterLink v-for="item in NavBarItemsList" :to="item.path" class="font-semibold transition-colors flex items-center py-5 px-2 focus:bg-blue-400 focus:text-white rounded-md" @click="scrolltoFunction($event)">{{ item.name }}</RouterLink>
            <div class="flex items-center gap-5 mt-5">
                <RouterLink to="/login" class="font-semibold p-2" id="navBarItemMobile">Login</RouterLink>
                <RouterLink to="/cadastro" class="font-semibold p-2" id="navBarItemMobile">Cadastre-se</RouterLink>
            </div>
        </div>
    </header>
</template>

<style scoped>
    .router-link-active#navBarItemMobile {
        color: white;
        border-radius: 0.5rem;
        background: #2142E7;
        box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
    }

    .headerTopScrollNot0 {
        background-color: rgba(255, 255, 247, 0.8);
        backdrop-filter: saturate(180%) blur(20px);
        -webkit-box-shadow: 0px 6px 23px -6px rgba(0,0,0,0.65);
        -moz-box-shadow: 0px 6px 23px -6px rgba(0,0,0,0.65);
        box-shadow: 0px 6px 23px -6px rgba(0,0,0,0.30);
        width: 100%;
    }


</style>