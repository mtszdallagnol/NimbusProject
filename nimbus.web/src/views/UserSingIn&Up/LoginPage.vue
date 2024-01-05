<script setup>
import { ref, reactive } from 'vue';
import http from '@/services/http';
import { useAuth } from '@/stores/auth';
import router from '@/router';

const auth = useAuth();
const listCadastroItems = ref([
    { title: 'Email', dataReference: 'emailUser' },
    { title: 'Senha', dataReference: 'pswUser' },
]);
const objectMapPostSingIn = reactive({
    emailUser: null,
    pswUser: null,
});
const inputsErrors = reactive({
    emailUser: [],
    pswUser: [],
});

const typeInput = (name) => {
    switch (name) {
        case 'Email':
            return 'email';
        case 'Senha':
            return 'password'
    }
};
const loginUser = () => {
    const keys = Object.keys(objectMapPostSingIn);
    keys.forEach((key) => {
        if ((!objectMapPostSingIn[key] || objectMapPostSingIn[key] == '') && !inputsErrors[key].includes('O campo é obrigatório')) {
            let error = 'O campo é obrigatório';
            inputsErrors[key].push(error);
        } else {
            if (inputsErrors[key].includes('O campo é obrigatório') && !(!objectMapPostSingIn[key] || objectMapPostSingIn[key] == '') ) {
                let index = inputsErrors[key].indexOf('O campo é obrigatório');
                if (index > -1) {
                    inputsErrors[key].splice(index, 1);
                }
            }  

            if (!inputsErrors[key].includes('O campo é obrigatório')) {
                if (key == 'emailUser') {
                    let regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    
                    if (inputsErrors[key].includes('O campo precisa ser um email válido') && objectMapPostSingIn[key].match(regEx)) {
                        let index = inputsErrors[key].indexOf('O campo precisa ser um email válido');
                        if (index > -1) {
                            inputsErrors[key].splice(index, 1);
                        }
                    }

                    if (!objectMapPostSingIn[key].match(regEx) && !inputsErrors[key].includes('O campo precisa ser um email válido')) {
                        let error = 'O campo precisa ser um email válido';
                        inputsErrors[key].push(error);
                    }
                }
            }
        }
    });

    let countError = 0;
    Object.keys(inputsErrors).forEach((key) => {
        if (inputsErrors[key].length > 0) {
            countError++;
        }
    });

    if (countError <= 0) {
        singIn();
    }
};

const hasLoginError = ref(false);
const msgError = ref()
async function singIn() {
    
    await http.get(`UserNimbus/SignIn/${objectMapPostSingIn.emailUser}/${objectMapPostSingIn.pswUser}`)
            .then(response => {
                const { data } = response;
                auth.setToken(data.token);
                auth.setUser(data.user);
                hasLoginError.value = false;
                router.push('/homelogado');
            }).catch(error => {
                hasLoginError.value = true;
                msgError.value = error.response.data.msg;
            })
                      
}
</script>

<template>
    <div class="h-full w-screen flex items-center">
        <div class="w-[80%] my-0 mx-auto flex gap-10 h-full sm:items-center">
            <div class="xl:block hidden w-full bg-[url('../assets/img/waterBoatImage.jpg')] h-[80%] rounded-3xl bg-cover bg-center bg-no-repeat"></div>
            <form class="w-full h-full flex items-center justify-center" onsubmit="return false">
                <div class="border max-h-[100%] w-[25rem] border-gray-300 rounded-3xl xl:p-10 p-5">
                    <p class="mb-4 text-base xl:text-lg text-center font-semibold">Login</p>
                    <p class="m-0 text-red-700 text-center font-semibold" v-if="hasLoginError">{{ msgError }}</p>
                    <div v-for="(item) in listCadastroItems">
                        <label class="text-base xl:text-lg font-semibold" :class="(inputsErrors[item.dataReference].length > 0) ? 'text-red-700' : '' " :for="item.title"> {{ item.title }} <span v-if="inputsErrors[item.dataReference].length > 0" class="text-base"> - {{ inputsErrors[item.dataReference][0] }} </span></label>
                        <input class="border rounded-lg p-1 w-full mb-3" :class="(inputsErrors[item.dataReference].length > 0) ? 'border-red-700' : 'border-gray-300' " :type="typeInput(item.title)" v-model="objectMapPostSingIn[item.dataReference]">
                    </div>
                    <div class="w-full flex items-center justify-center mt-6">
                        <button class="text-white text-lg bg-blue-800 py-1 px-7 rounded-3xl" @click="loginUser">Login</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<style scoped>
    input {
        outline: none;
    }
</style>