

angular.module("app.services")
    .factory("searchService", function($http){
        return{
            searchService:function(URL,METHOD,DATA){
                return $http({
                    method:METHOD,
                    url:URL,
                    data: DATA,
                    headers: {'Content-type': 'application/json'},
                    responseType: "json"
                }).then(function (response) {
                    console.log(response);
                    return response.data;
                });
            }
        }
    });

angular.module("app.services")
    .factory("dataFetcher", function ($http) {
        return {
            getData: function (url,methodType) {
                return $http({
                    method: methodType,
                    url: url,
                    headers: {'Content-type': 'application/json'},
                    responseType: 'json'
                }).then(function (response) {
                    console.log(response);
                    return response.data;
                });
            }
        }
    });