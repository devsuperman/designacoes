$(document).ready(function () {

    $('.montarDataTable').DataTable({
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.11/i18n/Portuguese-Brasil.json"
        },
        "bSort": false
    })
    
})

function CopiarDesignacao(botao) {

    let $designacao = botao.nextElementSibling
    
    $designacao.select()
    $designacao.setSelectionRange(0, 99999)

    navigator.clipboard.writeText($designacao.value.trim());
}