<script setup>
import { ref, computed, onMounted } from 'vue';
import { useAuth } from '@/stores/auth';

const emit = defineEmits(['changeVerticalTab']);
const auth = useAuth();
const isOpen = ref(true);
const innerWidth = ref();
const isMobile = computed(() => {
    return innerWidth.value <= 1280 ? true : false;
});
const listSideBarItems = ref([
    {title: 'Dashboard', icon: 'tv', idVerticalTab: 1, active: true},
    {title: 'Meus Dispositivos', icon: 'microchip', idVerticalTab: 2, active: false}
]);

onMounted(() => {
    innerWidth.value = window.innerWidth;
    window.addEventListener('resize', () => {
        innerWidth.value = window.innerWidth; 
    });
});

function setCurrentVerticalTab(toGoVerticalTab, indexCurrentTab) {
    for(var i = 0; i < listSideBarItems.value.length; i++) {
        if (listSideBarItems.value[i].active) {
            listSideBarItems.value[i].active = false;
        };
    };

    listSideBarItems.value[indexCurrentTab].active = true; 
    emit('changeVerticalTab', toGoVerticalTab.idVerticalTab);
};
function logOutUser() {
    auth.userLogout();
};

</script>

<template>
    <div class="transition-all bg-[#1C1A1A] h-full rounded-r-xl flex flex-col p-7 text-white" 
    :class="(() => {
        if (isOpen && isMobile) return 'w-[calc(1/4*100vw)]'
        else if (isOpen && !isMobile) return 'w-[calc(1/6*100vw)]'
        else return 'w-[9rem]'
    })()">
        <div class="w-full mb-4">
            <div class="flex items-center justify-between gap-2">
                <div class="flex items-center">
                    <img src="../assets/img/Logo.svg">
                    <span class="font-semibold text-lg"
                    v-show="isOpen">NIMBUS</span>
                </div>
                <font-awesome-icon v-if="!isOpen" @click="isOpen = !isOpen" icon="bars" class="cursor-pointer" size="xl"/>
                <font-awesome-icon v-else icon="x" @click="isOpen = !isOpen" class="cursor-pointer" size="xl"/>
            </div>
        </div>
        <div class="flex gap-3 flex-col text-lg mt-24">
            <div class="hover:bg-blue-400 hover:scale-105 transition-all hover:border-blue-400 border-2 ms-3 font-semibold rounded-lg cursor-pointer p-2 flex gap-3 items-center" 
            :class="item.active ? 'bg-blue-400 border-blue-400 iems-center justify-center' : 'bg-gray-950 border-blue-400 justify-center' " 
            @click="setCurrentVerticalTab(item, index)" 
            v-for="(item, index) in listSideBarItems">
                <font-awesome-icon :icon="item.icon" ></font-awesome-icon>
                <p v-show="isOpen">{{ item.title }}</p>
            </div>
        </div>
        <div class="mt-auto">
            <p class="font-semibold mb-1">Olá, {{ auth.user.nmNomeUsuario }}</p>
            <button class="bg-red-500 rounded-lg p-1 font-semibold" @click="logOutUser">Log-out</button>
        </div>
    </div>

</template>

<style scoped>

</style>