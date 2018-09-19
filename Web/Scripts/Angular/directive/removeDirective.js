/**
 * This is a directive that will control the saving of
 */
angular.module("app.directives")
    .directive("remove", ['$http','dataSender',function ($http,dataSender) {

        return {
            scope:true,
            link:function (scope, element, attr) {
                element.bind( 'click', function () {


                    
                    var loading = "loading";
                    var displayNone = "dn";
                    var data = {"TenderId":attr.id};
                    var parent = element.parent().parent();
                    var mainParent = element.parent().parent().parent().parent();
                    parent.addClass("loading");
                    element.addClass(loading);


                    dataSender.send("/User/RemoveSavedTender","POST",data)
                        .then(function (response) {
                            if(response === 0){
                                //success
                                mainParent.addClass(displayNone);
                            }
                            else if(response === 1){
                                //failure
                                parent.removeClass(loading);
                            }
                        },function (response) {

                        });

                });
                scope.handleResponse = function (response) {
                    alert("handled responce");
                }

            }
        }
    }]);