/**
 * Created by Elias on 10/18/2014.
 */
angular.module('helloApp')
    .controller('CoursePageCtrl', function ($scope, $location) {
        $scope.Videos = [
            {
                video: {
                    name: "Nexus",
                    date: "10/14/2014"

                }
            }
        ];
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
        $scope.addVideo = function (item) {
            $scope.Videos.push(item);
        }
        $scope.goHere = function(){
            $location.path("/video")
        }
    });