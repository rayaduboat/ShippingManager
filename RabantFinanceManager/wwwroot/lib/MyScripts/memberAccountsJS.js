$rab.memberAccounts = $rab.memberAccounts || {};
$rab.memberAccounts.Permissions = [];// array of login account permissions
$rab.memberAccounts = function () {
    this.RefreshPermissions = function () {
        new $rab.Server.Method().BeginInvoke("Accounts.aspx/PagePermissions",
            {},
            function (jsondata) {
                //
                $rab.memberAccounts.Permissions = JSON.parse(jsondata).PagePermissions;
            });
    }
    this.build = function () {

        if ($rab.Membership.Temp != null && $rab.Membership.Temp.length != 0) {

            try {


                var members = $rab.Membership.Temp;
                var tbody = document.getElementById("tbodyAccountMembers");
                $(tbody).empty();
                for (var i = 0; i < members.length; i++) {
                    try {
                        tbody.insertRow(i);
                        //tbody.rows[i].insertCell(0).innerHTML = members[i].MemberID;
                        tbody.rows[i].insertCell(0).innerHTML = members[i].FirstName;
                        tbody.rows[i].insertCell(1).innerHTML = members[i].LastName;

                        var password = GetPassword('enter password');
                        var confirmpassword = GetPassword('confirm password');
                        var loginType = GetLoginType();
                        var buttonCreate = GetButton('create login');
                        var buttonAddPages = GetButton('add pages');
                        var buttonDelete = GetButton('delete');
                        tbody.rows[i].insertCell(2).appendChild(loginType);
                        tbody.rows[i].insertCell(3).appendChild(password);
                        tbody.rows[i].insertCell(4).appendChild(confirmpassword);
                        tbody.rows[i].insertCell(5).appendChild(buttonCreate);
                        tbody.rows[i].insertCell(6).appendChild(buttonAddPages);
                        tbody.rows[i].insertCell(7).appendChild(buttonDelete);
                        //remember inputs for button create
                        buttonCreate.Record = members[i];
                        buttonCreate.password = password;
                        buttonCreate.confirmPassword = confirmpassword;
                        buttonCreate.loginType = loginType;
                        //remember inputs for button pages
                        buttonAddPages.Record = members[i];
                        buttonAddPages.password = password;
                        buttonAddPages.confirmPassword = confirmpassword;
                        buttonAddPages.loginType = loginType;
                        //remember inputs for button pages
                        buttonDelete.Record = members[i];
                        buttonDelete.password = password;
                        buttonDelete.confirmPassword = confirmpassword;
                        buttonDelete.loginType = loginType;
                        buttonDelete.tr = tbody.rows[i];
                        buttonDelete.style.backgroundColor = "red";
                        buttonDelete.style.color = "white";
                        buttonDelete.style.borderRadius="2px 2px"
                        buttonCreate.onclick = function () {

                            new $rab.memberAccounts().createAccount(this.Record, $(this.password).val(), $(this.confirmPassword).val(), $(this.loginType).val())

                        }
                        buttonAddPages.onclick = new $rab.memberAccounts().addPages;
                        buttonDelete.onclick = new $rab.memberAccounts().delete;


                    } catch (e) {

                    }
                }
                //get all page permissionsthis

                this.RefreshPermissions()
            } catch (e) {
                alert(e.message);
            }

        } else {
            alert("Please load members in membership tab before loading members");
            return false;
        }

        function GetPassword(placeholder) {
            //return select element with leader and assistant role
            var textbox = document.createElement("input");
            textbox.type = 'password';
            textbox.placeholder = placeholder;
            return textbox;
        }
        function GetLoginType() {
            //return select element with leader and assistant role
            var select = document.createElement("select");
            var option = document.createElement("option");
            var option2 = document.createElement("option");
            var option3 = document.createElement("option");

            select.appendChild(option);
            select.appendChild(option2);
            select.appendChild(option3);

            option.value = "Select login type"; option.innerHTML = "Select login type";
            option2.value = "Member"; option2.innerHTML = "Member";
            option3.value = "Presbyter"; option3.innerHTML = "Presbyter";

            option.selected = true;
            return select;

        }
        function GetButton(name) {
            //return select element with leader and assistant role
            var button = document.createElement("input");
            button.type = 'button';
            button.value = name;

            return button;
        }

    }
    this.createAccount = function (member, pwd, conpwd, loginType) {
        try {
            // get user email address and memberID
            // set new password for user
            var email = member.Email;
            var memberid = member.MemberID;
            var confirmPassword = pwd;
            var password = conpwd;
            var logintype = loginType;
            if (logintype.length == 0 || email === undefined) throw new $rab.Exception("Invalid account type");
            if (email.length == 0 || email === undefined) throw new $rab.Exception("Invalid email address");
            if (memberid.length == 0 || memberid === undefined) throw new $rab.Exception("failed getting memberID");
            if (password.length == 0 || password === undefined) throw new $rab.Exception("Invalid password");
            if (confirmPassword.length == 0 || confirmPassword === undefined) throw new $rab.Exception("Invalid password");
            if (confirmPassword != password) throw new $rab.Exception("Password miss match, please try again");

            var url, obj, handler
            url = 'MembershipList.aspx/UpdateLoginAccount';
            obj = { logintype: logintype, memberid: memberid, password: password };
            handler = this.AccountCreationResult;
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }
    }
    this.addPages = function () {
        if (typeof (this.Record) === "undefined") return;
        try {
            new $rab.memberAccounts().RefreshPermissions();
            var fullname = this.Record.FirstName + " " + this.Record.LastName;
            //assign modal title 
            $("#modal-general-title").html("Check to assign pages to:" + fullname);
            //clear existing table header
            $("#theadgeneral").empty();
            //clear existing table body
            $("#tbodygeneral").empty();
            //append table header
            var thead = document.getElementById("theadgeneral");
            thead.insertRow(0);
            thead.rows[0].insertCell(0).innerHTML = "Attendance"
            thead.rows[0].insertCell(1).innerHTML = "Baptism"
            thead.rows[0].insertCell(2).innerHTML = "Events"
            thead.rows[0].insertCell(3).innerHTML = "Follow-up"
            thead.rows[0].insertCell(4).innerHTML = "Finance"
            thead.rows[0].insertCell(5).innerHTML = "Gift aid"
            thead.rows[0].insertCell(6).innerHTML = "Membership"

            //apppend table body
            var thead = document.getElementById("tbodygeneral");
            thead.insertRow(0);
            var checkAttendance = getPage(this.Record.MemberID, "Attendance");
            var checkBaptism = getPage(this.Record.MemberID, "Baptism");
            var checkEvents = getPage(this.Record.MemberID, "Events");
            var checkFollowup = getPage(this.Record.MemberID, "Follow-up");
            var checkFinance = getPage(this.Record.MemberID, "Finances");
            var checkGiving = getPage(this.Record.MemberID, "Gift Aid");
            var checkmembership = getPage(this.Record.MemberID, "Membership List");

            thead.rows[0].insertCell(0).appendChild(checkAttendance);
            thead.rows[0].insertCell(1).appendChild(checkBaptism);
            thead.rows[0].insertCell(2).appendChild(checkEvents);
            thead.rows[0].insertCell(3).appendChild(checkFollowup);
            thead.rows[0].insertCell(4).appendChild(checkFinance);
            thead.rows[0].insertCell(5).appendChild(checkGiving);
            thead.rows[0].insertCell(6).appendChild(checkmembership);


            //show modal    
            $("#buttonGeneral").click();


        } catch (e) {

        }
        function getPage(memberID, pageName) {
            //check if user has page permission and check
            var find = $rab.memberAccounts.Permissions.filter(function (val) { return val.MemberID == memberID && val.PagePermission == pageName });
            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.memberID = memberID;
            checkbox.branchID = new $rab.Branch().BranchInfo.BranchID;
            checkbox.pageName = pageName;
            if (find.length == 1) {
                checkbox.checked = true;
            }
            checkbox.onchange = function () {
                if (this.checked) {
                    try {
                        new $rab.memberAccounts().AddPagePermission(this.branchID, this.pageName, this.memberID);
                    } catch (e) {
                        alert(e.message);
                    }
                }
                else if (!this.checked) {
                    //find page permission id and the delete permission
                    var find = $rab.memberAccounts.Permissions.filter(function (val) { return val.MemberID == memberID && val.PagePermission == pageName });
                    if (find != null && find.length == 1) {
                        //match found get the id and delete the permission
                        new $rab.Server.Method()
                            .BeginInvoke("Accounts.aspx/DeletePermission", { ID: find[0].ID }, function (jsondata) {
                                try {
                                    if (jsondata == "Permission for user deleted") {

                                    };
                                    //alert(jsondata);
                                } catch (e) {

                                }
                            })
                    }

                }

            }
            return checkbox;

        }
    }
    this.AccountCreationResult = function (jsondata) {
        try {
            //check result from server to see if user has been created
            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("Update failed");


            //var members = JSON.parse(jsondata);
            if (jsondata == "failed created account") {
                alert(jsondata);
            } else if (jsondata == "Created account successfully") {
                alert(jsondata);
            }
        } catch (e) {
            alert(e.message);
        }
    }
    this.AddPagePermission = function (branchid, pageName, memberid) {
        try {
            var url, obj, handler;
            url = 'Accounts.aspx/AddPagePermission';
            obj = {
                branchid: branchid,
                pageName: pageName,
                memberid: memberid,
                createdby: $rab.Login.User.MemberID
            };
            handler = function (jsondata) {
                try {
                    //check for error
                    if (jsondata == "failed adding page permission") throw new $rab.Exception(jsondata);
                    if (jsondata == "Member already has page permission") throw new $rab.Exception(jsondata);
                    if (JSON.parse(jsondata).message == "success") return





                } catch (e) {
                    alert(e.message);
                }
            };
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        }
        catch (e) {
            alert(e.message);
        }

    }
    this.delete = function () {

        try {
            if (confirm("Delete member " + this.Record.FirstName + " " + this.Record.LastName)) {


                var url, obj, handler
                url = 'MembershipList.aspx/DeleteMember';
                obj = {
                    branchid: new $rab.Branch().BranchInfo.BranchID,
                    memberid: this.Record.MemberID,
                };
                var tr = this.tr;
                new $rab.Server.Method().BeginInvoke(url, obj, function (jsondata) {
                    // alert(jsondata);
                    if (jsondata == "Member Deleted successfully!") {
                        $(tr).remove();
                        //reload model
                        $rab.Membership.GetMembersV2();
                    }
                });
            }
        } catch (e) {

        }
    }
}