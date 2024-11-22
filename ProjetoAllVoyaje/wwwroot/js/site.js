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
    // Remover isso
    return true;

    const origem = document.getElementById('origem').value;
    const destino = document.getElementById('destino').value;
    //const quantPessoas = document.querySelector('input[name="quantPessoas"]').value;
    const dataInicio = document.getElementById('dataInicio').value;
    const dataFim = document.getElementById('dataFim').value;

    if (!origem || !destino || !dataInicio || !dataFim) {
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

document.getElementById('dataSaida').addEventListener('change', function () {
    const dataSaida = new Date(this.value);
    const dataRetornoInput = document.getElementById('dataRetorno');
    const dataRetorno = new Date(dataRetornoInput.value);

    // Se a data de saída estiver definida e a data de retorno estiver antes dela
    if (dataRetornoInput.value && dataRetorno < dataSaida) {
        alert('A data de retorno não pode ser anterior à data de saída.');
        dataRetornoInput.value = ''; // Limpa o campo de data de retorno
    }
});

document.getElementById('dataRetorno').addEventListener('change', function () {
    const dataSaida = new Date(document.getElementById('dataSaida').value);
    const dataRetorno = new Date(this.value);

    // Verifica se a data de retorno é anterior à data de saída
    if (dataSaida && dataRetorno < dataSaida) {
        alert('A data de retorno não pode ser anterior à data de saída.');
        this.value = ''; // Limpa o campo de data de retorno
    }
});

// Função para atualizar o preço baseado na quantidade de pessoas
function atualizarPreco(unitario) {
    console.log("Olaladld")
    // Obtém o número de pessoas
    var quantidadePessoas = parseInt(document.getElementById('quantPessoas').value);

    let precoAtualizado = quantidadePessoas * unitario;

       // Exibe o preço atualizado
    document.getElementById('precoAtual').innerText = precoAtualizado.toFixed(2);
    document.getElementById('valorDesgracado').innerText = precoAtualizado.toFixed(2);
    
}

// Adiciona um evento para atualizar o preço sempre que a quantidade de pessoas for alterada
document.getElementById('quantPessoas').addEventListener('input', atualizarPreco);

// Chama a função para definir o preço ao carregar a página
atualizarPreco();


