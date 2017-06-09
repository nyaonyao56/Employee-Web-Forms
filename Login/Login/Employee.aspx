<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Login.Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script type="text/javascript" src="//cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">
        //$.support.cors = true;
        $(document).ready(function () {
            $.ajax({
                url: '<%=ResolveUrl("GetEmployee.aspx") %>',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var table = $('#datatable').DataTable({
                        data: data,
                        columns: [
                            { 'data': 'Id' },
                            { 'data': 'image' },
                            { 'data': 'lastName' },
                            { 'data': 'firstName' },
                            { 'data': 'jobTitle' },
                            {
                                'data': 'StartDate',
                                'render': function (jsonDate) {
                                    var date = new Date(parseInt(jsonDate.substr(6)));
                                    var month = date.getMonth() + 1;
                                    return month + "/" + date.getDate() + "/" + date.getFullYear();
                                }
                            },
                            {
                                'data': 'EndDate',
                                'render': function (jsonDate) {
                                    var date = new Date(parseInt(jsonDate.substr(6)));
                                    var month = date.getMonth() + 1;
                                    return month + "/" + date.getDate() + "/" + date.getFullYear();
                                }
                            },
                            {
                                'data': null,
                                'render': function (data, type, row) {
                                    return '<button id="editBtn" onclick="editClick(this)" type="button">Edit</button>'
                                }
                            },
                            {
                                'data': null,
                                'render': function (data, type, row) {
                                    return '<button id="' + row.id + '" onclick="deleteClick(this)" type="button">Delete</button>'
                                }
                            }
                        ]
                    });
                }
            });

            $('#dialog').dialog({
                width: 600,
                height: 500,
                autoOpen: false,
                title: 'Employee Form',
                buttons: {
                    'Submit': function () {
                        var lastName = $('#lName').val();
                        var firstName = $('#fName').val();
                        var jobTitle = $('#jobTitle').val();
                        var startDate = $('#startDate').val();
                        var endDate = $('#endDate').val();
                        var fileUpload = $('#fileUploads').val();
                        $.ajax({
                            url: '<%=ResolveUrl("AddEmployee.aspx/AddEmp") %>',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({lName: lastName, fName: firstName, jobTitle: jobTitle, startDate: startDate, endDate: endDate }),
                            success: function (data) {
                    
                            }
                        });
                    }
                }
            });
            $('#add').click(function () {
                $('#dialog').dialog('open');
            });
        });


        function editClick(obj) {
            var employeeID = $(obj).closest('tr').find('td:first').html();
            var lastName = $(obj).closest('tr').find('td:nth-child(3)').html();
            var firstName = $(obj).closest('tr').find('td:nth-child(4)').html();
            var jobTitle = $(obj).closest('tr').find('td:nth-child(5)').html();
            var startDate = $(obj).closest('tr').find('td:nth-child(6)').html();
            var endDate = $(obj).closest('tr').find('td:nth-child(7)').html();
            console.log(startDate);

            if (employeeID != null) {
                $('#lName').val(lastName);
                $('#fName').val(firstName);
                $('#jobTitle').val(jobTitle);
                $('#startDate').val(startDate);
                $('#endDate').val(endDate);

                $('#dialog').dialog({
                    width: 600,
                    height: 500,
                    autoOpen: false,
                    title: 'Employee Form',
                    buttons: {
                        'Edit': function () {
                            $.ajax({
                                url: '<%=ResolveUrl("EditEmployee.aspx/EditEmpl") %>',
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                data: JSON.stringify({ lName: $('#lName').val(), fName: $('#fName').val(), jobTitle: $('#jobTitle').val(), startDate: $('#startDate').val(), endDate: $('#endDate').val(), id: employeeID }),
                                success: function (data) {

                                }
                            });
                        }
                    }

                });
                $('#dialog').dialog('open');
            }
        }

        
        function deleteClick(obj) {
            var employeeID = $(obj).closest('tr').find('td:first').html();
            //alert(employeeID);
            //alert(lastName);
            var data = '{ id:' + employeeID + '}';
            $.ajax({
                url: '<%=ResolveUrl("DeleteEmployee.aspx/DeleteRecords") %>',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: data,
                success: function (data) {
                    
                }
            });
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border:1px solid black; padding:3px; width:1200px; margin:auto">
            <table id="datatable">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Picture</th>
                        <th>Last Name</th>
                        <th>First Name</th>
                        <th>Job Position</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
            </table>
                <button id="add" type="button">Add</button>
            <div id="dialog">
                <div style="padding-top:10px; padding-left:10px">
                    <label>Last Name: </label>
                     <input type="text" id="lName" placeholder="Last Name"/><br />
                </div>
                <div style="padding-top:10px; padding-left:10px">
                    <label>First Name: </label>
                     <input type="text" id="fName" placeholder="First Name"/><br />
                </div>
                <div style="padding-top:10px">
                    <label>Job Position: </label>
                    <input type="text" id="jobTitle" placeholder="Job Title" /><br />
                </div>
                <div style="padding-top:10px; padding-left:15px">
                    <label>Start Date: </label>
                    <input type="date" id="startDate"/><br />
                </div>
                <div style="padding-top:10px; padding-left:19px">
                    <label>End Date: </label>
                    <input type="date" id="endDate"/><br />
                </div>
                <div style="padding-top:10px">
                    <input type="file" id="fileUploads" name="fileUpload"/><br /> 
                </div>
                
            </div>
        </div>
    <div>
    
    </div>
        
        
    </form>
</body>
</html>
