<script setup>
import { ref, reactive, onMounted } from 'vue';
import http from '@/services/http'; 
import { useRouter } from 'vue-router'
import { useAuth } from '@/stores/auth';

const router = useRouter();
const auth = useAuth();
const listCadastroItems = ref([
    { title: 'Nome', dataReference: 'nmNomeUsuario' },
    { title: 'Email', dataReference: 'nmNomeEmailUsuario' },
    { title: 'Telefone', dataReference: 'nrTelefone' },
    { title: 'Senha', dataReference: 'nmSenha' },
    { title: 'Repita a senha', dataReference: 'pswRepeatUser' },
]);
const objectMapPostSingUp = reactive({
    nmNomeUsuario: null,
    nmNomeEmailUsuario: null,
    nrTelefone: null,
    nmSenha: null,
    pswRepeatUser: null
});
const inputsErrors = reactive({
    nmNomeUsuario: [],
    nmNomeEmailUsuario: [],
    nrTelefone: [],
    nmSenha: [],
    pswRepeatUser: []
});
const typeInput = (name) => {
    switch (name) {
        case 'Name':
            return 'text';
        case 'Email':
            return 'email';
        case 'Telefone': 
            return 'tel';
        case 'Senha':
        case 'Repita a senha':
            return 'password'
    }
};
const windowHeight = ref(null);

onMounted(() => {
    addEventListener('resize', () => {
        windowHeight.value = window.innerHeight;
    });
})
const createNewUser = () => {
    const keys = Object.keys(objectMapPostSingUp);
    keys.forEach((key) => {
        if ((!objectMapPostSingUp[key] || objectMapPostSingUp[key] == '') && !inputsErrors[key].includes('O campo é obrigatório')) {
            let error = 'O campo é obrigatório';
            inputsErrors[key].push(error);
        } else {
            if (inputsErrors[key].includes('O campo é obrigatório') && !(!objectMapPostSingUp[key] || objectMapPostSingUp[key] == '') ) {
                let index = inputsErrors[key].indexOf('O campo é obrigatório');
                if (index > -1) {
                    inputsErrors[key].splice(index, 1);
                }
            }  

            if (!inputsErrors[key].includes('O campo é obrigatório')) {
                if (key == 'nmNomeEmailUsuario') {
                    let regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    
                    if (inputsErrors[key].includes('O campo precisa ser um email válido') && objectMapPostSingUp[key].match(regEx)) {
                        let index = inputsErrors[key].indexOf('O campo precisa ser um email válido');
                        if (index > -1) {
                            inputsErrors[key].splice(index, 1);
                        }
                    }

                    if (!objectMapPostSingUp[key].match(regEx) && !inputsErrors[key].includes('O campo precisa ser um email válido')) {
                        let error = 'O campo precisa ser um email válido';
                        inputsErrors[key].push(error);
                    }
                }
                
                if (key == 'nrTelefone') {
                    if (inputsErrors[key].includes('O número de telefone é grande/pequeno de mais') && objectMapPostSingUp[key].length == 11) {
                        let index = inputsErrors[key].indexOf('O número de telefone é grande/pequeno de mais');
                        if (index > -1) {
                            inputsErrors[key].splice(index, 1);
                        }
                    }
                    if ((objectMapPostSingUp[key].length != 11 && objectMapPostSingUp[key].length != 0) && !inputsErrors[key].includes('O número de telefone é grande/pequeno de mais')) {
                        let error = 'O número de telefone é grande/pequeno de mais';
                        inputsErrors[key].push(error);
                    }
                }
                
                if (key == 'pswRepeatUser') {
                    if (inputsErrors[key].includes('As senhas não correspondem') && objectMapPostSingUp[key] == objectMapPostSingUp['nmSenha']) {
                        let index = inputsErrors[key].indexOf('As senhas não correspondem');
                        if (index > -1) {
                            inputsErrors[key].splice(index, 1);
                        }
                    }

                    if (objectMapPostSingUp['pswRepeatUser'] !== objectMapPostSingUp['nmSenha'] && !inputsErrors[key].includes('As senhas não correspondem')) {
                        let error = 'As senhas não correspondem';
                        inputsErrors[key].push(error);
                    }
                }
            }
        }
    });

    let errorCount = 0;
    Object.keys(inputsErrors).forEach((key) => {
        
        if (inputsErrors[key].length > 0) {
            errorCount++;
        }

    });

    if (errorCount <= 0) {
        let requestData = {...objectMapPostSingUp};
        
        delete requestData['pswRepeatUser'];
        requestData.nrTelefone = requestData.nrTelefone.toString();

        singUp(requestData);
    }
};

const hasCadastroError = ref(false);
const msgError = ref(null)
async function singUp(postData) {
  
    await http.post('/UserNimbus/postUserNimbus', postData).then((response) => {
        hasCadastroError.value = true;
        singIn(postData.nmNomeEmailUsuario, postData.nmSenha);
    }).catch(error => {
        hasCadastroError.value = true;
        msgError.value = error.response.data.msg;
    }) 
    
}

async function singIn(userEmail, userPassword) {
    await http.get(`UserNimbus/SignIn/${userEmail}/${userPassword}`).then(response => {
        const { data } = response;
        auth.setToken(data.token);
        auth.setUser(data.user);
        router.push('/homelogado');
    }).catch(error => {
        console.log(error);
    })
};

</script>

<template>
    <div class="h-full w-screen flex items-center">
        <div class="w-[80%] my-0 mx-auto flex gap-10 h-full sm:items-center">
            <div class="xl:block hidden w-full bg-[url('../assets/img/waterBoatImage.jpg')] h-[80%] rounded-3xl bg-cover bg-center bg-no-repeat"></div>
            <form class="w-full h-[100%] flex items-center justify-center" onsubmit="return false">
                <div class="border max-h-[100%] w-[25rem] border-gray-300 rounded-3xl xl:p-10 p-5 ">
                    <p class="mb-4 text-base xl:text-lg text-center font-semibold">Cadastre-se</p>
                    <p v-if="hasCadastroError" class="text-center text-red-700 font-semibold">{{ msgError }}</p>
                    <div v-for="(item) in listCadastroItems">
                        <label class="text-base xl:text-lg font-semibold" :class="(inputsErrors[item.dataReference].length > 0) ? 'text-red-700' : '' " :for="item.title">{{ item.title }} <span v-if="inputsErrors[item.dataReference].length > 0" class="text-base"> - {{ inputsErrors[item.dataReference][0] }} </span></label>
                        <input class="border rounded-lg p-1 w-full mb-3" :class="(inputsErrors[item.dataReference].length > 0) ? 'border-red-700' : 'border-gray-300' " :type="typeInput(item.title)" v-model="objectMapPostSingUp[item.dataReference]">
                    </div>
                    <div class="mt-3">
                        <input name="checkBox" type="checkbox" class="me-2">
                        <label for="checkBox" class="font-semibold text-sm sm:text-base">Li e concordo com os <a href="#" class="text-blue-400 underline">termos de serviço</a></label>
                    </div>
                    <div class="w-full flex items-center justify-center mt-6">
                        <button class="text-white text-lg bg-blue-800 py-1 px-7 rounded-3xl" :onclick="createNewUser">Cadastrar-se</button>
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