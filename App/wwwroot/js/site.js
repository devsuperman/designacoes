﻿$(document).ready(function () {

    $('.montarDataTable').DataTable({
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.11/i18n/Portuguese-Brasil.json"
        },
        "bSort": false
    });

    $('select').select2();

});

