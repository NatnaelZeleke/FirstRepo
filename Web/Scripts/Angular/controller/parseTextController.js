/**
 * Created by Natnael Zeleke on 5/26/2017.
 * This controller is for controlling the validation controller
 */

angular.module("app.controllers")
    .controller("validationController", function ($scope,
                                                  $http,
                                                  urlConstants,
                                                  fileSender) {


        $scope.status = "Working";
        $scope.sendingDocumentData = false;
        $scope.parsedText = "";
        $scope.lang = 1;
        //region for controlling the image and the data
        var postImage;
        var output;
        var validationResult = $("#validationResult");
        $scope.validationResult = 0;
        $scope.validationDocumentUrl = "";
        $scope.showValidationResult = false;

        $scope.isSendingDocumentData = function () {
            return $scope.sendingDocumentData === true ? "active":"";
        };

        $scope.postImageChanged = function (element) {
            postImage = element.files[0];
            output = document.getElementById("imgPreview");
            output.src = URL.createObjectURL(element.files[0]);
        };


        $scope.sendDocument = function () {
            $scope.sendingDocumentData = true;
            var document = new FormData();
            document.append("File", postImage);
            document.append("Lang",$scope.lang);
            fileSender.send(urlConstants.ParseText,"POST",document)
                .then(function (response) {
                    console.log(response);
                    $scope.parsedText = response;
                    $scope.sendingDocumentData = false;
                },function (response) {
                    $scope.parsedText = "Error happened";
                    $scope.sendingDocumentData = false;
                }).finally(function () {

            });
        };

 


    });
