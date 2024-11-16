const $async = ['$rootScope', '$log', ($rootScope, $log) => {
    "use strict";
    return cb => {
        const validArgument = (typeof cb === 'function');

        const wrapper = async function (...args) {
            try {
                await cb(...args);
            } catch (e) {
                $log.error(e);
            } finally {
                $rootScope.$digest();
            }
        };

        if (!validArgument) {
            $log.error(`$async expects a function argument, got ${typeof cb}`);
        }

        return validArgument ? wrapper : () => {/* noop */ }
    };
}];

angular.module('angular-async-await', []).factory('$async', $async)

const appLogin = angular.module('myAppLogin', ['ngSanitize', 'angular-async-await', 'ngRoute']);

const addController = (name, controller) => {
    try {
        appLogin.controller(name, controller);
    } catch (e) {
        console.log(JSON.stringify(e));
    }
}

const addDirective = function (name, directive) {
    try {
        appLogin.directive(name, directive);
    } catch (e) {
        console.log(JSON.stringify(e));
    }
}

const addFilter = function (name, filter) {
    try {
        appLogin.filter(name, filter);
    } catch (e) {
        console.log(JSON.stringify(e));
    }
}

const addFactory = (name, factory) => {
    try {
        appLogin.factory(name, factory);
    } catch (e) {
        console.log(JSON.stringify(e));
    }
}

addFilter("dateFormat", dateFormat);
addFilter("cDate", cDate);
