var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {


    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Employee/LeaveRequest/GetAll"
        },
        "columns": [
            { "data": "leaveTypeId", "width": "15%" },
            { "data": "employeeLeave.userId", "width": "5%" },
            { "data": "dayFrom", "width": "15%" },
            { "data": "dayTo", "width": "15%" },
            { "data": "days", "width": "5%" },
            {
                "data": "isApproved",
                "render": function (data) {
                    if (data === null) {
                        return ` <button type="button" disabled  class="btn btn-warning text-center btn-sm">Pending</button>`
                    } else if (data == 0) {
                        return ` <button type="button" disabled  class="btn btn-danger text-center btn-sm">Declined</button>`
                    } else {
                        return ` <button type="button" disabled class="btn btn-success text-center btn-sm">Approved</button>`
                    }
                },

                "width": "10%"
            },
            { "data": "comment", "width": "15%" },

            {

                "data": "leaveRequestId",
                
                
                "render": function (data, type, row, meta) {

                    //check isAdmin 
                    if (isAdmin==1) {
                        if (row.isApproved == null) {
                            return ` <div class=" btn-group" role="group">

                                <a onClick=Approve('/Employee/LeaveRequest/Approve/${data}')
                                 class="btn btn-success mx-1">Approve</a>

                                <a onClick=Decline('/Employee/LeaveRequest/Decline/${data}')
                                class="btn btn-danger mx-1">Decline</a>
                                </div>`
                        } else if (row.isApproved == 0) {
                            return ` <div class=" btn-group" role="group">

                                <a onClick=Approve('/Employee/LeaveRequest/Approve/${data}')
                                 class="btn disabled btn-success mx-1">Approve</a>
                                </div>
                                `
                        } else {

                            return ` <div class=" btn-group" role="group">

                                <a onClick=Decline('/Employee/LeaveRequest/Decline/${data}')
                                 class="btn disabled btn-danger mx-1">Decline</a>
                                </div>`
                        }
                    } else {
                        return ` <button type="button" disabled  class="btn btn-danger text-center btn-sm">Access Denied</button>`
                    }
                        
                   

                    
                },
                "width": "15%"

            }
        ]
    });
}


function Decline(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, decline it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "PUT",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}



function Approve(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#14A44D',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, approve it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "PUT",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}


