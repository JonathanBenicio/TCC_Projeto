$(document).ready(function () {

    if (sessionStorage.getItem("token")) {
        window.location.replace("/page/");
    }
    if ($("form#formCadastro").length > 0) {
        jQuery("form#formCadastro").submit(function (e) { e.preventDefault(); return false; });

        $("#formCadastro").validate({
            rules: {
                nome: {
                    required: true,
                    minlength: 7
                },
                cep: {
                    required: true,
                    minlength: 8,
                },
                endereco: {
                    required: true
                },
                numero: {
                    required: true
                },
                email: {
                    required: true,
                    email: true
                },
                senha: {
                    required: true,
                },
                senha2: {
                    required: true,
                    equalTo: '#senha'
                },
            },
            messages: {
                nome: {
                    required: "Preencha o nome",
                    minlength: "Insira o nome completo."
                },
                cep: {
                    required: "Preencha o CEP",
                    minlength: "digite um CEP valido.",
                },
                cep: {
                    required: "Preencha o CEP",
                },
                endereco: {
                    required: "Preencha o Endereço",
                },
                numero: {
                    required: "Preencha o Numero do Endereço"
                },
                telefone: {
                    required: "Preencha o Numero de Telefone"
                },
                email: {
                    required: "Preencha o e-mail",
                    email: "Insira uma e-mail válido."
                },
                senha: {
                    required: "Informe uma senha",
                },

                senha2: "As senhas digitadas não coincidem."
            },
            errorElement: 'div',
            errorLabelContainer: '.msgAvisos'
        });

    }

    if (jQuery("#cadastro").length > 0) {

        $("#cadastro").click(function () {
            if ($('form#formCadastro').valid()) {
                var url = "https://hedi-api.azurewebsites.net/v1/api/nutricionistas"
                //var url = "http://localhost:5000/v1/api/nutricionistas"
                var options = {};
                // options.url = "https://hedi-api.azurewebsites.net/api/Login";
                options.url = url;
                options.type = "POST";
                // var obj = {};
                // obj.Email = $("#email").val();
                // obj.Senha = $("#senha").val(); 
                // obj.Nome =  
                // obj.Telefone = $("#telefone").val(); 
                // obj.Endereco = $("#endereco").val()+ " - "+$("#numero").val()+ " - "+$("#cep").val(); 
                var postJson = {
                    "nome": $("#nome").val(),
                    "telefone": $("#telefone").val(),
                    "endereco": $("#endereco").val() + " - " + $("#numero").val() + " - " + $("#cep").val(),
                    "login": { "email": $("#email").val(), "senha": $("#senha").val() }
                };
                options.data = JSON.stringify(postJson);
                options.contentType = "application/json";
                options.dataType = "json";
                options.success = function (obj) {
                    console.log(obj);
                    window.location.replace("/page/Cadastrar/CadastroConcluido");
                };
                options.error = function (obj) {
                    var msg = obj.responseJSON
                    if (msg) {
                        $(".msgAvisos").html("<h3>" + msg.message + "</h3>");
                    } else {
                        $(".msgAvisos").html("<h3> Não foi possivel realizar Cadastro </h3>");
                    }
                    window.location.replace("/page/Cadastrar/errocadastro.html");
                };
                $.ajax(options);
            }
        });


    }
});