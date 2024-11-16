const GlobalAppController = function ($http, $scope, $async, $rootScope) {
    $rootScope.globalCurrentUser = {};
    const getSystem_User = $async(async () => {
        try {
            const response = await $http.get("/Home/GetSystem_User").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $rootScope.globalCurrentUser = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu thông tin đăng nhập');
        }
    })

    getSystem_User();
}
GlobalAppController.$inject = ["$http", "$scope", "$async", "$rootScope"];