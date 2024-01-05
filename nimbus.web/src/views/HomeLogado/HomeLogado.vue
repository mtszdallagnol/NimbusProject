<script setup>
import { ref } from 'vue';
import SideBarHomeLogado from '../../components/SideBarHomeLogado.vue'
import DashBoardPage from './DashBoardPage.vue'
import DispositivosPage from './DispositivosPage.vue';

const currentVerticalTabId = ref(1);

function changeVerticalTab(idSetTab) {
    currentVerticalTabId.value = idSetTab;
}
</script>

<template>
    <div class="flex h-full">
        <SideBarHomeLogado @changeVerticalTab="changeVerticalTab"></SideBarHomeLogado>
        <div class="max-h-[100vh] overflow-y-scroll w-full p-5">
            <DashBoardPage v-if="currentVerticalTabId === 1"></DashBoardPage>
            <DispositivosPage v-else-if="currentVerticalTabId === 2"></DispositivosPage>
        </div>
    </div>
</template>

<style scoped>

</style>