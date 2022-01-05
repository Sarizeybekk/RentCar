var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("beklenen")) {
        loadDataTable("GetOrderList?status=beklenen");
    }
    else {
        if (url.includes("onaylanan")) {
            loadDataTable("GetOrderList?status=onaylanan");
        }
        else {
            if (url.includes("teslimyolda")) {
                loadDataTable("GetOrderList?status=teslimyolda");
            }
            else {
                loadDataTable("GetOrderList?status=hepsi");
            }
        }
    }

});
function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Order/" +url
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "surname", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `
                            <div class="text-center">
                                <a href="/Admin/Order/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                              
                            </div>
                           `;
                }, "width": "5%"
            }
        ]
    });
}
