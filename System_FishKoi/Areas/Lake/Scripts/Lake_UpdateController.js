const Lake_UpdateController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/Lake/Lake"
    const DIRECTORY = "Upload/Lake"

    $scope.filter = {}

    $scope.items = [];

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

    const checkValid_Item = (messageError = "Vui lòng lưu dòng hiện tại") => {
        const listResult = $scope.items.filter(item => item.IsEdit);
        if (listResult.length > 0) {
            toastr.warning(messageError);
            return false;
        }
        return true;
    }

    $scope.update = $async(async ($event) => {
        $event.preventDefault();
        debugger;
        if ($scope.items.length < 1) {
            toastr.warning("Vui lòng thêm ít nhất một dữ liệu cá Koi");
            return;
        }

        if (checkValid_Item("Vui lòng lưu dòng hiện tại trước khi tạo dữ liệu hồ cá")) {
            const submitButton = document.querySelector("#btn-submit-lake");

            try {
                submitButton.setAttribute('data-kt-indicator', 'on');
                $scope.filter.LakeImage = $scope.listFile.map(x => x.url).join(',');

                const bo = {
                    ...$scope.filter,
                    items: $scope.items
                }

                const response = await $http.post(URL_REQUEST + "/Update", bo).then(success => success.data);
                if (response.Status === MESSAGE_STATUS.success) {
                    swalCommon("success", "Cập nhật dữ liệu hồ cá thành công", false).then(() => {
                        window.location.href = "/danh-sach-ho-ca";
                    });
                } else {
                    toastr.warning(response.Message)
                    submitButton.removeAttribute('data-kt-indicator');
                }
            } catch (e) {
                console.log(e);
                toastr.error('Lỗi cập nhật dữ liệu hồ cá');
            }
        }
    })

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

    const initData = $async(async () => {
        showLoading();
        try {
            const lakeID = $('#lakeID').val() || 0;

            if (isNullOrEmpty(lakeID) || lakeID < 1) {
                $('#set-html').html('');
                toastr.warning('Không tìm thấy dữ liệu hồ cá này trong hệ thống!!!');
                return;
            }
            getAllFishKoi();
            getDetail(lakeID);
        } catch (e) {

        } finally {
            hideLoading()
        }
    })

    const getDetail = $async(async (lakeID) => {
        try {
            const params = {
                lakeID: lakeID
            }
            const response = await $http.get(URL_REQUEST + "/GetDetail", { params }).then(success => success.data);

            if (response.Status === MESSAGE_STATUS.success) {
                $scope.filter = {
                    ...response.Data,
                };

                if (!isNullOrEmpty($scope.filter.LakeImage)) {
                    const arrayFile = $scope.filter.LakeImage.split(',');
                    for (let index = 0; index < arrayFile.length; index++) {
                        $scope.listFile.push({
                            url: arrayFile[index],
                            name: arrayFile[index],
                            index: index,
                            baseURL: $scope.filter.LakeLink + arrayFile[index]
                        });
                    }
                }

                if ($scope.filter.items != null && $scope.filter.items.length > 0) {
                    $scope.items = $scope.filter.items.map(item => {
                        const objItem = {
                            ...item,
                            FishKoiID: item.FishKoiID.toString()
                        }
                        return objItem
                    });
                }

                new Dropzone("#kt_dropzonejs_example_1", {
                    url: URL_REQUEST + "/UploadFile",
                    paramName: "file",
                    method: "post",
                    maxFiles: 10,
                    maxFilesize: 1024, // MB
                    addRemoveLinks: true,
                    acceptedFiles: ".jpeg,.jpg,.png",

                    init: function () {
                        const thisDropzone = this;
                        for (let i = 0; i < $scope.listFile.length; i++) {
                            thisDropzone.emit('addedfile', $scope.listFile[i]);
                            thisDropzone.emit('complete', $scope.listFile[i]);
                            thisDropzone.emit("thumbnail", $scope.listFile[i], $scope.listFile[i].baseURL);
                        }

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
                            file.status = "error";
                            toastr.warning(response.Message);
                            this.removeFile(file);
                        }
                    },
                    removedfile: function (file) {
                        const index = $scope.listFile.findIndex(x => x.index == file.index);
                        $scope.listFile.splice(index, 1);
                        file.previewElement.remove();
                    },
                    complete: function (file) {
                        if (file.status == "success") {
                            const acceptedFiles = this.getAcceptedFiles();
                            console.log(acceptedFiles);

                            $scope.listFile_Update = acceptedFiles.map(item => {
                                const objTemp = {
                                    url: item.url
                                }
                                return objTemp
                            })
                        }

                        const progressElement = file.previewElement.querySelector(".dz-progress");
                        if (progressElement) {
                            progressElement.remove();
                        }
                    }
                });

            }
            else {
                $('#set-html').html('');
                toastr.warning(response.Message);
                return;
            }
        } catch (e) {
            toastr.error('Lỗi load chi tiết dữ liệu hồ cá');
            console.log(e);
        }
    })

    initData();
}
Lake_UpdateController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];