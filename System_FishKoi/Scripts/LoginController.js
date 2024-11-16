const LoginController = function ($http, $scope, $async, $timeout, $compile) {
    const URL_REQUEST = "/Login"
    $scope.filter = {
        Username: '',
        Password: '',
        FullName: "",
        Email: "",
        RepeatPassword: ''
    };

    $scope.isSignIn = true;
    $scope.isShowPassword = false;

    $scope.signIn = $async(async ($event) => {
        $event.preventDefault();

        if (isNullOrEmpty($scope.filter.Username) || isNullOrEmpty($scope.filter.Password)) {
            toastr.warning('Vui lòng nhập đầy đủ tài khoản và mật khẩu');
            return false;
        }

        const submitButton = document.querySelector("#kt_sign_in_submit");
        submitButton.setAttribute('data-kt-indicator', 'on');
        try {

            let action = "/SignIn";
            if (!$scope.isSignIn) {
                action = "/Register";
            }

            const response = await $http.post(URL_REQUEST + action, $scope.filter).then(success => success.data);
            if (response.Status === MESSAGE_STATUS.success) {
                if ($scope.isSignIn) {
                    window.location.href = "/";
                }
                else {
                    swalCommon("success", "Bạn đã đăng ký tài khoản thành viên thành công vui lòng chờ duyệt", false);
                }

            } else {
                toastr.warning(response.Message)
            }
        } catch (e) {
            console.log(e);
            toastr.error('Lỗi đăng nhập hệ thống');
        } finally {
            submitButton.removeAttribute('data-kt-indicator');
        }
    })

    $scope.getView = ($event) => {
        $event.preventDefault();
        $scope.isSignIn = !$scope.isSignIn;
        $scope.filter = {
            Username: '',
            Password: '',
            FullName: "",
            Email: "",
            RepeatPassword: ''
        }
    }

    $scope.showPassword = ($event) => {
        $event.preventDefault();
        $scope.isShowPassword = !$scope.isShowPassword;
    }
}
LoginController.$inject = ["$http", "$scope", "$async", "$timeout", "$compile"];