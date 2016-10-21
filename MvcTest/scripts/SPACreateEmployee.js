function ResetForm() {
    document.getElementById('txtFname').value = "";
    document.getElementById('txtLname').value = "";
    document.getElementById('txtSalary').value = "";
}
function Cancel() {
    //$('#DivCreateEmployee').dialog('close');
    $('#DivCreateEmployee').remove();
}

function SaveEmployee() {
    if (IsValid()) {
        var e = {
            FirstName: document.getElementById('txtFname').value, //FirstName: $('#txtFname').val(),
            LastName: $('#txtLname').val(),
            Salary: $('#txtSalary').val()
        };
        $.ajax({
            type: 'post',
            url: '/SPA/Main/SaveEmployee',
            data: e,
            dataType: 'json',
            success: function (r) {
                var html = '<tr><td>' + r.EmployeeName + '</td><td style="background:' + r.SalaryColor + '">' + r.Salary + '</td></tr>';
                $('#EmployeeTable').append(html);   //注意，id="EmployeeTable"是EmployeeList  View里面的表格ID
                $('#DivCreateEmployee').dialog('close');
            }
        });
        function aa() {
            //注释用函数，下面的代码等同上面，上面改用Ajax回传数据
            //$.post("/SPA/Main/SaveEmployee", e).then(
            //    //function (r) {
            //    //    var newTr = $('<tr></tr>');
            //    //    var nameTD = $('<td></td>');
            //    //    var salaryTD = $('<td></td>');

            //    //    nameTD.text(r.EmployeeName);
            //    //    salaryTD.text(r.Salary);

            //    //    salaryTD.css("background-color", r.SalaryColor);

            //    //    newTr.append(nameTD);
            //    //    newTr.append(salaryTD);

            //    //    $('#EmployeeTable').append(newTr);
            //    //    $('#DivCreateEmployee').dialog('close');

            //    //}
            //    function (r) {
            //        var html = '<tr><td>' + r.EmployeeName + '</td><td style="background:' + r.SalaryColor + '">' + r.Salary + '</td></tr>';
            //        alert(html);
            //        $('#EmployeeTable').append(html);
            //        $('#DivCreateEmployee').dialog('close');
            //    }
            //);
        }

    }
}