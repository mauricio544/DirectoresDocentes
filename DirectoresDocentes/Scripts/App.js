$(document).ready(function () {
    $("#dishType").ejTab({ showCloseButton: true, height: "1000px" });
    var loadImagenPersona = function () {
        $.ajax({
            method: "GET",
            url: "/api/Persona/?id=" + parseInt(persona),
            dataType: "json",
            processData: false,
            contentType: "application/json"
        })
            .done(function (data) {
                if (data.FOTO == null) {
                    $("img#fotoPersona").attr("src", "/Content/" + data.SEXO + ".png");
                } else {
                    $("img#fotoPersona").attr("src", "data:image/png;base64," + data.FOTO);
                }
            });
    };

    /* Locale para las grillas de syncfusion
    ****************************************/
    ej.Grid.locale["es-ES"] = {
        EmptyRecord: "No hay registros que mostrar",
        GroupDropArea: "Arrastre un encabezado de columna aquí",
        DeleteOperationAlert: "No hay registros seleccionados para la operación de eliminación",
        EditOperationAlert: "No hay registros seleccionados para la operación de edición",
        SaveButton: "Guardar",
        CancelButton: "Cancelar",
        OkButton: "OK",
        EditFormTitle: "Editar detalles de: ",
        AddFormTitle: "Añadir nuevo registro",
        GroupCaptionFormat: "{{:field}}: {{:key}} - {{:count}} {{if count == 1}}registro {{else}}registros{{/if}}",
        ConfirmDeleteMessage: "Seguro que desea borrar este registro?",
        UnGroup: "Haga clic aquí para desagrupar",
        Add: "Add",
        Edit: "Edit",
        Delete: "Delete",
        Update: "Update",
        Cancel: "Cancel",
        columns: "Columnas",
        showDeleteConfirmDialog: "",
        DoneButton: "Hecho",
        StringMenuOptions: [{ text: "Empieza en", value: "StartsWith" }, { text: "Termina en", value: "EndsWith" }, { text: "Contiene", value: "Contains" }, { text: "Igual", value: "Equal" }, { text: "Diferente", value: "NotEqual" }],
        NumberMenuOptions: [{ text: "Menor que", value: "LessThan" }, { text: "Mayor que", value: "GreaterThan" }, { text: "Menor o igual", value: "LessThanOrEqual" }, { text: "mayor o igual", value: "GreaterThanOrEqual" }, { text: "Igual", value: "Equal" }, { text: "Diferente", value: "NotEqual" }],
        Filter: "Filtrar",
        MatchCase: "Mayúsculas",
        Clear: "Limpiar",
        FilterValue: "Valor"
    };

    /* manejos de datamanager para las grilas y otros componentes syncfusion
    ************************************************************************/
    var dataProfesores = ej.DataManager({
        url: "/api/Profesores/DataSource",
        adaptor: new ej.UrlAdaptor()
    });

    var profesoresLista = function() {
        $("#profesoresLista").ejGrid({
            dataSource: dataProfesores,
            allowPaging: true,
            isResponsive: true,
            enableResponsiveRow: true,
            minWidth: 700,
            allowFiltering: true,
            filterSettings: { filterType: ej.Grid.FilterType.Bar },
            pageSettings: { pageSize: 5 },
            locale: "es-ES",
            allowScrolling: true,
            allowTextWrap: true,
            allowGrouping: true,
            enableHeaderHover: true,
            columns: [
                { field: "CODIGO", isPrimaryKey: true, headerText: "Orden", visible: false, allowEditing: false },
                { field: "PERSONA_ROLES.PERSONAS.NRO_DOCUMENTO", isPrimaryKey: true, headerText: "Documento", visible: false, allowEditing: false },
                { field: "PERIODOS_CODIGO", headerText: "Periodo", visible: false, allowEditing: false },
                { headerText: "Foto", field: "PERSONA_ROLES.PERSONAS.FOTO", textAlign: ej.TextAlign.Center, width: 120, allowFiltering: false },
                { field: "PERSONA_ROLES.PERSONAS.NOMBRE_COMPLETO", headerText: "Nombres", visible: true, allowEditing: false },
                { field: "HORAS_MENSUAL", headerText: "Horas Mensual", visible: true, allowEditing: false }
            ],
            rowSelected: function (args) {                
                var dataSourceCapacitacion = new ej.DataManager({
                    url: "/api/Profesores/CapacitacionByDoc/?documento=" + args.data.PERSONA_ROLES.PERSONAS.NRO_DOCUMENTO,
                    adaptor: new ej.UrlAdaptor()
                });
                var gridObj = $("#profesoresCapacitacion").ejGrid("instance");
                gridObj.dataSource(dataSourceCapacitacion);
                $("#profesoresCapacitacion").ejGrid("refreshContent");
            },
            queryCellInfo: function (args) {
                var value = args.text;
                var $element = $(args.cell);
                switch (args.column.headerText) {
                    case "Foto":
                        if (value) {
                            $element.html("<img style=\"width: 120px; margin: 0 auto;\" src=\"data:image/png;base64," + value + "\" alt=\"Foto Estudiante\" class=\"thumbnail\" />");
                        } else {
                            $element.html("<div class=\"fa fa-user thumbnail\" style=\"width: 100%; font-size: 7em; color: #428bca;\"></div>");
                        }
                }
            }
        });
    }

    $("#profesoresCapacitacion").ejGrid({        
        dataSource: null,
        allowPaging: true,
        allowFiltering: true,
        allowTextWrap: true,
        isResponsive: true,
        enableResponsiveRow: true,
        minWidth: 700,
        pageSettings: { pageSize: 10 },        
        locale: "es-ES",
        columns: [
            { field: "identificacion", headerText: "Nro Documento", visible: true, allowEditing: false },
            { field: "nombre", headerText: "Nombres", visible: true, allowEditing: false },
            { field: "tipo_academico", headerText: "Tipo", visible: true, allowEditing: false },
            { field: "descripcion", headerText: "Descripción", visible: true, allowEditing: false },
            { field: "CARRERA", headerText: "Carrera", allowEditing: false, allowFiltering: false },
            { field: "INSTITUCION", headerText: "Institución", allowEditing: false, allowFiltering: false }
        ]        
    });

    function init() {
        // cargar fotografía de persona
        loadImagenPersona();
        profesoresLista();
    }

    // inicialización
    init();
})