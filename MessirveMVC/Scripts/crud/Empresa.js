﻿var tableHdr = null;
var IdRecord = 0;

$(document).ready(function () {
    loadData();

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
        IdRecord = data.IdEmpresa;
    });

    $('#dt-records').on('click', 'button.btn-delete', function (e) {
        var _this = $(this).parents('tr');
        var data = tableHdr.row(_this).data();
        IdRecord = data.IdEmpresa;
        if (confirm('¿Seguro de eliminar el registro?')) {
            Eliminar();
        }
    });

});

function loadData() {
    tableHdr = $('#dt-records').DataTable({
        responsive: true,
        destroy: true,
        ajax: "/Empresa/Lista",
        order: [],
        columns: [
            { "data": "IdEmpresa" },
            { "data": "Nombre" },
            { "data": "RUC" },
            { "data": "Telefono" },
            { "data": "Correo" },
            { "data": "Descripcion" },

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
                data: "IdEmpresa"
            },
            {
                width: "10%",
                targets: 1,
                data: "Nombre"
            },
            {
                width: "10%",
                targets: 2,
                data: "RUC"
            },
            {
                width: "10%",
                targets: 3,
                data: "Telefono"
            },
            {
                width: "10%",
                targets: 4,
                data: "Correo"
            },
            {
                width: "10%",
                targets: 5,
                data: "Descripcion"
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
    $(".modal-header h3").text("Crear Empresa");

    $('#txtIdEmpresa').val('');
    $('#txtNombre').val('');
    $('#txtRUC').val('');
    $('#txtTelefono').val('');
    $('#txtCorreo').val('');
    $('#txtDescripcion').val('');


    $('#modal-record').modal('toggle');
}

function loadDtl(data) {
    $(".modal-header h3").text("Editar Empresa");

    $("#txtIdEmpresa").val(data.IdEmpresa);
    $('#txtNombre').val(data.Nombre);
    $("#txtRUC").val(data.RUC);
    $('#txtTelefono').val(data.Telefono);
    $('#txtCorreo').val(data.Correo);
    $("#txtDescripcion").val(data.Descripcion);
    

    $('#modal-record').modal('toggle');
}

function Guardar() {
    var record = "'IdEmpresa':" + IdRecord;
    record += ",'Nombre':'" + $.trim($('#txtNombre').val()) + "'";
    record += ",'RUC':'" + $.trim($('#txtRUC').val()) + "'";
    record += ",'Telefono':'" + $.trim($('#txtTelefono').val()) + "'";
    record += ",'Correo':'" + $.trim($('#txtCorreo').val()) + "'";
    record += ",'Descripcion':'" + $.trim($('#txtDescripcion').val()) + "'";
   

    $.ajax({
        type: 'POST',
        url: '/Empresa/Guardar',
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
        url: '/Empresa/Eliminar/?IdEmpresa=' + IdRecord,
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