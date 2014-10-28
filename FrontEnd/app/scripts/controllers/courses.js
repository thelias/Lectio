/**
 * Created by Elias on 10/13/2014.
 */
angular.module('helloApp')
    .controller('CoursesCtrl', function ($scope, $location, server) {

        //    $scope.Courses = [
        //    {
        //        Course:
        //        {
        //            name:'MC 101',
        //            desc:'A general music class involving stuff',
        //            instructor: 'Ed Walker'
        //        }


        //    } ,
        //    {
        //        Course:
        //        {
        //            name:'MC 203',
        //            desc:'A general music class involving stuff',
        //            instructor: 'Pete Tucker'
        //        }
        //    },
        //    {
        //        Course:
        //        {
        //            name:'CS 171',
        //            desc:'A general music class involving stuff',
        //            instructor: 'Susan Mabery'
        //        }


        //    } ,
        //    {
        //        Course:
        //        {
        //            name:'CS 272',
        //            desc:'A general music class involving stuff',
        //            instructor: 'Kent Jones'
        //        }
        //    },
        //    {
        //        Course:
        //        {
        //            name:'CS 374',
        //            desc:'A general music class involving stuff',
        //            instructor: 'Pete Tucker'
        //        }


        //    } ,
        //    {
        //        Course:
        //        {
        //            name:'CS 403',
        //            desc:'A general music class involving stuff',
        //            instructor: 'ED Walker'
        //        }
        //    }
        //];

        $scope.Courses = [];

        $scope.openAddCourseModal = function () {
            $("#AddCourseModal").modal('show');
        }
        $scope.init = function() {
            $scope.getLectures();
            if($location.path() == '/courses')  {
                $("#courses").addClass('active');
                $("#home").attr('class', '');
                $("#about").attr('class', '');
            }
        }
        $scope.getLectures = function() {
            server.getLectures().then(function(lectures) {
                angular.forEach(lectures, function(lecture, key) {
                    var item = {
                        Course: {
                            name: '',
                            desc: '',
                            instructor: '',
                            id: ""
                        }
                    };
                    item.Course.name = lecture.LectureName;
                    item.Course.desc = '';
                    item.Course.id= lecture.LectureId
                    item.Course.instructor = '';
                    $scope.Courses.push(item);
                });
            });
        }
        $scope.addCourse = function(item){
            //$scope.Courses.push(item);
            server.addLecture(item.Course.name).then(function (data) {
                item.Course.desc = '';
                item.Course.instructor = '';
                $scope.Courses.push(item);
            });
        }
        $scope.goHere = function(id) {
            $location.path("/course-page/id="+id);
        }


    });