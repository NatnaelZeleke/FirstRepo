angular.module("app.constants",[]);
angular.module("app.services",[]);
angular.module("app.controllers",["app.services","app.constants"]);
angular.module("app.directives",["app.services","app.constants"]);
angular.module("app", ["app.controllers", "app.services", "app.directives", "app.constants"])
    .controller("appController", function ($scope,$http) {
        
    });