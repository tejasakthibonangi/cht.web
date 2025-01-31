function AccountController() {
    var self = this;
    var requests = [];
    var actions = [];
    var dataObjects = [];
    self.dbActivities = [];
    self.dbFeatures = [];
    self.dbPermissions = [];
    actions.push(API_URLS.FetchRolePermissionsAsync);
    actions.push(API_URLS.FetchFeaturesAsync);
    actions.push(API_URLS.FetchActivitiesAsync);
    actions.push(API_URLS.FetchTenantDetailsAsync);
    self.userResponceData = {};
    self.init = function () {
        var form = $('#formAuthentication');
        var signUpButton = $('#btnSubmit');
        form.on('input', 'input, select, textarea', checkFormValidity);
        checkFormValidity();
        function checkFormValidity() {
            if (form[0].checkValidity()) {
                signUpButton.prop('disabled', false);
            } else {
                signUpButton.prop('disabled', true);
            }
        }
        $(document).on("click", "#btnSubmit", function (e) {
            e.preventDefault();
            $(".se-pre-con").show();

            var userAuthetnication = {
                username: $("#username").val(),
                password: $("#password").val()
            };

            makeAjaxRequest({
                url: API_URLS.AuthenticateAsync,
                data: userAuthetnication,
                type: 'POST',
                successCallback: handleAuthenticationSuccess,
                errorCallback: handleAuthenticationError
            });
        });

        function handleAuthenticationSuccess(response) {
            console.info(response);
            if (response.status) {
                var appUserInfo = storageService.get('ApplicationUser');
                if (appUserInfo) {
                    storageService.remove('ApplicationUser');
                }

                var applicationUser = response.appUser;
                storageService.set('ApplicationUser', applicationUser);

                if (response.appUser.RoleId) {
                    dataObjects.push({ roleId: response.appUser.RoleId });
                }
                if (response.appUser.TenantId) {
                    dataObjects.push({ tenantId: response.appUser.TenantId });
                }
                var requests = actions.map((action, index) => {
                    var ajaxConfig = {
                        url: action,
                        method: 'GET'
                    };
                    if (index === 0) {
                        ajaxConfig.data = dataObjects[0];
                    }
                    if (index === 3) {
                        ajaxConfig.data = dataObjects[1];
                    }
                    return $.ajax(ajaxConfig);
                });
                $(".se-pre-con").show();
                $.when.apply($, requests).done(handlePermissionsSuccess).fail(handlePermissionsError);
            }
        }

        function handleAuthenticationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
        }

        function handlePermissionsSuccess() {
            $(".se-pre-con").show();
            var responses = arguments;
            var dbPermissions = responses[0][0].data;
            var dbFeatures = responses[1][0].data;
            var dbActivities = responses[2][0].data;
            var dbTenant = responses[3][0].data;

            var permissions = preparePermissions(dbFeatures, dbPermissions, dbActivities);
            console.log(permissions);
            var userPermissions = storageService.get('UserPermissions');
            if (userPermissions) {
                storageService.remove('UserPermissions');
            }
            storageService.set('UserPermissions', permissions);

            var userTenant = storageService.get('UserTenant');
            if (userTenant) {
                storageService.remove('UserTenant');
            }
            storageService.set('UserTenant', dbTenant);

            updateEnvironmentAndVersion();

            var appUserInfo = storageService.get('ApplicationUser');
            if (appUserInfo) {
                if (appUserInfo.TenantId && appUserInfo.DealerId) {
                    window.location.href = "/DealerDashBoard/Index";
                } else if (appUserInfo.TenantId) {
                    window.location.href = "/TenantDashBoard/Index";
                } else {
                    window.location.href = "/Home/Index";
                }
            }
            $(".se-pre-con").hide();
        }

        function handlePermissionsError() {
            console.log('One or more requests failed.');
            $(".se-pre-con").hide();
        }

        function preparePermissions(features, permissions, activities) {
            var activityMap = {};
            if (activities) {
                activities.forEach(activity => {
                    activityMap[activity.ActivityId] = activity;
                });

                return features.map(feature => ({
                    FeatureId: feature.FeatureId,
                    FeatureName: feature.Code,
                    Activities: permissions
                        .filter(permission => permission.FeatureId === feature.FeatureId)
                        .map(permission => {
                            var activity = activityMap[permission.ActivityId];
                            return {
                                PermissionId: permission.PermissionId,
                                RoleId: permission.RoleId,
                                IsEnabled: permission.IsEnabled,
                                IsDisabled: permission.IsDisabled,
                                ActivityId: activity.ActivityId,
                                ActivityName: activity.Name
                            };
                        })
                }));
            }
            return [];
        }

        function updateEnvironmentAndVersion() {
            var environment = storageService.get('Environment');
            if (environment) {
                storageService.remove('Environment');
            }
            storageService.set('Environment', window.location.hostname);

            var version = storageService.get('Version');
            if (version) {
                storageService.remove('Version');
            }
            storageService.set('Version', '1.0.0.0');
        }
    };
}