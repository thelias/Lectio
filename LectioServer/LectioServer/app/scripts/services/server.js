(function() {
    var serviceID = "server";
    angular.module('helloApp').factory(serviceID, ['$http', datacontext]);
    function datacontext($http) {
        var service =
        {
            addLecture: addLecture

        }
        return service;
        function addLecture() {
            var innerconfig = {
                url: "/api/v1/lectures/GetLecture",
                method: "GET",
                headers: {
                    'Accept': 'text/json'
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed)
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function requestFailed(error) {
            alert(error);
        }
    }
})();