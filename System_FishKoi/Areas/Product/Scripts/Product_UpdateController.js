const FishKoi_UpdateController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/FishKoi/FishKoi"
    $scope.filter = {}

    $scope.backHome = () => window.location.href = "/danh-sach-ca-koi";

    const initData = $async(async () => {
        showLoading();
        try {
            const fishKoiID = $('#fishKoiID').val() || 0;

            if (isNullOrEmpty(fishKoiID) || fishKoiID < 1) {
                $('#set-html').html('');
                toastr.warning('Không tìm thấy dữ liệu cá Koi này trong hệ thống!!!');
                return;
            }
            getDetail(fishKoiID);
        } catch (e) {

        } finally {
            hideLoading()
        }
    })

    const getDetail = $async(async (fishKoiID) => {
        try {
            const params = {
                fishKoiID: fishKoiID
            }
            const response = await $http.get(URL_REQUEST + "/GetDetail", { params }).then(success => success.data);

            if (response.Status === MESSAGE_STATUS.success) {
                $scope.filter = {
                    ...response.Data,
                    FishKoiGender: response.Data.FishKoiGender.toString(),
                };

                $("#imgAvatarFishKoi").css('background-image', 'url("' + response.Data.FishKoiLink + '")');
            }
            else {
                $('#set-html').html('');
                toastr.warning(response.Message);
                return;
            }
        } catch (e) {
            toastr.error('Lỗi load chi tiết dữ liệu cá Koi');
            console.log(e);
        }
    })

    $scope.update = $async(async ($event) => {
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
                url: URL_REQUEST + "/Update",
                data: formData,
                headers: {
                    'Content-Type': undefined
                }
            }

            const response = await $http(objHttp).then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                swalCommon("success", "Cập nhật dữ liệu cá Koi thành công", false).then(() => {
                    window.location.href = "/danh-sach-ca-koi";
                });
            } else {
                toastr.warning(response.Message)
                submitButton.removeAttribute('data-kt-indicator');
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi cập nhật dữ liệu cá Koi');
        }
    })


    initData();

}
FishKoi_UpdateController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];