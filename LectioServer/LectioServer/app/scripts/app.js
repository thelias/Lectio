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
        templateUrl: 'app/views/main.html',
        controller: 'MainCtrl'
      })
      .when('/about', {
        templateUrl: 'app/views/about.html',
        controller: 'AboutCtrl'
      }).when('/courses', {
            templateUrl: 'app/views/courses.html',
            controller: 'CoursesCtrl'
        }).when('/course-page', {
            templateUrl: 'app/views/course-page.html',
            controller: 'CoursePageCtrl'
        }).when()
      .otherwise({
        redirectTo: '/'
      });
  });
