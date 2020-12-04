var tableHdr = null;
var IdRecord = 0;
var cateCargada = false;
$(document).ready(function () {
    loadData();
    NewRecord();
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
        IdRecord = data.IdProducto;
    });

    $('#dt-records').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        IdRecord = data.IdProducto;
        if (confirm('¿Seguro de eliminar el registro?')) {
            Eliminar();
        }
    });

});

function loadData() {
    tableHdr = $('#dt-records').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/Producto/Lista",
        order: [],
        columns: [
            { "data": "IdProducto" },
            { "data": "Nombre" },
            { "data": "Codigo" },
            { "data": "Decripcion" },
            { "data": "Activo" },
            { "data": "Exento" },
            { "data": "IdMarca" },
            { "data": "SubCategoria1.Nombre" },
            //{ "data": "IdSubCategoria" },
            { "data": "Categoria1.Nombre" },
            
           

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
                data: "IdProducto"
            },
            {
                width: "10%",
                targets: 1,
                data: "Nombre"
            },
            {
                width: "10%",
                targets: 2,
                data: "Codigo"
            },
            {
                width: "10%",
                targets: 3,
                data: "Decripcion"
            },
            {
                width: "10%",
                targets: 4,
                data: "Activo"
            },
            {
                width: "10%",
                targets: 5,
                data: "Exento"
            },
            {
                width: "10%",
                targets: 6,
                data: "IdMarca"
            },
            {
                width: "10%",
                targets: 7,
                data: "SubCategoria1.Nombre"
            },
            {
                width: "10%",
                targets: 8,
                data: "Categoria1.Nombre"
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
    $(".modal-header h3").text("Crear Producto");

    $('#txtIdProducto').val('');
    $('#txtNombre').val('');
    $('#txtCodigo').val('');
    $('#txtDecripcion').val('');
    $('#txtActivo').val('');
    $('#txtExento').val('');
    $('#txtIdMarca').val('');
    $('#txtIdSubCategoria').val('');
    $('#txtIdCategoria').val('');
    if (cateCargada != true) {
        cargarCate();
    }
    

    $('#modal-record').modal('toggle');
}

function loadDtl(data) {
    $(".modal-header h3").text("Editar Producto");

    $("#txtIdProducto").val(data.IdProducto);
    $('#txtNombre').val(data.Nombre);
    $("#txtCodigo").val(data.Codigo);
    $("#txtDecripcion").val(data.Decripcion);
    $('#txtActivo').val(data.Activo);
    $("#txtExento").val(data.Exento);
    $("#txtIdMarca").val(data.IdMarca);
    $('#txtIdSubCategoria').val(data.IdSubCategoria);
    $("#txtIdCategoria").val(data.IdCategoria);
    //$('#txtSubCategoria').val(data.SubCategoria.Nombre);
    //$("#txtCategoria").val(data.Categoria.Nombre);

    if (cateCargada != true) {
        cargarCate();
    }
    $('#modal-record').modal('toggle');
}

function Guardar() {
    var record = "'IdProducto':" + IdRecord;
    record += ",'Nombre':'" + $.trim($('#txtNombre').val()) + "'";
    record += ",'Codigo':'" + $.trim($('#txtCodigo').val()) + "'";
    record += ",'Decripcion':'" + $.trim($('#txtDecripcion').val()) + "'";
    record += ",'Activo':'" + $.trim($('#txtActivo').val()) + "'";
    record += ",'Exento':'" + $.trim($('#txtExento').val()) + "'";
    record += ",'IdMarca':'" + $.trim($('#txtIdMarca').val()) + "'";
    record += ",'IdSubCategoria':'" + $.trim($('#txtIdSubCategoria').val()) + "'";
    record += ",'IdCategoria':'" + $.trim($('#txtIdCategoria').val()) + "'";

    $.ajax({
        type: 'POST',
        url: '/Producto/Guardar',
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
        url: '/Producto/Eliminar/?IdProducto=' + IdRecord,
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

function cargarCate() {

    $.ajax({
        type: 'GET',
        url: '/Categoria/Lista',
        success: function (response) {
            
            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#txtIdCategoria").append(`<option value="${response.data[i].IdCategoria}"> 
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
        url: '/SubCategoria/Lista',
        success: function (response) {

            if (response.success) {
                cateCargada = true;
                $.each(response.data, function (i, val) {
                    $("#txtIdSubCategoria").append(`<option value="${response.data[i].IdSubCategoria}"> 
                                       ${response.data[i].Nombre}
                                  </option>`);
                });

            } else {
                $.notify(response.message, { globalPosition: "top center", className: "error" });
            }
        }
    });
 
}