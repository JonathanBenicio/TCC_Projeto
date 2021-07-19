var table;
$(document).ready(function () {
    //Esconde o menu lateral esqued

    compBuyByDay();
    getRelatorio();
});

function compBuyByDay() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/alimento_historicos/grafico/" + sessionStorage.getItem("idHist")
    //var url = "http://localhost:5000/v1/api/alimento_historicos/grafico/" + sessionStorage.getItem("idHist")
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Authorization":
                "Bearer " + sessionStorage.getItem("token")
        },
        success: function (chData) {


            var aDatasets1 = chData;

            var dataT = {
                labels: ['January'],
                datasets: [
                    {
                        label: "Quantidade",
                        data: aDatasets1,
                        backgroundColor: 'rgba(60,141,188,0.9)',
                        borderColor: 'rgba(60,141,188,0.8)',
                        pointRadius: false,
                        pointColor: '#3b8bba',
                        pointStrokeColor: 'rgba(60,141,188,1)',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgba(60,141,188,1)',
                    },
                ]
            };
            var ctx = $("#barChart").get(0).getContext("2d");
            var myNewChart = new Chart(ctx, {
                type: 'bar',
                data: dataT,
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    legend: { display: false },
                    scales: {
                        xAxes: [{

                            stacked: true,

                            gridLines: { display: false },
                            display: true,
                            scaleLabel: { display: false, labelString: '' }
                        }],
                        yAxes: [{
                            stacked: true,


                            gridLines: { display: false },
                            display: true,
                            scaleLabel: { display: false, labelString: '' },
                            ticks: {
                                min: 0,
                                callback: function (value, index, values) {
                                    if (Math.floor(value) === value) {
                                        return value;
                                    }
                                }
                            }
                        }]
                    },
                }
            });

        }
    });
}
function getRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/alimento_historicos/" + sessionStorage.getItem("idHist")
    //var url = "http://localhost:5000/v1/api/alimento_historicos/" + sessionStorage.getItem("idHist")
    table = $('#AlimentosHistorico').DataTable({
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
            { data: "alimento" },
            { data: "quantidade", },
            { data: "carboTotal", },
            { data: "marca", },
            { data: "tipo", },
            { data: "porcao", },
            { data: "carboPorcao", },
            // { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaAlimentoHistorico('+data.id+') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}


function postRelatorio() {
    var url = "https://hedi-api.azurewebsites.net/v1/api/alimento_historicos"+ sessionStorage.getItem("idHist")
    //var url = "http://localhost:5000/v1/api/alimento_historicos/" + sessionStorage.getItem("idHist")
    table = $('#AlimentosHistorico').DataTable({
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
                // +"select": $("#select-refeicao option:selected").val(),
                "pesquisa": $("#pesquisa").val(),
                "selectColuna": $("#select-coluna option:selected").val(),
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
            { data: "alimento" },
            { data: "quantidade", },
            { data: "carboTotal", },
            { data: "marca", },
            { data: "tipo", },
            { data: "porcao", },
            { data: "carboPorcao", },
            // { data: null, render: function (data, type, row) { return '<div> <a onclick=ConsultaAlimentoHistorico('+data.id+') class="btnconsultar"> Consultar </button></div>' } },
        ],

    });
}

function ConsultaAlimentoHistorico(id) {

    sessionStorage.setItem("id ", id);
    window.location.replace("AlimentosRefeicao");
}
