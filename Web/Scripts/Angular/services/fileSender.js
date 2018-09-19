/**
 * Created by Natnael Zeleke on 2/16/2018.
 */
angular.module("app.services")
    .factory("fileSender", function($http){
        return{
            send:function(URL,METHOD,DATA){
                return $http({
                    method:METHOD,
                    url:URL,
                    data: DATA,
                    transformRequest: angular.identity,
                    headers: {'Content-type': undefined}
                }).then( function(response){
                    return response.data;
                });
            }
        }
    });