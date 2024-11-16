const select2 = ($timeout, $compile) => {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {

            let initialLoad = true;
            let isSelect2Change = false;
            let countSelect2 = 0;

            $timeout(() => {
                const { hideSearch } = attrs;

                if (!isNullOrEmpty(hideSearch) && hideSearch == "true") {
                    $(element).select2({

                        minimumResultsForSearch: Infinity
                    });
                }
                else {
                    $(element).select2();
                }

                element.select2Initialized = true;
            }, 0);

            scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                if (!isSelect2Change && newVal !== oldVal) {
                    $(element).trigger('change');
                    countSelect2++;
                }
                isSelect2Change = false;
            });

            $(element).on('change.select2', function () {
                isSelect2Change = true;
                $timeout(() => {
                    const selectedValue = $(element).val();
                    scope.$eval(attrs.ngModel + "='" + selectedValue + "'");
                    if (countSelect2 == 0 || (!initialLoad && attrs.ngChange)) {
                        scope.$eval(attrs.ngChange);
                        countSelect2++;
                    }
                    initialLoad = false;
                })
            });
        }
    }
}
select2.$inject = ["$timeout", "$compile"];