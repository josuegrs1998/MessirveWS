var tableHdr = null;
var tableHdrP = null;
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
        IdRecord = data.IdOrden;
    });
    //Productos
    $('#dt-records').on('click', 'button.btn-products', function (e) {
   
        //ordenDiv
        document.getElementById("ordenDiv").style.display = "none";
        document.getElementById("modalProductos").style.display = "block";
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        var idOrden = data.IdOrden;
        loadDataP(idOrden);
        $("#tidOrdenP").val(idOrden);
        
        //
       // cargarSelect();
        
    });

    $('#btnnuevoP').on('click', function (e) {
        e.preventDefault();
        IdRecord = 0;
        NewRecordP();
    });

    $('#btnguardarP').on('click', function (e) {
        e.preventDefault();

        GuardarP();
    });

    $('#dt-recordsP').on('click', 'button.btn-edit', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdrP.row(_this).data();

        loadDtlP(data);
        IdRecord = data.IdOrdenProducto;
    });

    $('#dt-recordsP').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdrP.row(_this).data();
        IdRecord = data.IdOrdenProducto;
        if (confirm('¿Seguro de eliminar el registro?')) {
            EliminarP();
        }
    });
    //FinProductos
    $('#dt-records').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        IdRecord = data.IdOrden;
        if (confirm('¿Seguro de eliminar el registro?')) {
            Eliminar();
        }
    });

});

function loadData() {
   
    tableHdr = $('#dt-records').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/Orden/Lista",
        order: [],
        columns: [
            { "data": "IdUsuarioNormal" },
            { "data": "NumeroOrden" },
            { "data": "Estado" },
            { "data": "fecha" },
            { "data": "Envio" },
            { "data": "SubTotal" },
            { "data": "Impuesto" },
            { "data": "TotalOrden" },
            

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
                data: "IdUsuarioNormal"
            },
            {
                width: "8%",
                targets: 1,
                data: "NumeroOrden"
            },
            {
                width: "8%",
                targets: 2,
                data: "Estado"
            },
            {
                width: "8%",
                targets: 3,
                data: "fecha"
            },
            {
                width: "8%",
                targets: 4,
                data: "Envio"
            },
            {
                width: "8%",
                targets: 5,
                data: "Subtotal"
            },
            {
                width: "8%",
                targets: 6,
                data: "Impuesto"
            },
            {
                width: "8%",
                targets: 7,
                data: "TotalOrden"
            },
            {
                width: "5%",
                targets: 8,
                data: null,
                defaultContent: '<button type="button" class="btn btn-info btn-sm btn-products" ><i class=""></i>Productos</button>'

            },
            {
                width: "5%",
                targets: 9,
                data: null,
                defaultContent: '<button type="button" class="btn btn-info btn-sm btn-edit" data-target="#modal-record"><i class="fa fa-pencil"></i></button>'
            },
            {
                width: "5%",
                targets: 10,
                data: null,
                defaultContent: '<button type="button" class="btn btn-danger btn-sm btn-delete"><i class="fa fa-trash"></i></button>'

            }
        ]
    });
}

function NewRecord() {
    $(".modal-header h3").text("Crear Orden");

    $('#tIdOrden').val('');
    $('#tNumOrden').val('');
    $('#tEstado').val('');
    $('#tImpuesto').val('');
    $('#tEnvio').val('');
    $('#tSubtotal').val('');
    $('#ttotal').val('');
    $('#tfechai').val('');
    $('#tcupon').val('');
    $('#tusuario').val('');

    $('#modal-record').modal('toggle');
}

function loadDtl(data) {
    $(".modal-header h3").text("Editar Orden");

    $("#tIdOrden").val(data.IdOrden);
    $('#tNumOrden').val(data.NumeroOrden);
    $("#tEstado").val(data.Estado);
    $('#tImpuesto').val(data.Impuesto);
    $('#tEnvio').val(data.Envio);
    $("#tSubtotal").val(data.SubTotal);
    $("#ttotal").val(data.TotalOrden);
    //var from = $("tfechai").val().split("/");
    //var f = new Date(from[2], from[1] - 1, from[0]);
    //$("#tfechai").val(fecha);
    $("#idOrdenP").text(data.IdOrden);
    $("#tcupon").val(data.IdCupon);
    $("#tusuario").val(data.IdUsuarioNormal);
   

    $('#modal-record').modal('toggle');
}

function Guardar() {
    var record = "'IdOrden':" + IdRecord;
    record += ",'NumeroOrden':'" + $.trim($('#tNumOrden').val()) + "'";
    record += ",'Estado':'" + $.trim($('#tEstado').val()) + "'";
    record += ",'Impuesto':'" + $.trim($('#tImpuesto').val()) + "'";
    record += ",'Envio':'" + $.trim($('#tEnvio').val()) + "'";
    record += ",'Subtotal':'" + $.trim($('#tSubtotal').val()) + "'";
    record += ",'TotalOrden':'" + $.trim($('#ttotal').val()) + "'";
    record += ",'FechaIngreso':'" + $.trim($('#tfechai').val()) + "'";
    record += ",'IdCupon':'" + $.trim($('#tcupon').val()) + "'";
    record += ",'IdUsuarioNormal':'" + $.trim($('#tusuario').val()) + "'";


    $.ajax({
        type: 'POST',
        url: '/Orden/Guardar',
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
        url: '/Orden/Eliminar/?IdOrden=' + IdRecord,
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

//Funciones OrdenProducto


function desaparecerProductos() {
  
    document.getElementById("modalProductos").style.display = "none";
    document.getElementById("ordenDiv").style.display = "block";
}


function loadDataP(idOrden) {
    
  
    tableHdrP = $('#dt-recordsP').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/OrdenProduto/Lista/?idOrden=" + idOrden,
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


function NewRecordP() {
    $(".modal-header h3").text("Crear OrdenProducto");

    $('#tIdOrdenProducto').val('');
    $('#tidOrden').val('');
    $('#tIdProducto').val('');
    $('#tPrecio').val('');
    $('#tIva').val('');
    $('#tsubtotal').val('');
    $('#tCantidad').val('');

    $('#modal-record').modal('toggle');
}

function loadDtlP(data) {
    $(".modal-header h3").text("Editar OrdenProducto");

    $("#tIdOrdenProducto").val(data.IdOrdenProducto);
    $('#tidOrden').val(data.IdOrden);
    $("#tIdProducto").val(data.IdProducto);
    $('#tPrecio').val(data.Precio);
    $('#tIva').val(0);
    $("#tsubtotal").val(0);
    $("#tCantidad").val(data.Cantidad);


    $('#modal-record').modal('toggle');
}

function GuardarP() {
    var record = "'IdOrdenProducto':" + IdRecord;
    record += ",'IdOrden':'" + $.trim($('#tidOrdenP').val()) + "'";
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
                $('#dt-recordsP').DataTable().ajax.reload(null, false);
            }
            else {
                $("#modal-record").modal('hide');
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });
}

function EliminarP() {
    $.ajax({
        type: 'POST',
        url: '/OrdenProduto/Eliminar/?IdOrdenProducto=' + IdRecord,
        success: function (response) {
            if (response.success) {
                $.notify(response.message, { globalPosition: "top center", className: "success" });
                $('#dt-recordsP').DataTable().ajax.reload(null, false);
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