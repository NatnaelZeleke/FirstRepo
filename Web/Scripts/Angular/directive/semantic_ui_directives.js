
angular.module("app.directives")
    .directive('wdrop', function() {
        return {
            restrict: 'A',
            link:function (scope, element, attr) {
               $('.ui.accordion')
                .accordion();
            }
        }
    });


angular.module("app.directives")
    .directive( 'wpop', function(){
        return{
            restrict: 'A',
            link: function(scope, elem, attrs) {
                $('.wpop').popup({inline: true,
                    hoverable: true,
                    position: 'top center',
                    transition: 'fade up'});
            }
        }
    });

angular.module("app.directives")
    .directive( 'dd', function(){
        return{
            restrict: 'A',
            link: function(scope, elem, attrs) {
                $('.ui.dropdown').dropdown();
            }
        }
    });