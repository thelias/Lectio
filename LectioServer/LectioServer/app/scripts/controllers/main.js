'use strict';

/**
 * @ngdoc function
 * @name helloApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the helloApp
 */
angular.module('helloApp')
  .controller('MainCtrl', function ($scope) {
      $scope.awesomeThings = [
        'HTML5 Boilerplate',
        'AngularJS',
        'Karma'
      ];
      $scope.openSignUpModal = function () {
          $("#SignUpModal").modal('show');
      }
      $scope.openLoginModal = function () {
          $("#LoginModal").modal('show');
      }
      $scope.closeVideo = function () {
          $("#vid").remove();
          $("#skip").remove();
          $(".header").css("opacity", 1);
      }
  });
