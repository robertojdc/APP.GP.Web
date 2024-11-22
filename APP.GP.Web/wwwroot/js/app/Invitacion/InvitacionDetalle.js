$(document).ready(function () {
    $('#buscarActor').on('click', function (e) {
        e.preventDefault();
        buscarActor();
    });

    obtenerGrupos();

    $("#crearActor").on('click', function (e) {
        e.preventDefault();
        window.location.href = '/Actor/Index';
    });
    obtenerAfinidad();
    obtenerCompromisos();
});
function MostrarModalActor() {
    var $kendoWindow = $("#modalBuscarActor").kendoWindow({
        width: "1024px",
        title: "Buscar Actor",
        visible: false,
        modal: true,
        actions: ["Close"],
        resizable: false
    }).data("kendoWindow");

    $kendoWindow.center().open();
}

function buscarActor() {
    var nombre = $('#nombre').val();
    var apellidoPaterno = $('#apellidoPaterno').val();
    var apellidoMaterno = $('#apellidoMaterno').val();

    MuestraCargando();

    $("#gridResultadosActor").empty();
    $.ajax({
        url: '/Actor/Consultar',
        type: 'GET',
        data: {
            nombre: nombre,
            apellidoPaterno: apellidoPaterno,
            apellidoMaterno: apellidoMaterno,
            idGrupo: $("#grupo").data('kendoDropDownList') ? $("#grupo").data('kendoDropDownList').value() : 0,
            idSubGrupo: $("#subgrupo").data('kendoDropDownList') ? $("#subgrupo").data('kendoDropDownList').value() : 0,
            idCategoria: $("#clasificacion").data('kendoDropDownList') ? $("#clasificacion").data('kendoDropDownList').value() : 0,
            afinidad: $("#afinidad").data('kendoDropDownList') ? $("#afinidad").data('kendoDropDownList').value() : 0,
            compromiso: $("#compromiso").data('kendoDropDownList') ? $("#compromiso").data('kendoDropDownList').value() : 0
        },
        success: function (response) {

            OcultaCargando();

            $("#gridResultadosActor").kendoGrid({
                dataSource: {
                    data: response,
                    pageSize: 10
                },
                pageable: true,
                columns: [
                    {
                        field: "nombreCompleto",
                        title: "Información del Contacto",
                        width: "20%",
                        template: function (dataItem) {
                            var imgSrc = dataItem.fotoBase64
                                ? `data:image/jpeg;base64,${dataItem.fotoBase64}`
                                : "/path/to/default-image.jpg";
                            return `
                                                        <div style="display: flex; align-items: center;">
                                                            <div class="customer-photo" style="background-image: url('${imgSrc}'); width: 50px; height: 50px; border-radius: 50%; background-size: cover; background-position: center; margin-right: 10px;"></div>
                                                            <div>
                                                                <div class="customer-name" style="font-weight: bold;">
                                                                    ${dataItem.nombre} ${dataItem.apellidoPaterno} ${dataItem.apellidoMaterno}
                                                                </div>
                                                            </div>
                                                        </div>
                                                    `;
                        }
                    },
                    { field: "cargoActual", width: "15%", title: "Cargo Actual" },
                    { field: "telefonoPersonal", width: "10%", title: "Teléfono" },
                    {
                        field: "informacionCombinada",
                        title: "Grupos",
                        width: "35%",
                        template: function (dataItem) {
                            return dataItem.grupos ? dataItem.grupos.replace(/;/g, '<br>') : 'N/A';
                        }
                    },
                    {
                        title: "Seleccionar",
                        template: "#= PlantillaSeleccionarActorBuscar(idActor) #"
                    }
                ],
                resizable: true,
                scrollable: false
            });
        },
        error: function (xhr, status, error) {
            OcultaCargando();
            MensajeError('Error', 'Ocurrió un error al consultar los actores.');
        }
    });
}

function PlantillaSeleccionarActorBuscar(id) {
    return "<button class='btn btn-danger me-2' onclick='VincularActorCatalogo(" + id + ")' title='Vincular Actor'><span class='bi bi-check-lg'></span></button>";
}

function obtenerGrupos() {
    $.ajax({
        url: '/Actor/ObtenerGrupos',
        type: 'GET',
        success: function (response) {
            $("#grupo").kendoDropDownList({
                dataTextField: "nombre",
                dataValueField: "id",
                dataSource: response,
                optionLabel: "Seleccione un grupo",
                change: function (e) {
                    var idGrupo = this.value();
                    if (idGrupo) {
                        obtenerSubGrupos(idGrupo);
                    }
                }
            });
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function obtenerSubGrupos(idGrupo) {
    $.ajax({
        url: '/Actor/ObtenerSubGrupos',
        type: 'POST',
        data: { idGrupo: idGrupo },
        success: function (response) {
            $("#subgrupo").kendoDropDownList({
                dataTextField: "nombre",
                dataValueField: "id",
                dataSource: response,
                optionLabel: "Seleccione un subgrupo",
                change: function (e) {
                    var idSubGrupo = this.value();
                    if (idSubGrupo) {
                        obtenerCategoria(idSubGrupo);
                    }
                }
            });
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function obtenerCategoria(idSubGrupo) {
    $.ajax({
        url: '/Actor/ObtenerCategorias',
        type: 'POST',
        data: { idSubGrupo: idSubGrupo },
        success: function (response) {
            $("#clasificacion").kendoDropDownList({
                dataTextField: "descripcion",
                dataValueField: "idCategoria",
                dataSource: response,
                optionLabel: "Seleccione una clasificación",
                change: function (e) {
                    var idSubCategoria = this.value();
                    if (idSubCategoria) {
                        obtenerSubCategoria(idSubCategoria);
                    }
                }
            });
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function obtenerSubCategoria(idSubCategoria) {
    $.ajax({
        url: '/Actor/ObtenerSubCategorias',
        type: 'POST',
        data: { idCategoria: idSubCategoria },
        success: function (response) {
            if (response && response.length > 0) {
                var subCategoriaContainer = $('<div class="form-group row subcategoria-container">')
                    .append('<label class="col-3 col-form-label">SUBCATEGORÍA</label>')
                    .append('<div class="col-9"><input id="subcategoria' + idSubCategoria + '" name="subcategoria" class="form-control"></div>');

                $('#clasificacionForm').append(subCategoriaContainer);

                $("#subcategoria" + idSubCategoria).kendoDropDownList({
                    dataTextField: "descripcion",
                    dataValueField: "idCategoria",
                    dataSource: response,
                    optionLabel: "Seleccione una subcategoría",
                    change: function (e) {
                        var idSubCategoria = this.value();
                        if (idSubCategoria) {
                            obtenerSubCategoria(idSubCategoria);
                        }
                    }
                });
            } else {
                alert('No hay subcategorías disponibles para esta categoría.');
            }
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function obtenerAfinidad() {
    $.ajax({
        url: '/Actor/GetAfinidades',
        type: 'GET',
        success: function (response) {
            $("#afinidad").kendoDropDownList({
                dataTextField: "nombre",
                dataValueField: "id",
                dataSource: response,
                optionLabel: "Seleccione una afinidad"
            });
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function obtenerCompromisos() {
    $.ajax({
        url: '/Actor/GetCompromisos',
        type: 'GET',
        success: function (response) {
            $("#compromiso").kendoDropDownList({
                dataTextField: "nombre",
                dataValueField: "id",
                dataSource: response,
                optionLabel: "Seleccione nivel de compromiso"
            });
        },
        error: function (xhr, status, error) {
            console.log('Ocurrió un error: ' + error);
        }
    });
}

function VincularActorCatalogo(idActor) {
    MuestraCargando();
    $.ajax({
        url: '../Invitacion/AddActorRegistroInvitacion',
        type: 'POST',
        data: {
            idRegistroInvitacion: $("#hdfIdRegistroInvitacion").val(),
            idActor: idActor
        },
        success: function (response) {
            if (response.procesoExitoso == 0) {
                MensajeError("¡Error!", response.mensaje);
            }
            OcultaCargando();

            MensajeExito("¡Éxito!", "El actor ha sido vinculado al invitado");

            var popCloseBuscar = $("#modalBuscarActor");
            popCloseBuscar.data("kendoWindow").close();

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