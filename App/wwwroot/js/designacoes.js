document.addEventListener('DOMContentLoaded', () => {

    MarcarCheckboxDeAcordoComModel()
    ConverterHTMlemImagem()

})

function MarcarCheckboxDeAcordoComModel() {
    let tipoDaDesignacao = document.querySelector('#Tipo').value;
    let checkboxes = document.querySelectorAll('[name=Tipo]');

    checkboxes.forEach(checkbox => {

        if (checkbox.value == tipoDaDesignacao) {
            checkbox.checked = true;
        }

    });
}

function ConverterHTMlemImagem() {

    var $divDesignacao = document.querySelector("#divDesignacao")
    let $printDesignacao = document.querySelector('#printDesignacao')

    html2canvas($divDesignacao).then(function(canvas) {
        $divDesignacao.style.display = 'none';
        $printDesignacao.appendChild(canvas);
    });
}