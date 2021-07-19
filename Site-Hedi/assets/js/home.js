var user = JSON.parse(sessionStorage.getItem("user"));
var table;

$(document).ready(function () {
    //Esconde o menu lateral esqued
    if (!sessionStorage.getItem("token")) {
        window.location.replace("/");
    }
    getRelatorio();
});

function getRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/pacientes/search/" + user.id;
    //var url = "http://localhost:5000/v1/api/pacientes/search/" + user.id;
    table = $('#pacientes').DataTable({
        processing: true,
        pagingType: "full_numbers",
        searching: false,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.20/i18n/Portuguese-Brasil.json",
        },
        rowId: "Id",
        ajax: {
            // url: "https://hedi-api.azurewebsites.net/api/pacientes/logado",
            url: url,
            async: true,
            type: "GET",
            // data: { "select": $("#select-refeicao option:selected").val() },
            data: {

            },
            dataType: 'json',
            headers: {
                "Authorization":
                    "Bearer " + sessionStorage.getItem("token")
            },
            error: function (ex) {
                // console.log(ex.ToString())
                sessionStorage.clear();
                window.location.replace("/");
            },


        },
        contentType: 'application/json; charset=utf-8',
        // Headers = function (request) {
        //     request.setRequestHeader("Authorization",
        //         "Bearer " + sessionStorage.getItem("token"));
        // },
        columns: [
            { data: "nome" },
            { data: "fator_Sen" },
            { data: "tipo_Diab" },
            { data: "telefone" },
            { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaPaciente(' + data.id + ') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}
function postRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/pacientes/search/" + user.id;
    //var url = "http://localhost:5000/v1/api/pacientes/search/" + user.id;
    table = $('#pacientes').DataTable({
        processing: true,
        serverSide: true,
        ordering: true,
        destroy: true,
        pagingType: "full_numbers",
        searching: false,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.20/i18n/Portuguese-Brasil.json",
        },
        rowId: "Id",

        ajax: {
            // url: "https://hedi-api.azurewebsites.net/api/pacientes/logado",
            url: url,
            async: true,
            type: "POST",
            // data: { "select": $("#select-refeicao option:selected").val() },
            data: {
                "pesquisa": $("#pesquisa").val(),
                "selectColuna": $("#select-coluna option:selected").val(),
                "selectDiabetes": $("#select-diabetes option:selected").val(),
            },
            dataType: 'json',
            headers: {
                "Authorization":
                    "Bearer " + sessionStorage.getItem("token")
            },
            error: function (ex) {
                console.log(ex.ToString())
                sessionStorage.clear();
                window.location.replace("/");
            },


        },
        contentType: 'application/json; charset=utf-8',
        // Headers = function (request) {
        //     request.setRequestHeader("Authorization",
        //         "Bearer " + sessionStorage.getItem("token"));
        // },
        columns: [
            { data: "nome" },
            { data: "fator_Sen" },
            { data: "tipo_Diab", render: function (data) { if (data == 1) { return "Tipo 1"; } if (data == 2) { return "Tipo 2"; } if (data == 3) { return "Gestacional"; } if (data == 4) { return "Pré Diabetes"; } } },
            { data: "telefone" },
            { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaPaciente(' + data.id + ') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}

function ConsultaPaciente(id) {

    sessionStorage.setItem("idPac", id);
    window.location.replace("/page/HistoricoAlimentar/");
}
