<script setup>
    import { ref } from 'vue';
    
    const HomeCardsItems = ref(['Informações metereológicas em tempo real da sua região.', 'Dados sobre temporadas passadas.', 'Dados sobre temporadas passadas.'])
</script>

<template>
    <main class="container mx-auto  mt-10 md:mt-20 lg:mt-40 p-5">
        <div class="flex flex-col items-center">
            <p class="font-bold text-center text-6xl">Projeto <span class="gradientText">Nimbus</span> </p>
            <span class="text-center font-bold mt-8 max-w-[35rem]">Nós da NIMBUS buscamos desenvolver uma estação meteorológica capaz de coletar dados meteorológicos em tempo real para fins de análise e previsão do tempo.</span>
            <div class="flex flex-wrap gap-16 justify-center mt-28">
                <div v-for="(item, index) in HomeCardsItems" class="flex flex-col items-center rounded-2xl border-[1px] border-solid border-[rgba(17, 24, 39, 0.12)] hover:shadow-lg transition-all hover:scale-105">
                    <img :src="require('../assets/img/Card' + (index + 1) + 'Image.png')" class="rounded-t-2xl object-fill w-full">
                    <span class="p-3 font-semibold text-base max-w-[17rem] text-center">{{ item }}</span>
                </div>
            </div>
        </div>

        <div id="produto" class="grid lg:grid-cols-2 grid-cols-1 mt-24 md:mt-48">
            <div class="flex flex-col justify-start lg:mx-auto">
                <p class="font-bold lg:text-4xl text-3xl mb-8">Sobre o produto Nimbus!</p>
                <span class="lg:max-w-[26rem] lg:text-xl md:font-normal font-medium md:text-lg sm:text-base text-base">O produto Nimbus é uma estação meteorológica compacta que fornece dados detalhados em tempo real sobre as condições climáticas em áreas específicas, como quintais, jardins e pequenas propriedades agrícolas. Ele coleta informações abrangentes, como temperatura, umidade, velocidade e direção do vento, precipitação e radiação solar. Os usuários podem acessar esses dados remotamente por meio de um aplicativo ou plataforma online, o que torna o monitoramento das condições climáticas acessível de qualquer lugar. Além disso, o Nimbus apresenta gráficos de fácil interpretação, tornando-o uma ferramenta valiosa para tomar decisões informadas com base em dados meteorológicos locais precisos.</span>
            </div>
            <div class="flex justify-center lg:mt-0 mt-10">
                <img src="../assets/img/ImageProjeto.png" class="xl:max-w-xl md:max-w-lg">
            </div>
        </div>
        <div id="projeto" class="mt-16 md:mt-36 w-full mx-auto flex flex-col py-14 md:py-20 lg:py-28 rounded-3xl xl:max-w-[85rem] md:bg-[url(../assets/img/GeeneralMeeting.jpg)] bg-[cover]">
            <div class="bg-[#FFFFF7] md:w-60 w-full md:p-5 rounded-r-xl md:self-start self-center">
                <span class="font-bold md:text-base text-3xl">Sobre o projeto Nimbus!</span>
            </div>
            <div class="max-w-[45rem] text-start mt-5 md:mt-16 md:mx-5 px-0 md:px-5 py-3 md:bg-[rgba(0,0,0,0.5)] rounded-lg">
                <span class="md:text-lg text-base font-medium text-black md:text-white">O Projeto Nimbus é uma notável estação meteorológica projetada com a finalidade de simplificar a vida dos fazendeiros, oferecendo dados meteorológicos precisos e cruciais para aprimorar a gestão de suas operações agrícolas. Esta sofisticada estação meteorológica representa um exemplo exemplar de tecnologia de agricultura de precisão, cuidadosamente elaborada para satisfazer as necessidades individuais e específicas dos agricultores, capacitando-os a tomar decisões informadas que têm um impacto positivo em suas atividades cotidianas.</span>
            </div>
            <img class="md:hidden rounded-xl mt-8" src="../assets/img/GeeneralMeeting.jpg">
        </div>
        <div id="contato" class="grid grid-cols-1 md:grid-cols-2 justify-items-center md:mt-36 mt-24 mx-auto">
            <div class="rounded-3xl self-center border-solid border border-[rgba(0, 0, 0, 0.12)] w-80 p-5">
                <p class="text-center font-medium mb-5">Fale conosco</p>
                <p class="font-medium">Nome</p>
                <input class="mb-3 p-1 border border-solid border-[rgba(0, 0, 0, 0.12)] rounded-md focus:outline-blue-400 block w-full">
                <p class="font-medium">Assunto</p>
                <input class=" mb-3 p-1 border border-solid border-[rgba(0, 0, 0, 0.12)] rounded-md focus:outline-blue-400 block w-full"/>
                <p class="font-medium">Descrição</p>
                <textarea class="border border-solid border-[rgba(0, 0, 0, 0.12)] rounded-md focus:outline-blue-400 block w-full" rows="12" />
                <div class="flex justify-center mt-5">
                    <button class="py-2 px-10 bg-blue-800 text-white rounded-2xl">Enviar</button>
                </div>
            </div>
            <div>
                <p class="mb-7 md:mt-0 mt-10 md:mb-10 font-medium">Contate-nos</p>

                <p class="font-medium">Email: Nimbus@gmail.com.br</p>
                <p class="font-medium">Telefone: (41)99999-9999</p>
            </div>
        </div>
    </main>
    <footer class="border-t border-t-[#E5E7EB] border-solid grid grid-cols-1 md:grid-cols-2 justify-items-stretch px-10 py-24 mt-24 md:mt-36">
        <div class="grid grid-rows-2 md:justify-items-start justify-items-center">
            <div class="flex">
                <img src="../assets/img/Logo.svg" width="50">
                <span class="font-bold self-center ms-8 text-xl max-w-[15rem]">A maneira mais rápida de ver a meteorologia</span>
            </div>
            <p class="md:ms-[calc(2rem+50px)] mt-5 text-start"><font-awesome-icon icon="fa-brands fa-twitter" class="me-5"/> <font-awesome-icon icon="fa-brands fa-linkedin"/></p>
        </div>
        <div class="grid grid-cols-3 gap-16 md:mt-0 mt-10">
            <div class="flex flex-col text-center">
                <p class="font-medium">Sobre nós</p>
                <p class="font-medium max-w-[11rem] self-center mt-5 md:text-base text-xs">Somos um projeto que busca trazer a meteorologia mais próxima de você</p>
            </div>
            <div class="flex flex-col text-center">
                <p class="font-medium mb-5">Explore</p>
                <RouterLink class="text-xs md:text-sm font-400 text-[#4B5563] mb-5" to="/login">Login</RouterLink>
                <RouterLink class="text-xs md:text-sm font-400 text-[#4B5563]" to="/cadastro">Cadastro</RouterLink>
            </div>
            <div class="flex flex-col text-center">
                <p class="font-medium mb-5">Legal</p>
                <a class="text-xs md:text-sm font-400 text-[#4B5563] mb-5" target="_blank" href="https://www.google.com">Política de Privacidade</a>
                <a class="text-xs md:text-sm font-400 text-[#4B5563]" target="_blank" href="https://www.google.com">Termos de serviço</a>
            </div>
        </div>
    </footer>
</template>

<style scoped>
    .gradientText {
        background-size: 100%;
        background-color: #002AFF;
        background-image: linear-gradient(87deg, #ACE6FF 0%, #002AFF 100%);
        background-clip: text;
        -webkit-background-clip: text;
        -moz-background-clip: text;
        -webkit-text-fill-color: transparent; 
        -moz-text-fill-color: transparent;
    }
</style>