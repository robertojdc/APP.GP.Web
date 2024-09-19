// Función para mostrar mensajes de éxito
function MensajeExito(titulo = '¡Éxito!', mensaje = 'Operación realizada con éxito.') {
    Swal.fire({
        icon: 'success',
        title: titulo,
        text: mensaje,
        confirmButtonText: 'Aceptar'
    });
}

// Función para mostrar mensajes de error
function MensajeError(titulo = 'Error', mensaje = 'Ocurrió un error inesperado.') {
    Swal.fire({
        icon: 'error',
        title: titulo,
        text: mensaje,
        confirmButtonText: 'Aceptar'
    });
}

// Función para mostrar mensajes de advertencia
function MensajeAdvertencia(titulo = 'Advertencia', mensaje = 'Algo necesita tu atención.') {
    Swal.fire({
        icon: 'warning',
        title: titulo,
        text: mensaje,
        confirmButtonText: 'Aceptar'
    });
}

// Función para mostrar mensajes de confirmación con callback
function MensajeConfirmacion(titulo = '¿Estás seguro?', mensaje = 'Esta acción no se puede deshacer.', callback) {
    Swal.fire({
        icon: 'question',
        title: titulo,
        text: mensaje,
        showCancelButton: true,
        confirmButtonText: 'Sí, confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Ejecutar el callback si se confirma
            if (callback && typeof callback === 'function') {
                callback();
            }
        }
    });
}
