function BuscarInvitados() {
    MuestraCargando();
    $.ajax({
        url: '../Invitacion/BuscarInvitados',
        type: 'POST',
        data: {
            nombre: $("#txtNombreBuscar").val(),
            apellidoPaterno: $("#txtAppBuscar").val(),
            apellidoMaterno: $("#txtApmBuscar").val(),
            telefono: $("#txtTelefonoBuscar").val(),
            correoElectronico: $("#txtCorreoBuscar").val(),
            estatus: $("#ddlVinculado").val(),
        },
        success: function (response) {
            llenarGridInvitacion(response);
            $(".opcionesCSS").show();
            OcultaCargando();
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function llenarGridInvitacion(datos) {
    $("#gridResultados").kendoGrid({
        dataSource: {
            data: datos,
            pageSize: 10
        },
        pageable: true,
        columns: [
            {
                title: "Invitado",
                columns: [
                    { field: "nombreCompleto", title: "Nombre Invitado" },
                    { field: "correoElectronico", title: "Correo Electrónico", attributes: { 'style': "text-wrap: wrap;" } },
                    { field: "telefonoPersonal", title: "Teléfono", attributes: { 'style': "text-wrap: wrap;" } },
                ]
            },
            {
                title: "Actor",
                attributes: { 'style': "background-color: lightgray; " },
                columns: [
                    { field: "nombreCompletoActor", title: "Nombre Completo", attributes: { 'style': "background-color: lightgray; text-wrap: wrap;" } },
                    { field: "cargoActual", title: "Cargo", attributes: { 'style': "background-color: lightgray; text-wrap: wrap;" } },
                    { field: "telefonoParticular", title: "Particular", attributes: { 'style': "background-color: lightgray; text-wrap: wrap;" } },
                    { field: "telefonoLaboral", title: "Laboral", attributes: { 'style': "background-color: lightgray; text-wrap: wrap;" } },
                    { field: "correoElectronicoActor", title: "Correo Electrónico", attributes: { 'style': "background-color: lightgray; text-wrap: wrap;" } },
                ]
            },
            {
                title: "Opciones",
                template: "#= opcionesGridInvitados(idRegistroInvitacion, vinculado, opcionVinculacion) #"
            }
        ],
        resizable: true,
        scrollable: false
    });
}

function opcionesGridInvitados(id, vinculado, totalOpcion) {
    var cadena = "<center>";
    if (vinculado == 0) {

        cadena += "<button class='btn btn-danger me-2' onclick='VerCoincidencias(" + id + ")' title='Coincidencias'><span class='badge text-bg-secondary'>" + totalOpcion + "</span></button>";

    }
    else if (vinculado == 1) {
        cadena += "<button class='btn btn-outline-secondary' onclick='VerDetalle(" + id + ")' title='Detalle'><span class='bi bi-card-list'></span></button>";
    }

    cadena += "</center>";
    return cadena;
}

function SeleccionarFiltro(tipo) {
    if (tipo == 0) {
        var activo = $("#chkTodos").is(":checked");

        $("#chkNombre").prop("checked", activo);
        $("#chkApellido").prop("checked", activo);
        $("#chkCorreo").prop("checked", activo);
        $("#chkTelefono").prop("checked", activo);
    }
    else {
        if ($("#chkNombre").is(":checked") && $("#chkApellido").is(":checked") && $("#chkCorreo").is(":checked") &&
            $("#chkTelefono").is(":checked")) {
            $("#chkTodos").prop("checked", true);
        }
        else {
            $("#chkTodos").prop("checked", false);
        }
    }
}

function RelacionarInvitados() {
    MuestraCargando();
    $.ajax({
        url: '../Invitacion/RelacionarInvitados',
        type: 'POST',
        data: {
            todos: $("#chkTodos").is(":checked"),
            nombre: $("#chkNombre").is(":checked"),
            apellidos: $("#chkApellido").is(":checked"),
            correo: $("#chkCorreo").is(":checked"),
            telefono: $("#chkTelefono").is(":checked")
        },
        success: function (response) {
            if (response.procesoExitoso == 0) {
                MensajeError("¡Error!", response.mensaje);
            }

            llenarGridInvitacion(response.lista);
            OcultaCargando();
            MensajeExito("Éxito", "Se ha concluido el proceso de vinculación");
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function VerCoincidencias(id) {
    MuestraCargando();
    $("#hdfIdRegistroInvitacion").val(id);
    $.ajax({
        url: '../Invitacion/ObtenerOpcionesVinculacion',
        type: 'POST',
        data: {
            idRegistroInvitacion: id
        },
        success: function (response) {
            if (response.procesoExitoso == 0) {
                MensajeError("¡Error!", response.mensaje);
            }
            OcultaCargando();
            CargarGridOpcionesVinculacion(response.lista);

            var $kendoWindow = $("#modalOpcionesVinculacion").kendoWindow({
                width: "900px",
                title: "Coincidencias",
                visible: false,
                modal: true,
                actions: ["Close"],
                resizable: false
            }).data("kendoWindow");

            $kendoWindow.center().open();
        },
        error: function (xhr, status, error) {
            OcultaCargando();
            MensajeError("¡Error!", error);
        }
    });
}

function CargarGridOpcionesVinculacion(datos) {
    $("#GridOpcionesVinculacion").kendoGrid({
        dataSource: {
            data: datos
        },
        pageable: false,
        filterable: true,
        columns: [
            {
                title: "Nombre",
                template: "#= PlantillaNombre(nombre, apellidoPaterno, apellidoMaterno, cargoActual) #",
                width: "40%"
            },
            {
                title: "Teléfonos",
                template: "#= PlantillaTelefonos(telefonoParticular, telefonoLaboral) #",
                width: "30%"
            },
            {
                title: "Correo Electrónico",
                field: "correoElectronico",
                width: "20%"
            },
            {
                title: "Seleccionar",
                template: "#= PlantillaSeleccionarActor(idRegistroInvitacion, idActor) #"
            }
        ],
        resizable: false,
        scrollable: false
    });
}

function PlantillaNombre(nombre, apellidoPaterno, apellidoMaterno, cargo) {
    return nombre + " " + apellidoPaterno + " " + apellidoMaterno + "<br/>" +
        cargo;
}
function PlantillaTelefonos(telefonoParticular, telefonoLaboral) {
    return "Particular: " + telefonoParticular + "<br/> Laboral: " + telefonoLaboral;
}

function PlantillaSeleccionarActor(idRegistroInvitacion, idActor) {
    return "<button class='btn btn-danger me-2' onclick='VincularActor(" + idRegistroInvitacion + "," + idActor + ")' title='Vincular Actor'><span class='bi bi-check-lg'></span></button>";
}

function VincularActor(idRegistroInvitacion, idActor) {
    MuestraCargando();
    $.ajax({
        url: '../Invitacion/AddActorRegistroInvitacion',
        type: 'POST',
        data: {
            idRegistroInvitacion: idRegistroInvitacion,
            idActor: idActor
        },
        success: function (response) {
            if (response.procesoExitoso == 0) {
                MensajeError("¡Error!", response.mensaje);
            }
            OcultaCargando();

            MensajeExito("¡Éxito!", "El actor ha sido vinculado al invitado");

            var popClose = $("#modalOpcionesVinculacion");
            popClose.data("kendoWindow").close();

            BuscarInvitados();
        },
        error: function (xhr, status, error) {
            OcultaCargando();
            MensajeError("¡Error!", error);
        }
    });
}

function VerDetalle(id) {
    MuestraCargando();
    var detalleModal = $("#modalDetalleInvitacion").kendoWindow({
        title: "Detalle de Vinculación",
        modal: true,
        visible: false,
        width: "650px",
        height: "550px",
        actions: ["Close"]
    }).data("kendoWindow");

    $.ajax({
        url: '../Invitacion/GetInvitacionDetalle',
        type: 'GET',
        data: { id: id },
        success: function (response) {
            OcultaCargando();
            detalleModal.content(response);
            detalleModal.center().open();
        },
        error: function (xhr, status, error) {
            console.error('Error al obtener los detalles del actor:', error);
        }
    });
}

function LimpiarFiltros() {
    $("#txtNombreBuscar").val("");
    $("#txtAppBuscar").val("");
    $("#txtApmBuscar").val("");
    $("#txtTelefonoBuscar").val("");
    $("#txtCorreoBuscar").val("");
    $("#ddlVinculado").val(-1);
}