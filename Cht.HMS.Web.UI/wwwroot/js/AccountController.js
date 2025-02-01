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

                var appUserInfo = storageService.get('ApplicationUser');
                if (appUserInfo) {
                    if (appUserInfo.RoleName === "Doctor") {
                        window.location.href = "/DoctorDashboard/Index";
                    } else if (appUserInfo.RoleName === "Executive") {
                        window.location.href = "/ExecutiveDashboard/Index";
                    }
                    else if (appUserInfo.RoleName === "Pharmacist") {
                        window.location.href = "/PharmacistDashBoard/Index";
                    }
                    else if (appUserInfo.RoleName === "Lab technicians") {
                        window.location.href = "/LabTechnicianDashBoard/Index";
                    } else {

                        window.location.href = "/Home/Index";
                    }
                }
                updateEnvironmentAndVersion();

                $(".se-pre-con").hide();
            }
        }

        function handleAuthenticationError(xhr, status, error) {
            console.error("Error in upserting data to server: " + error);
            $(".se-pre-con").hide();
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