function checkDescription(){
            alert("God");
            //$('#ExpensesItems option:selected').val();
            //var expenseItem = $('#ExpensesItems option:selected').text();
            var expenseItem = $('#ExpensesItems').val();
            var mydata = { 'data': '"' + expenseItem + '"' };
            var ActualData = JSON.stringify(mydata);
            $.ajax({
                type: 'POST',
                url: '@Url.Action("GetDescription")',
                data: ActualData,
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    var myDescriptionReceived = [];
                     myDescriptionReceived = result.split(',');
                     alert(myDescriptionReceived[1]);
                    
                    alert(myDescriptionReceived);

                }
            });

        }