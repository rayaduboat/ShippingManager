var $rab = $rab || {};
$rab.Initialise = $rab.Initialise || {};
$rab.Login = $rab.Login || {};
$rab.Utilities = $rab.Utilities || {};
$rab.Report = $rab.Report || {};
$rab.Login.User = "";
$rab.Report.ConventionAndConference = $rab.Report.ConventionAndConference || {};

$rab.Login.Credentials = function () {
    this.TempEmailAddress = "temp@piwc.com";
    this.TempPassword = "password123";
    this.Validate = function () {
        try {
            // check entered email address and password
            var isCredentialsValid = false;

            var email = $("#textboxEmail").val();
            var pwd   = $("#textboxPassword").val();
            //pass data to server to validate credentials
            $.ajax({
                type: "POST",
                url: '/Index.aspx/ValidateCredentials',
                data: JSON.stringify({email:email,pwd:pwd}),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var message = "Invalid credentials or member not known";
                    if (data.d != message) {
                        //alert(data.d);
                        $("#loginContainer").css("display", "none");
                        //enable menu and hide login
                        $("#dropdown-menu-2btn-0").css("display", "block");
                        //set login credentials to allow pages navigation

                        var user = JSON.parse(data.d);
                        //welcome the user for logging in
                        $("#h2loggeduser").html("Welcome <br/>" + user.FirstName + " " + user.LastName);
                        localStorage.setItem("user", data.d);
                        return true;
                    }
                    else {
                           document.getElementById("modalBody").innerHTML = "Invalid email or password";
                            $("#buttonModal").click();
                            
                        
                        return false;
                    }
                },
                failure: function (response) {
                    alert(response.d);
                    return false;
                }
            });
           


        } catch (e) {

        }
    }
};
$rab.Initialise.Menus = function () {
    this.Logout = function () {
        try {
            //wire logout control
            //remove local storage
            localStorage.removeItem("user");
            location.assign("Default.aspx");

        } catch (e) {

        }
    }

};