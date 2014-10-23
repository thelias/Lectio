'use strict';

/**
 * @ngdoc function
 * @name helloApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the helloApp
 */
angular.module('helloApp')
  .controller('MainCtrl', function ($scope, server, $location) {

      $scope.awesomeThings = [
          {

          }
    ];
        $scope.openSignUpModal = function(){
                $("#SignUpModal").modal('show');
        }
        $scope.openLoginModal = function(){
            $("#LoginModal").modal('show');
        }
        $scope.closeVideo = function()
        {
            $("#vid").remove();
            $("#skip").remove();
            $(".header").css("opacity", 1);
            $(".jumbotron").css("opacity", 1);

        };
        $scope.login = function(){
              $('#signUpBut').remove()
              $('#loginBut').remove()
              $('#welcome').text('Welcome Elias Nichols')
              $('#hom1').text('Home')
             $('#courses').css('opacity',1);


        }
  });
