document.addEventListener('DOMContentLoaded', () => {

    MarcarCheckboxDeAcordoComModel()
    ConverterHTMlemImagem()

})

function MarcarCheckboxDeAcordoComModel() {
    let tipoDaDesignacao = document.querySelector('#Tipo').value;
    let observacao = document.querySelector('#Observacao').value;
    let checkboxes = document.querySelectorAll('.check-tipo');

    checkboxes.forEach(checkbox => {

        console.log(checkbox)

        if (checkbox.value == tipoDaDesignacao) {            

            checkbox.checked = true;

            var $span = checkbox.parentNode.querySelector('.observacao')

            if ($span) {
                $span.textContent = observacao
            }
        }

    });
}

function ConverterHTMlemImagem() {

    var $divDesignacao = document.querySelector("#divDesignacao")
    let $printDesignacao = document.querySelector('#printDesignacao')

    html2canvas($divDesignacao).then(function (canvas) {
        $divDesignacao.style.display = 'none';
        $printDesignacao.appendChild(canvas);
    });
}