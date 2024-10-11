// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function formatPhone(input) {
    // Remove todos os caracteres que não são dígitos
    let value = input.value.replace(/\D/g, '');

    // Verifica se o valor tem exatamente 10 ou 11 dígitos
    if (value.length > 11) {
        value = value.slice(0, 11); // Limita a 11 dígitos
    }

    // Formata o número
    if (value.length === 11) {
        value = value.replace(/^(\d{2})(\d{5})(\d{4})$/, '($1) $2-$3');
    } else if (value.length === 10) {
        value = value.replace(/^(\d{2})(\d{4})(\d{4})$/, '($1) $2-$3');
    }

    input.value = value;
}

function validatePhone(input) {
    // Remove todos os caracteres que não são dígitos
    let value = input.value.replace(/\D/g, '');
    if (value.length !== 10 && value.length !== 11) {
        alert("O número de telefone deve ter 10 ou 11 dígitos.");
    }
}



/******* função para testar inputs do filtssro ******/

function validateForm() {
    const origem = document.getElementById('origem').value;
    const destino = document.getElementById('destino').value;
    const quantPessoas = document.querySelector('input[name="quantPessoas"]').value;
    const dataInicio = document.getElementById('dataInicio').value;
    const dataFim = document.getElementById('dataFim').value;

    if (!origem || !destino || !quantPessoas || !dataInicio || !dataFim) {
        alert("Por favor, preencha todos os campos.");
        return false; // Impede o envio do formulário
    }

    // Verifica se a data de fim é anterior à data de início
    if (new Date(dataFim) < new Date(dataInicio)) {
        alert("A data de fim não pode ser anterior à data de início.");
        return false; // Impede o envio do formulário
    }

    return true; // Permite o envio do formulário
}

function toggleDestinos() {
    const tipoViagem = document.querySelector('input[name="tipoViagem"]:checked').value;
    const destinos = document.querySelectorAll('#destino option');

    destinos.forEach(option => {
        if (tipoViagem === "nacional" && option.classList.contains("nacional")) {
            option.style.display = "block";
        } else if (tipoViagem === "internacional" && option.classList.contains("internacional")) {
            option.style.display = "block";
        } else {
            option.style.display = "none";
        }
    });

    // Reset 
    document.getElementById('destino').value = "";
}

window.onload = function () {
    const params = new URLSearchParams(window.location.search);

    const origemValue = params.get('origem');
    const destinoValue = params.get('destino');
    const dataInicio = params.get('dataInicio');
    const dataFim = params.get('dataFim');
    const quantPessoas = params.get('quantPessoas');

    // Mapeamento das origens e destinos
    const origens = {
        "1": "São Paulo",
        "2": "Chile",
        "3": "China",
        "4": "Brasilia"
    };

    const destinos = {
        "1": "Rio de Janeiro",
        "2": "Salvador",
        "3": "Dubai",
        "4": "Chicago"
    };

    // Mapeamento de imagens
    const imagens = {
        "1": "../img/SaoPaulo.jpg", // Imagem para São Paulo
        "2": "../img/Chile.webp",      
        "3": "../img/China.webp",      
        "4": "../img/Brasilia.webp",
        "1_1": "../img/rio.jpg",      
        "1_2": "../img/salvador.jpg",  
        "1_3": "../img/dubai.jpg",   
        "1_4": "../img/chicago.webp",   
        "2_1": "../img/rio.jpg",
        "2_2": "../img/salvador.jpg",
        "2_3": "../img/dubai.jpg",
        "2_4": "../img/chicago.webp",
        "3_1": "../img/rio.jpg",
        "3_2": "../img/salvador.jpg",
        "3_3": "../img/dubai.jpg",
        "3_4": "../img/chicago.webp",
        "4_1": "../img/rio.jpg",
        "4_2": "../img/salvador.jpg",
        "4_3": "../img/dubai.jpg",
        "4_4": "../img/chicago.webp"   
    };

    // Obter os nomes correspondentes
    const origem = origens[origemValue] || "Desconhecida";
    const destino = destinos[destinoValue] || "Desconhecido";

    // Definir a imagem com base na origem e no destino
    const imageKey = `${origemValue}_${destinoValue}`;
    const imageSrc = imagens[imageKey] || "~/img/default.jpg"; // imagem padrão
    document.getElementById('cardImage').src = imageSrc;

    // Exibir as informações dentro do card
    document.getElementById('resultado').innerHTML = `
        <strong>Origem:</strong> ${origem}<br>
        <strong>Destino:</strong> ${destino}<br>
        <strong>Data de Início:</strong> ${dataInicio}<br>
        <strong>Data de Fim:</strong> ${dataFim}<br>
        <strong>Quantidade de Pessoas:</strong> ${quantPessoas}
    `;
};