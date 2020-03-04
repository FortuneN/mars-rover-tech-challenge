app.directive('chooseFile', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ngModel) {
            
            var button = elem.find('button');
            var input = angular.element(elem[0].querySelector('input#fileInput'));

            button.bind('click', function () {
                input[0].click();
            });

            input.bind('change', function (e) {
                ngModel.$setViewValue(input[0].files[0]);
                scope.$apply(function () {
                    var files = e.target.files;
                    if (files[0]) {
                        scope.fileName = files[0].name;
                    } else {
                        scope.fileName = null;
                    }
                });
            });
        }
    };
}]);