(function () {
    var serviceID = "server";
    angular.module('helloApp').factory(serviceID, ['$http', '$rootScope', datacontext]);
    function datacontext($http, $rootScope) {
        var baseUrl = "http://lectioserver.azurewebsites.net";
        var service =
        {
            login: login,
            register: registerinstructor,
            confirmation: confirmation,
            getLectures: getLectures,
            getLecture: getLecture,
            getVideos: getVideos,
            getComments: getComments,
            addLecture: addLecture,
            addComment: addComment,
            addVideo: uploadVideo,
            testEndpoint: testEndpoint
        }
        return service;
         //Every onsuccess function within the queries is a successful query callback, telling the javascript what exactly to do if the request is successful
        //Login query
        function login(username, password) {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/login",
                data: $.param({
                    username: username,
                    password: password,
                    'grant_type': "password"
                }),
                method: "POST",
                headers: {
                    'Accept': 'text/json',
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            };
            return $http(innerconfig).then(onSuccess, requestFailed);
            //success callback
            function onSuccess(results) {
                if (results && results.data) {
                    $rootScope.access_token = results.data.access_token;
                    $rootScope.token_type = results.data.token_type;
                    $rootScope.userid = results.data.id;
                    return results.data;
                }
                return null;
            }
        }
        //testing query used for testing server
        function testEndpoint() {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/testEndpoint",
                method: "GET",
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
        //registering an instructor post query
        function registerinstructor(fname, lname, username, email) {
            var innerconfig = {
                url: baseUrl + "/api/v1/accounts/instructorregistration",
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

        function registerstudent(fname, lname, username, email) {
            //todo: finish
        }
         //authenticating user
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
        //get all lectures the user is associated with
        function getLectures() {
            var innerconfig =
            {
                url: baseUrl + "/api/v1/lectures/getLectures",
                method: "GET",
                params: {
                    pg: 0,
                    num: 5
                },
                headers:
                {
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
          //get specific lecture.
        function getLecture(lectureid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/lectures/getlecture",
                params: {
                    lectureid: lectureid
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json',
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
        //add a lecture
        function addLecture(lecturename) {
            var innerconfig = {
                url: baseUrl + "/api/v1/lectures/addLecture",
                data: {
                    lecturename: lecturename
                },
                method: "POST",
                headers: {
                    'Accept': 'text/json',
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
         //get videos associated with lecture
        function getVideos(lectureid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/videos/GetVideos",
                params: {
                    lectureid: lectureid
                },
                method: "GET",
                headers: {
                    'Accept': 'text/json',
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
                //get specific video
        function getVideo(videoid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/videos/getVideo",
                params: {
                    videoid: videoid
                },
                method: "GET",
                headers: {
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
             //upload          a video
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
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
                              //get comments from video
        function getComments(videoid) {
            var innerconfig = {
                url: baseUrl + "/api/v1/comments/getComments",
                params: {
                    videoid: videoid
                },
                method: "GET",
                headers: {
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
                      //add a comment
        function addComment(comment) {
            var innerconfig = {
                url: baseUrl + "/api/v1/addComment",
                data: {
                    commenttext: comment
                },
                method: "POST",
                headers: {
                    'Authorization': $rootScope.token_type + ' ' + $rootScope.access_token
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
                       //deals with failed requests
        function requestFailed(error, error_description) {
            console.log(error)
        }
    }
})();