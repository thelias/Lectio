'use strict';

/**
 * @ngdoc function
 * @name helloApp.controller:AboutCtrl
 * @description
 * # AboutCtrl
 * Controller of the helloApp
 */
angular.module('helloApp')
  .controller('AboutCtrl', function ($scope, $location) {
    $scope.awesomeThings = [
      'HTML5 Boilerplate',
      'AngularJS',
      'Karma'
    ];
        $scope.init = function(){

            if($location.path() == '/about')  {
                $("#about").addClass('active');
                $("#home").attr('class', '');
                $("#courses").attr('class', '');
            }
        }
  });
