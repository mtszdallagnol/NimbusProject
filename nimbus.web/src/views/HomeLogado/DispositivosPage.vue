<script setup>
import ListaDispositvos from './Dispositivos/ListaDispositvos.vue';
import DispositivoIndividualPage from './Dispositivos/DispositivoIndividualPage.vue';
import { onUpdated, ref } from 'vue';

const currentIDProduto = ref();
const showList = ref(true);
const getUpdate = (ID) => {
     showList.value = false;
     currentIDProduto.value = ID;
}
const getUpdateBack = () => {
     showList.value = true;
}

</script>
    
<template>


     <ListaDispositvos v-if="showList" @update="getUpdate"> </ListaDispositvos>

     <DispositivoIndividualPage v-else @updateVoltar="getUpdateBack" :currentIDProduto="currentIDProduto" ></DispositivoIndividualPage> 


</template>
    
<style scoped>

</style>