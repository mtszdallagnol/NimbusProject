<script setup>
import { ref, onMounted, onBeforeUnmount, reactive } from 'vue';
import http from '@/services/http';
import { useAuth } from '@/stores/auth';
import Chart from 'chart.js/auto';
import { useRouter } from 'vue-router';
import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr';

const currentIdRequesition = ref(props.currentIDProduto);
const auth = useAuth();
const router = useRouter();
const optionsMediumAllNimbusWithCurrentId = reactive([]);
const optionsRecentAllNimbusWithCurrentId = reactive([]);
const windowWidth = ref();
const signalRConnection = ref();
const config = ref();
const configUmidade = ref();
const configVelVento = ref();

onMounted(() => {
    getDataMediumCurrentIdNimbus();

    windowWidth.value = window.innerWidth;
    window.addEventListener('resize', () => {
        windowWidth.value = window.innerWidth;
    });

    configureSignalR();
});

function configureSignalR() {
    signalRConnection.value = new HubConnectionBuilder()
        .withUrl("http://localhost:5290/stateResultsHub", {
            withCredentials: false
        }).configureLogging(LogLevel.Information)
        .build();
    signalRConnection.value.start().then(() => {
        signalRConnection.value.on("RecievedMessage", (event) => {
            console.log(event);
            if (event.currentIdNimbusProduct == currentIdRequesition.value) {
                let index = 0;
                for (var i = 0; i < event.listMedium5days.length; i++) {
                    if (event.listMedium5days[i].idUserNimbus == currentIdRequesition.value) {
                        index = i;
                    };
                } 
                optionsMediumAllNimbusWithCurrentId.value = event.listMedium5days[index];
                optionsRecentAllNimbusWithCurrentId.value = event.nimbusMostRecent;

                setTemperatura();
                setUmidade();
                setVelVento();

                console.log(config.value.data);

                window.myChart.data.datasets = config.value.data.datasets
                window.myChart2.data.datasets = configUmidade.value.data.datasets
                window.myChart3.data.datasets = configVelVento.value.data.datasets

                window.myChart.data.labels = config.value.data.labels;
                window.myChart2.data.labels = configUmidade.value.data.labels;
                window.myChart3.data.labels = configVelVento.value.data.labels;

                window.myChart.update('none');
                window.myChart2.update('none');
                window.myChart3.update('none');
            }
        })
    });
};

function getDataMediumCurrentIdNimbus() {
    if(auth.isAuthenticated()){
         http.get(`NimbusBox/getAllNimbusMediumById?idUserNimbus=${currentIdRequesition.value}`, {
            headers: {
                Authorization: `Bearer ${ auth.token }`
            }
        }).then(response => {
            optionsMediumAllNimbusWithCurrentId.value = response.data.data.listMediumMonts;
            optionsRecentAllNimbusWithCurrentId.value = response.data.data.nimbusMostRecent;
            
            setTemperatura();
            setUmidade();
            setVelVento();

            const myChartHtml = document.getElementById('myChartHtml');
            window.myChart = new Chart(myChartHtml, config.value)

            const myChartHtml2 = document.getElementById('myChartHtml2');
            window.myChart2 = new Chart(myChartHtml2, configUmidade.value);

            const myChartHtml3 = document.getElementById('myChartHtml3');
            window.myChart3 = new Chart(myChartHtml3, configVelVento.value);
        })
    }else router.push('/')
};

onBeforeUnmount(() => {
    window.myChart.destroy();
    window.myChart2.destroy();
    window.myChart3.destroy();
});

const props = defineProps({
    currentIDProduto: Number,
});

function setTemperatura() {
    const _TemperaturaLabel = [];
    const _Temperatura = [];
    for (var i = 0; i < optionsMediumAllNimbusWithCurrentId.value.length; i++) {
        
        _TemperaturaLabel.push(`Mês ${i + 1}`);

        _Temperatura.push(optionsMediumAllNimbusWithCurrentId.value[i].nrTemperatura);
    };
    
//const labels = ["Mês 1", "Mês 2", "Mês 3", "Mês 4", "Mês 5"];
const myChartTemperaturaData = {
    labels: _TemperaturaLabel,
    datasets: [{
        label: 'Total',
        //data: [65, 59, 80, 81, 56],
        data: _Temperatura.reverse(),
        fill: false,
        borderColor: 'rgb(173, 216, 230)',
        tension: 0.1,
    }]
};   
config.value = {
    type: 'line',
    data: myChartTemperaturaData,
    options: {
    plugins: {
        legend: {
            display: true,
            labels: {
                color: 'rgb(173, 216, 230)',
            }
        }
    },
    scales:{
            x: {
            ticks: {
                color:'white'
            }
            },
            y:{
            ticks:{
                color:'white'
            }
            }

        }
    }
};
}

//const labels2 = ["Mês 1", "Mês 2", "Mês 3", "Mês 4", "Mês 5"];
function setUmidade() {
    const _UmidadeLabel = [];
    const _Umidade = [];
    for (var i = 0; i < optionsMediumAllNimbusWithCurrentId.value.length; i++) {
        
        _UmidadeLabel.push(`Mês ${i + 1}`);

        _Umidade.push(optionsMediumAllNimbusWithCurrentId.value[i].nrUmidade);
    };

const myChartUmidadeData = {
    labels: _UmidadeLabel,
    datasets: [{
        label: 'Total',
        //data: [65, 59, 80, 81, 56],
        data: _Umidade.reverse(),
        fill: false,
        borderColor: 'rgb(173, 216, 230)',
        tension: 0.1,
    }]
};   
configUmidade.value = {
    type: 'line',
    data: myChartUmidadeData,
    options: {
    plugins: {
        legend: {
            display: true,
            labels: {
                color: 'rgb(173, 216, 230)',
            }
        }
    },
    scales:{
            x: {
            ticks: {
                color:'white'
            }
            },
            y:{
            ticks:{
                color:'white'
            }
            }

        }
    }
};
}

function setVelVento() {
    const _VelVentoLabel = [];
    const _VelVento = [];
    for (var i = 0; i < optionsMediumAllNimbusWithCurrentId.value.length; i++) {
        
        _VelVentoLabel.push(`Mês ${i + 1}`);

        _VelVento.push(optionsMediumAllNimbusWithCurrentId.value[i].nrVelVento);
    };

const myChartVelVentoData = {
    labels: _VelVentoLabel,
    datasets: [{
        label: 'Total',
        //data: [65, 59, 80, 81, 56],
        data: _VelVento.reverse(),
        fill: false,
        borderColor: 'rgb(173, 216, 230)',
        tension: 0.1,
    }]
};   

configVelVento.value = {
    type: 'line',
    data: myChartVelVentoData,
    options: {
    plugins: {
        legend: {
            display: true,
            labels: {
                color: 'rgb(173, 216, 230)',
            }
        }
    },
    scales:{
            x: {
            ticks: {
                color:'white'
            }
            },
            y:{
            ticks:{
                color:'white'
            }
            }

        }
    }

};
}

const emit = defineEmits(['updateVoltar']);
const voltarPage = () => {
    emit('updateVoltar')
};
</script>

<template> 
<div>
    <div>
        <div class="flex">
                    <button  @click="voltarPage" class="bg-[#046de6] hover:bg-blue-500 text-white font-bold py-2 px-4 rounded">
                        <font-awesome-icon icon="arrow-left" />
                        Voltar
                        </button>             
             </div>
             <div class="flex flex-col items-center mt-20 font-bold text-5xl ">
                <p>Média mensal do produto</p>
             </div>
        <div class="flex flex-col items-center">
                <div class="grid grid-cols-1 flex-wrap gap-12 justify-center mt-20 w-full mx-6 xl:grid-cols-3">
                    
                    <div class="bg-gray-600 grafico-2 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105">
                        <p>Temperatura</p>
                        <canvas class="gra-2" id="myChartHtml"></canvas>
                    </div>
    
                    <div class="bg-gray-600 grafico-2 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105">
                        <p>Umidade</p>
                        <canvas class="gra-2" id="myChartHtml2"></canvas>
                    </div>
    
                    <div class="bg-gray-600 grafico-3 text-white font-bold flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-3xl transition-all hover:scale-105 mr-5" >
                        <p>Velocidade</p>
                        <canvas class="gra-3" id="myChartHtml3"></canvas>
                    </div>
    
                </div>
        </div> 
    </div> 
    

    <div v-if="optionsRecentAllNimbusWithCurrentId.value" class="mt-14">
        <div class="grid grid-flow-col xl:grid-flow-row ">            
            <div class=" grid grid-flow-row xl:grid-flow-col xl:grid-cols-8 text-center font-bold p-3 text-slate-200 bg-gray-600 rounded-l-xl xl:rounded-bl-none xl:rounded-t-xl xl:rounded-b-0">
                <p class="xl:border-none border-b border-gray-300 mb-2">Temperatura</p>
                <p class="xl:border-none border-b border-gray-300 mb-2">Pressão</p>
                <p class="xl:border-none border-b border-gray-300 mb-2">Umidade</p>  
                <p class="xl:border-none border-b border-gray-300 mb-2">Velocidade do vento</p>
                <p class="xl:border-none border-b border-gray-300 mb-2">Luz UV</p>
                <p class="xl:border-none border-b border-gray-300 mb-2">Luz infravermelho</p>
                <p class="xl:border-none border-b border-gray-300 mb-2">Precipitação</p>
                <p>Spectro de Luz</p>
            </div>

            <div class="grid xl:grid-flow-col xl:grid-cols-8 xl:rounded-none rounded-r-xl text-center bg-gray-200 font-semibold p-3">
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrTemperatura}} ºC</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrPressao}} ATM</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrUmidade}}%</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrVelVento}} Km/h</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrLuzUv}} mW/cm²</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrInfraVermelho}} LUX</p>
                <p class="xl:border-none mb-2 border-b border-gray-700">{{optionsRecentAllNimbusWithCurrentId.value.nrAguaAltura}} mm</p>
                <p>{{optionsRecentAllNimbusWithCurrentId.value.nrEspectroLuz}} LUX</p>
            </div>
        
            <div class="hidden bg-gray-600 xl:block p-6 xl:rounded-b-xl"></div>
        </div>
    </div>
</div>
        
</template>

<style scoped>

</style>