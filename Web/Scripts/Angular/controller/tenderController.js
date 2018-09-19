/**
 * Created by Natnael Zeleke on 1/8/2018.
 */
angular.module("app.controllers")
    .controller("tenderController", function ($scope,
                                              $http,
                                              urlConstants,
                                              dataSender) {
        
        $scope.status = "Working";
        $scope.loading = false;
        $scope.sendingData = false;
        var post = "POST";
        $scope.message = "";
        $scope.tenders = [];
        $scope.data = {Skip:$scope.tenders.length,Top:6};
        var finsihedLoadingTenders = "ጨረታዎችን ጭኖ ጨርሷል "; 
        var activateYourAccountMessage = "በየቀኑ የሚወጡ ሁሉንም ጨረታዎች ለማየት አካውንቶን አክቲቬት ያስደርጉ። አካውንቶን አክቲቬት ለማስደረግ በ 0118931990 ወይም 0924144733 ላየ በመደወል መረጃ ያግኙ ።";
        
        $scope.getTenders = function () {
            $scope.loading = true;
            $scope.data.Skip = $scope.tenders.length;//update the amount of works to be skipeed
            dataSender.send(urlConstants.GetTenders, post,$scope.data)
                .then(function (response) {
                    
                    $scope.loading = false;
                    //check the status first
                    if (response.Status === 0) {//user is still active
                        Array.prototype.push.apply($scope.tenders, response.Tenders);
                        if (response.Tenders.length === 0) {
                            $scope.message = finsihedLoadingTenders;
                        }
                    }
                    else if (response.Status === 1) {
                        //user account has expired
                        if ($scope.tenders.length < 3) {
                            if (response.Tenders.length === 0) {
                                $scope.message = finsihedLoadingTenders + activateYourAccountMessage;
                            }
                                                        //only add three enders 
                            if (response.Tenders.length > 3) {
                                 Array.prototype.push.apply($scope.tenders, response.Tenders.slice(0,3));
                            } else {
                                Array.prototype.push.apply($scope.tenders, response.Tenders);
                            }
                            //
                                                       
                        } else {
                            $scope.message = activateYourAccountMessage;
                        }

                    } else {
                        //other errors
                        $scope.message = "ERROR HAPPENED : CODE " + response.Status;
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