angular.module('helloApp')
    .controller('VideoCtrl', function ($scope, server, $location) {
         $scope.Comments = [
             {
               comment:
               {
                    desc:"This looks awesome!",
                    name:"Ian Jones"
               }

             },
             {
                 comment:
                 {
                     desc:"Meh",
                     name:"Jordanne Perry"
                 }

             },
             {
                 comment:
                 {
                     desc:"Not Bad",
                     name:"Will Czifro"
                 }

             }

         ]


        $scope.addComment = function (item) {
            item.comment.name = "Elias Nichols";
            $scope.Comments.push(item);
        }

    });