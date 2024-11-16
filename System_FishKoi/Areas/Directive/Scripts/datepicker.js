const datepicker = ($timeout) => {
    return {
        restrict: 'E',
        scope: {
            model: "=",
            id: "@",
            format: "@"
        },
        templateUrl: "/Directive/Directive/DatePicker",
        link: function (scope, element, attrs) {
            const picker = new tempusDominus.TempusDominus(document.getElementById(scope.id), {
                localization: {
                    startOfTheWeek: 1,
                    format: isNullOrEmpty(scope.format) ? "dd/MM/yyyy" : format
                }
            });

            scope.$watch('model', (newValue, oldValue) => {
                if (newValue != oldValue && JSON.stringify(newValue) != JSON.stringify(oldValue)) {
                    if (!isNullOrEmpty(newValue)) picker.dates.setValue(tempusDominus.DateTime.convert(newValue));
                }
            })

            $(element).on('change.td', (e) => {
                scope.model = e.date;
            });
        }
    }
}
datepicker.$inject = ["$timeout"];
