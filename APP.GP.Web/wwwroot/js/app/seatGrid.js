$(document).ready(function () {
    var seatData = [
        { Row: '1', A: 'available', B: 'reserved', C: 'available', D: 'available', E: 'reserved' },
        { Row: '2', A: 'available', B: 'available', C: 'reserved', D: 'available', E: 'available' },
        // Añadir más filas según sea necesario
    ];

    $(document).ready(function () {
        var seatData = [
            { Row: '1', A: 'available', B: 'reserved', C: 'available', D: 'available', E: 'reserved' },
            { Row: '2', A: 'available', B: 'available', C: 'reserved', D: 'available', E: 'available' },
            // Añadir más filas según sea necesario
        ];

        $("#seatGrid").kendoGrid({
            dataSource: {
                data: seatData,
                schema: {
                    model: {
                        fields: {
                            Row: { type: "string" },
                            A: { type: "string" },
                            B: { type: "string" },
                            C: { type: "string" },
                            D: { type: "string" },
                            E: { type: "string" }
                        }
                    }
                }
            },
            columns: [
                { field: "Row", title: "Row" },
                { field: "A", title: "A", template: kendo.template($("#seatTemplate").html()) },
                { field: "B", title: "B", template: kendo.template($("#seatTemplate").html()) },
                { field: "C", title: "C", template: kendo.template($("#seatTemplate").html()) },
                { field: "D", title: "D", template: kendo.template($("#seatTemplate").html()) },
                { field: "E", title: "E", template: kendo.template($("#seatTemplate").html()) }
            ]
        });

        $(document).on('click', '.seat-button', function () {
            var seat = $(this).data('seat');
            var row = $(this).data('row');
            var grid = $("#seatGrid").data("kendoGrid");
            var dataItem = grid.dataSource.getByUid($(this).closest("tr").data("uid"));

            dataItem.set(seat, dataItem[seat] === 'available' ? 'reserved' : 'available');
            grid.refresh();
        });
    });
});

