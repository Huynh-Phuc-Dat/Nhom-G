const Lake_SettingController = function ($http, $scope, $async, $timeout, $compile, $filter) {
    const URL_REQUEST = "/Lake/Lake_Setting"
    $scope.getPage_Lake_Setting_Insert = () => window.open("/thong-so-nuoc/thiet-lap", "_blank");
    $scope.getPage_Lake_Setting_Update = (lakeID) => window.open("/thong-so-nuoc/chinh-sua/" + lakeID, "_blank");

    $scope.filter = {
        keySearch: '',
        lakeID: '-1'
    };

    var _myDataTable = null;
    $scope.list_Lake = [];


    $scope.search = () => {
        if (!isNullOrEmpty(_myDataTable)) {
            _myDataTable.table().draw();
        }
        else {
            _myDataTable = $('#kt_lake_setting_table').DataTable({
                serverSide: true,
                processing: true,
                ordering: false,
                columns: [
                    { data: null },
                    { data: 'Temperature' },
                    { data: 'NO2' },
                    { data: 'NO3' },
                    { data: 'O2' },
                    { data: 'PH' },
                    { data: 'PO4' },
                    { data: 'Salt' },
                    { data: null },
                    { data: null },
                ],

                columnDefs: [
                    {
                        targets: [0, 1, 2, 3, 4, 5, 6, 7, 8],
                        className: "text-gray-800 text-hover-primary mb-1",
                    },
                    {
                        targets: [0],
                        className: "fw-bold text-info",
                        render: function (data, type, row, meta) {
                            return `<a href="javascript:;" ng-click="getPage_Lake_Setting_Update(${row.SettingID})">${row.LakeName}</a>`
                        }
                    },
                    {
                        targets: [8],
                        className: "fw-bold text-info",
                        render: function (data, type, row, meta) {
                            return `${$filter('dateFormat')(row.SettingDate)}`
                        }
                    },
                    {
                        targets: 9,
                        className: 'text-end',
                        render: function (ID, type, row, meta) {
                            let html = '';
                            html += `<a  class="btn btn-sm btn-light btn-flex btn-center btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end">Actions
                <i class="ki-outline ki-down fs-5 ms-1"></i></a>`
                            html += `<div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-semibold fs-7 w-200px py-4" data-kt-menu="true">
                	<div class="menu-item px-3">
                		<a ng-click="getPage_Lake_Setting_Update(${row.SettingID})" href="javascript:;" class="menu-link px-3">Xem</a>
                	</div>
                	<div class="menu-item px-3">
                		<a ng-show="${$scope.info.IsAdmin || $scope.info.Username == row.CreatedUser}" ng-click="delete(${row.SettingID},$event)" class="menu-link px-3">Xóa</a>
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
                    let tableElement = $('#kt_lake_setting_table')
                    if (tableElement.length > 0) {
                        $compile(tableElement.contents())($scope);
                    }
                    KTMenu.createInstances();
                }, 0);
            })
        }
    }

    $scope.delete = $async(async (settingID, $event) => {
        $event.preventDefault();

        const { value } = await Swal.fire({
            text: "Bạn có chắc chắn xóa dữ liệu này không?",
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
                    SettingID: settingID
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

    const getSystem_User = $async(async () => {
        try {
            const response = await $http.get("/Home/GetSystem_User").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $scope.info = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu thông tin đăng nhập');
        }
    })

    const getAllLake = $async(async () => {
        try {
            const response = await $http.get("/Lake/Lake/GetAll").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $scope.list_Lake = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu hồ cá');
        }
    })

    const initData = async () => {
        getAllLake();
        await getSystem_User();
        $scope.search();
    }
    initData();
}
Lake_SettingController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile", "$filter"];