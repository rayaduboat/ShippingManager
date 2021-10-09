$rab.Accounts.Branch = function (branchName) {
    this.LoadBranchMembers = function (branchName) {
        try {
            $("#selectMemberNames").empty();
            var url, obj, handler
            url = 'MembershipList.aspx/GetMembers';
            obj = { branchName: branchName };
            handler = this.LoadDropDownList
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }

    }
    this.LoadDropDownList = function (jsondata) {
        try {

            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("No member record found");


            var members = JSON.parse(jsondata);
            document.getElementById("selectMemberNames").MemberList = members;
            $rab.Utilities.FillOptions(['Select member name'], 'selectMemberNames');
            for (var i = 0; i < members.length; i++) {
                var option = document.createElement("option");
                option.className = "form-control";
                option.value = members[i].LastName + " " + members[i].FirstName
                option.innerText = members[i].LastName + " " + members[i].FirstName
                option.MemberID = members[i].MemberID;
                option.Email = members[i].Email;
                document.getElementById("selectMemberNames").appendChild(option);

            }

        } catch (e) {
            alert(e.message);
        }
    }
    this.LoadMemberEmail = function (member) {
        try {
            //option selected has two properties assigned
            //email and member ID
            // update email address textbox with email attached to 
            //option button
            document.getElementById("textboxAdminEmail").value = "";
            document.getElementById("textboxAdminMemberID").value = ""
            var option = document.getElementById("selectMemberNames").options[document.getElementById("selectMemberNames").selectedIndex];
            if (typeof option.Email == 'undefined') return;
            if (typeof option.MemberID == 'undefined') return;
            document.getElementById("textboxAdminEmail").value = option.Email;
            document.getElementById("textboxAdminMemberID").value = option.MemberID;

        } catch (e) {

        }
    }
    this.CreateAccount = function () {
        try {
            // get user email address and memberID
            // set new password for user
            var email = $("#textboxAdminEmail").val();
            var memberid = document.getElementById("textboxAdminMemberID").value;
            var confirmPassword = $("#textboxAdminConfirmPassword").val();
            var password = $("#textboxAdminPassword").val();
            var logintype = $("#selectAdminAccountType").val();
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
    this.AccountCreationResult = function (jsondata) {
        try {
            //check result from server to see if user has been created
            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("Update failed");


            //var members = JSON.parse(jsondata);
            if (jsondata == "failed created account") {
                alert('failed creating account');
            } else if (jsondata == "Created account successfully") {
                alert(jsondata);
                $("button[class=close]").click();
            }
        } catch (e) {
            alert(e.message);
        }
    }
    this.CreateBranch = function () {
        try {


            var url, obj, handler;
            url = "Accounts.aspx/AddNewBranch"
            obj = {
                name: $("#textboxNewBranchName").val(),
                address: $("#textboxNewBranchAddress").val(),
                telephone: $("#textboxNewBranchTelephone").val(),
                website: $("#textboxNewBranchWebsite").val(),
                presidingofficer: $("#textboxNewBranchPresidingElder").val(),
                email: $("#textboxNewBranchEmail").val()

            };
            // validate inputs
            //********************************************************************************************
            if (obj.name.trim().length == 0) throw new $rab.Exception("Invalid name specified");
            if (obj.address.trim().length == 0) throw new $rab.Exception("Invalid address");
            if (obj.telephone.trim().length == 0) throw new $rab.Exception("Invalid telephone specified");
            //if (obj.website.trim().length=0)          throw new $rab.Exception("Invalid name specified");
            if (obj.presidingofficer.trim().length == 0) throw new $rab.Exception("Invalid name specified");
            //if (obj.email.trim().length=0)            throw new $rab.Exception("Invalid name specified");
            //********************************************************************************************

            handler = function (jsondata) {
                try {
                    if (jsondata == "Creating branch failed") throw new $rab.Exception(jsondata)
                    alert(jsondata);
                    $("#closeNewBranchModal").click();
                    $("#textboxNewBranchEmail, #textboxNewBranchPresidingElder, #textboxNewBranchName, #textboxNewBranchAddress, #textboxNewBranchTelephone, #textboxNewBranchWebsite").val("");
                } catch (e) {
                    alert(e.message);
                }
            }
            new $rab.Server.Method().BeginInvoke(url, obj, handler)
        } catch (e) {
            alert(e.message);
        }
    }
}
$rab.BranchGroups = new Array();
$rab.Security.Authourised = function () {
    this.LoginType = JSON.parse(localStorage.getItem("user")).LoginType;
    this.AccountMenu = function () {
        try {
            if (this.LoginType == "SuperUser") {
                // Load all controls needing branchNames
                $rab.Church.LoadBranches("selectPermissionBranchNames");
            } else {
                $("a[href='Accounts.aspx']").hide();
                //person came here illegally, possibly knows the page
                //re-route him/her back to the management panel
                window.location.href = "ManagementList.aspx";
            }
        } catch (e) {
            alert(e.message);
        }
    }
    this.FinanceMenu = function () {
        try {

        } catch (e) {

        }
    }
}
$(function () {
    try {
        new $rab.Security.Authourised().AccountMenu();
        setTimeout(new $rab.AuditTrail().PageVisited("Accounts"), 1000);
        $rab.Church.LoadBranches("selectLoadBranchNames");
        //apply table filter on all searchable tables
        new $rab.Branch().TableFilter('textboxSearchAreaDistrictSetUp', 'tbodyGroupedTobeAssigned')
    } catch (e) {

    }

})
$rab.Accounts.Permission = function (branchName) {

    this.LoadBranchMembers = function (branchName) {

        try {
            $("#selectPermissionMemberNames").empty();
            var url, obj, handler
            url = 'MembershipList.aspx/GetMembers';
            obj = {
                branchName: branchName
            };
            handler = this.LoadDropDownList;
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }

    }

    this.LoadDropDownList = function (jsondata) {
        try {

            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("No member record found");


            var members = JSON.parse(jsondata);
            document.getElementById("selectPermissionMemberNames").MemberList = members;

            for (var i = 0; i < members.length; i++) {
                var option = document.createElement("option");
                option.className = "form-control";
                option.value = members[i].LastName + " " + members[i].FirstName
                option.innerText = members[i].LastName + " " + members[i].FirstName
                option.MemberID = members[i].MemberID;
                option.Email = members[i].Email;
                document.getElementById("selectPermissionMemberNames").appendChild(option);

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
                branchid: branchid.options[branchid.selectedIndex].BranchID,
                pageName: pageName.value,
                memberid: memberid.options[memberid.selectedIndex].MemberID,
                createdby: $rab.Login.User.MemberID
            };
            handler = function (jsondata) {
                try {
                    //check for error
                    if (jsondata == "failed adding page permission") throw new $rab.Exception(jsondata);
                    if (jsondata == "Member already has page permission") throw new $rab.Exception(jsondata);
                    var parse = JSON.parse(jsondata)
                    //append to table listing
                    var table = document.createElement("table");
                    table.className = "table table-responsible "
                    table.width = "100%";
                    table.height = "100%";
                    var thead = document.createElement("thead");
                    var tbody = document.createElement("tbody");
                    var tr = document.createElement("tr");
                    var td = document.createElement("td");
                    var tdEdit = document.createElement("td");
                    tdEdit.innerHTML = "Edit";
                    td.style.border = 10;
                    td.innerHTML = "Member pages allowed";
                    td.className = "form-control btn-secondary"
                    tr.appendChild(td);
                    tr.appendChild(tdEdit);
                    table.appendChild(thead);
                    table.appendChild(tbody);
                    thead.appendChild(tr);
                    $("#table-permission-container").empty();
                    document.getElementById("table-permission-container").appendChild(table);
                    for (var i = 0; i < parse.PagePermissions.length; i++) {
                        var tr = document.createElement("tr");
                        var td = document.createElement("td");
                        var tdDelete = document.createElement("td");
                        var button = document.createElement("button");
                        button.Permission = parse.PagePermissions[i];
                        button.innerHTML = "Delete"
                        button.rowRecord = tr;
                        button.onclick = function () {
                            try {
                                //call to delete record from database
                                var tr = this.rowRecord;
                                new $rab.Server.Method()
                                    .BeginInvoke("Accounts.aspx/DeletePermission", { ID: this.Permission.ID }, function (jsondata) {
                                        try {
                                            if (jsondata == "Permission for user deleted") {
                                                $(tr).hide();
                                            };
                                            alert(jsondata);
                                        } catch (e) {

                                        }
                                    })
                            } catch (e) {

                            }
                        }
                        tdDelete.appendChild(button);
                        td.innerHTML = parse.PagePermissions[i].PagePermission;
                        tr.appendChild(td);
                        tr.appendChild(tdDelete);
                        tbody.appendChild(tr);
                    }





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
    this.ViewPermission = function (branchid, pageName, memberid) {
        try {
            var url, obj, handler;
            url = 'Accounts.aspx/GetPagePermission';
            obj = {
                branchid: branchid.options[branchid.selectedIndex].BranchID,

                memberid: memberid.options[memberid.selectedIndex].MemberID,

            };
            handler = function (jsondata) {
                try {
                    //check for error
                    if (jsondata == "failed adding page permission") throw new $rab.Exception(jsondata);
                    if (jsondata == "Member already has page permission") throw new $rab.Exception(jsondata);
                    var parse = JSON.parse(jsondata)
                    //append to table listing
                    var table = document.createElement("table");
                    table.className = "table table-responsible "
                    table.width = "100%";
                    table.height = "100%";
                    var thead = document.createElement("thead");
                    var tbody = document.createElement("tbody");
                    var tr = document.createElement("tr");
                    var td = document.createElement("td");
                    var tdEdit = document.createElement("td");
                    tdEdit.innerHTML = "Edit";
                    td.style.border = 10;
                    td.innerHTML = "Member pages allowed";
                    td.className = "form-control btn-secondary"
                    tr.appendChild(td);
                    tr.appendChild(tdEdit);
                    table.appendChild(thead);
                    table.appendChild(tbody);
                    thead.appendChild(tr);
                    $("#table-permission-container").empty();
                    document.getElementById("table-permission-container").appendChild(table);
                    for (var i = 0; i < parse.PagePermissions.length; i++) {
                        var tr = document.createElement("tr");
                        var td = document.createElement("td");
                        var tdDelete = document.createElement("td");
                        var button = document.createElement("button");
                        button.Permission = parse.PagePermissions[i];
                        button.innerHTML = "Delete"
                        button.rowRecord = tr;
                        button.onclick = function () {
                            try {
                                //call to delete record from database
                                var tr = this.rowRecord;
                                new $rab.Server.Method()
                                    .BeginInvoke("Accounts.aspx/DeletePermission", { ID: this.Permission.ID }, function (jsondata) {
                                        try {
                                            if (jsondata == "Permission for user deleted") {
                                                $(tr).hide();
                                            };
                                            alert(jsondata);
                                        } catch (e) {

                                        }
                                    })
                            } catch (e) {

                            }
                        }
                        tdDelete.appendChild(button);
                        td.innerHTML = parse.PagePermissions[i].PagePermission;
                        tr.appendChild(td);
                        tr.appendChild(tdDelete);
                        tbody.appendChild(tr);
                    }






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
}
$rab.BranchAssignment = function () {
    this.LoadBranchMembers = function (branchName) {

        try {
            $("#selectBranchAssignmentMemberNames").empty();
            var url, obj, handler
            url = 'MembershipList.aspx/GetMembers';
            obj = {
                branchName: branchName
            };
            handler = this.LoadDropDownList;
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }

    }
    this.LoadDropDownList = function (jsondata) {
        try {

            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("No member record found");

            $rab.Utilities.FillOptions(['Select member name'], 'selectBranchAssignmentMemberNames');
            var members = JSON.parse(jsondata);
            document.getElementById("selectBranchAssignmentMemberNames").MemberList = members;

            for (var i = 0; i < members.length; i++) {
                var option = document.createElement("option");
                option.className = "form-control";
                option.value = members[i].MemberID
                option.innerText = members[i].LastName + " " + members[i].FirstName
                option.MemberID = members[i].MemberID;
                option.Email = members[i].Email;
                document.getElementById("selectBranchAssignmentMemberNames").appendChild(option);

            }

        } catch (e) {
            alert(e.message);
        }
    }
    this.Add = function () {

        try {
            var obj = {
                ID: 0,
                MemberID: $('#selectBranchAssignmentMemberNames').val(),
                MemberBranchID: document.getElementById("selectBranchAssignmentBranchNames").options[document.getElementById("selectBranchAssignmentBranchNames").selectedIndex].BranchID,
                AssignedBranchID: document.getElementById("selectBranchAssignmentSelection").options[document.getElementById("selectBranchAssignmentSelection").selectedIndex].BranchID,
                CreatedBy: $rab.Login.User.MemberID,
                CreatedOn: 0,
            }
            var url = "Accounts.aspx/AssignBranchToMember";
            if (!isFinite(obj.MemberID)) throw new Error('Invalid member selected');
            if (!isFinite(obj.MemberBranchID)) throw new Error('Invalid Member BranchID selected');
            if (!isFinite(obj.AssignedBranchID)) throw new Error('Invalid Assigned BranchID selected');
            new $rab.Server.Method().BeginInvoke(url, { assigned: obj }, this.handler);
        } catch (e) {
            alert(e.message);
        }
    }
    this.handler = function (jsondata) {

        try {
            //build table
            var tbody = document.getElementById('tbodyAssignedBranches');
            $(tbody).empty();
            var func = function (jsondata) {
                var query = JSON.parse(jsondata);
                for (var i = 0; i < query.AssignedBranches.length; i++) {
                    try {
                        tbody.insertRow(i);

                        //tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].MemberID;  
                        tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].AssignedBranchID;
                        tbody.rows[i].insertCell(1).innerHTML = query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].insertCell(2).innerHTML = query.AssignedBranches[i].CreatedBy;
                        tbody.rows[i].insertCell(3)//.innerHTML = query.AssignedBranches[i].CreatedOn;
                        var img = document.createElement('img');
                        img.src = 'assets/images/delete.png'; img.height = '30'; img.width = '30'
                        img.className = 'img img-circle img-responsive'
                        img.record = query.AssignedBranches[i];
                        img.tr = tbody.rows[i];
                        img.title = "Click to remove " + query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].cells[3].appendChild(img);
                        img.onclick = function () {
                            try {
                                var tr = this.tr;
                                var url = 'Accounts.aspx/RemovedAssignedBranch';
                                new $rab.Server.Method().BeginInvoke(url,
                                    {
                                        ID: this.record.ID
                                    }, function (jsondata) {
                                        //remove row
                                        if (JSON.parse(jsondata).message == 'success') {
                                            $(tr).remove();
                                        }
                                    })
                            } catch (e) {

                            }
                        }

                    } catch (e) {

                    }
                }
            }
            if (typeof jsondata == 'undefined') return;
            if (JSON.parse(jsondata).message == "success") {
                func(jsondata);
            } else {
                alert(JSON.parse(jsondata).message);
                if (JSON.parse(jsondata).message == 'member already assigned branch') {
                    func(jsondata);
                }
            }

        } catch (e) {

        }
    }
    this.buildTable = function (memberID) {
        try {
            var tbody = document.getElementById('tbodyAssignedBranches');
            $(tbody).empty();
            if (typeof memberID == 'undefined') return;
            var jsonD = new $rab.Server.Dispatcher();
            jsonD.url = "Accounts.aspx/GetMemberAssignedBranches";
            jsonD.obj = { memberID: memberID };
            jsonD.handler = this.handler;
            jsonD.invoke();
        } catch (e) {

        }
    }

}
$rab.GroupAssignment = function () {
    this.LoadBranchMembers = function (branchName) {

        try {
            $("#selectGroupsMemberNames").empty();
            var url, obj, handler
            url = 'MembershipList.aspx/GetMembers';
            obj = {
                branchName: branchName
            };
            handler = this.LoadDropDownList;
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }

    }
    this.LoadDropDownList = function (jsondata) {
        try {

            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("No member record found");

            $rab.Utilities.FillOptions(['Select member name'], 'selectGroupsMemberNames');
            var members = JSON.parse(jsondata);
            document.getElementById("selectGroupsMemberNames").MemberList = members;

            for (var i = 0; i < members.length; i++) {
                var option = document.createElement("option");
                option.className = "form-control";
                option.value = members[i].MemberID
                option.innerText = members[i].LastName + " " + members[i].FirstName
                option.MemberID = members[i].MemberID;
                option.Email = members[i].Email;
                document.getElementById("selectGroupsMemberNames").appendChild(option);

            }

        } catch (e) {
            alert(e.message);
        }
    }
    this.Add = function () {

        try {
            var obj = {
                ID: 0,
                MemberID: $('#selectBranchAssignmentMemberNames').val(),
                MemberBranchID: document.getElementById("selectBranchAssignmentBranchNames").options[document.getElementById("selectBranchAssignmentBranchNames").selectedIndex].BranchID,
                AssignedBranchID: document.getElementById("selectBranchAssignmentSelection").options[document.getElementById("selectBranchAssignmentSelection").selectedIndex].BranchID,
                CreatedBy: $rab.Login.User.MemberID,
                CreatedOn: 0,
            }
            var url = "Accounts.aspx/AssignBranchToMember";
            if (!isFinite(obj.MemberID)) throw new Error('Invalid member selected');
            if (!isFinite(obj.MemberBranchID)) throw new Error('Invalid Member BranchID selected');
            if (!isFinite(obj.AssignedBranchID)) throw new Error('Invalid Assigned BranchID selected');
            new $rab.Server.Method().BeginInvoke(url, { assigned: obj }, this.handler);
        } catch (e) {
            alert(e.message);
        }
    }
    this.handler = function (jsondata) {

        try {
            //build table
            var tbody = document.getElementById('tbodyAssignedGroups');
            $(tbody).empty();
            var func = function (jsondata) {
                var query = JSON.parse(jsondata);
                for (var i = 0; i < query.AssignedBranches.length; i++) {
                    try {
                        tbody.insertRow(i);

                        //tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].MemberID;  
                        tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].AssignedBranchID;
                        tbody.rows[i].insertCell(1).innerHTML = query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].insertCell(2).innerHTML = query.AssignedBranches[i].CreatedBy;
                        tbody.rows[i].insertCell(3)//.innerHTML = query.AssignedBranches[i].CreatedOn;
                        var img = document.createElement('img');
                        img.src = 'assets/images/delete.png'; img.height = '30'; img.width = '30'
                        img.className = 'img img-circle img-responsive'
                        img.record = query.AssignedBranches[i];
                        img.tr = tbody.rows[i];
                        img.title = "Click to remove " + query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].cells[3].appendChild(img);
                        img.onclick = function () {
                            try {
                                var tr = this.tr;
                                var url = 'Accounts.aspx/RemovedAssignedBranch';
                                new $rab.Server.Method().BeginInvoke(url,
                                    {
                                        ID: this.record.ID
                                    }, function (jsondata) {
                                        //remove row
                                        if (JSON.parse(jsondata).message == 'success') {
                                            $(tr).remove();
                                        }
                                    })
                            } catch (e) {

                            }
                        }

                    } catch (e) {

                    }
                }
            }
            if (typeof jsondata == 'undefined') return;
            if (JSON.parse(jsondata).message == "success") {
                func(jsondata);
            } else {
                alert(JSON.parse(jsondata).message);
                if (JSON.parse(jsondata).message == 'member already assigned branch') {
                    func(jsondata);
                }
            }

        } catch (e) {

        }
    }
    this.buildTable = function (memberID) {
        try {

            var tbody = document.getElementById('tbodyAssignedGroups');
            $(tbody).empty();
            if (typeof memberID == 'undefined') return;
            var jsonD = new $rab.Server.Dispatcher();
            jsonD.url = "Accounts.aspx/GetMemberBranchGrouping";
            jsonD.obj = { memberID: memberID };
            jsonD.handler = this.handler;
            jsonD.invoke();

        } catch (e) {

        }
    }
    this.buildGroups = function () {
       
        try {
            var tbody = document.getElementById('tbodyAssignedGroups');
            $(tbody).empty();
             
            var jsonD = new $rab.Server.Dispatcher();
            jsonD.url = "Accounts.aspx/GetMemberBranchGrouping";
            jsonD.obj = { };
            jsonD.handler = function (jsondata) {
                
                $rab.BranchGroups = JSON.parse(jsondata).result;

            };
            jsonD.invoke();
        } catch (e) {

        }
    }
    this.loadFilter = function (filter,element) {

        try {
            if (typeof element == 'undefined') return;
            $("#" + element).empty()
            if ($rab.BranchGroups.length == 0) return;
            var res = $rab.BranchGroups.filter(function (val) { return val.BranchGroupingType == filter });
            var match = res.map(function (val) { return val.GroupingName; });
            $rab.Utilities.FillOptions(["Select group name"], element);
            $rab.Utilities.FillOptions(match.sort(), element);
        } catch (e) {

        }
    }
    this.Assign = function () {
        try {
            //add group name
            var func = new $rab.Server.Dispatcher();
            func.obj = {};
            func.url = "Accounts.aspx/AssignGroup"
            func.handler = function (jsondata) {
                try {
                    if (typeof jsondata == 'undefined') throw new Error('No data returned from server');
                    var parsed = JSON.parse(jsondata);
                    if (parsed.message != 'success') throw new Error(parsed.message);

                } catch (e) {
                    alert(e.message);
                }
            }
        } catch (e) {
            alert(e.message);
        }
    }
}
$rab.AreaDistrictAssignment = function () {
    this.handler = function (jsondata) {

        try {
            //build table
            var tbody = document.getElementById('tbodyAssignedGroups');
            $(tbody).empty();
            var func = function (jsondata) {
                var query = JSON.parse(jsondata);
                for (var i = 0; i < query.AssignedBranches.length; i++) {
                    try {
                        tbody.insertRow(i);

                        //tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].MemberID;  
                        tbody.rows[i].insertCell(0).innerHTML = query.AssignedBranches[i].AssignedBranchID;
                        tbody.rows[i].insertCell(1).innerHTML = query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].insertCell(2).innerHTML = query.AssignedBranches[i].CreatedBy;
                        tbody.rows[i].insertCell(3)//.innerHTML = query.AssignedBranches[i].CreatedOn;
                        var img = document.createElement('img');
                        img.src = 'assets/images/delete.png'; img.height = '30'; img.width = '30'
                        img.className = 'img img-circle img-responsive'
                        img.record = query.AssignedBranches[i];
                        img.tr = tbody.rows[i];
                        img.title = "Click to remove " + query.AssignedBranches[i].AssignedBranchName;
                        tbody.rows[i].cells[3].appendChild(img);
                        img.onclick = function () {
                            try {
                                var tr = this.tr;
                                var url = 'Accounts.aspx/RemovedAssignedBranch';
                                new $rab.Server.Method().BeginInvoke(url,
                                    {
                                        ID: this.record.ID
                                    }, function (jsondata) {
                                        //remove row
                                        if (JSON.parse(jsondata).message == 'success') {
                                            $(tr).remove();
                                        }
                                    })
                            } catch (e) {

                            }
                        }

                    } catch (e) {

                    }
                }
            }
            if (typeof jsondata == 'undefined') return;
            if (JSON.parse(jsondata).message == "success") {
                func(jsondata);
            } else {
                alert(JSON.parse(jsondata).message);
                if (JSON.parse(jsondata).message == 'member already assigned branch') {
                    func(jsondata);
                }
            }

        } catch (e) {

        }
    }
    this.buildTable = function (memberID) {
        try {

            var tbody = document.getElementById('tbodyAssignedGroups');
            $(tbody).empty();
            if (typeof memberID == 'undefined') return;
            var jsonD = new $rab.Server.Dispatcher();
            jsonD.url = "Accounts.aspx/GetMemberBranchGrouping";
            jsonD.obj = { memberID: memberID };
            jsonD.handler = this.handler;
            jsonD.invoke();

        } catch (e) {

        }
    }
    this.Assign = function () {
        try {
            //add group name
            var func = new $rab.Server.Dispatcher();
            var evalHandler = this.handler;
            func.obj = {
                groupName: $('#selectGroupsBranchGroupingNames').val(),
                memberID:  $('#selectGroupsMemberNames').val()
            };
            func.url = "Accounts.aspx/AssignGroup"
            func.handler = function (jsondata) {
                try {
                    var tbody = document.getElementById('tbodyAssignedGroups');
                    $(tbody).empty();
                    if (typeof jsondata == 'undefined') throw new Error('No data returned from server');
                    var parsed = JSON.parse(jsondata);
                    if (parsed.message != 'success') throw new Error(parsed.message);
                    parsed = JSON.parse(parsed.result);
                    for (var i = 0; i < parsed.result.length; i++) {
                        tbody.insertRow(i);
                        tbody.rows[i].insertCell(0).innerHTML = parsed.result[i].FullName;
                        tbody.rows[i].insertCell(1).innerHTML = parsed.result[i].GroupName;
                        tbody.rows[i].insertCell(2);
                        var img = document.createElement('img');
                        img.className = 'img img-circle';
                        img.style.width = '25px';
                        img.style.height = '25px';
                        img.src = 'assets/images/delete.png';
                        img.record = parsed.result[i];
                        tbody.rows[i].cells[2].appendChild(img);
                        img.style.cursor = 'pointer';
                        img.onclick = function () {
                            var inv = new $rab.Server.Dispatcher();
                            inv.obj = {memberID:this.record.memberID};
                            inv.url = "Accounts.aspx/RemoveAssignedGroup";
                            inv.handler = function (jsondata) {
                                try {
                                    new $rab.AreaDistrictAssignment().LoadGroupingTable(jsondata);
                                } catch (e) {

                                }
                            }
                            inv.invoke();
                        };
                    }
                    
                } catch (e) {
                    alert(e.message);
                }
            }
            func.invoke();
        } catch (e) {
            alert(e.message);
        }
    }
    this.LoadGroupingTable = function (jsondata) {
        var tbody = document.getElementById('tbodyAssignedGroups');
        $(tbody).empty();
        if (typeof jsondata == 'undefined') throw new Error('No data returned from server');
        var parsed = JSON.parse(jsondata);
        if (parsed.message != 'success') throw new Error(parsed.message);
        parsed = JSON.parse(parsed.result);
        for (var i = 0; i < parsed.result.length; i++) {
            tbody.insertRow(i);
            tbody.rows[i].insertCell(0).innerHTML = parsed.result[i].FullName;
            tbody.rows[i].insertCell(1).innerHTML = parsed.result[i].GroupName;
            tbody.rows[i].insertCell(2);
            var img = document.createElement('img');
            img.className = 'img img-circle';
            img.style.width = '25px';
            img.style.height = '25px';
            img.src = 'assets/images/delete.png';
            img.record = parsed.result[i];
            tbody.rows[i].cells[2].appendChild(img);
            img.style.cursor = 'pointer';
            img.onclick = function () {
                var inv = new $rab.Server.Dispatcher();
                inv.obj = { memberID: this.record.memberID };
                inv.url = "Accounts.aspx/RemoveAssignedGroup";
                inv.handler = function (jsondata) {
                    try {
                        new $rab.AreaDistrictAssignment().LoadGroupingTable(jsondata);
                    } catch (e) {

                    }
                }
                inv.invoke();
            };
        }
    }
}
$rab.PagesAudit = function () {
    this.LoadBranches = function (dropdown) {
        $("#" + dropdown).empty();
        $rab.Utilities.FillOptions(["All"], dropdown);
        $rab.Security.Church.LoadBranches(dropdown);

    }
    this.GetAuditTrail = function (branchName) {
        try {
            $("#tbodyAuditTrailBody").empty();
            new $rab.Server.Method().BeginInvoke("Accounts.aspx/GetAuditTrail", { branchName: branchName }, function (jsondata) {
                try {
                    //empty audit trail listing
                    $("#tbodyAuditTrailBody").empty();
                    if (jsondata == "failed getting audit trail") throw new $rab.Exception(jsondata);
                    var parsed = JSON.parse(jsondata);
                    for (var i = 0; i < parsed.audits.length; i++) {
                        try {
                            tr = document.createElement("tr");
                            //tdID           = document.createElement("td");tdID          .innerHTML=parsed.audits[i].ID          
                            tdBranchName = document.createElement("td"); tdBranchName.innerHTML = parsed.audits[i].BranchName
                            tdMemberName = document.createElement("td"); tdMemberName.innerHTML = parsed.audits[i].MemberName
                            tdDateVisited = document.createElement("td"); tdDateVisited.innerHTML = parsed.audits[i].DateVisited
                            tdTimeLoggedIn = document.createElement("td"); tdTimeLoggedIn.innerHTML = parsed.audits[i].TimeLoggedIn
                            tdPageVisited = document.createElement("td"); tdPageVisited.innerHTML = parsed.audits[i].PageVisited
                            //tr.appendChild(tdID          );
                            tr.appendChild(tdBranchName);
                            tr.appendChild(tdMemberName);
                            tr.appendChild(tdDateVisited);
                            tr.appendChild(tdTimeLoggedIn);
                            tr.appendChild(tdPageVisited);

                            document.getElementById("tbodyAuditTrailBody").appendChild(tr);

                        } catch (e) {

                        }
                    }
                } catch (e) {

                }

            });
        } catch (e) {

        }
    }
}
$rab.AccountRevocation = function (dropdown) {
    this.GetMemberNames = function (branchName) {
        try {
            $("#" + dropdown).empty();
            var url, obj, handler
            url = 'MembershipList.aspx/GetMembers';
            obj = {
                branchName: branchName
            };
            handler = this.LoadDropDownList;
            new $rab.Server.Method().BeginInvoke(url, obj, handler);
        } catch (e) {
            alert(e.message);
        }
    }
    this.LoadDropDownList = function (jsondata) {
        try {

            // update select member name with names supplied from server
            if (jsondata === undefined || jsondata.length == 0) throw new $rab.Exception("No member record found");


            var members = JSON.parse(jsondata);
            document.getElementById(dropdown).MemberList = members;

            for (var i = 0; i < members.length; i++) {
                var option = document.createElement("option");
                option.className = "form-control";
                option.value = members[i].LastName + " " + members[i].FirstName
                option.innerText = members[i].LastName + " " + members[i].FirstName
                option.MemberID = members[i].MemberID;
                option.Email = members[i].Email;
                document.getElementById(dropdown).appendChild(option);

            }

        } catch (e) {
            alert(e.message);
        }
    }
    this.Revoke = function (type) {

        try {
            var obj = {
                memberid: document.getElementById("selectRevokeMembers").options[document.getElementById("selectRevokeMembers").selectedIndex].MemberID,
                revokeType: type
            }
            //validate inputs
            if ($("#selectRevokeMembers").val() == "Select member") throw new $rab.Exception("please select a member");
            if ($("#selectRevokeBranch").val() == "Select branch") throw new $rab.Exception("Select branch");
            if ($("#seletRevocationType").val() == "Select revocation type") throw new $rab.Exception("Select revocation type");

            var memid = document.getElementById("selectRevokeMembers").options[document.getElementById("selectRevokeMembers").selectedIndex].MemberID;
            new $rab.Server.Method().BeginInvoke("Accounts.aspx/RevokeAccount", { memberid: memid, revokeType: type }, function (jsondata) {
                try {
                    alert(jsondata);
                } catch (e) {

                }

            });
        } catch (e) {
            alert(e.message);
        }
    }
}
$rab.Accounts.LoadAssignedLogins = function () {
    try {
        //load all assigned logins
        var url, obj, handler
        url = "Accounts.aspx/GetAssignedLogins"
        obj = {};
        handler = function (jsondata) {
            try {
                var parsed = JSON.parse(jsondata);
                if (parsed.message = 'success') {
                    var tbody = document.getElementById("tbodyAssignedLogins");
                    $(tbody).empty();
                    for (var i = 0; i < parsed.result.length; i++) {
                        try {
                            tbody.insertRow(i);
                            tbody.rows[i].insertCell(0).innerHTML = parsed.result[i].FullName;
                            tbody.rows[i].insertCell(1);//.innerHTML  = parsed.result[i].Attendance            ;
                            tbody.rows[i].insertCell(2);//.innerHTML  = parsed.result[i].Appointment           ;
                            tbody.rows[i].insertCell(3);//.innerHTML  = parsed.result[i].Baptism               ;
                            tbody.rows[i].insertCell(4);//.innerHTML  = parsed.result[i].Census                ;
                            tbody.rows[i].insertCell(5);//.innerHTML  = parsed.result[i].Events                ;
                            tbody.rows[i].insertCell(6);//.innerHTML =  parsed.result[i].Finances              ;
                            tbody.rows[i].insertCell(7);//.innerHTML =  parsed.result[i].FollowUp              ;
                            tbody.rows[i].insertCell(8);//.innerHTML =  parsed.result[i].GiftAid               ;
                            tbody.rows[i].insertCell(9);//.innerHTML =  parsed.result[i].MembershipList        ;
                            tbody.rows[i].insertCell(10);//.innerHTML =  parsed.result[i].StandardReports       ;
                            tbody.rows[i].insertCell(11);//.innerHTML =  parsed.result[i].WeeklyServiceReport   ;
                            tbody.rows[i].cells[1].appendChild($rab.Accounts.CheckPermission("Attendance", parsed.result[i].Attendance, parsed.result[i]));
                            tbody.rows[i].cells[2].appendChild($rab.Accounts.CheckPermission("Appointment", parsed.result[i].Appointment, parsed.result[i]));
                            tbody.rows[i].cells[3].appendChild($rab.Accounts.CheckPermission("Baptism", parsed.result[i].Baptism, parsed.result[i]));
                            tbody.rows[i].cells[4].appendChild($rab.Accounts.CheckPermission("Census", parsed.result[i].Census, parsed.result[i]));
                            tbody.rows[i].cells[5].appendChild($rab.Accounts.CheckPermission("Events", parsed.result[i].Events, parsed.result[i]));
                            tbody.rows[i].cells[6].appendChild($rab.Accounts.CheckPermission("Finances", parsed.result[i].Finances, parsed.result[i]));
                            tbody.rows[i].cells[7].appendChild($rab.Accounts.CheckPermission("Follow-up", parsed.result[i].FollowUp, parsed.result[i]));
                            tbody.rows[i].cells[8].appendChild($rab.Accounts.CheckPermission("Gift Aid", parsed.result[i].GiftAid, parsed.result[i]));
                            tbody.rows[i].cells[9].appendChild($rab.Accounts.CheckPermission("Membership List", parsed.result[i].MembershipList, parsed.result[i]));
                            tbody.rows[i].cells[10].appendChild($rab.Accounts.CheckPermission("Standard Reports", parsed.result[i].StandardReports, parsed.result[i]));
                            tbody.rows[i].cells[11].appendChild($rab.Accounts.CheckPermission("Weekly service report", parsed.result[i].WeeklyServiceReport, parsed.result[i]));
                            // provide check boxes for each category
                        } catch (e) {

                        }

                    }
                }

            } catch (e) {

            }
        };
        new $rab.Server.Method().BeginInvoke(url, obj, function (jsondata) {
            handler(jsondata);
        })

    } catch (e) {

    }
} 
$rab.Accounts.CheckPermission = function (pageName, hasPermission, person) {
    /*pageName: name of page
      hasPermission: integer 0 meaning no page assigned and 1 meaning page assigned
      person: all permission assigned to person
    */
    if (pageName == "" || pageName == null) return;
    //if (hasPermission == null || hasPermission.toString() == "") return;
    if (person == null || person == "") return;
    var checkbox = document.createElement("input");
    try {

        checkbox.type = "checkbox";
        checkbox.Person = person;
        checkbox.PageName = pageName;
        checkbox.onchange = function () {

            //if checkbox is checked update person for person
            if (this.checked == true) {
                //addpage permission to user
                //new $rab.Accounts.Permission().AddPagePermission(person.BranchID, this.PageName, person.MemberID)
                var url, obj, handler;
                url = 'Accounts.aspx/AddPagePermission';
                obj = {
                    branchid: person.BranchID,
                    pageName: this.PageName,
                    memberid: person.MemberID,
                    createdby: $rab.Login.User.MemberID
                };
                handler = function (jsondata) {
                    if (jsondata == 'Member already has page permission') return;
                    if (JSON.parse(jsondata).message == 'successfully added permission') {
                        //alert('success');
                    }
                };
                new $rab.Server.Method().BeginInvoke(url, obj, handler);
            } else if (this.checked == false) {
                if (hasPermission.length != 0) {
                    var id = hasPermission[0].PermissionID
                    new $rab.Server.Method()
                        .BeginInvoke("Accounts.aspx/DeletePermission", { ID: id }, function (jsondata) {
                            if (jsondata == 'Member already has page permission') return;
                            if (jsondata == 'Permission for user deleted') {
                                // alert('Permission for user deleted');
                            }
                            else {
                                alert(jsondata);
                            }
                        });
                }
            }
        };
        if (hasPermission.length == 1) {
            checkbox.checked = true;
            //  
        } else {
            checkbox.checked = false;


        }
    } catch (e) {

    }
    return checkbox;
}
$rab.BranchGroupingSetUp = function () {
    this.Add = function () {

        try {
            var server = new $rab.Server.Dispatcher();
            server.url = "Accounts.aspx/AddBranchGroup";
            server.obj = {
                item: {
                    ID: 0,
                    BranchID: document.getElementById("selectBranchGroupAdditions").options[document.getElementById("selectBranchGroupAdditions").selectedIndex].BranchID,//$('#selectBranchGroupAdditions').val(),
                    BranchGroupingType: $('#selectBranchesGroupingType').val(),
                    GroupingName: $('#textboxBranchGroupName').val(),
                    BranchName: $('#selectBranchGroupAdditions').val(),

                }
            };
            server.handler = this.loadTable
            server.invoke();
        } catch (e) {

        }
    }
    this.loadTable = function (Jsondata) {

        try {
            if (JSON.parse(Jsondata).message != 'success') { alert(JSON.parse(Jsondata).message); return; }
            //rebuild table
            var items = JSON.parse(Jsondata).result;
            $("#tbodyGroupedTobeAssigned").empty();
            var tbody = document.getElementById('tbodyGroupedTobeAssigned');
            for (var i = 0; i < items.length; i++) {
                try {
                    tbody.insertRow(i);
                    tbody.rows[i].insertCell(0).innerHTML = items[i].BranchGroupingType;
                    tbody.rows[i].insertCell(1).innerHTML = items[i].GroupingName;
                    tbody.rows[i].insertCell(2).innerHTML = items[i].BranchName;
                    tbody.rows[i].insertCell(3).innerHTML = items[i].BranchID;
                    tbody.rows[i].insertCell(4).appendChild(document.createElement('img'));
                    var img = tbody.rows[i].cells[4].firstChild;
                    img.src = "assets/images/delete.png"
                    img.width = 25;
                    img.height = 25;
                    img.ID = items[i].ID;
                    img.tr = tbody.rows[i];
                    img.onclick = function () {
                        try {
                            var tr = this.tr;
                            new $rab.Server
                                .Method()
                                .BeginInvoke("Accounts.aspx/DeleteGroupBranches",
                                { ID: this.ID },
                                function (jsondata) {
                                    if (JSON.parse(jsondata).message='success')    $(tr).remove();

                                });
                        } catch (e) {

                        }
                    }
                } catch (e) {

                }
            }

        } catch (e) {

        }
    }
    this.initialiseTable = function () {
        try {
            var server = new $rab.Server.Dispatcher();
            server.url = "Accounts.aspx/GetGroupBranches";
            server.obj = {};
            server.handler = this.loadTable
            server.invoke();
        } catch (e) {

        }
    }
}