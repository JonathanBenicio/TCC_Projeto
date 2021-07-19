$(document).ready(function () {
    var url = "https://hedi-api.azurewebsites.net/v1/api/logins"
    //var url = "http://localhost:5000/v1/api/logins"
    if (sessionStorage.getItem("token")) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // url: "https://hedi-api.azurewebsites.net/api/logins",
            url: url,
            headers: {
                "Authorization":
                    "Bearer " + sessionStorage.getItem("token")
            },
            success: (resp) => {
                console.log("logado " + resp.toString())
            },
            error: function (resp) {
                console.log("logado " + resp.toString())
                sessionStorage.clear();
                window.location.replace("/");
            }
        });

    } else {
        window.location.replace("/");
    }
});


function logout() {
    sessionStorage.clear();
    window.location.replace("/");

}