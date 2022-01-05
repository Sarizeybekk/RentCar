var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "surname", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "role", "width": "15%" },




            {
                "data": { id: "id",lockoutEnd:"lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {


                        return `
                            <div class="text-center">

                                <a onclick=LockUnLock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-lock-open"></i> Aç
                                </a>
                            </div>
                           `;
                    }
                    else {
                        return `
                            <div class="text-center">

                                <a onclick=LockUnLock('${data.id}') class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-lock"></i> kapat
                                </a>
                            </div>
                           `;
                    }
                }, "width": "25%"
            }
        ]
    });
}
function LockUnLock(id) {
    
            $.ajax({
                type: "POST",
                url: '/Admin/User/LockUnLock',
                data: JSON.stringify(id),
                contentType:"application/json",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
     
}
