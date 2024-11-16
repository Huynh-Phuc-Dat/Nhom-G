const ProductController = function ($http, $scope, $async, $timeout, $compile, $rootScope) {
    const URL_REQUEST = "/Product/Product"
    var _myDataTable = null;

    $scope.filter = {
        keySearch: '',
        categoryID: "-1",
    }

    $scope.objProduct = {};

    $scope.showModal = (item) => {
        if (!isNullOrEmpty(item)) {
            $scope.objProduct = {
                ...item,
                CategoryID: item.CategoryID.toString(),
            }

            $("#imgAvatarProduct").css('background-image', 'url("' + item.ProductLink + '")');
        }
        else {
            $scope.objProduct = {
                ProductID: 0,
                CategoryID: '-1',
                ProductName: '',
                ProductCode: '',
                Description: '',
                Price: ''
            }

            $("#imgAvatarProduct").css('background-image', 'url("")');
        }
        $('#kt_modal_product').modal('show');
    }

    $scope.search = () => {
        if (!isNullOrEmpty(_myDataTable)) {
            _myDataTable.table().draw();
        }
        else {
            _myDataTable = $('#kt_product_table').DataTable({
                serverSide: true,
                processing: true,
                ordering: false,
                columns: [
                    { data: null },
                    { data: 'ProductCode' },
                    { data: 'CategoryName' },
                    { data: 'Price' },
                    { data: null },
                ],

                columnDefs: [
                    {
                        targets: [0, 1, 2, 3],
                        className: "text-gray-800 text-hover-primary mb-1",
                    },

                    {
                        targets: [0],
                        className: "fw-bold text-info",
                        render: function (data, type, row, meta) {
                            const json = JSON.stringify(row).replace(/"/g, '&quot;');
                            let html = `<div class="d-flex align-items-center">`;
                            html += `<a href="javascript:;" class="symbol symbol-50px" ng-click="showModal(${json})">`
                            html += `<span class="symbol-label" style="background-image:url('${row.ProductLink}');"></span>`
                            html += '</a>'
                            html += `<div class="ms-5">
                                        <a href="javascript:;" class="text-gray-800 text-hover-primary fs-5 fw-bold" ng-click="showModal(${json})">${row.ProductName}</a>
                                    </div>`;
                            html += '</div>'
                            return html;
                        }
                    },
                    {
                        targets: 3,
                        render: function (data) {
                            return formatMoney(data);
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
                		<a ng-click="showModal(${json})" href="javascript:;" class="menu-link px-3">Xem</a>
                	</div>
                	<div class="menu-item px-3">
                		<a ng-show="${$scope.info.IsAdmin}"  ng-click="delete(${row.ProductID},$event)" class="menu-link px-3">Xóa</a>
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
                    let tableElement = $('#kt_product_table')
                    if (tableElement.length > 0) {
                        $compile(tableElement.contents())($scope);
                    }
                    KTMenu.createInstances();
                }, 0);
            })
        }
    }

    $scope.delete = $async(async (productID, $event) => {
        $event.preventDefault();

        const { value } = await Swal.fire({
            text: "Bạn có chắc chắn xóa dữ liệu sản phẩm này không?",
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
                    ProductID: productID
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

    $scope.insertOrUpdate = $async(async ($event) => {
        $event.preventDefault();
        const submitButton = document.querySelector("#btn_modal_product_submit");

        try {
            submitButton.setAttribute('data-kt-indicator', 'on');

            let action = '/Insert'
            if ($scope.objProduct.ProductID > 0) {
                action = '/Update';
            }

            const formData = new FormData();
            const listFile = document.getElementById("value_file").files;
            if (listFile.length > 0) {
                formData.append("value_file", listFile[0]);
            }

            for (let key in $scope.objProduct) {
                formData.append(key, isNullOrEmpty($scope.objProduct[key]) ? "" : $scope.objProduct[key]);
            }

            const objHttp = {
                method: 'POST',
                url: URL_REQUEST + action,
                data: formData,
                headers: {
                    'Content-Type': undefined
                }
            }

            const response = await $http(objHttp).then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                swalCommon("success", "Tạo dữ liệu cá Koi thành công", false).then(() => {
                    window.location.href = "/danh-sach-san-pham";
                });
            } else {
                toastr.warning(response.Message)
                submitButton.removeAttribute('data-kt-indicator');
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi tạo dữ liệu cá Koi');
        }
    })

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

    const initData = async () => {
        await getSystem_User();
        $scope.search();
    }
    initData();

    $scope.search();
}
ProductController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile", "$rootScope"];