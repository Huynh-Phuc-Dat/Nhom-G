const Lake_Setting_InsertController = function ($http, $scope, $async, $timeout, $compile, $filter) {
    const URL_REQUEST = "/Lake/Lake_Setting";

    $scope.filter = {
        LakeID: "-1",
        Temperature: "",
        Salt: "",
        PH: "",
        O2: "",
        NO2: "",
        NO3: "",
        PO4: "",
        Note: "",
        SettingDate: ""
    }

    $scope.list_FishKoi = [];
    $scope.backHome = () => window.location.href = "/thong-so-nuoc";

    $scope.insert = $async(async ($event) => {
        $event.preventDefault();
        const submitButton = document.querySelector("#btn-submit-lake-setting");
        submitButton.setAttribute('data-kt-indicator', 'on');
        try {
            const bo = {
                ...$scope.filter,
                SettingDate: formatDate($scope.filter.SettingDate)
            }
            const response = await $http.post(URL_REQUEST + "/Insert", bo).then(success => success.data);
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
            toastr.error('Lỗi lấy dữ liệu cá Koi');
        }
    })

    const initData = () => getAllLake();
    initData();

}
Lake_Setting_InsertController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile", "$filter"];