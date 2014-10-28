'use strict';

/**
 * @ngdoc overview
 * @name helloApp
 * @description
 * # helloApp
 *
 * Main module of the application.
 */
angular
  .module('helloApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl'
      }).when('/courses', {
            templateUrl: 'views/courses.html',
            controller: 'CoursesCtrl'
        }).when('/course-page/:id', {
            templateUrl: 'views/course-page.html',
            controller: 'CoursePageCtrl'
        }).when('/video', {
            templateUrl: 'views/video.html',
            controller: 'VideoCtrl'
        }).when()
      .otherwise({
        redirectTo: '/'
      });
  });
