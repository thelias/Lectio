/**
 * Created by Elias on 10/13/2014.
 */
angular.module('helloApp')
    .controller('CoursesCtrl', function ($scope, $http) {
        $scope.Courses = [
            {
                Course:
                {
                    name:'MUSIC 101',
                    instructor: 'Ed Walker'
                }


            } ,
            {
                Course:
                {
                    name:'MUSIC 203',
                    instructor: 'pete talker'
                }
            }
        ];
        $scope.openAddCourseModal = function () {
            $("#AddCourseModal").modal('show');
        }
        $scope.addLecture = function() {
        };


    });