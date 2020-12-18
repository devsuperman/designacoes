document.addEventListener('DOMContentLoaded', () => {

    let tipoDaDesignacao = document.querySelector('#Tipo').value
    let checkboxes = document.querySelectorAll('[name=Tipo]')

    checkboxes.forEach(checkbox => {

        if (checkbox.value == tipoDaDesignacao) {
            checkbox.checked = true
        }

    });
})