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
        IdRecord = data.IdOrdenProducto;
    });

    $('#dt-records').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        IdRecord = data.IdOrdenProducto;
        if (confirm('¿Seguro de eliminar el registro?')) {
            Eliminar();
        }
    });

});

function loadData() {
    tableHdr = $('#dt-records').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/OrdenProduto/Lista",
        order: [],
        columns: [
           
            { "data": "IdOrden" },
            { "data": "Producto.Nombre" },
            { "data": "Precio" },
            { "data": "Iva" },
            { "data": "Cantidad" },
            { "data": "Subtotal" },
           

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
                width: "8%",
                targets: 0,
                data: "IdOrden"
            },
            {
                width: "8%",
                targets: 1,
                data: "Producto.Nombre"
            },
            {
                width: "8%",
                targets: 2,
                data: "Precio"
            },
            {
                width: "8%",
                targets: 3,
                data: "Iva"
            },
            {
                width: "8%",
                targets: 4,
                data: "Cantidad"
            },
            {
                width: "8%",
                targets: 5,
                data: "Subtotal"
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
    $(".modal-header h3").text("Crear Orden");

    $('#tIdOrdenProducto').val('');
    $('#tidOrden').val('');
    $('#tIdProducto').val('');
    $('#tPrecio').val('');
    $('#tIva').val('');
    $('#tsubtotal').val('');
    $('#tCantidad').val('');

    $('#modal-record').modal('toggle');
}

function loadDtl(data) {
    $(".modal-header h3").text("Editar Orden");

    $("#tIdOrdenProducto").val(data.IdOrdenProducto);
    $('#tidOrden').val(data.IdOrden);
    $("#tIdProducto").val(data.IdProducto);
    $('#tPrecio').val(data.Precio);
    $('#tIva').val(0);
    $("#tsubtotal").val(0);
    $("#tCantidad").val(data.Cantidad);


    $('#modal-record').modal('toggle');
}

function Guardar() {
    var record = "'IdOrdenProducto':" + IdRecord;
    record += ",'IdOrden':'" + $.trim($('#tidOrden').val()) + "'";
    record += ",'IdProducto':'" + $.trim($('#tIdProducto').val()) + "'";
    record += ",'Precio':'" + $.trim($('#tPrecio').val()) + "'";
    record += ",'Iva':'" + $.trim($('#tIva').val()) + "'";
    record += ",'Subtotal':'" + $.trim($('#tsubtotal').val()) + "'";
    record += ",'Cantidad':'" + $.trim($('#tCantidad').val()) + "'";


    $.ajax({
        type: 'POST',
        url: '/OrdenProduto/Guardar',
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
        url: '/OrdenProduto/Eliminar/?IdOrdenProducto=' + IdRecord,
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
        url: '/Producto/Lista',
        success: function (response) {

            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#tIdProducto").append(`<option value="${response.data[i].IdProducto}"> 
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
        url: '/Orden/Lista',
        success: function (response) {

            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#tidOrden").append(`<option value="${response.data[i].IdOrden}"> 
                                       ${response.data[i].IdOrden}
                                  </option>`);
                });

            } else {
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });

}