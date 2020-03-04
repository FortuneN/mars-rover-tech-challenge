app.service('apiServer', ['$q', '$http', 'appConfig', function ($q, $http, appConfig) {
    
    var $this = this,
        apiUrl = (appConfig.apiUrl || '').trim(),
        methods = ['post'/*, 'get', 'put', 'delete', 'head', 'connect', 'options', 'trace', 'patch'*/];

    if (!apiUrl || !angular.isString(apiUrl)) {
        throw "String 'appConfig.apiUrl' is required";
    }

    if (apiUrl[apiUrl.length - 1] === '/') {
        apiUrl = apiUrl.substr(0, apiUrl.length - 1);
    }
    
    $this.post = function (url, parameters) {
        return this.request({
            url: url,
            method: 'post',
            parameters: parameters
        });
    };

    $this.request = function (config) {
        
        // config validation

        if (!angular.isObject(config)) {
            throw "Object 'config' is required";
        }

        if (!config.url || !angular.isString(config.url)) {
            throw "String 'config.url' is required";
        }

        if (config.url[0] !== '/') {
            throw "Invalid  'config.url'. Url must start with '/'";
        }

        if (!config.method || !angular.isString(config.method)) {
            throw "String 'config.method' is required";
        }

        if (methods.indexOf(config.method) === -1) {
            throw "Invalid 'config.url'. Valid options are [" + methods.join(', ') + "]";
        }

        if (!angular.isObject(config.parameters)) {
            config.parameters = {};
        }

        // prepare request

        var parameters = {},
            formData = new FormData(),
            transformRequest = undefined,
            $httpMethod = $http[config.method],
            requestUrl = apiUrl + config.url,
            headers = { 'Accept': 'application/json' };
        
        Object.getOwnPropertyNames(config.parameters).forEach(function (name) {
            
            var value = config.parameters[name];

            if (value === null || value === undefined) {
                return;
            }
            
            if (value instanceof File || value instanceof Blob) {
                formData.append(name, value);
            } else {
                formData.append(name, angular.isObject(value) ? JSON.stringify(value) : value);
            }
        });

        if (config.method !== 'get') {
            parameters = formData;
            headers['Content-Type'] = undefined;
            transformRequest = angular.identity;
        } else {
            parameters = JSON.stringify(config.parameters);
            headers['Content-Type'] = 'application/json';
            transformRequest = undefined;
        }

        // execute request

        return $q(function (resolve, reject) {
            return $httpMethod(requestUrl, parameters, {
				
                headers: headers,
                cache: config.cache,
                transformRequest: transformRequest
                
            }).then(function (response) {
                
                return resolve(response.data);

            }, function (response) {
                
                var message = null,
                    statusTextFormated = response.statusText ? "[" + response.statusText + "] " : "";

                if (response.status === -1) {
                    message = statusTextFormated + "Failed to connect to api server";
                }
                else if (response.status === 404) {
                    message = statusTextFormated + "Server endpoint not found '" + config.url + "'";
                }
                else if (response.status === 403) {
                    message = statusTextFormated + "You are not allowed to perfom this action";
                }
                else if (response.data && angular.isString(response.data)) {
                    message = response.data.trim();
                }
                
                if (!message) {
                    message = response.statusText || 'Unknown error';
                }
                
                return reject(message);
            });
        });
    };

}]);