class DropdownManager {
    static initGrupos() {
        AjaxService.get('/Actor/ObtenerGrupos')
            .then(response => {
                $("#grupo").kendoDropDownList({
                    dataTextField: "nombre",
                    dataValueField: "id",
                    dataSource: response,
                    optionLabel: "Seleccione un grupo",
                    change: function () {
                        const idGrupo = this.value();
                        if (idGrupo) {
                            console.log('idGrupo', idGrupo);
                            DropdownManager.initSubGrupos(idGrupo);
                        }
                    }
                });
            })
            .catch(error => console.log('Ocurrió un error: ' + error));
    }

    static initSubGrupos(idGrupo) {
        AjaxService.post('/Actor/ObtenerSubGrupos', { idGrupo })
            .then(response => {
                $("#subgrupo").kendoDropDownList({
                    dataTextField: "nombre",
                    dataValueField: "id",
                    dataSource: response,
                    optionLabel: "Seleccione un subgrupo",
                    change: function () {
                        const idSubGrupo = this.value();
                        if (idSubGrupo) {
                            DropdownManager.initCategorias(idSubGrupo);
                        }
                    }
                });
            })
            .catch(error => console.log('Ocurrió un error: ' + error));
    }

    static initCategorias(idSubGrupo) {
        AjaxService.post('/Actor/ObtenerCategorias', { idSubGrupo })
            .then(response => {
                $("#clasificacion").kendoDropDownList({
                    dataTextField: "descripcion",
                    dataValueField: "idCategoria",
                    dataSource: response,
                    optionLabel: "Seleccione una clasificación",
                    change: function () {
                        const idCategoria = this.value();
                        if (idCategoria) {
                            DropdownManager.initSubCategorias(idCategoria);
                        }
                    }
                });
            })
            .catch(error => console.log('Ocurrió un error: ' + error));
    }

    static initSubCategorias(idCategoria) {
        AjaxService.post('/Actor/ObtenerSubCategorias', { idCategoria })
            .then(response => {
                if (response && response.length > 0) {
                    const subCategoriaContainer = $('<div class="form-group row subcategoria-container">')
                        .append('<label class="col-3 col-form-label">SUBCATEGORÍA</label>')
                        .append('<div class="col-9"><input id="subcategoria' + idCategoria + '" name="subcategoria" class="form-control"></div>');

                    $('#clasificacionForm').append(subCategoriaContainer);

                    $("#subcategoria" + idCategoria).kendoDropDownList({
                        dataTextField: "descripcion",
                        dataValueField: "idCategoria",
                        dataSource: response,
                        optionLabel: "Seleccione una subcategoría",
                        change: function () {
                            const idSubCategoria = this.value();
                            if (idSubCategoria) {
                                DropdownManager.initSubCategorias(idSubCategoria);
                            }
                        }
                    });
                }
            })
            .catch(error => console.log('Ocurrió un error: ' + error));
    }
}
