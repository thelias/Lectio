(function () {
    var serviceID = "server";
    angular.module('helloApp').factory(serviceID, ['$http', datacontext]);
    function datacontext($http, $rootScope) {
        var baseUrl = "http://lectioserver.azurewebsites.net";

        var service =
        {
            addLecture: addLecture,
            login: login,
            register: register,
            confirmation: confirmation,
            getLectures: getLectures,
            getLecture: getLecture

        }
        return service;


        function login(username, password) {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/login",
                data: {
                    username: username,
                    password: password,
                    grant_type: "password"
                },
                method: "POST",
                headers:
                {
                    'Accept': 'text/json'
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    $rootScope.access_token = results.data.access_token;
                    return results.data;
                }
                return null;
            }
        }

        function register(fname, lname, username, email) {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/register",
                data: {
                    firstname: fname,
                    lastname: lname,
                    username: username,
                    email: email
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json'
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function confirmation(userid, code, password, validate) {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/confirmation",
                data: {
                    userid: userid,
                    confirmationtoken: code,
                    password: password,
                    confirmpassword: validate
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json'
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function getLectures() {
            var innerconfig =
            {
                url: baseUrl + "/api/v1/lectures/getLectures",
                method: "GET",
                headers:
                {
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function getLecture(lectureid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/lectures/getlecture",
                param: {
                    lectureid: lectureid
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json',
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            }
        }
        function addLecture(lecturename) {
            var innerconfig = {
                url: "/api/v1/lectures/addLecture",
                data: {
                    lecturename: lecturename
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json',
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function getVideos(lectureid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/videos/getVideos",
                param: {
                    lectureid: lectureid
                },
                method: "GET",
                headers: {
                    'Accept': 'text/json',
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function getVideo(videoid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/videos/getVideo",
                param: {
                    videoid: videoid
                },
                method: "GET",
                headers: {
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function uploadVideo(file, lectureid) {
            var fd = new FormData();
            fd.append('file', file);
            var innerconfig = {
                url: baseUrl + "/api/v1/videos/uploadVideo",
                data: {
                    lectureid: lectureid
                },
                formData: fd,
                method: "POST",
                headers: {
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function getComments(videoid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/comments/getComments",
                param: {
                    videoid: videoid
                },
                method: "GET",
                headers: {
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function addComment(comment) {
            var innerconfig = {
                url: baseUrl + "/api/v1/addComment",
                data: {
                    commenttext: comment
                },
                method: "POST",
                headers: {
                    'Authentication': 'bearer ' + $rootScope.access_token
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            function onSuccess(results) {
                if (results && results.data) {
                    return results.data;
                }
                return null;
            }
        }

        function requestFailed(error) {
            alert(JSON.stringify(error));



        }
    }
})();