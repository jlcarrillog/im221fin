(function () {
    'use strict'
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    tooltipTriggerList.forEach(function (tooltipTriggerEl) {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
})();

///////

//http://www.nikolay.rocks/2015-10-29-rainbows-generator-in-javascript
function interpolateColors(steps) {
    var data = new Array(steps);
    var frequency = 2 * Math.PI / steps;
    var redFrequency = frequency; //1.666;
    var grnFrequency = frequency; //2.666;
    var bluFrequency = frequency; //3.666;
    var phase1 = 0; //0
    var phase2 = 2; //2
    var phase3 = 4; //4
    var center = 128;
    var width = 127;
    for (var i = 0; i < steps; ++i) {
        var r = Math.sin(redFrequency * i + phase1) * width + center;
        var g = Math.sin(grnFrequency * i + phase2) * width + center;
        var b = Math.sin(bluFrequency * i + phase3) * width + center;
        r /= 255.0, g /= 255.0, b /= 255.0;
        var max = Math.max(r, g, b), min = Math.min(r, g, b);
        var h, s, l = (max + min) / 2.0;
        if (max == min) {
            h = s = 0.0;
        } else {
            var d = max - min;
            s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
            switch (max) {
                case r: h = (g - b) / d + (g < b ? 6.0 : 0.0); break;
                case g: h = (b - r) / d + 2; break;
                case b: h = (r - g) / d + 4; break;
            }
            h /= 6.0;
        }
        data[i] = 'hsl(' + ((h * 100 + 0.5) | 0) + ',' + ((s * 100 + 0.5) | 0) + '%,' + ((l * 100 + 0.5) | 0) + '%)';
    }
    return data;
}


//////////////////////////////


function dataTables(table) {
    var table = $(table).DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                autoFilter: true,
                sheetName: 'Ventas'
            },
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL'
            },
            'print',
            'colvis'
        ],
        lengthChange: false,
        scrollX: true,
        searchable: false,
        info: false,
        language: {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            oAria: {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    });
    table.buttons().container().appendTo('#btn-toolbar .col-md-6:eq(0)');
}

//////////////////////////////////

function details(controller, id) {
    $('#detailsModal').modal('show');
    $.ajax("/" + controller + "/Details/" + id)
        .done(function (data) {
            $("#detallesModalContent").html(data);
        });
}