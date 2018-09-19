/**
 * Created by Natnael Zeleke on 2/26/2018.
 */

angular.module("app.controllers")
    .controller("reportController", function ($scope,
                                                    $http,
                                                    urlConstants,
                                                    dataFetcher) {


        $scope.allReport = {};
        var post = "POST";
        $scope.loading = true;

        dataFetcher.getData(urlConstants.GetAllReport, post)
            .then(function (fetchedData) {
                $scope.allReport = fetchedData;
                $scope.loading = false;
            }, function (error) {
                $scope.loading = false;
            });

        $scope.isLoading = function () {
            return $scope.loading?"active":"";
        }


    });