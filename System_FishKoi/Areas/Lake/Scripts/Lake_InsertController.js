const Lake_InsertController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/Lake/Lake";
    const DIRECTORY = "Upload/Lake"

    $scope.filter = {
        FishKoiGender: "-1",
        FishKoiAge: "",
        FishKoiWeight: "",
        FishKoiSource: "",
        FishKoiFace: "",
        FishKoiName: "",
        FishKoiPrice: "",
    }

    $scope.items = [{
        Quantity: "",
        IsEdit: true,
        FishKoiID: "-1"
    }];

    $scope.listFile = [];

    $scope.list_FishKoi = [];
    $scope.backHome = () => window.location.href = "/danh-sach-ho-ca";
    $scope.remove_Item = ($index) => $scope.items.splice($index, 1);

    $scope.insert_Item = ($event) => {
        $event.preventDefault();
        if (checkValid_Item()) {
            $scope.items.push({
                Quantity: "",
                IsEdit: true,
                FishKoiID: "-1"
            });
        }
    }

    $scope.check_Item = ($index) => {
        if ($scope.items[$index].FishKoiID == "-1") {
            toastr.warning("Vui lòng chọn cá");
            return false;
        }



        if (isNullOrEmpty($scope.items[$index].Quantity) || $scope.items[$index].Quantity < 0) {
            toastr.warning("Vui lòng nhập số lượng cá");
            return false;
        }

        const filterArray = $scope.items.filter(x => x.FishKoiID == $scope.items[$index].FishKoiID);
        if (filterArray.length > 1) {
            toastr.warning("Bạn đã nhập số lượng cá này rồi");
            return false;
        }

        $scope.items[$index].IsEdit = false;

        const objFishKoi = $scope.list_FishKoi.find(x => x.FishKoiID == $scope.items[$index].FishKoiID);
        $scope.items[$index].FishKoiName = objFishKoi.FishKoiName;
        $scope.items[$index].FishKoiGenderName = objFishKoi.FishKoiGenderName;

    }

    $scope.edit_Item = ($index) => {
        if (checkValid_Item()) {
            $scope.items[$index].IsEdit = true;
        }
    }


    $scope.delete_Item = ($index) => {
        if (checkValid_Item()) {
            $scope.items.splice($index, 1);
        }
    }

    $scope.insert = $async(async ($event) => {
        $event.preventDefault();
        if ($scope.items.length < 1) {
            toastr.warning("Vui lòng thêm ít nhất một dữ liệu cá Koi");
            return;
        }

        if (checkValid_Item("Vui lòng lưu dòng hiện tại trước khi cập nhật dữ liệu hồ cá")) {
            const submitButton = document.querySelector("#btn-submit-lake");
            submitButton.setAttribute('data-kt-indicator', 'on');
            try {
                $scope.filter.LakeImage = $scope.listFile.map(x => x.url).join(',');
                const bo = {
                    ...$scope.filter,
                    items: $scope.items
                }
                const response = await $http.post(URL_REQUEST + "/Insert", bo).then(success => success.data);
                if (response.Status === MESSAGE_STATUS.success) {
                    swalCommon("success", "Tạo dữ liệu hồ cá thành công", false).then(() => {
                        window.location.href = "/danh-sach-ho-ca";
                    });
                } else {
                    toastr.warning(response.Message)
                }
            } catch (e) {
                console.log(e);
                toastr.error('Lỗi tạo dữ liệu hồ cá');
            } finally {
                submitButton.removeAttribute('data-kt-indicator');
            }
        }
    })

    const checkValid_Item = (messageError = "Vui lòng lưu dòng hiện tại") => {
        const listResult = $scope.items.filter(item => item.IsEdit);
        if (listResult.length > 0) {
            toastr.warning(messageError);
            return false;
        }
        return true;
    }

    const getAllFishKoi = $async(async () => {
        try {
            const response = await $http.get("/FishKoi/FishKoi/GetAll").then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                $scope.list_FishKoi = response.Data;
            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi lấy dữ liệu cá Koi');
        }
    })

    const initData = () => getAllFishKoi();
    initData();

    new Dropzone("#kt_dropzonejs_example_1", {
        url: URL_REQUEST + "/UploadFile",
        paramName: "file",
        method: "post",
        maxFiles: 10,
        thumbnailWidth: "200",
        thumbnailHeight: "200",
        maxFilesize: 1024,
        addRemoveLinks: true,
        acceptedFiles: ".jpeg,.jpg,.png",

        init: function () {
            const thisDropzone = this;

            thisDropzone.on("addedfile", function (file) {
                file.index = $scope.listFile.length;
            });
        },

        sending: function (file, xhr, formData) {
            const bo = {
                Directory: DIRECTORY
            }

            for (let key in bo) {
                formData.append(key, bo[key]);
            }
        },
        success: function (file, response) {
            if (response.Status === MESSAGE_STATUS.success) {
                const fileSuccess = {
                    ...file,
                    url: response.Data,
                }
                file.previewElement.classList.add("dz-success");
                $scope.listFile.push(fileSuccess);
            }
            else {
                toastr.warning(response.Message);
                this.removeFile(file);
            }
        },
        removedfile: function (file) {
            const index = $scope.listFile.findIndex(x => x.index == file.index);
            $scope.listFile.splice(index, 1);
            file.previewElement.remove();
        },
    });
}
Lake_InsertController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];