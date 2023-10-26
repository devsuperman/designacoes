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

    const $divDesignacao = document.querySelector("#divDesignacao")
    const $printDesignacao = document.querySelector('#printDesignacao')
    const $img = document.querySelector('#imgDesignacao')

    html2canvas($divDesignacao).then(function (canvas) {
        
        $divDesignacao.style.display = 'none';
        $printDesignacao.appendChild(canvas);
        $img.src = canvas.toDataURL('image/png')

    });

}