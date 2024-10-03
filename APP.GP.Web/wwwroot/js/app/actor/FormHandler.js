class FormHandler {
    static handleSubmit(event) {
        event.preventDefault();
        const formData = new FormData($('#personalForm')[0]);
        const categorias = TableManager.extractCategorias();
        formData.append('categoriasJson', JSON.stringify(categorias));

        AjaxService.post('/Actor/Create', formData)
            .then(() => {
                alert('Actor creado exitosamente!');
                $('#tablaCategorias tbody').empty();
                $('#personalForm')[0].reset();
            })
            .catch(error => alert('Ocurrió un error: ' + error));
    }

    static extractCategorias() {
        const categorias = [];
        $('#tablaCategorias tbody tr').each(function () {
            categorias.push({
                IdGrupo: $(this).data('grupoid'),
                IdSubGrupo: $(this).data('subgrupoid'),
                IdCategoria: $(this).data('clasificacionid'),
                IdSubcategoria: $(this).data('subcategoriaid')
            });
        });
        return categorias;
    }
}
