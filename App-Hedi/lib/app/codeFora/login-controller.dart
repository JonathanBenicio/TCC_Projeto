import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:hedi/app/model/Login.dart';
import 'package:hedi/app/view/home/home-page.dart';
import 'package:http/http.dart' as http;

String url = "https://hedi-api.azurewebsites.net/v1/api/Login/pac";

class LoginController {
  String email;
  String senha;
  var status = false;

  Future<void> logar(BuildContext context) async {
    try {
      Login user = new Login(email: email, senha: senha);
      print(user.toJson());
      final response = await http.post(url,
          headers: {"Content-Type": "application/json"},
          body: user.toJson().toString());
      if (response.statusCode == 200 || response.statusCode == 400) {
        Navigator.push(
            context, MaterialPageRoute(builder: (context) => HomePage()));
      } else {
        print(response.body);
        
      }

      // Map<String, dynamic> resp = _valida(user);
      // status = resp["status"];
      // print(resp);
      // if (status) {
      //   print(resp["response"]);
      //   print("Ok Login");
      // } else {
      //   print("falhou");
      // }
    } catch (e) {
      print(e.toString());
      print("dd");
    }
  }
}

//  _valida(Login login)  {

  // Map<String, dynamic> val =
  //     jsonDecode('{"email":"eu@teste.com", "senha":"123"}');
  // print(val["email"]);
  // if (val["email"] == login.email) {
  //   return jsonDecode(
  //       '{"status": true, "response":{"user": {"fator_Sensibilidade": 15.0,"tipo_Diabetes": "Diabetes Tipo 1","login": {"id": 2,"email": "eu@teste.com","senha": "","nutricionista": null},"fk_Nutricionista_Id": 1,"nutricionista": null,"historico_Alimentars": null,"id": 1,"nome": "Jonathan","telefone": "15-987654321","fk_Login_Id": 2},"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImV1QHRlc3RlLmNvbSIsIm5iZiI6MTYxMDUxNzAyNSwiZXhwIjoxNjEwNTE3MDU1LCJpYXQiOjE2MTA1MTcwMjV9.SF0659ceyNEoYAPja-fQ5bihexHDZ3jRw0ju3SLwG1Y"}}');
  // }
  // return null;
// }
