var table;
var id;
$(document).ready(function () {
    //Esconde o menu lateral esqued
    id = sessionStorage.getItem("idPac")

    getRelatorio();
});

function getRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/historico_alimentars/" + id
    //var url = "http://localhost:5000/v1/api/historico_alimentars/" + id
    table = $('#Histo').DataTable({
        processing: true,
        pagingType: "full_numbers",
        searching: false,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.10.20/i18n/Portuguese-Brasil.json",
        },
        rowId: "Id",
        ajax: {
            url: url,
            // url: "https://hedi-api.azurewebsites.net/v1/api/Historico_Alimentar",
            async: true,
            type: "GET",
            data: {
            },

            dataType: 'json',
            headers: {
                "Authorization":
                    "Bearer " + sessionStorage.getItem("token")
            },
            error: function (ex) {
                sessionStorage.clear();
                window.location.replace("/");
                console.log(ex);
                console.log(ex.textStatus);
                console.log(ex.errorThrown);

            }

        },
        contentType: 'application/json; charset=utf-8',
        // Headers = function (request) {
        //     request.setRequestHeader("Authorization",
        //         "Bearer " + sessionStorage.getItem("token"));
        // },
        columns: [
            { data: "refeicao" },
            { data: "glicemia_Alvo", "type": "num" },
            { data: "glicemia_Obtida", type: "num" },
            { data: "carboidratos_Total", type: "num" },
            { data: "insulina_Calculada", type: "num" },
            { data: "data_Hora", type: "num" },
            { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaAlimentoHistorico(' + data.id + ') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}

function postRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/historico_alimentars/" + id
    //var url = "http://localhost:5000/v1/api/historico_alimentars/" + id
    table = $('#Histo').DataTable({
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
            url: url,
            // url: "https://hedi-api.azurewebsites.net/v1/api/Historico_Alimentar",
            async: true,
            type: "POST",
            data: {
            },

            dataType: 'json',
            headers: {
                "Authorization":
                    "Bearer " + sessionStorage.getItem("token")
            },
            error: function (ex) {
                sessionStorage.clear();
                window.location.replace("/");
                console.log(ex);
                console.log(ex.textStatus);
                console.log(ex.errorThrown);

            }

        },
        contentType: 'application/json; charset=utf-8',
        // Headers = function (request) {
        //     request.setRequestHeader("Authorization",
        //         "Bearer " + sessionStorage.getItem("token"));
        // },
        columns: [
            { data: "refeicao" },
            { data: "glicemia_Alvo" },
            { data: "glicemia_Obtida" },
            { data: "carboidratos_Total" },
            { data: "insulina_Calculada" },
            { data: "data_Hora" },
            { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaAlimentoHistorico(' + data.id + ') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}

function ConsultaAlimentoHistorico(id) {

    sessionStorage.setItem("idHist", id);
    window.location.replace("/page/HistoricoAlimentar/AlimentosRefeicao");
}
