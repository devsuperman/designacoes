document.addEventListener('DOMContentLoaded', () => {

    OcultarObservacoes()
    MarcarCheckboxDeAcordoComModel()
    ConverterHTMlemImagem()

})

function OcultarObservacoes() {    
    $('.observacao').hide()
}

function MarcarCheckboxDeAcordoComModel() {
    
    let tipoDaDesignacao = document.querySelector('#Tipo').value;
    let checkboxes = document.querySelectorAll('.check-tipo');

    checkboxes.forEach(checkbox => {

        console.log(tipoDaDesignacao)

        if (checkbox.value == tipoDaDesignacao) {            

            checkbox.checked = true;
            var $span = checkbox.parentNode.querySelector('.observacao')
            
            if ($span) {
                $span.style.display = 'block' // exibir linha da observacao                
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