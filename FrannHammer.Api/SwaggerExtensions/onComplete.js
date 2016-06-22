(function () {
    $(function () {

        if (!$("#input_username").length && !$("#input_password").length) {
            var basicAuthUI =
                '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"></div>' +
                    '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"></div>';
            $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
            $("#input_apiKey").hide();
        }
    });

    $('#explore').off();
    $('#explore').text('Login');
    $('#explore').click(function () {

        var username = $('#input_username').val();
        var password = $('#input_password').val();
        if (username && username.trim() != "" && password && password.trim() != "") {
            $.ajax({
                //url: "http://localhost/KHApi/api/token",
                //url: "http://admin.kuroganehammer.com/api/token",
                url: "http://api.kuroganehammer.com/api/token",
                type: "post",
                contenttype: 'x-www-form-urlencoded',
                data: "grant_type=password&username=" + username + "&password=" + password,
                success: function (response) {
                    var bearerToken = 'Bearer ' + response.access_token;

                    window.swaggerUi.api.clientAuthorizations.add('Authorization', new SwaggerClient.ApiKeyAuthorization('Authorization', bearerToken, 'header'));
                    window.swaggerUi.api.clientAuthorizations.remove("api_key");
                    alert("Successfully logged in as: " + username);
                },
                error: function (xhr, ajaxoptions, thrownerror) {
                    alert("Login failed!");
                }
            });
        }
    });
})();