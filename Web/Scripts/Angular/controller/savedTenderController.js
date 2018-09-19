/**
 * Created by Natnael Zeleke on 1/8/2018.
 */
angular.module("app.controllers")
    .controller("savedTenderController", function ($scope,
                                              $http,
                                              urlConstants,
                                              dataSender) {
        
        $scope.status = "Working";
        $scope.loading = false;
        $scope.sendingData = false;
        var post = "POST";
        $scope.message = "";
        $scope.tenders = [];
        $scope.data = {Skip:$scope.tenders.length,Top:3};
       
        
        
        $scope.getTenders = function () {
            $scope.loading = true;
            $scope.data.Skip = $scope.tenders.length;//update the amount of works to be skipeed
            dataSender.send(urlConstants.GetSavedTenders, post,$scope.data)
                .then(function (response) {
                    Array.prototype.push.apply($scope.tenders,response);
                    $scope.loading = false;
                    if (response.length === 0) {
                        if($scope.tenders.length !== 0){
                            $scope.message = "ጨረታዎችን ጭኖ ጨርሷል";
                        }
                        else if ($scope.tenders.length === 0) {
                            $scope.message = "ሴቭ ያደረጉት ጨረታ የለም";
                        }
                    }
                }, function (error) {
                    $scope.loading = false;
                });
        };

        //This will control the scroll controller
        //if the loader is getting information and is on progress don't ask to load more info again

        $(window).scroll(function () {
            if ($(document).height() <= $(window).scrollTop() + $(window).height() + 400) {
                if(!$scope.loading){
                    $scope.getTenders();
                    $scope.$apply();
                }
            }
        });

        $scope.isLoading = function() {
            return $scope.loading === true ? "loading" : "";
        };
        
        //on scroll call getworks method
        $scope.getTenders();
    });