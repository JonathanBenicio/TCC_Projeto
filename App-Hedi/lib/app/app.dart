import 'package:flutter/material.dart';
import 'package:hedi/app/view/home/home-page.dart';
import 'package:hedi/app/view/login/login-page.dart';

class App extends StatelessWidget {
  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Flutter Demo',
      
      theme: ThemeData(
        textTheme: TextTheme(),
        // This is the theme of you r application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
        // This makes the visual density adapt to the platform that you run
        // the app on. For desktop platforms, the controls will be smaller and
        // closer together (more dense) than on mobile platforms.
        visualDensity: VisualDensity.adaptivePlatformDensity,
      ),
      home: LoginPage ()
      // Container(width: 200.0, height: 200.0, margin: EdgeInsets.all(10), child: SplashScreen(seconds: 10, navigateAfterSeconds: Cadastro(),image: Image.asset(iconApp, width: 300, height: 150, fit:BoxFit.fill))),
    );
  }
}
