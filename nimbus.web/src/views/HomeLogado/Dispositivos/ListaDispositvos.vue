<script setup>
import { ref, onMounted } from 'vue';
import http from '@/services/http';
import { useAuth } from '@/stores/auth';
import { useRouter } from 'vue-router';

const router = useRouter();
const emit = defineEmits(['update']);
const alterarStatusPage = (ID) => {
    emit('update',ID)
};
const auth = useAuth();
const listProductsNimbus = ref([])
const getStatusColor = (status) => {
    switch(status) {
        case 'On-line':
            return 'color: green';
        case 'Waiting':
            return 'color: yellow';
        case 'Off-line':
            return 'color: red';
    }
};

async function getListDispositives() {
    if(auth.isAuthenticated()) {
        http.get(`NimbusUserUnion/getAllUserNimbusBoxes`, {
            headers: { Authorization: `Bearer ${ auth.token }` }
        }).then(response => {
            listProductsNimbus.value = response.data.data;
        }).catch(error => {
            console.log(error);
        });
    } else router.push('/')
}

onMounted(() => {
    getListDispositives();
});
</script>

<template>
    <div>
        <div class="h-full w-full flex items-center  flex-col">
            <div class="container">
                <div v-for="(item) in listProductsNimbus">
                    <div class="flex flex-col mx-auto text-[#FFFFFF] bg-[#1C1A1A] rounded-3xl my-10 border-4  border-solid border-[#04ACE6] shadow-lg hover:shadow-3xl transition-all hover:scale-105">
                        <div class="p-2 mx-10 my-10 justify-between px-5 py-2 text-sm flex lg:text-lg">
                            <div class="">
                                <p>id do produto:</p>
                                <p class="font-semibold text-center">{{ item.idUserNimbus }}</p>
                            </div>
                            <div class="">
                                <p>Nome do produto:</p>
                                <p class="font-semibold">{{ item.nmProduto }}</p>
                            </div>
                            <div class="">
                                <p>Status:</p>
                                <p class="font-semibold" :style="getStatusColor(item.stsProduto)">{{ item.stsProduto }}</p>
                            </div>
                            <div class="flex">
                                <button  @click="alterarStatusPage(item.idUserNimbus)" class="bg-[#046de6] hover:bg-blue-500 text-white font-bold py-2 px-4 rounded ">
                                    Acessar
                                    <font-awesome-icon icon="arrow-right" />
                                </button>             
                            </div>
    
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<style scoped>
</style>