<script setup>
import { onMounted, ref, onBeforeUnmount, computed, isMemoSame } from 'vue';
import { useAuth } from '@/stores/auth';
import http from '@/services/http';
import Chart from 'chart.js/auto';
import { useRouter } from 'vue-router';

const auth = useAuth();
const router = useRouter();
const listNimbusValues = ref(null);
const listNimbus = ref(null);
const windowWidth = ref();
const isTablet = computed(() => {
    return windowWidth.value <= 1280 ? true : false;
});

async function getAllNimbusProductsValues() {
    if(auth.isAuthenticated()) {
        await http.get('NimbusBox/getAllNimbusDataMedium', {
            headers: { Authorization: `Bearer ${auth.token}` }
        }).then(response => {
            listNimbusValues.value = response.data.data;
            setTemp_X_UmidateGeneral();
            setPress_X_Vel_AguaGeneral();
            setLuz_X_LuzInfra_LuzUv();
        }).catch(error => {
            console.log(error);
        });
    } else router.push('/');
};

async function getAllNimbusProducts() {
    if(auth.isAuthenticated()) {
        await http.get('NimbusUserUnion/getAllUserNimbusBoxes', {
            headers: { Authorization: `Bearer ${auth.token}` }
        }).then(response => {
            listNimbus.value = response.data.data;
        }).catch(error => {
            console.log(error);
        });
    } else router.push('/');
};

function setLuz_X_LuzInfra_LuzUv() {
    const _Luz_X_LuzInfra_LuzUvLabel = [];
    const _Luz_X_LuzInfra_LuzUvLuz = [];
    const _Luz_X_LuzInfra_LuzUvLuzInfra = [];
    const _Luz_X_LuzInfra_LuzUvLuzUv = [];
    for (var i = 0; i < listNimbusValues.value.length; i++) {
        _Luz_X_LuzInfra_LuzUvLabel.push(`Mês ${i + 1}`);
        _Luz_X_LuzInfra_LuzUvLuz.push(listNimbusValues.value[i].nrEspectroLuz);
        _Luz_X_LuzInfra_LuzUvLuzInfra.push(listNimbusValues.value[i].nrInfraVermelho);
        _Luz_X_LuzInfra_LuzUvLuzUv.push(listNimbusValues.value[i].nrLuzUv);
    };

    const myChartLuz_X_LuzInfra_LuzUvData = {
        labels: _Luz_X_LuzInfra_LuzUvLabel,
        datasets: [{
            type: 'line',
            label: 'Espectro de luz',
            backgroundColor: 'rgb(23 37 84)',
            borderColor: 'rgb(23 37 84)',
            data: _Luz_X_LuzInfra_LuzUvLuz
        }, {
            type: 'line',
            label: 'Luz infravermelha',
            backgroundColor: 'red',
            borderColor: 'red',
            data: _Luz_X_LuzInfra_LuzUvLuzInfra
        }, {
            type: 'line',
            label: 'Luz uv',
            backgroundColor: 'rgb(109 40 217)',
            borderColor: 'rgb(109 40 217)',
            data: _Luz_X_LuzInfra_LuzUvLuzUv
        }]
    };
    const myChartLuz_X_LuzInfra_LuzUvConfig = {
        type: 'line',
        data: myChartLuz_X_LuzInfra_LuzUvData,
        options: {
            plugins: {
                title: {
                    text: 'Espectro de luz X Luz infravermelho X Luz uv',
                    display: true,
                    color: 'black',
                },
                legend:{
                    display: true,
                labels: {
                color: 'black',
                }
                }
            },
            scales:{
                x: {
                ticks: {
                    color:'black'
                }
                },
                y:{
                ticks:{
                    color:'black'
                }
                }
            }
        },
    };
    const myChartHtmlLuz_X_LuzInfra_LuzUv = document.getElementById('myChartHtmlLuz_X_LuzInfra_LuzUv');
    window.myChartLuz_X_LuzInfra_LuzUv = new Chart(myChartHtmlLuz_X_LuzInfra_LuzUv, myChartLuz_X_LuzInfra_LuzUvConfig)
}

function setPress_X_Vel_AguaGeneral() {
    const _Press_X_Vel_AguaLabel = [];
    const _Press_X_Vel_AguaPress = [];
    const _Press_X_Vel_AguaVel = [];
    const _Press_X_Vel_AguaAgua=[];
    for (var i = 0; i < listNimbusValues.value.length; i++) {
        _Press_X_Vel_AguaLabel.push(`Mês ${i + 1}`);
        _Press_X_Vel_AguaPress.push(listNimbusValues.value[i].nrPressao);
        _Press_X_Vel_AguaVel.push(listNimbusValues.value[i].nrVelVento);
        _Press_X_Vel_AguaAgua.push(listNimbusValues.value[i].nrAguaAltura);
    };

    const myChartPress_X_Vel_AguaData = {
        labels: _Press_X_Vel_AguaLabel,
        datasets: [{
            type: 'line',
            label: 'Precipitação',
            backgroundColor: 'black',
            borderColor: 'black',
            fill: false,
            data: _Press_X_Vel_AguaAgua
        },{
            type: 'bar',
            label: 'Precisão',
            backgroundColor: 'rgb(23 37 84)',
            borderColor: 'rgb(23 37 84)',
            data: _Press_X_Vel_AguaPress,
        }, {
            type: 'bar',
            label: 'Velocidade do vento',
            backgroundColor: 'rgb(34 211 238)',
            borderColor: 'rgb(34 211 238)',
            data: _Press_X_Vel_AguaVel
        }],
    };
    const myChartPress_X_Vel_AguaConfig = {
        type: 'line',
        data: myChartPress_X_Vel_AguaData,
        options: {
            plugins: {
                title: {
                    text: 'Pressão X Velocidade do vento X Precipitação',
                    display: true,
                    color: 'black',
                },
                legend:{
                    display: true,
                labels: {
                color: 'black',
                }
                }
            },
            scales:{
            x: {
            ticks: {
                color:'black'
            }
            },
            y:{
            ticks:{
                color:'black'
            }
            }
            }
        },
    };

    const myChartHtmlPress_X_Vel_Agua = document.getElementById('myChartHtmlPress_X_Vel_Agua');
    window.myChartPress_X_Vel_Agua = new Chart(myChartHtmlPress_X_Vel_Agua, myChartPress_X_Vel_AguaConfig);
};

function setTemp_X_UmidateGeneral() {
    const _Temp_X_UmidadeLabel = [];
    const _Temp_X_UmidadeDataTemp = [];
    const _Temp_X_UmidadeDataUmidade = [];
    for (var i = 0; i < listNimbusValues.value.length; i++) {
        _Temp_X_UmidadeLabel.push(`Mês ${i + 1}`);
        _Temp_X_UmidadeDataTemp.push(listNimbusValues.value[i].nrTemperatura);
        _Temp_X_UmidadeDataUmidade.push(listNimbusValues.value[i].nrUmidade);
    };

    const myChartTemp_X_UmidadeData = {
        labels: _Temp_X_UmidadeLabel,
        datasets: [
            {
                label: 'Temperatura',
                data: _Temp_X_UmidadeDataTemp,
                borderColor: 'red',
                backgroundColor : 'red'
            },
            {
                label: 'Umidade',
                data: _Temp_X_UmidadeDataUmidade,
                borderColor: 'blue',
                backgroundColor: 'blue'
            }
        ]
    }
    const myChartTemp_X_UmidadeConfig = {
        type: 'line',
        data: myChartTemp_X_UmidadeData,
        options: {
        responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                        display: true,
                        labels: {
                        color: 'black',
                        }
                },
                title: {
                    display: true,
                    text: 'Temperatura X Umidade',
                    color: 'black'
                },
                },
                scales:{
                x: {
                ticks: {
                    color:'black'
                }
                },
                y:{
                ticks:{
                    color:'black'
                }
                }
            }
        },
    };

    const myChartHtmlTemp_X_Umidade = document.getElementById('myChartHtmlTemp_X_Umidade');
    window.myChartTemp_X_Umidade = new Chart(myChartHtmlTemp_X_Umidade, myChartTemp_X_UmidadeConfig);
};


onMounted(() => {
    getAllNimbusProductsValues();
    getAllNimbusProducts();

    windowWidth.value = window.innerWidth;
    window.addEventListener('resize', () => {
        windowWidth.value = window.innerWidth;
    })
});

onBeforeUnmount(() => {
    window.myChartTemp_X_Umidade.destroy();
    window.myChartPress_X_Vel_Agua.destroy();
    window.myChartLuz_X_LuzInfra_LuzUv.destroy();
});

function getStatusColor(status) {
    switch(status) {
        case 'On-line':
            return 'color: green';
        case 'Off-line':
            return 'color: red';
        case 'Waiting':
            return 'color: rgb(250 204 21)'
    }
};
</script>

<template>
    <div class="flex flex-col h-full" :class="isTablet ? 'gap-10': ''">
        <div class="justify-center grid gap-10 " :class="isTablet ? 'grid-cols-1' : 'grid-cols-2'">
            <div>
                <div :class="isTablet ? 'w-full flex justify-center ': ''" class="bg-stone-300 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105">
                    <canvas id="myChartHtmlTemp_X_Umidade">oi</canvas>
                </div>
            </div>
            <div>
                <div :class="isTablet ? 'w-full flex justify-center' : ''" class="bg-stone-300 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105">
                    <canvas id="myChartHtmlPress_X_Vel_Agua"></canvas>
                </div>
            </div>
        </div>
        <div class="w-full h-full grid gap-10" :class="isTablet ? 'grid-cols-1' : 'grid-cols-2'">
            <div class="h-full flex items-center justify-center">
                <div class="border-2 border-gray-300 rounded-xl w-full hover:scale-105 transition-all">
                    <div class="grid grid-cols-3 text-center font-bold p-3 text-white bg-slate-800 rounded-t-xl">
                        <p>ID do produto</p>
                        <p>Nome do produto</p>
                        <p>Status do produto</p>
                    </div>
                    <div class="flex flex-col bg-slate-800">
                        <div class="grid grid-cols-3 text-center text-white font-semibold p-3" v-for="(item) in listNimbus">
                            <p>{{ item.idUserNimbus }}</p>
                            <p>{{ item.nmProduto }}</p>
                            <p :style="getStatusColor(item.stsProduto)">{{ item.stsProduto }}</p>
                        </div>
                    </div>
                    <div class="bg-slate-800 p-6 rounded-b-xl"></div>
                </div>
            </div>
            <div class="my-auto">
                <div class="bg-stone-300 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105 mt-5 mb-16" :class="isTablet ? 'justify-center' : ''">
                    <canvas id="myChartHtmlLuz_X_LuzInfra_LuzUv"></canvas>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

</style>