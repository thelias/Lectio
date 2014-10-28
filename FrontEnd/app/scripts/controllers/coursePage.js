/**
 * Created by Elias on 10/18/2014.
 */
angular.module('helloApp')
    .controller('CoursePageCtrl', function ($scope, $location, server) {
        $scope.Videos = [
            {
                video:
                {
                    name: "Nexus"
                }

            }
        ];
        $scope.id = 0;
        $scope.awesomeThings = [
            'HTML5 Boilerplate',
            'AngularJS',
            'Karma'
        ];
        $scope.init = function () {
            var id = $location.path().split('=');
            $scope.getVideos(id[1]);
            $scope.id = id[1];

        }

        $scope.openUploadModal = function () {
            $("#UploadModal").modal('show');

        }
        $scope.addVideo = function (item) {
            server.addVideo(item.video.file, $scope.id).then(function (data) {
                $scope.Videos.push(item);
            });
        }
        $scope.goHere = function () {
            $location.path("/video")
        }
        $scope.getVideos = function (id) {

            server.getVideos(id).then(function(videos) {
                angular.forEach(videos, function(video, key) {
                    var item = {
                        Video: {
                            name: '',
                            id: ""
                        }
                    };
                    item.Video.name = video.VideoName;
                    item.Video.id= video.VideoId
                    $scope.Videos.push(item);
                });
            })
        }
    });