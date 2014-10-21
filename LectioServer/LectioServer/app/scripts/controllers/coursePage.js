/**
 * Created by Elias on 10/18/2014.
 */
angular.module('helloApp')
    .controller('CoursePageCtrl', function ($scope) {
        $scope.awesomeThings = [
            'HTML5 Boilerplate',
            'AngularJS',
            'Karma'
        ];
        $scope.openVideoModal = function () {
            $("#VideoModal").modal('show');

        }
        $scope.openUploadModal = function () {
            $("#UploadModal").modal('show');

        }
    });