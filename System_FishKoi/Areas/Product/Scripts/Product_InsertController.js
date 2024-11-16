const FishKoi_InsertController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/FishKoi/FishKoi"

    $scope.filter = {
        FishKoiGender: "-1",
        FishKoiAge: "",
        FishKoiWeight: "",
        FishKoiSource: "",
        FishKoiFace: "",
        FishKoiName: "",
        FishKoiPrice: "",
    }

    $scope.backHome = () => window.location.href = "/danh-sach-ca-koi";

    $scope.insert = $async(async ($event) => {
        $event.preventDefault();
        const submitButton = document.querySelector("#btn-submit-fishkoi");

        try {
            submitButton.setAttribute('data-kt-indicator', 'on');
            const formData = new FormData();

            const listFile = document.getElementById("value_file").files;
            if (listFile.length > 0) {
                formData.append("value_file", listFile[0]);
            }

            for (let key in $scope.filter) {
                formData.append(key, isNullOrEmpty($scope.filter[key]) ? "" : $scope.filter[key]);
            }

            const objHttp = {
                method: 'POST',
                url: URL_REQUEST + "/Insert",
                data: formData,
                headers: {
                    'Content-Type': undefined
                }
            }

            const response = await $http(objHttp).then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                swalCommon("success", "Tạo dữ liệu cá Koi thành công", false).then(() => {
                    window.location.href = "/danh-sach-ca-koi";
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



}
FishKoi_InsertController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];