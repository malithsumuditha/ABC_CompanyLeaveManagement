var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/EmployeeType/GetAll"
        },
        "columns": [
            { "data": "employeeTypeName", "width": "15%" },  
            { "data": "annualLeaves", "width": "15%" }, 
            { "data": "casualLeaves", "width": "15%" },             
            { "data": "medicalLeaves", "width": "15%" },   
            {
                "data": "employeeTypeId",
                "render": function (data) {
                    return `
                     <div class=" btn-group" role="group">
                    <a href="/Admin/EmployeeType/Upsert?id=${data}"
                   class="btn btn-warning mx-1"><i class="bi bi-pencil-square"></i></a>

                    <a onClick=Delete('/Admin/EmployeeType/Delete/${data}')
                   class="btn btn-danger mx-1"><i class="bi bi-trash"></i></a>

                    <a onClick=AllocateLeaves('/Admin/EmployeeType/AllocateLeaves/${data}')
                   class="btn btn-primary mx-1">Allocate Leaves</a>
                </div>

                 
                    `
                },
                "width": "15%"

            }
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
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

function AllocateLeaves(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#14A44D',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, allocate it!'
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