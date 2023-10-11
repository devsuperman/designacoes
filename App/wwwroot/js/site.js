$(document).ready(function () {

    $('.montarDataTable').DataTable({
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.11/i18n/Portuguese-Brasil.json"
        },
        "bSort": false
    })

    $('select').select2({
        theme: 'bootstrap-5'
    })

})

function CopiarDesignacao(botao) {

    let $designacao = botao.nextElementSibling

    $designacao.select()
    $designacao.setSelectionRange(0, 99999) /* For mobile devices */

    document.execCommand("copy", false, $designacao.value.trim())
}