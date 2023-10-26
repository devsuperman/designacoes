document.addEventListener('DOMContentLoaded', () => {

    const $designado = document.querySelector("#DesignadoId")
    const $ajudante = document.querySelector("#AjudanteId")

    $designado.addEventListener('change', AtualizarAjudantes)

    function AtualizarAjudantes() {

        $ajudante.innerHTML = "<option value='0'> Carregando... </option>"

        const url = '/Designacoes/ListarAjudantes?designadoId=' + $designado.value

        fetch(url)
            .then(t => t.json())
            .then(lista => {


                let options = '<option value="0"> Pronto! Selecione! </option>'

                lista.forEach(publicador => {

                    let ultimaDesignacao = ''

                    if (publicador.data) {
                        let date = new Date(publicador.data)
                        ultimaDesignacao = date.toLocaleDateString()
                    }

                    options += `<option value='${publicador.id}'>${publicador.nome} - ${ultimaDesignacao} </option>`
                });

                $ajudante.innerHTML = options

            })
    }
})