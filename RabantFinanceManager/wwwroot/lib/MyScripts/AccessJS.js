
var $pac = $pac || {};
$pac.Company = $pac.Company || {};
$pac.Company.Brand = "AnthonyBobbie Limited ©copyright 2018";
$pac.AuditTrail = $pac.AuditTrail || {};
$pac.Initialise = $pac.Initialise || {};
$pac.GiftAid = $pac.GiftAid || {};
$pac.Login = $pac.Login || {};
$pac.HomePage = "../../default.aspx";
$pac.Utilities = $pac.Utilities || {};
$pac.Report = $pac.Report || {};
$pac.Server = $pac.Server || {};
$pac.Church = $pac.Church || {};
$pac.Membership = $pac.Membership || {};
$pac.Accounts = $pac.Accounts || {};
$pac.Security = $pac.Security || {};
$pac.Security.Church = $pac.Security.Church || {};
$pac.Security.Branch = $pac.Security.Branch || {};
$pac.Utilities = $pac.Utilities || {};
$pac.Church.Minister = "";
$pac.isValidEmail = false;
$pac.Security.PermissionSet = ["Presbyter", "Administrator", "FinanceAdmin", "FinanceController", "SuperUser"];
$pac.Login.User = "";
$pac.Exception = function (message) { this.message = message; }
Array.prototype.unique = function () {
    return this.filter(function (value, index, array) {
        return array.indexOf(value, index + 1) < 0;
    });
};
$pac.Report.ConventionAndConference = function () {
    this.Submit = function () {
        try {
            // read contents of controls and validate and send data to 
            // database

            var data = new $pac.Report.ConConferenceClass();
            //alert(JSON.stringify(data));
            $.ajax({
                type: 'POST',
                url: 'WeeklyActivities.aspx/PostConventionAndConference',
                data: JSON.stringify(data),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    alert(response.d);
                },
                error: function (e) {
                    alert(e.responseText);
                }

            });


        } catch (e) {
            alert(e.message);
        }
    }
}
$pac.Report.ConConferenceClass = function () {
    this.name = $("#textboxEventName").val();
    this.date = $("#textboxDate").val();
    this.speaker = $("#textboxSpeaker").val();
    this.title = $("#textboxTitle").val();
    this.malecount = $("#textboxAdultmalecount").val();
    this.femalecount = $("#textboxfemaleCount").val();
    this.children = $("#textboxchildrenCount").val();
    this.spectacularevent = $("#textboxspectacularEvent").val();
    this.specialOccurence = $("#textboxspecialOccurence").val();
    this.SoulsWon = $("#textboxSoulsWon").val();
    this.convertsWaterBaptised = $("#textboxConvertsWaterBaptised").val();
    this.membersWaterBaptised = $("#textboxMembersWaterBaptised").val();
    this.holySpiritBaptised = $("#textboxHolySpiritBaptised").val();
    this.titheAndOffering = $("#textboxTitheAndOffering").val();
    this.otherOffering = $("#textboxOtherOffering").val();
    this.authorisedExpenditure = $("#textboxAuthorisedExpenditure").val();
    this.generalAssessment = $("#textboxGeneralAssessment").val();
    this.certificationMinister = $("#textboxStartDate").val();
    this.officiatingMMinister = $("#textboxEventEndDate").val();
}
$pac.Configuration = function () {

    try {
        var config = {
            name: $pac.Login.User.Config[0].Value,
            logo: $pac.Login.User.Config[1].Value,
            slogan: $pac.Login.User.Config[2].Value,
            twitter: $pac.Login.User.Config[3].Value,
            facebook: $pac.Login.User.Config[4].Value,
            yahoo: $pac.Login.User.Config[5].Value,
            instagram: $pac.Login.User.Config[6].Value,
            hangout: $pac.Login.User.Config[7].Value,
            caption: $pac.Login.User.Config[8].Value,
            title: $pac.Login.User.Config[9].Value,
            //theme: $pac.Login.User.Config[10].Value,
            youtube: $pac.Login.User.Config[11].Value,
            //caption: $pac.Login.User.Config[10].Value,
        }

        //set panel caption
        $("#panel-caption").text(config.caption)
        // set panel title 
        $("#panel-title").text(config.title)
        // set panel theme 
        $("#panel-theme").text(config.theme)
        // set panel church footer name 
        $("#panel-church-name").text('©' + config.name)
        // set panel twitter 
        document.getElementById("panel-church-twitter").href = config.twitter;
        // set panel Facebook 
        document.getElementById("panel-church-facebook").href = config.facebook;
        // set panel youtube ***to be corrected
        document.getElementById("panel-church-youtube").href = config.yahoo;
        // set panel instagram
        document.getElementById("panel-church-instagram").href = config.instagram;
        // set panel panel-church-googleplus
        document.getElementById("panel-church-googleplus").href = config.hangout;
        // set panel logo
        document.getElementById("panel-logo-footer").src = config.logo;
        document.getElementById("panel-logo-header").src = config.logo;
    } catch (e) {
        //alert(e.message);
    }

}
$(function () {
    /*check if user is validated before loading
      step one the menu, if not return to index.html to login
      
   */
    var userLogin = User.Email;
    alert(userLogin);
    try {
        if (localStorage.getItem("user") === null) { location.href = $pac.HomePage; return; }
        $pac.Login.User = JSON.parse(localStorage.getItem("user"));
        // check if credentials is registered
        $("#aLogout").click(function () { new $pac.Initialise.Menus().Logout()() });
        $pac.Configuration();
        //alert(localStorage.getItem("user"));


        $pac.Initialise.Menus();
        new $pac.Security.LockDown().AccountMenu();
        new $pac.Security.LockDown().DisplayLoggedInUser();
        new $pac.Security.LockDown().FinanceMenu();
        new $pac.Security.LockDown().HideMenuDropdowns();
        new $pac.Security.LockDown().AddTheme();
    } catch (e) {
        //alert(e.message);
    }
});
$pac.Login.Credentials = function () {
    this.TempEmailAddress = "temp@piwc.com";
    this.TempPassword = "password123";
    this.Validate = function () {
        try {
            // check entered email address and password
            var isCredentialsValid = false;

            var email = $("#textboxEmail").val();
            var pwd = $("#textboxPassword").val();
            //pass data to server to validate credentials

            //if validation fails and credentials are temporary
            if (!isCredentialsValid && email == this.TempEmailAddress && pwd == this.TempPassword) {
                $("#loginContainer").css("display", "none");
                //enable menu and hide login
                $("#dropdown-menu-2btn-0").css("display", "block");
                //set login credentials to allow pages navigation
                $pac.Login.User = { Email: this.TempEmailAddress, LogInDate: new Date().toLocaleDateString(), LogInTime: new Date().getTime() };
                $("#h2loggeduser").html(this.TempEmailAddress);
                localStorage.setItem("user", JSON.stringify($pac.Login.User));

            }


        } catch (e) {

        }
    }
};
$pac.Initialise.Menus = function () {
    this.Logout = function () {
        try {
            //wire logout control
            //remove local storage
            localStorage.removeItem("user");
            location.assign($pac.HomePage);

        } catch (e) {

        }
    }

};
$pac.Membership.GetMembers = function (branchName) {
    try {
        if (branchName.trim().length == 0 || branchName == "Please select branch")
            throw new $pac.Exception("Please select a branch name")
        $.ajax({
            type: "POST",
            url: 'MembershipList.aspx/GetMembers',
            data: JSON.stringify({ branchName: branchName }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var message = "Invalid credentials or member not known";
                new $pac.Membership.Table(data.d).Build();//build the table
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    } catch (e) {
        // alert(e.message);
    }
}
$pac.Membership.GetMembersList = function (branchName, handler) {
    try {
        if (branchName.trim().length == 0 || branchName == "Please select branch") throw new $pac.Exception("Please select a branch name")
        if (typeof handler !== "function") throw new $pac.Exception("invalid function handle passed")

        $.ajax({
            type: "POST",
            url: 'MembershipList.aspx/GetMembers',
            data: JSON.stringify({ branchName: branchName }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var message = "Invalid credentials or member not known";
                handler(data.d);//build the table
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    } catch (e) {
        //alert(e.message);
    }
}
$pac.Membership.Table = function (jsondata) {

    this.Build = function () {
        try {
            var members = JSON.parse(jsondata);
            //alert(JSON.stringify(jsondata));
            $("#tbodyMembers").empty();
            var table = document.getElementById("tbodyMembers");
            for (var i = 0; i < members.length; i++) {
                // add member records 
                var tr = document.createElement("tr");
                var tdFirstName = document.createElement("td"); tdFirstName.className = "body-item mbr-fonts-style display-7"; tdFirstName.innerHTML = members[i].FirstName;
                var tdLastName = document.createElement("td"); tdLastName.className = "body-item mbr-fonts-style display-7"; tdLastName.innerHTML = members[i].LastName;
                var tdTelephone = document.createElement("td"); tdTelephone.className = "body-item mbr-fonts-style display-7"; tdTelephone.innerHTML = members[i].Telephone;
                var tdEmail = document.createElement("td"); tdEmail.className = "body-item mbr-fonts-style display-7"; tdEmail.innerHTML = members[i].Email;
                var tdMembershipType = document.createElement("td"); tdMembershipType.className = "body-item mbr-fonts-style display-7"; tdMembershipType.innerHTML = members[i].MembershipType;
                var tdEdit = document.createElement("td"); tdEdit.className = "body-item mbr-fonts-style display-7";//
                var button = document.createElement("span"); button.innerHTML = "Edit";
                button.Record = members[i];
                button.onclick = function () {
                    document.getElementById("memberID").value = this.Record.MemberID;
                    document.getElementById("textboxFirstName").value = this.Record.FirstName;
                    document.getElementById("textboxLastName").value = this.Record.LastName;
                    document.getElementById("textboxTelephone").value = this.Record.Telephone;
                    document.getElementById("textboxEmail").value = this.Record.Email;
                    document.getElementById("selectMemType").value = this.Record.MembershipType == 'Non' ? 'Non-Member' : 'Member';
                    document.getElementById("buttonDialog").click();
                }


                //var spanEmail = document.createElement("i");// spanEmail.innerText="email"// button.type = "button"; button.value = "Send email";
                //spanEmail.MemberRecord = members[i];
                //spanEmail.onclick = function () {
                //    //$("#textareaEmailComment").empty()
                //    //document.getElementById("buttonSendEmailModal").click();
                //    //document.getElementById("textboxEmail").value = this.MemberRecord.Email;

                //}
                button.className = "form-control";
                //spanEmail.className = "fa fa-envelope fa-lg";

                tdEdit.appendChild(button);
                //tdEdit.appendChild(spanEmail);
                tr.appendChild(tdFirstName);
                tr.appendChild(tdLastName);
                tr.appendChild(tdTelephone);
                tr.appendChild(tdEmail);
                tr.appendChild(tdMembershipType);
                tr.appendChild(tdEdit);
                table.appendChild(tr);
            }

        } catch (e) {
            alert(e.message);
        }
    }
    this.Update = function () {
        // alert("got here okay");
        try {
            var url = "MembershipList.aspx/UpdateMemberDetails"
            var obj = { memberID: document.getElementById("memberID").value, firstName: $("#textboxFirstName").val(), lastName: $("#textboxLastName").val(), telephone: $("#textboxTelephone").val(), email: $("#textboxEmail").val(), membershipType: $("#selectMemType").val() }
            var handler = function (serverdata) {
                if (serverdata == 'Failed updating data') {
                    alert('failed updating date');
                } else if (serverdata == 'update successful') {
                    alert('Update successful');
                    // reload 

                    $("#closeButton").click();
                    $("#buttonSearch").click();
                    //location.reload();
                }
            };
            new $pac.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {

        }
    };
}
$pac.Server.Method = function () {
    this.BeginInvoke = function (url, obj, handler) {
        //url: webpage where webmethod lives
        //obj: object to pass to server method as a parameter
        //handler: method to invoke after successful return
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // Do something interesting here.
                handler(msg.d);

            },
            error: function (e) {
                //alert(e.responseText);
                console.log(e);
            }
        });
    }
    this.BeginInvokeAtt = function (url, obj, handlers) {
        //url: webpage where webmethod lives
        //obj: object to pass to server method as a parameter
        //handlers: methods to invoke after successful return
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                // Invoke all the methods passed.
                for (var i = 0; i < handlers.length; i++) {
                    handler(i)();
                }


            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    }
}
$pac.Church.LoadBranches = function (dropdown) {
    //loads church branches into dropdown control supplied
    try {
        var url, obj, handler;
        url = "Events.aspx/LoadBranches";
        obj = {};
        handler = function (jsonData) {
            try {


                if (jsonData == "failed getting branches") throw new $pac.Exception(jsonData);
                var parsed = JSON.parse(jsonData);
                for (var i = 0; i < parsed.length; i++) {
                    var option = document.createElement("option");
                    option.value = parsed[i].BranchName;
                    option.text = parsed[i].BranchName;
                    option.innerHTML = parsed[i].BranchName;
                    option.BranchID = parsed[i].BranchID;
                    document.getElementById(dropdown).appendChild(option);
                }
            } catch (e) {

            }
        }
        new $pac.Server.Method().BeginInvoke(url, obj, handler);
    } catch (e) {

    }
}

$pac.Church.LoadBranchNamesInSelectControls = function (dropdownArray) {
    //loads church branches into dropdown control supplied
    // dropdownArray:string array of select element to load branch names
    try {
        var url, obj, handler;
        url = "Events.aspx/LoadBranches";
        obj = {};
        handler = function (jsonData) {
            try {


                if (jsonData == "failed getting branches") throw new $pac.Exception(jsonData);
                var parsed = JSON.parse(jsonData);
                for (var j = 0; j < dropdownArray.length; j++) {


                    for (var i = 0; i < parsed.length; i++) {
                        var option = document.createElement("option");
                        option.value = parsed[i].BranchName;
                        option.text = parsed[i].BranchName;
                        option.innerHTML = parsed[i].BranchName;
                        option.BranchID = parsed[i].BranchID;
                        document.getElementById(dropdownArray[j]).appendChild(option);
                    }


                }

            } catch (e) {

            }
        }
        new $pac.Server.Method().BeginInvoke(url, obj, handler);
    } catch (e) {

    }
}

$pac.Church.GetAllBranches = function (func) {
    //loads church branches into dropdown control supplied
    //func handler handler to passed read branches
    var branches = "";
    try {
        var url, obj, handler;
        url = "Events.aspx/LoadBranches";
        obj = {};
        handler = function (jsonData) {
            try {


                if (jsonData == "failed getting branches") throw new $pac.Exception(jsonData);
                branches = JSON.parse(jsonData);
                func(branches);

            } catch (e) {

            }
        }
        new $pac.Server.Method().BeginInvoke(url, obj, handler);
    } catch (e) {

    }
    return branches;
}
$pac.Church.GetDistrictAndAreas = function (func) {
    //loads church branches into dropdown control supplied
    //func handler handler to passed read branches
    var branches = "";
    try {
        var url, obj, handler;
        url = "Accounts.aspx/GetDistrictAndAreas";
        obj = {};
        handler = function (jsonData) {
            try {


                if (JSON.parse(jsonData).message == "failed")
                    throw new $pac.Exception('failed to get district and area names');
                result = JSON.parse(jsonData);
                func(result);

            } catch (e) {

            }
        }
        new $pac.Server.Method().BeginInvoke(url, obj, handler);
    } catch (e) {

    }
    return branches;
}
$pac.Security.Church.LoadBranches = function (branchName) {
    try {
        var user = JSON.parse(localStorage.getItem("user"));
        //CHECK IF USER IS A SUPER USER AND ALLOW USER TO SELECT ALL BRANCH NAMES
        //ELSE RESTRICT USER TO THEIR LOCAL
        //load branches assigned into combobox

        // load branches into control



        if (user.LoginType == "SuperUser" || user.LoginType == "FinanceController") {
            //load all branch names
            $pac.Church.LoadBranches(branchName);
            //$pac.Branch.Events.LoadEvents(user.BranchName);
        } else {
            new $pac.AssignedBranches().LoadBranches(user, branchName); return;
        }


    } catch (e) {
        alert(e.message);
    }
}
$pac.AssignedBranches = function () {
    this.LoadBranches = function (user, branchName) {

        try {
            if (typeof user == 'undefined') { return; };
            if (typeof branchName == 'undefined') { return; };

            //user has groups assigned
            if (user.AssignedBranchGroups.length > 0) {
                //user has been assigned some branches, load assigned branches
                for (var i = 0; i < user.AssignedBranchGroups.length; i++) {

                    try {

                        var option = document.createElement("option");
                        option.value = user.AssignedBranchGroups[i].BranchName;
                        option.innerHTML = user.AssignedBranchGroups[i].BranchName;
                        option.BranchID = user.AssignedBranchGroups[i].BranchID
                        document.getElementById(branchName).appendChild(option);
                    } catch (e) {

                    }
                }
                return;
            }
            //user has been assigned some branches, load assigned branches
            if (user.AssignedBranches.length > 0) {


                for (var i = 0; i < user.AssignedBranches.length; i++) {

                    try {

                        var option = document.createElement("option");
                        option.value = user.AssignedBranches[i].BranchName;
                        option.innerHTML = user.AssignedBranches[i].BranchName;
                        option.BranchID = user.AssignedBranches[i].BranchID
                        document.getElementById(branchName).appendChild(option);
                    } catch (e) {

                    }
                }
                return;
            }
            //user is a member
            if (user.AssignedBranches.length == 0) {
                // user is not assigned branch, point user to his home branch
                //alert('You have no branches assigned, please request branches from administrator');
                $("#" + branchName).empty();
                var option = document.createElement("option");
                option.value = user.BranchName; option.innerHTML = user.BranchName;
                option.BranchID = user.BranchId
                document.getElementById(branchName).appendChild(option);
                return;
            };
        } catch (e) {

        }
    }
}
$pac.Security.LockDown = function () {
    this.LoginType = JSON.parse(localStorage.getItem("user")).LoginType;
    this.AccountMenu = function () {
        try {
            if (this.LoginType == "SuperUser") {
                //do nothing
            } else {
                $("a[href='Accounts.aspx']").hide();
            }
        } catch (e) {

        }
    }
    this.FinanceMenu = function () {
        //try {
        //    if (["FinanceAdmin","FinanceController","SuperUser"].indexOf(this.LoginType) != -1) {
        //        //do nothing
        //    } else {
        //        $("a[href='Finances.aspx']").hide();
        //    }
        //} catch (e) {

        //}
    }
    this.DisplayLoggedInUser = function () {
        $("#aloginUser").html("Welcome " + $pac.Login.User.FirstName + " " + $pac.Login.User.LastName + " [" + $pac.Login.User.BranchName + "]");
    }
    this.PagePermission = function (checkPermission) {
        if ($pac.Login.User.LoginType != "SuperUser") {
            //do nothing
            if (typeof $pac.Login.User.Permissionset == 'object') {
                // user has more than one permission
                var matchfound = false
                for (var i = 0; i < $pac.Login.User.Permissionset.length; i++) {
                    if (checkPermission.indexOf($pac.Login.User.Permissionset[i]) > -1) {
                        matchfound = true;
                        break;
                    }
                }
                if (!matchfound) { location.assign("ManagementList.aspx"); }
                return false;
            }
        }
    }
    this.user_is_allowed_to_stay_on_page = function (checkPermission) {
        if ($pac.Login.User.LoginType != "SuperUser") {
            //do nothing
            if (typeof $pac.Login.User.Permissionset == 'object') {
                // user has more than one permission
                var matchfound = false
                for (var i = 0; i < $pac.Login.User.Permissionset.length; i++) {
                    if (checkPermission.indexOf($pac.Login.User.Permissionset[i]) > -1) {
                        matchfound = true;
                        return true;
                        break;

                    }
                }
                if (!matchfound)  return false; 
                 
            }
        } else {
            return true;
        }
    }
    this.HideMenuDropdowns = function () {
        try {

            if ($pac.Login.User.LoginType == 'SuperUser') return false;
            // check if user has accounts permission and hide menu 
            // if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Accounts' }).length==0) $("a[href='Accounts.aspx']").hide();
            // check if user has attendance permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Attendance' }).length == 0) $("a[href='Attendance.aspx']").hide();
            //// check if user has census permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Census' }).length == 0) $("a[href='BranchCensus.aspx']").hide();
            //// check if user has events permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Events' }).length == 0) $("a[href='Events.aspx']").hide();
            //// check if user has finance permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Finances' }).length == 0) $("a[href='Finances.aspx']").hide();
            //// check if user has followup permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Follow-up' }).length == 0) $("a[href='Followup.aspx']").hide();
            //// check if user has membershiplist permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Membership List' }).length == 0) $("a[href='MembershipList.aspx']").hide();
            //// check if user has monthly planner permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Monthly planner' }).length == 0) $("a[href='MonthlyPlanner.aspx']").hide();
            //// check if user has preaching plan permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Preaching plan' }).length == 0) $("a[href='Preaching plan.aspx']").hide();
            //// check if user has weekly service report permission and hide menu
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Weekly service report' }).length == 0) $("a[href='WeeklyServiceReport.aspx']").hide();
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Baptism' }).length == 0) $("a[href='Baptism.aspx']").hide();
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Custom Reports' }).length == 0) $("a[href='Custom.aspx']").hide();
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Standard Reports' }).length == 0) $("a[href='Standard.aspx']").hide();

            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Gift Aid' }).length == 0) $("a[href='Giving.aspx']").hide();
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Appointment' }).length == 0) $("a[href='Appointment.aspx']").hide();
            if ($pac.Login.User.Permissionset.filter(function (val) { return val == 'Advanced search' }).length == 0) $("a[href='Search.aspx']").hide();

        } catch (e) {
            alert(e.message);
        }
    }
    this.AddTheme = function () {
        var user = JSON.parse(localStorage.getItem("user"));
        if (user.ThemeID != null) {
            //get theme path
            //apply theme
            var server = new $pac.Server.Dispatcher;
            server.url = "ManagementList.aspx/GetThemePath";
            server.obj = { themeID: user.ThemeID }
            server.handler = function (path) {
                if (path != null) {
                    var link = document.createElement('link');
                    link.href = path;
                    link.type = "text/css"
                    link.rel = "stylesheet"
                    document.head.appendChild(link);
                }

            }
            server.invoke();
        }
    }
}
$pac.Server.Dispatcher = function (url,obj,handler) {
    this.url = url;
    this.obj = obj;
    this.handler = handler;
    this.invoke = function () {
        new $pac.Server.Method().BeginInvoke(this.url, this.obj, this.handler)
    }
}
$pac.Utilities.FillOptions = function (filter, element) {
    //filter: array of strings
    //element: select element to populate
    if (filter === null) return false;
    var array = [];
    for (var i = 0; i < filter.length; i++) {
        var option = document.createElement("option");
        if (array.indexOf(filter[i]) == -1) {
            option.value = filter[i].ID;
                option.innerHTML = filter[i].activityName; 
            document.getElementById(element).appendChild(option);
            array.push(filter[i]);
        }
    }

};
$pac.MasterBranch = function () {

    this.LoadBranch = function (element) {

        //element: select element name to populate
        if (element == null) return false;
        var option = document.createElement("option");
        option.value = "Master";
        option.BranchID = 1000;
        option.innerHTML = "Master";
        document.getElementById(element).appendChild(option);
        array.push(filter[i]);



    }

};
$pac.AuditTrail = function () {
    this.PageVisited = function (page) {
        try {
            var date = new Date();
            obj = {
                memid: $pac.Login.User.MemberID,
                brnchid: $pac.Login.User.BranchID,

                pagevisited: page
            };
            new $pac.Server.Method().BeginInvoke("Accounts.aspx/AddAudit", obj, function (jsondata) {
                // string memid, string brnchid, string datevisited, string timevisited, string pagevisited
                try {

                } catch (e) {

                }
            });
        } catch (e) {

        }
    }
    this.GetAuditTrail = function () {

    }
}
$pac.Export = function (data) {
    this.ToExcel = function () {
        try {
            var excelApp = new ActiveXObject("Excel.Application");
            var excelWb = excelApp.Workbooks.Add();
            var excelWs = excelWb.Worksheets.Add();
            excelApp.Visible = true;
        } catch (e) {
            alert(e.message);
        }
    }
    this.ToWord = function () {

    }
    this.ToPDF = function () {

    }
}
$pac.Branch = function () {
    this.BranchInfo = {
        BranchName: $("#selectBranchNames").val(),
        BranchID: document.getElementById("selectBranchNames").options[document.getElementById("selectBranchNames").selectedIndex].BranchID
    }

    this.TableFilter = function (textboxID, tbodyID) {
        //textboxID: textbox
        //tbodyID  : id of table body

        if (typeof textboxID != 'undefined' && typeof tbodyID != 'undefined') {
            try {
                $("#" + textboxID).on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $("#" + tbodyID + " tr").filter(function () {
                        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                    });
                });
            } catch (e) {

            }
        }

    }
    this.ApplyTableFilters = function (searchBox) {
        // searchBox: array object
        // searchBox{
        //      textboxID: textbox id
        //      tbodyID:   table body id
        //    }

        if (typeof searchBox != 'undefined') {
            for (var i = 0; i < searchBox.length; i++) {
                try {
                    this.TableFilter(searchBox[i].textboxID, searchBox[i].tbodyID);
                } catch (e) {

                }

            }
        }
    }

}
$pac.CreateTable = function (columns) {
    var table = document.createElement("table");
    table.className = 'table table-hover table-responsive'
    var tr = document.createElement("tr");

    var thead = document.createElement("thead");
    var tbody = document.createElement("tbody");
    tbody.id = "tablebodyexport";
    table.id = 'tableexport'

    try {
        if (columns == null || columns.length == 0) return false;
        for (var i = 0; i < columns.length; i++) {
            //create table column
            var td = document.createElement("td");
            td.innerHTML = columns[i];
            tr.appendChild(td);
        }
        thead.appendChild(tr);

        table.appendChild(thead);
        table.appendChild(tbody);

    } catch (e) {

    }
    return { table: table, tbody: tbody, thead: thead };
}
$pac.Dictionary = function () {
    this.Create = function (name, value, callhandler) {
        //name: of dictionary item
        //value: value of dictionary item
        try {
            //validate arguments
            if (name == null || name.trim().length == 0) throw new $pac.Exception('Invalid dictionary name passed');
            if (value == null || value.trim().length == 0) throw new $pac.Exception('Invalid value  passed');
            if (typeof callhandler !== 'function') throw new $pac.Exception('Invalid function handle  passed');

            var url, obj, handler;
            url = 'Accounts.aspx/CreateDictionary';
            obj = { namee: name };
            handler = function (jsondata) {
                try {
                    if (jsondata == 'failed getting dictionary values') throw new $pac.Exception(jsondata);
                    if (typeof jsondata == 'object') {
                        var parsed = JSON.parse(jsondata);
                        callhandler(jsondata);
                    }
                } catch (e) {
                    alert(e.message);
                }
            }
            new $pac.Server.Method().BeginInvoke(url, obj, handler);


        } catch (e) {
            alert(e.message);
        }
    }
    this.GetValues = function (name, callhandler) {
        //name of dictionary to get
        //function to handle dictionary values returned from the server
        try {
            //validate arguments
            if (name == null || name.trim().length == 0) throw new $pac.Exception('Invalid dictionary name passed');
            if (typeof callhandler !== 'function') throw new $pac.Exception('Invalid function handle  passed');
            var url, obj, handler;
            url = 'Accounts.aspx/GetDictionaryValues';
            obj = { namee: name };
            handler = function (jsondata) {
                try {
                    if (jsondata == 'failed getting dictionary values') throw new $pac.Exception(jsondata);
                    if (typeof jsondata == 'object') {
                        var parsed = JSON.parse(jsondata);
                        callhandler(jsondata);
                    }
                } catch (e) {
                    alert(e.message);
                }
            }
            new $pac.Server.Method().BeginInvoke(url, obj, handler);


        } catch (e) {
            alert(e.message);
        }
    }
    this.Add = function (arrayValue, name, pageName, handler) {
        //arrayValue: array value to be stored
        //name: filter name to be used
        //page: filter belongs to
        if (name == null || name.toString().trim().length == 0) return;
        //if (typeof arrayValue != 'object') return;
        if (arrayValue == null || arrayValue.toString().trim().length == 0) return;
        if (pageName == null || pageName.toString().trim().length == 0) return;
        try {
            //add data to dictionary table
            var url, obj, handler;
            url = 'Accounts.aspx/AddDictionary';
            obj = {
                array: arrayValue,
                name: name,
                pageName: pageName
            };
            handler = function (jsondata) {
                try {
                    // var parsed = JSON.parse(jsondata);   
                    handler(jsondata);
                } catch (e) {
                    alert(e.message);
                }
            }
            new $pac.Server.Method().BeginInvoke(url, obj, handler);

        } catch (e) {

        }
    }

}
$pac.Utilities.Common = function () {
    this.yyyymmdd = function (date) {
        //change a date in the form yyyy/mm/dd to yyyymmdd
        if (date == null || typeof date != "string" || date.length != 10) {
            alert("cant convert " + date + " to yyyymmdd"); return;
        }
        var yyyy = date.split("/")[2];
        var mm = date.split("/")[1];
        var dd = date.split("/")[0];
        return yyyy + mm + dd;
    }
    this.UKdate = function (date) {
        //change a date in the form yyyymmdd to yyyy/mm/dd
        if (date.length != 8) {
            alert("cant convert " + date + " to dd/mm/yyyy"); return;
        }
        var yyyy = date.substring(0, 4);
        var mm = date.substring(4, 6);
        var dd = date.substring(6, 8);
        return dd + "/" + mm + "/" + yyyy;
    }
    this.selectOptions = function (array, selected) {
        //array: array to add to newly created select element
        //selected: option to select when select is populated
        var select = document.createElement('select');
        var option = document.createElement('option');
        option.value = 'Select item';
        option.innerHTML = 'Select item';
        select.appendChild(option);
        select.selectedIndex = 0;
        if (typeof array == 'undefined') return;
        for (var i = 0; i < array.length; i++) {
            var option = document.createElement('option');
            option.value = array[i];
            option.innerHTML = array[i];
            select.appendChild(option);
        }
        //if an option should be selected check if selected is valid
        if (typeof selected != 'undefined') select.value = selected;
        else select.selectedIndex = 0;
        return select;
    }
    this.createTextBox = function (text) {
        var textbox = document.createElement('input');
        textbox.type = 'text';
        if (typeof text != null) textbox.value = text;
        else textbox.placeholder = "enter answer";
        return textbox;
    }
    this.createTextBoxWithLength = function (text, maxlength) {
        var textbox = document.createElement('input');
        textbox.type = 'text';
        if (typeof maxlength != 'undefined' && isFinite(maxlength)) textbox.maxLength = maxlength;
        if (typeof text != null) textbox.value = text;
        else textbox.placeholder = "enter answer";
        return textbox;
    }
}
$pac.View = function (container) {
    this.DisplayIn = null;
    this.birthday = function (branchID) {
        //branchID: ID of branch to search for birthdays
        //container:container to append table on successful completion

        try {
            //get members celebrating their birthday this month
            var dispatcher = new $pac.Server.Dispatcher();

            dispatcher.handler = function (jsondata) {
                try {
                    if (container == null) return;
                    if (JSON.parse(jsondata).message == "success") {
                        //build a table and then return the table built
                        var table = document.createElement("table");
                        var tbody = document.createElement("tbody");
                        var thead = document.createElement("thead");
                        table.appendChild(thead);
                        table.appendChild(tbody);
                        table.className = "table table-responsive table-striped";
                        thead.style.fontWeight = "bold";
                        thead.style.fontSize = "16pt";
                        thead.insertRow(0);
                        thead.rows[0].insertCell(0).innerHTML = "FirstName";
                        thead.rows[0].insertCell(1).innerHTML = "LastName";
                        thead.rows[0].insertCell(2).innerHTML = "Day";
                        thead.rows[0].insertCell(3).innerHTML = "Month";
                        thead.rows[0].insertCell(4).innerHTML = "Image";

                        var list = JSON.parse(jsondata).result;

                        var func = function (list) {
                            for (var i = 0; i < list.length; i++) {
                                tbody.insertRow(i);
                                tbody.rows[i].insertCell(0).innerHTML = list[i].FirstName;
                                tbody.rows[i].insertCell(1).innerHTML = list[i].LastName;
                                tbody.rows[i].insertCell(2).innerHTML = list[i].Day;
                                tbody.rows[i].insertCell(3).innerHTML = list[i].Month;

                                tbody.rows[i].insertCell(4).appendChild(document.createElement('img'));
                                tbody.rows[i].cells[4].firstChild.src = "assets/images/happyBirthday.png";
                                tbody.rows[i].cells[4].firstChild.style.height = "40px";
                                tbody.rows[i].cells[4].firstChild.style.width = "40px";

                            }
                        }


                        var searchTable = document.createElement('table');
                        searchTable.insertRow(0);
                        searchTable.rows[0].insertCell(0).innerHTML = 'Select month';
                        searchTable.rows[0].insertCell(1).appendChild(document.createElement('select'));
                        var select = searchTable.rows[0].cells[1].firstChild;
                        //select.selectedIndex = 0;
                        select.style.margin = '5px';
                        select.style.width = '100px';
                        select.appendChild(document.createElement('option'));
                        select.options[0].innerHTML = 'January';
                        select.appendChild(document.createElement('option'));
                        select.options[1].innerHTML = 'February';
                        select.appendChild(document.createElement('option'));
                        select.options[2].innerHTML = 'March';
                        select.appendChild(document.createElement('option'));
                        select.options[3].innerHTML = 'April';
                        select.appendChild(document.createElement('option'));
                        select.options[4].innerHTML = 'May';
                        select.appendChild(document.createElement('option'));
                        select.options[5].innerHTML = 'June';
                        select.appendChild(document.createElement('option'));
                        select.options[6].innerHTML = 'July';
                        select.appendChild(document.createElement('option'));
                        select.options[7].innerHTML = 'August';
                        select.appendChild(document.createElement('option'));
                        select.options[8].innerHTML = 'September';
                        select.appendChild(document.createElement('option'));
                        select.options[9].innerHTML = 'October';
                        select.appendChild(document.createElement('option'));
                        select.options[10].innerHTML = 'November';
                        select.appendChild(document.createElement('option'));
                        select.options[11].innerHTML = 'December';
                        select.list = list;
                        select.onchange = function () {
                            //filter table
                            $(tbody).empty();
                            var index = this.selectedIndex + 1;
                            func(this.list.filter(function (val) { return val.Month == index; }));
                        }
                        //display todays date
                        container.appendChild(searchTable)
                        container.appendChild(table);
                        select.selectedIndex = new Date().getMonth();
                        var listfilter = (list.filter(function (val) {
                            var ibt = parseInt(val.Month);
                            var month = new Date().getMonth()+1;
                            return parseInt(val.Month) == new Date().getMonth()+1;
                        }));
                        func(listfilter);
                    } else {
                        //alert('No birthday to report');
                    }

                } catch (e) {

                }
                return table;
            }
            dispatcher.obj = { branchid: branchID };
            dispatcher.url = "MembershipList.aspx/GetMembersCelebratingTheirBirthdayThisMonth"
            dispatcher.invoke();


        } catch (e) {

        }
    }

}
$pac.Customerization = function () {
    this.theme = function () {
        $('.modal-title').html('Select theme for your table reports');
        $('#defaultBody').empty();
        var html = '<ul class= "list-group" ><li onclick="new $pac.Customerization().select(2)" class="list-group-item">';
        html = html + 'Navy blue <img  src="assets/images/navyblue.png"   width=40 height=40  class="m-2 img img-responsive rounded" alt="Cinque Terre">';
        html = html + '</li><li onclick="new $pac.Customerization().select(1)" class="list-group-item">';
        html = html + 'Aqua marine<img src="assets/images/aquamarine.png" width=40 height=40  class="m-2 img img-responsive rounded" alt="Cinque Terre">';
        html = html + '</li>';
        html = html + '<li onclick="new $pac.Customerization().select(3)" class="list-group-item">';
        html = html + 'Boway billy<img src="assets/images/bowaybilly.png" width=30 height=40  class="m-2 img img-responsive rounded" alt="Cinque Terre">';
        html = html + '</li></ul > ';
        $('#defaultBody').html(html);
        //add theme
        //displays dialog for them selection
        $('button[data-target="#mydefault"]').click();
    }
    this.select = function (id) {
        try {
            // update membership table with theme ID
            var theme = new $pac.Server.Dispatcher();
            theme.obj = { memberID: $pac.Login.User.MemberID, themeID: id };
            theme.handler = function (obj) {
                if (obj == 'success') {
                    //update localstorage with themeID
                    $pac.Login.User.ThemeID = id;
                    localStorage.setItem('user', JSON.stringify($pac.Login.User));
                    alert('Table theme updated');
                }
            };
            theme.url = "ManagementList.aspx/UpdateTheme"
            theme.invoke();
        } catch (e) {

        }
    }
    this.remove = function () {
        //remove theme
        //removes current theme
    }
}