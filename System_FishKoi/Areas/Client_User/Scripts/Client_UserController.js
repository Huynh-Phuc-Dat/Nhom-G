const Client_UserController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/Client_User/Client_User"

    $scope.filter = {
        keySearch: ''
    };

    var _myDataTable = null;

    $scope.search = () => {
        if (!isNullOrEmpty(_myDataTable)) {
            _myDataTable.table().draw();
        }
        else {
            _myDataTable = $('#kt_client_user_table').DataTable({
                serverSide: true,
                processing: true,
                ordering: false,
                columns: [
                    { data: 'Username' },
                    { data: 'FullName' },
                    { data: 'Email' },
                    { data: 'Status' },
                    { data: null },
                ],

                columnDefs: [
                    {
                        targets: [0, 1, 2, 3],
                        className: "text-gray-800 text-hover-primary mb-1",
                    },
                    {
                        targets: 3,
                        render: function (data, type, row, meta) {
                            if (data != 1) {
                                return '<div class="badge badge-light-danger">Khoá</div>';
                            }
                            else {
                                return '<div class="badge badge-light-success">Đang hoạt động</div>';
                            }
                        }
                    },
                    {
                        targets: 4,
                        className: 'text-end',
                        render: function (ID, type, row, meta) {
                            const json = JSON.stringify(row).replace(/"/g, '&quot;');
                            let html = '';
                            html += `<a  class="btn btn-sm btn-light btn-flex btn-center btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">Actions
                                        <i class="ki-outline ki-down fs-5 ms-1"></i></a>`
                            html += `<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-200px py-4" data-kt-menu="true">
                	                        <div class="menu-item px-3">
                		                        <a  ng-click="delete(${row.ClientUserID},$event)" class="menu-link px-3">Xóa</a>
                	                        </div>

                                            <div class="menu-item px-3">
                		                        <a  ng-click="updateStatus(${row.ClientUserID},$event)" class="menu-link px-3">Duyệt</a>
                	                        </div>
                                    </div>`
                            return html;
                        }
                    },
                ],

                ajax: function (data, callback, settings) {
                    $scope.filter.draw = data.draw;
                    $scope.filter.offset = data.start;
                    $scope.filter.pageSize = data.length;

                    $.ajax({
                        type: "GET",
                        url: URL_REQUEST + "/GetPagedList",
                        data: $scope.filter
                    }).done(function (response) {
                        if (response.Status != MESSAGE_STATUS.success) {
                            toastr.warning(response.Message);
                        }
                        callback(response.Data);
                    })
                },
            })

            _myDataTable.on('draw', function () {
                $timeout(() => {
                    let tableElement = $('#kt_client_user_table')
                    if (tableElement.length > 0) {
                        $compile(tableElement.contents())($scope);
                    }
                    KTMenu.createInstances();
                }, 0);
            })
        }
    }

    $scope.updateStatus = $async(async (clientUserID, $event) => {
        $event.preventDefault();

        const { value } = await Swal.fire({
            text: `Bạn có chắc chắn duyệt tài khoản thành viên này không?`,
            icon: "warning",
            showCancelButton: true,
            buttonsStyling: false,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Không",
            customClass: {
                confirmButton: "btn fw-bold btn-danger",
                cancelButton: "btn fw-bold btn-active-light-primary"
            }
        });

        try {
            if (value) {
                const bo = {
                    ClientUserID: clientUserID
                }
                const response = await $http.post(URL_REQUEST + "/UpdateStatus", bo).then(success => success.data);
                if (response.Status === MESSAGE_STATUS.success) {
                    swalCommon("success", "Cập nhật dữ liệu thành công", false).then(() => {
                        $scope.search();
                    });
                }
                else {
                    toastr.warning(response.Message)
                }
            }
        } catch (e) {
            toastr.error(JSON.stringify(e));
        }
    });

    $scope.delete = $async(async (cientUserID, $event) => {
        $event.preventDefault();

        const { value } = await Swal.fire({
            text: "Bạn có chắc chắn xóa dữ liệu thành viên này không?",
            icon: "warning",
            showCancelButton: true,
            buttonsStyling: false,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Không",
            customClass: {
                confirmButton: "btn fw-bold btn-danger",
                cancelButton: "btn fw-bold btn-active-light-primary"
            }
        });

        try {
            if (value) {
                const bo = {
                    ClientUserID: cientUserID
                }
                const response = await $http.post(URL_REQUEST + "/Delete", bo).then(success => success.data);
                if (response.Status === MESSAGE_STATUS.success) {
                    swalCommon("success", "Cập nhật dữ liệu thành công", false).then(() => {
                        $scope.search();
                    });
                }
                else {
                    toastr.warning(response.Message)
                }
            }
        } catch (e) {
            toastr.error(JSON.stringify(e));
        }
    });

    $scope.search();
}
Client_UserController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];