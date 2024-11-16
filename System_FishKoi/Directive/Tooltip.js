const Tooltip = ($timeout) => {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                const placement = isNullOrEmpty(attrs.placement) ? "top" : attrs.placement;
                $(element).tooltip({
                    placement: placement
                });
            });
        }
    }
}
Tooltip.$inject = ["$timeout"];
