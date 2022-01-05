var dataTable;
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Vehicle/GetAll"
        },
        "columns": [
            { "data": "carName", "width": "15%" },
            { "data": "carModel", "width": "15%" },
            { "data": "driverLisenseAge", "width": "15%" },
            { "data": "minimumAgeLimit", "width": "15%" },
            { "data": "dailyKmLimit", "width": "15%" },
            { "data": "currentKm", "width": "55%" },
            { "data": "airBag", "width": "15%" },
            { "data": "baggageCapacity", "width": "15%" },
            { "data": "seatCount", "width": "15%" },
            { "data": "dailyRentalPrice", "width": "15%" },
            { "data": "category.name", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {

                    return `
                            <div class="text-center">
                                <a href="/Admin/Vehicle/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Vehicle/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "40%"
            }
        ]
    });
}
function Delete(url) {
    swal({
        title: "Bu kaydı silmek istediğinizden emin misiniz?",
        text: "bu kaydı geri alamazsınız",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {

        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
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
    })
}
