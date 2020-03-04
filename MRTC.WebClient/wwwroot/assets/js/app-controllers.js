app.controller('appController', ['$scope', '$timeout', '$mdDialog', 'apiServer', function ($scope, $timeout, $mdDialog, apiServer) {
    
    $scope.rows = [];
    $scope.output = {};
    $scope.requestInProgress = false;

    var roverImgUrl = '/assets/images/rover.png';
    
    function renderGrid (plateau) {
        
        if (!angular.isObject(plateau)) {
            return;
        }

        $timeout(function () {
            
            $scope.rows = [];
        
            for (var y = plateau.maximumYCoordinate; y >= plateau.minimumYCoordinate; y--) {
                
                var row = { y: y, columns: [] };
                
                for (var x = plateau.minimumXCoordinate; x <= plateau.maximumXCoordinate; x++) {
                    row.columns.push({ y: y, x: x });
                }
                
                $scope.rows.push(row);
            }
        });
    }

    function renderRovers (rovers) {
        
        if (!angular.isArray(rovers)) {
            return;
        }

        $timeout(function () {
            
            var roverNumber = 0;

            rovers.forEach(function (rover) {
                
                roverNumber++;

                // compose rover elements

                var $initialRover = angular
                .element('<img/>')
                .attr('title', 'Rover ' + roverNumber + ' (initial position)')
                .attr('src', roverImgUrl)
                .attr('class', 'rover rover-initial rover-' + rover.initialHeading);
                
                var $finalRover = angular
                .element('<img/>')
                .attr('title', 'Rover ' + roverNumber + ' (final position)')
                .attr('src', roverImgUrl)
                .attr('class', 'rover rover-' + rover.finalHeading);
                
                // get rover destination cells

                var $initialCell = angular.element('.cell-' + rover.initialXCoordinate + '-' + rover.initialYCoordinate);
                var $finalCell = angular.element('.cell-' + rover.finalXCoordinate + '-' + rover.finalYCoordinate);

                // add rovers to cells

                $initialCell.append($initialRover);
                $finalCell.append($finalRover);
            });

        });
    }

    function renderAll () {
        
        if (!angular.isObject($scope.output)) {
            return;
        }
        
        renderGrid($scope.output.plateau);
        renderRovers($scope.output.rovers);
    };

    $scope.sendFile = function ($event) {
        $scope.requestInProgress = true;
        apiServer.post('/fileInput', {
            file: $scope.fileInput
        }).then(function (output) {
            $scope.output = output;
            renderAll();
        }, function (error) {
            $scope.output = {};
            $mdDialog.show(
                $mdDialog.alert()
                .title('Error')
                .textContent(error)
                .ok('OK')
            );
        }).finally(function() {
            $scope.requestInProgress = false;
        });
    };

    $scope.sendText = function ($event) {
        $scope.requestInProgress = true;
        apiServer.post('/textInput', {
            text: $scope.textInput
        }).then(function (output) {
            $scope.output = output;
            renderAll();
        }, function (error) {
            $scope.output = {};
            $mdDialog.show(
                $mdDialog.alert()
                .title('Error')
                .textContent(error)
                .ok('OK')
            );
        }).finally(function() {
            $scope.requestInProgress = false;
        });
    };

}]);