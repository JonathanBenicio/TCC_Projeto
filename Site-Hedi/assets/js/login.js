$(document).ready(function () {

    if (sessionStorage.getItem("token")) {
        window.location.replace("/page/");
    }
    if ($("form#formLogin").length > 0) {
        jQuery("form#formLogin").submit(function (e) { e.preventDefault(); return false; });
        $("#formLogin").validate({
            rules: {
                email: {
                    required: true,
                    email: true
                },
                senha: {
                    required: true,
                },
            },
            messages: {
                email: {
                    required: "Preencha o e-mail",
                    email: "Insira uma e-mail válido."
                },
                senha: {
                    required: "Informe uma senha",
                },
            },
            errorElement: 'div',
            errorLabelContainer: '.msgAvisos'
        });

    }

    if (jQuery("#login").length > 0) {

        $("#login").click(function () {
            if ($('form#formLogin').valid()) {
                var url = "https://hedi-api.azurewebsites.net/v1/api/logins/nutri"
                //var url = "http://localhost:5000/v1/api/logins/nutri"

                var options = {};
                // options.url = "https://hedi-api.azurewebsites.net/api/Login";
                options.url = url;
                options.type = "POST";
                var obj = {};
                obj.Email = $("#email").val();
                obj.Senha = $("#senha").val();
                options.data = JSON.stringify(obj);
                options.data = JSON.stringify(obj);
                options.contentType = "application/json";
                options.dataType = "json";
                options.success = function (obj) {
                    sessionStorage.setItem("user", JSON.stringify(obj.user));
                    sessionStorage.setItem("token", obj.token);
                    window.location.replace("/page/");
                };
                options.error = function (obj) {
                    var msg = obj.responseJSON
                    if (msg) {
                        $(".msgAvisos").html("<h3>" + msg.message + "</h3>");
                    } else {
                        $(".msgAvisos").html("<h3> Não foi possivel realizar login </h3>");
                    }
                };
                $.ajax(options);
            }
        });


    }
});


// $(document).ready(function () {

//     jQuery(".cpf").mask("000.000.000-00");
//     jQuery(".data").mask("00/00/0000");

//     var loader = '<div class="ofebas-loader"><div class="position-spinner"><div class="lds-spinner"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></div></div>';



//     //Form de Cadastro de usuario
//     if (jQuery("form#formCadastro").length > 0) {
//         if (jQuery("#etapas #step-1").length > 0) jQuery("#etapas #step-1").show();

//         jQuery("form#formCadastro").submit(function (e) { e.preventDefault(); return false; });


//         jQuery('form#formCadastro').validate({
//             rules: {
//                 titular_cpf: {
//                     required: true,
//                     minlength: 14,
//                     maxlength: 14
//                 },
//                 // titular_datanasc: {
//                 //     required: true,
//                 //     minlength: 10,
//                 //     maxlength: 10
//                 // },
//                 nome: {
//                     required: true,
//                     minlength: 7
//                 },
//                 email: {
//                     required: true,
//                     email: true
//                 },
//                 opass: {
//                     required: true,
//                     minlength: 4
//                 },
//                 reopass: {
//                     required: true,
//                     minlength: 4,
//                     equalTo: '#opass'
//                 }
//             },
//             messages: {
//                 titular_cpf: "Informe um cfp vÃ¡lido.",
//                 // titular_datanasc: "Informe uma data vÃ¡lida.",
//                 nome: {
//                     required: "Preencha o nome",
//                     minlength: "Insira o nome completo."
//                 },
//                 email: {
//                     required: "Preencha o email",
//                     email: "Insira uma e-mail vÃ¡lido."
//                 },
//                 opass: {
//                     required: "Informe uma senha",
//                     minlength: "A senha precisa ter no mÃ­nimo 4 dÃ­gitos."
//                 },
//                 reopass: "As senhas digitadas nÃ£o coincidem."
//             },
//             errorElement: 'div',
//             errorLabelContainer: '.msgAvisos'
//         });


//     }

//     if (jQuery("#validaTitular").length > 0) {
//         jQuery("#validaTitular").click(function (event) {
//             jQuery(".msgAvisos").slideUp();


//             console.log(jQuery("input[name=titular_cpf]").val());

//             if (jQuery('form#formCadastro').valid()) {

//                 jQuery(".ofebas-loader").remove();
//                 jQuery(".login-body").removeClass("login-body-waint");

//                 jQuery(".login-body").addClass("login-body-waint").append(loader);

//                 jQuery("input, button").prop('disabled', true);

//                 jQuery.ajax({
//                     method: "POST",
//                     url: urlBase + "cadastro/valida-titular",
//                     data: {
//                         cpf: jQuery("input[name=titular_cpf]").val(),
//                         // data: jQuery("input[name=titular_datanasc]").val()
//                     }
//                 }).done(function (reposta) {
//                     console.log(reposta);

//                     jQuery(".ofebas-loader").remove();
//                     jQuery(".login-body").removeClass("login-body-waint");

//                     jQuery("input, button").prop('disabled', false);

//                     var jsonR = ((reposta != "") ? JSON.parse(reposta) : []);

//                     if (jsonR.hasOwnProperty("retorno") && jsonR.retorno == "ok") {

//                         if (jsonR.hasOwnProperty("titular") && jsonR.titular != "") {
//                             jQuery("input[name=nome]").val(jsonR.titular).show();
//                             console.log(jsonR.titular);
//                         }

//                         jQuery(".msgAvisos").slideUp();
//                         jQuery("#etapas #step-1").hide();
//                         jQuery("#etapas #step-2").show();

//                     } else if (jsonR.hasOwnProperty("msg") && jsonR.msg != "") {
//                         jQuery(".msgAvisos").html(jsonR.msg).slideDown();
//                     }

//                     //console.log(reposta, jsonR);
//                 });

//                 //console.log("ok!");
//             } else {
//                 //console.log("ainda nÃ£o..");
//             }
//         });
//     }

//     if (jQuery("button#btCadastrar").length > 0) {
//         jQuery("button#btCadastrar").click(function (event) {
//             jQuery(".msgAvisos").slideUp();

//             if (jQuery('form#formCadastro').valid()) {

//                 jQuery(".ofebas-loader").remove();
//                 jQuery(".login-body").removeClass("login-body-waint");

//                 jQuery(".login-body").addClass("login-body-waint").append(loader);

//                 var dataForm = jQuery("form#formCadastro").serialize();

//                 jQuery("input, button").prop('disabled', true);

//                 jQuery.ajax({
//                     method: "POST",
//                     url: urlBase + "cadastro/cadastrar",
//                     data: dataForm
//                 }).done(function (reposta) {

//                     jQuery(".ofebas-loader").remove();
//                     jQuery(".login-body").removeClass("login-body-waint");

//                     jQuery("input, button").prop('disabled', false);

//                     var jsonR = ((reposta != "") ? JSON.parse(reposta) : []);

//                     if (jsonR.hasOwnProperty("retorno") && jsonR.retorno == "ok") {
//                         jQuery(".msgAvisos").slideUp();
//                         jQuery("#etapas #step-2").hide();
//                         jQuery("#etapas #step-3").show();

//                     }

//                     if (jsonR.hasOwnProperty("msg") && jsonR.msg != "") {
//                         jQuery(".msgAvisos").html(jsonR.msg).slideDown();;
//                     }

//                     //console.log(reposta, jsonR);
//                 });


//                 //console.log("ok!");
//                 /*jQuery("#etapas #step-1").hide();
//                 jQuery("#etapas #step-2").show()*/
//             } else {
//                 //console.log("ainda nÃ£o..");
//             }
//         });
//     }

//     // Form envia email resetar senha
//     if ($("form#formReset").length > 0) {
//         if ($("#etapas #step-1").length > 0) $("#etapas #step-1").show();

//         $('form#formReset').validate({
//             rules: {
//                 email: {
//                     required: true,
//                     email: true
//                 }
//             },
//             messages: {
//                 email: {
//                     required: "Preencha o email",
//                     email: "Insira uma e-mail vÃ¡lido."
//                 }
//             },
//             errorElement: 'div',
//             errorLabelContainer: '.msgAvisos'
//         });

//     }

//     if ($("button#enviarEmail").length > 0) {
//         $('#enviarEmail').click(function (event) {
//             $(".msgAvisos").slideUp();


//             if ($('form#formReset').valid()) {
//                 jQuery(".ofebas-loader").remove();
//                 jQuery(".login-body").removeClass("login-body-waint");

//                 jQuery(".login-body").addClass("login-body-waint").append(loader);

//                 jQuery("input, button").prop('disabled', true);

//                 $.ajax({
//                     type: "POST",
//                     url: urlBase + "login/recuperar",
//                     data: { email: $('input[name=email]').val() },

//                     success: function (response) {

//                         jQuery(".ofebas-loader").remove();
//                         jQuery(".login-body").removeClass("login-body-waint");

//                         jQuery("input, button").prop('disabled', false);

//                         console.log("ok POST! ");

//                         console.log(response.toString());



//                         var jsonR = ((response != "") ? JSON.parse(response) : []);

//                         if (jsonR.retorno == 'ok') {
//                             $('.msgAvisos').slideUp();
//                             $("#etapas #step-1").hide();
//                             $("#etapas #step-2").show();


//                         } else if (jsonR.retorno == 'fall') {

//                             $(".msgAvisos").html(jsonR.msg).slideDown();



//                         }
//                     }

//                 });
//                 console.log("ok!");
//             } else {
//                 console.log("ainda nÃ£o..");
//             }
//         });
//     }

//     //Formulario Cadastrar nova senha
//     if ($("form#formNovaSenha").length > 0) {
//         if ($("#etapas #step-1").length > 0) $("#etapas #step-1").show();

//         jQuery('form#formNovaSenha').validate({
//             rules: {
//                 opass: {
//                     required: true,
//                     minlength: 4
//                 },
//                 reopass: {
//                     required: true,
//                     minlength: 4,
//                     equalTo: '#opass'
//                 }
//             },
//             messages: {
//                 opass: {
//                     required: "Informe uma senha",
//                     minlength: "A senha precisa ter no mÃ­nimo 4 dÃ­gitos."
//                 },
//                 reopass: "As senhas digitadas nÃ£o coincidem."
//             },
//             errorElement: 'div',
//             errorLabelContainer: '.msgAvisos'
//         });

//     }
//     if ($("button#btNovaSenha").length > 0) {

//         $('#btNovaSenha').click(function (event) {
//             $(".msgAvisos").slideUp();


//             if ($('form#formNovaSenha').valid()) {
//                 jQuery(".ofebas-loader").remove();
//                 jQuery(".login-body").removeClass("login-body-waint");

//                 jQuery(".login-body").addClass("login-body-waint").append(loader);

//                 jQuery("input, button").prop('disabled', true);

//                 $.ajax({
//                     type: "POST",
//                     url: urlBase + "login/definir-nova-senha",
//                     data: { opass: $('input[name=opass]').val(), token: $('input[name=token]').val() },

//                     success: function (response) {

//                         jQuery(".ofebas-loader").remove();
//                         jQuery(".login-body").removeClass("login-body-waint");

//                         jQuery("input, button").prop('disabled', false);

//                         console.log("ok POST! ");

//                         console.log(response.toString());



//                         var jsonR = ((response != "") ? JSON.parse(response) : []);

//                         if (jsonR.retorno == 'ok') {
//                             $('.msgAvisos').slideUp();
//                             $("#etapas #step-1").hide();
//                             $("#etapas #step-2").show();

//                         } else if (jsonR.retorno == 'fall') {

//                             $(".msgAvisos").html(jsonR.msg).slideDown();


//                         }
//                     }

//                 });
//                 console.log("ok!");
//             } else {
//                 console.log("ainda nÃ£o..");
//             }
//         });


//     }


// });