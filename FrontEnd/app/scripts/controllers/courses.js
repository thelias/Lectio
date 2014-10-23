/**
 * Created by Elias on 10/13/2014.
 */
angular.module('helloApp')
    .controller('CoursesCtrl', function ($scope, $location) {

            $scope.Courses = [
            {
                Course:
                {
                    name:'MC 101',
                    desc:'A general music class involving stuff',
                    instructor: 'Ed Walker'
                }


            } ,
            {
                Course:
                {
                    name:'MC 203',
                    desc:'A general music class involving stuff',
                    instructor: 'Pete Tucker'
                }
            },
            {
                Course:
                {
                    name:'CS 171',
                    desc:'A general music class involving stuff',
                    instructor: 'Susan Mabery'
                }


            } ,
            {
                Course:
                {
                    name:'CS 272',
                    desc:'A general music class involving stuff',
                    instructor: 'Kent Jones'
                }
            },
            {
                Course:
                {
                    name:'CS 374',
                    desc:'A general music class involving stuff',
                    instructor: 'Pete Tucker'
                }


            } ,
            {
                Course:
                {
                    name:'CS 403',
                    desc:'A general music class involving stuff',
                    instructor: 'ED Walker'
                }
            }
        ];
        $scope.openAddCourseModal = function () {
            $("#AddCourseModal").modal('show');
        }
        $scope.init = function(){

            if($location.path() == '/courses')  {
                $("#courses").addClass('active');
                $("#home").attr('class', '');
                $("#about").attr('class', '');
            }
        }
        $scope.addCourse = function(item){
            $scope.Courses.push(item);
        }
        $scope.goHere = function(){
            $location.path("/course-page")
        }


    });