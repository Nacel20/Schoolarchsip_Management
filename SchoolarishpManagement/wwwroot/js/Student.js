var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Student/GetAll"
        },
        "columns": [
            { "data": "firstName", "width": "10%" },
            { "data": "lastName", "width": "13%" },
            { "data": "email", "width": "18%" },
            { "data": "gender.name", "width": "5%" },
            { "data": "address", "width": "20%" },
            { "data": "phoneNumber", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Student/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fa fa-eye">Details</i> 
                                </a>&nbsp;
                                <a href="/Student/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>&nbsp;
                                <a onclick=Delete("/Student/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                }, "width": "32%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
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
            });
        }
    });
}

//$(document).ready(function () {

//    var ddlCountry = $('#ddlCountry');
//    ddlCountry.append($("<option></option>").val('').html('Please Select Country'));
//    $.ajax({
//        url: 'http://localhost:54188/api/Cascading/CountryDetails',
//        type: 'GET',
//        dataType: 'json',
//        success: function (d) {
//            $.each(d, function (i, country) {
//                ddlCountry.append($("<option></option>").val(country.CountryId).html(country.CountryName));
//            });
//        },
//        error: function () {
//            alert('Error!');
//        }
//    });

//    //State details by country id

//    $("#ddlCountry").change(function () {
//        var CountryId = parseInt($(this).val());

//        if (!isNaN(CountryId)) {
//            var ddlState = $('#ddlState');
//            ddlState.empty();
//            ddlState.append($("<option></option>").val('').html('Please wait ...'));

//            debugger;
//            $.ajax({
//                url: 'http://localhost:54188/api/Cascading/StateDetails',
//                type: 'GET',
//                dataType: 'json',
//                data: { CountryId: CountryId },
//                success: function (d) {

//                    ddlState.empty(); // Clear the please wait
//                    ddlState.append($("<option></option>").val('').html('Select State'));
//                    $.each(d, function (i, states) {
//                        ddlState.append($("<option></option>").val(states.StateId).html(states.StateName));
//                    });
//                },
//                error: function () {
//                    alert('Error!');
//                }
//            });
//        }

//    });

//    //City Bind By satate id
//    $("#ddlState").change(function () {
//        var StateId = parseInt($(this).val());
//        if (!isNaN(StateId)) {
//            var ddlCity = $('#ddlCity');
//            ddlCity.append($("<option></option>").val('').html('Please wait ...'));

//            debugger;
//            $.ajax({
//                url: 'http://localhost:54188/api/Cascading/CityDetails',
//                type: 'GET',
//                dataType: 'json',
//                data: { stateId: StateId },
//                success: function (d) {


//                    ddlCity.empty(); // Clear the plese wait
//                    ddlCity.append($("<option></option>").val('').html('Select City Name'));
//                    $.each(d, function (i, cities) {
//                        ddlCity.append($("<option></option>").val(cities.CityId).html(cities.CityName));
//                    });
//                },
//                error: function () {
//                    alert('Error!');
//                }
//            });
//        }


//    });
//});