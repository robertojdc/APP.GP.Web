$(document).ready(function () {
    $.ajax({
        url: '/Home/GetContentData',
        type: 'GET',
        success: function (data) {
            renderContent(data);
        },
        error: function (xhr, status, error) {
            console.error('Error fetching content data:', error);
        }
    });
});

function renderContent(data) {
    const container = document.querySelector("#container");

    const mainRow = document.createElement("div");
    mainRow.classList.add("row", "mt-3");

    const poderesSection = createSection(data.titleGroup, data.sections, data.colClasses);
    mainRow.appendChild(poderesSection);

    const stateSection = createSection(data.titleGroupState, [data.statePowers], data.stateColClasses, true);
    mainRow.appendChild(stateSection);

    data.otherSections.forEach(section => {
        const otherSection = createSection(section.titleGroup, [section], section.colClasses);
        mainRow.appendChild(otherSection);
    });

    container.appendChild(mainRow);

    $(window).resize(function () {
        checkAndMaximizeWindow();
    });
}

function createSection(title, sections, colClasses, isSingleColumn = false) {
    const sectionDiv = document.createElement("div");
    colClasses.forEach(colClass => sectionDiv.classList.add(colClass));

    const titleGroup = document.createElement("div");
    titleGroup.classList.add("title-group");
    titleGroup.textContent = title;
    sectionDiv.appendChild(titleGroup);

    const rowDiv = document.createElement("div");
    rowDiv.classList.add("row");

    sections.forEach(section => {
        const colDiv = document.createElement("div");
        section.colDivClasses.forEach(colDivClass => colDiv.classList.add(colDivClass));

        const cardDiv = document.createElement("div");
        cardDiv.classList.add("k-card");

        const cardHeader = document.createElement("div");
        cardHeader.classList.add("k-card-header");
        const headerTitle = document.createElement("h5");
        headerTitle.textContent = section.header;
        cardHeader.appendChild(headerTitle);
        cardDiv.appendChild(cardHeader);

        const cardBody = document.createElement("div");
        cardBody.classList.add("k-card-body");

        const table = document.createElement("table");
        table.classList.add("custom-table");

        const thead = document.createElement("thead");
        const headerRow = document.createElement("tr");
        section.columns.forEach(column => {
            const th = document.createElement("th");
            th.textContent = column;
            headerRow.appendChild(th);
        });
        thead.appendChild(headerRow);
        table.appendChild(thead);

        const tbody = document.createElement("tbody");
        section.rows.forEach(row => {
            const tr = document.createElement("tr");
            row.forEach(cell => {
                const td = document.createElement("td");
                td.textContent = cell.text;
                td.addEventListener("click", () => {
                    showPopup(cell.popupId);
                });
                tr.appendChild(td);
            });
            tbody.appendChild(tr);
        });
        table.appendChild(tbody);

        cardBody.appendChild(table);
        cardDiv.appendChild(cardBody);

        const cardFooter = document.createElement("div");
        cardFooter.classList.add("k-card-footer");
        cardDiv.appendChild(cardFooter);

        colDiv.appendChild(cardDiv);
        rowDiv.appendChild(colDiv);
    });

    sectionDiv.appendChild(rowDiv);
    return sectionDiv;
}

function showPopup(popupId) {
    switch (popupId) {
        case 1:
            showKendoWindow('.detPrimerNivel', '700px', 'MEDIOS DE COMUNICACIÓN', popupId);
            break;
        case 2:
            showKendoWindow('.popupDet', '900px', 'MEDIOS DE COMUNICACIÓN / IMPRESOS / LOCALES', popupId);
            break;
        default:
            console.error(`Popup ID ${popupId} no reconocido`);
            break;
    }
}

function showKendoWindow(selector, width, title, popupId) {
    var kendoWindow = $(selector).kendoWindow({
        width: width,
        title: title,
        visible: false,
        modal: true,
        activate: function () {
            checkAndMaximizeWindow(kendoWindow);
        }
    }).data("kendoWindow");

    $.ajax({
        url: '/Home/GetPopupContent',
        type: 'GET',
        data: { popupId: popupId },
        success: function (data) {
            var content = generatePopupContent(data, popupId);
            kendoWindow.content(content);
            kendoWindow.center().open();
            $(".openPopupDet").click(function () {
                showPopup(2);
            });

            if (popupId == 2) {
                renderListView(data.contacts);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error fetching popup content:', error);
        }
    });
}

function generatePopupContent(data, popupId) {
    var container = document.createElement('div');
    container.classList.add('detPrimerNivel');

    if (popupId == 2) {
        var colDiv = document.createElement('div');
        colDiv.classList.add('col-12');
        var cardHeader = document.createElement('div');
        cardHeader.classList.add('k-card-header');
        var titleGroup = document.createElement('div');
        titleGroup.classList.add('title-group');
        titleGroup.textContent = data.titleGroup;
        cardHeader.appendChild(titleGroup);
        colDiv.appendChild(cardHeader);
        var table = document.createElement('table');
        table.classList.add('detalle');
        var thead = document.createElement('thead');
        var headerRow = document.createElement('tr');
        var th1 = document.createElement('th');
        var th2 = document.createElement('th');
        headerRow.appendChild(th1);
        headerRow.appendChild(th2);
        thead.appendChild(headerRow);
        table.appendChild(thead);

        var tbody = document.createElement('tbody');
        data.details.forEach(function (detail) {
            var tr = document.createElement('tr');
            var th = document.createElement('th');
            th.textContent = detail.label;
            var td = document.createElement('td');
            td.innerHTML = detail.value;
            tr.appendChild(th);
            tr.appendChild(td);
            tbody.appendChild(tr);
        });
        table.appendChild(tbody);
        colDiv.appendChild(table);
        container.appendChild(colDiv);

        var contactHeader = document.createElement('div');
        contactHeader.classList.add('k-card-header');
        var contactsTitleGroup = document.createElement('div');
        contactsTitleGroup.classList.add('title-group');
        contactsTitleGroup.textContent = data.contactsTitle;
        contactHeader.appendChild(contactsTitleGroup);
        container.appendChild(contactHeader);
        var exampleDiv = document.createElement('div');
        exampleDiv.id = 'example';
        var listViewDiv = document.createElement('div');
        listViewDiv.id = 'listView';
        exampleDiv.appendChild(listViewDiv);
        container.appendChild(exampleDiv);
    } else {
        var cardHeader = document.createElement('div');
        cardHeader.classList.add('k-card-header');
        var titleGroup = document.createElement('div');
        titleGroup.classList.add('title-group');
        titleGroup.textContent = data.titleGroup;
        cardHeader.appendChild(titleGroup);
        container.appendChild(cardHeader);

        var cardsContainer = document.createElement('div');
        cardsContainer.classList.add('cards-container');
        data.sections.forEach(function (section) {
            var colDiv = document.createElement('div');
            colDiv.classList.add('col-12');
            var cardDiv = document.createElement('div');
            cardDiv.classList.add('k-card');
            var sectionHeader = document.createElement('div');
            sectionHeader.classList.add('k-card-header');
            var headerTitle = document.createElement('h5');
            headerTitle.textContent = section.header;
            sectionHeader.appendChild(headerTitle);
            cardDiv.appendChild(sectionHeader);
            var cardBody = document.createElement('div');
            cardBody.classList.add('k-card-body');
            var table = document.createElement('table');
            table.classList.add('custom-table');
            var tbody = document.createElement('tbody');
            section.rows.forEach(function (row) {
                var tr = document.createElement('tr');
                row.forEach(function (cell) {
                    var td = document.createElement('td');
                    td.className = 'openPopupDet';
                    td.dataset.popupid = cell.popupId;
                    td.textContent = cell.text;
                    tr.appendChild(td);
                });
                tbody.appendChild(tr);
            });
            table.appendChild(tbody);

            cardBody.appendChild(table);
            cardDiv.appendChild(cardBody);

            var cardFooter = document.createElement('div');
            cardFooter.classList.add('k-card-footer');
            cardDiv.appendChild(cardFooter);

            colDiv.appendChild(cardDiv);
            cardsContainer.appendChild(colDiv);
        });
        container.appendChild(cardsContainer);
    }
    return container.outerHTML;
}

function renderListView(data) {
    var jsonData = data.map(function (item) {
        return {
            ProductID: item.productID,
            ProductName: item.productName,
            Description: item.description
        };
    });

    $("#listView").kendoListView({
        dataSource: {
            data: jsonData,
            pageSize: 15
        },
        grid: {
            cols: 5,
            rows: 2
        },
        selectable: "single",
        pageable: true,
        template: kendo.template($("#template").html())
    });

    $("#listView").on("click", ".product", function () {
        var productId = $(this).data("id");

        $.ajax({
            url: '/Home/GetContactDetails',
            type: 'GET',
            data: { productId: productId },
            success: function (product) {
                if (product) {
                    $("#modalImage").attr("src", "/img/" + product.productID + ".png");
                    $("#modalDescription").text(product.description);
                    $("#modalName").text(product.productName);
                    $("#modalPaterno").text(product.paterno);
                    $("#modalMaterno").text(product.materno);
                    $("#modalCumple").text(product.cumple);
                    $("#modalPartido").text(product.partido);
                    $("#modalGrupo").text(product.grupo);
                    $("#modalAfinidad").text(product.afinidad);
                    $("#modalTelefonoLaboral").text(product.telefonoLaboral);
                    $("#modalTelefonoCasa").text(product.telefonoCasa);
                    $("#modalCelular1").text(product.celular1);
                    $("#modalCorreo").html(product.correo + ", " + product.correo2);
                    $("#modalDomicilioOficina").text(product.domicilioOficina);
                    $("#modalDomicilioResidencia").text(product.domicilioResidencia);
                    $("#modalDomicilioDescanso").text(product.domicilioDescanso);
                    var kendoWindow = $("#detailsWindow").kendoWindow({
                        width: 950,
                        title: product.description,
                        visible: false,
                        modal: true,
                        activate: function () {
                            checkAndMaximizeWindow(kendoWindow);
                        }
                    }).data("kendoWindow");
                    kendoWindow.open().center();
                    createMap();
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching contact details:', error);
            }
        });
    });
}

function checkAndMaximizeWindow(kendoWindow) {
    var windowWidth = $(window).width();
    if (windowWidth < 576)
        kendoWindow.maximize();
    else
        kendoWindow.restore();
}

function createMap() {
    $("#map").kendoMap({
        center: [19.047836, -98.230083],
        zoom: 12,
        layers: [{
            type: "tile",
            urlTemplate: "https://#= subdomain #.tile.openstreetmap.org/#= zoom #/#= x #/#= y #.png",
            subdomains: ["a", "b", "c"]
        }],
        markers: [{
            location: [19.047836, -98.230083],
            shape: "pinTarget",
            tooltip: {
                content: "Austin, TX"
            }
        }]
    });
}