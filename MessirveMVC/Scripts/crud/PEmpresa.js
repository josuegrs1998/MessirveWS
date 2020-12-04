var tableHdr = null;
var IdRecord = 0;

$(document).ready(function () {
    loadData();
    cargarSelect();
    $('#btnnuevo').on('click', function (e) {
        e.preventDefault();
        IdRecord = 0;
        NewRecord();
    });

    $('#btnguardar').on('click', function (e) {
        e.preventDefault();

        Guardar();
    });

    $('#dt-records').on('click', 'button.btn-edit', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();

        loadDtl(data);
        IdRecord = data.IdProductoEmpresa;
    });

    $('#dt-records').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        IdRecord = data.IdProductoEmpresa;
        if (confirm('¿Seguro de eliminar el registro?')) {
            Eliminar();
        }
    });

});

function loadData() {
    tableHdr = $('#dt-records').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/PEmpresa/Lista",
        order: [],
        columns: [
            { "data": "IdProductoEmpresa" },
            //{ "data": "IdEmpresa" },
            //{ "data": "IdProducto" },
            { "data": "Empresa.Nombre" },
            { "data": "Producto.Nombre" },
            { "data": "Cantidad" },
            { "data": "Descuento" },
            { "data": "PrecioBase" },

        ],
        processing: true,
        language: {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        columnDefs: [
            {
                width: "10%",
                targets: 0,
                data: "IdProductoEmpresa"
            },
            {
                width: "10%",
                targets: 1,
                data: "Empresa.Nombre"
            },
            {
                width: "10%",
                targets: 2,
                data: "Producto.Nombre"
            },
            {
                width: "10%",
                targets: 3,
                data: "Cantidad"
            },
            {
                width: "10%",
                targets: 4,
                data: "Descuento"
            },
            {
                width: "10%",
                targets: 5,
                data: "PrecioBase"
            },
            {
                width: "5%",
                targets: 6,
                data: null,
                defaultContent: '<button type="button" class="btn btn-info btn-sm btn-edit" data-target="#modal-record"><i class="fa fa-pencil"></i></button>'
            },
            {
                width: "5%",
                targets: 7,
                data: null,
                defaultContent: '<button type="button" class="btn btn-danger btn-sm btn-delete"><i class="fa fa-trash"></i></button>'

            }
        ]
    });
}

function NewRecord() {
    $(".modal-header h3").text("Crear Producto Empresa");

    $('#txtIdProductoEmpresa').val('');
    $('#txtIdEmpresa').val('');
    $('#txtIdProducto').val('');
    $('#txtCantidad').val('');
    $('#txtDescuento').val('');
    $('#txtPrecioBase').val('');


    $('#modal-record').modal('toggle');
}

function loadDtl(data) {
    $(".modal-header h3").text("Editar Empresa");

    $("#txtIdProductoEmpresa").val(data.IdProductoEmpresa);
    $('#txtIdEmpresa').val(data.IdEmpresa);
    $("#txtIdProducto").val(data.IdProducto);
    $('#txtCantidad').val(data.Cantidad);
    $('#txtDescuento').val(data.Descuento);
    $("#txtPrecioBase").val(data.PrecioBase);


    $('#modal-record').modal('toggle');
}

function Guardar() {
    var record = "'IdProductoEmpresa':" + IdRecord;
    record += ",'IdEmpresa':'" + $.trim($('#txtIdEmpresa').val()) + "'";
    record += ",'IdProducto':'" + $.trim($('#txtIdProducto').val()) + "'";
    record += ",'Cantidad':'" + $.trim($('#txtCantidad').val()) + "'";
    record += ",'Descuento':'" + $.trim($('#txtDescuento').val()) + "'";
    record += ",'PrecioBase':'" + $.trim($('#txtPrecioBase').val()) + "'";


    $.ajax({
        type: 'POST',
        url: '/PEmpresa/Guardar',
        data: eval('({' + record + '})'),
        success: function (response) {
            if (response.success) {
                $("#modal-record").modal('hide');
                $.notify(response.message, { globalPosition: "top center", className: "success" });
                $('#dt-records').DataTable().ajax.reload(null, false);
            }
            else {
                $("#modal-record").modal('hide');
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });
}

function Eliminar() {
    $.ajax({
        type: 'POST',
        url: '/PEmpresa/Eliminar/?IdProductoEmpresa=' + IdRecord,
        success: function (response) {
            if (response.success) {
                $.notify(response.message, { globalPosition: "top center", className: "success" });
                $('#dt-records').DataTable().ajax.reload(null, false);
            } else {
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });
}

function cargarSelect() {

    $.ajax({
        type: 'GET',
        url: '/Empresa/Lista',
        success: function (response) {

            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#txtIdEmpresa").append(`<option value="${response.data[i].IdEmpresa}"> 
                                       ${response.data[i].Nombre}
                                  </option>`);
                });

            } else {
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Producto/Lista',
        success: function (response) {

            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#txtIdProducto").append(`<option value="${response.data[i].IdProducto}"> 
                                       ${response.data[i].Nombre}
                                  </option>`);
                });

            } else {
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });

}