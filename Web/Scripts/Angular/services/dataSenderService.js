/**
 * Created by Natnael Zeleke on 8/27/2016.
 * This is a service that will send raw data to the database
 */
angular.module("app.services")
    .factory("dataSender", function($http){
        return{
            send:function(URL,METHOD,DATA){
                return $http({
                    method:METHOD,
                    url:URL,
                    data: DATA,
                    headers: {'Content-type': 'application/json'},
                    responseType: "json"
                }).then(function (response) {
                    console.log(response.data);
                    return response.data;
                });
            }
        }
    });
