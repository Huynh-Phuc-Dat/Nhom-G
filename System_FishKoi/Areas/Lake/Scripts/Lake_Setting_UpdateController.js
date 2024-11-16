const Lake_Setting_UpdateController = function ($http, $scope, $async, $timeout, $compile, $filter) {
    const URL_REQUEST = "/Lake/Lake_Setting";

    $scope.filter = {}

    $scope.list_FishKoi = [];
    $scope.backHome = () => window.location.href = "/thong-so-nuoc";

    $scope.update = $async(async ($event) => {
        $event.preventDefault();
        const submitButton = document.querySelector("#btn-submit-lake-setting");
        submitButton.setAttribute('data-kt-indicator', 'on');
        try {
            const bo = {
                ...$scope.filter,
                SettingDate: formatDate($scope.filter.SettingDate)
            }
            const response = await $http.post(URL_REQUEST + "/Update", bo).then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                swalCommon("success", "Thiết lập thông số nước thành công", false).then(() => {
                    window.location.href = "/thong-so-nuoc";
                });
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi thiết lập thông số nước thành công');
        } finally {
            submitButton.removeAttribute('data-kt-indicator');
        }
    })

    const getDetail = $async(async (settingID) => {
        try {
            const params = {
                settingID: settingID
            }
            const response = await $http.get(URL_REQUEST + "/GetDetail", { params }).then(success => success.data);

            if (response.Status === MESSAGE_STATUS.success) {
                $scope.filter = {
                    ...response.Data,
                    SettingDate: $filter('cDate')(response.Data.SettingDate),
                    LakeID: response.Data.LakeID.toString(),
                };
            }
            else {
                $('#set-html').html('');
                toastr.warning(response.Message);
                return;
            }
        } catch (e) {
            toastr.error('Lỗi load chi tiết thiết lập thông số nước');
            console.log(e);
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

    const initData = $async(async () => {
        showLoading();
        try {
            debugger;
            const settingID = $('#settingID').val() || 0;

            if (isNullOrEmpty(settingID) || settingID < 1) {
                $('#set-html').html('');
                toastr.warning('Không tìm thấy dữ liệu cá Koi này trong hệ thống!!!');
                return;
            }
            await getAllLake();
            getDetail(settingID);
        } catch (e) {

        } finally {
            hideLoading()
        }
    })

    initData();
}
Lake_Setting_UpdateController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile", "$filter"];