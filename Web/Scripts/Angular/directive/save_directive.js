angular.module("app.directives")
    .directive("save", ['$http','dataSender',function ($http,dataSender) {

        return {
            scope: true,
            link: function(scope, element, attr) {
                element.bind('click',
                    function() {


                        var saved = "saved";
                        var loading = "loading";
                        var data = { "TenderId": attr.id };
                        if (element.hasClass(saved)) {
                            // element.removeClass(saved);
                        } else {

                            element.addClass(loading);
                            dataSender.send("/User/SaveTender", "POST", data)
                                .then(function(response) {
                                        console.log("got response");
                                        console.log(response);
                                        element.removeClass(loading);
                                        element.addClass(saved);
                                    },
                                    function(response) {
                                        console.log("failed");
                                        console.log(response);
                                    });
                        }


                    });
                scope.handleResponse = function(response) {
                    alert("handled responce");
                };

            }
        };
    }]);