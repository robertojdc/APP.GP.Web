class TableManager {
    static addRedSocialRow(redSocial) {
        const nuevaFila = `
            <tr>
                <td>
                    <input type="hidden" name="RedesSociales[]" value="${redSocial}">
                    ${redSocial}
                </td>
                <td>
                    <button type="button" class="btn btn-danger btn-sm removeRedSocial">Eliminar</button>
                </td>
            </tr>`;
        $('#tablaRedesSociales tbody').append(nuevaFila);
    }

    static removeRow(element) {
        element.closest('tr').remove();
    }

    static addCategoriaRow(data) {
        const nuevaFila = `
            <tr data-grupoid="${data.grupoId}" data-subgrupoid="${data.subgrupoId}" data-clasificacionid="${data.clasificacionId}" data-subcategoriaid="${data.subcategoriaId || ''}">
                <td>${data.grupo}</td>
                <td>${data.subgrupo}</td>
                <td>${data.subcategoria || data.clasificacion}</td>
                <td>
                    <button type="button" class="btn btn-danger btn-sm eliminarFila">Eliminar</button>
                </td>
            </tr>`;
        $('#tablaCategorias tbody').append(nuevaFila);
    }
}
