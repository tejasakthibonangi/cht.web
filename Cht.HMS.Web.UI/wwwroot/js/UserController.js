function UserController() {
    var self = this;
    self.coreDBRoles = [];
    self.coreDBTenants = [];
    self.coreDBDealers = [];
    self.ApplicationUser = {};
    var actions = [];
    var dataObjects = [];
    actions.push('/Role/FetchRoles');
    actions.push('/Tenant/fetchTenants');

    self.init = function () {
        var appuser = storageService.get("ApplicationUser");
        if (appuser) {
            self.ApplicationUser = appuser;
        }
        if (self.ApplicationUser.TenantId && !self.ApplicationUser.DealerId) {
            actions.push('/Dealer/GetDealer');
            dataObjects.push({ tenantId: self.ApplicationUser.TenantId });
        }

        self.usersgrid = new Tabulator("#usersgrid", {
            ajaxURL: '/User/FetchUsers',
            ajaxParams: {},
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                var userData = [];
                if (self.ApplicationUser.TenantId && self.ApplicationUser.DealerId) {
                    userData = response.data.filter(x => x.TenantId == self.ApplicationUser.TenantId && x.DealerId == self.ApplicationUser.DealerId);
                } else if (self.ApplicationUser.TenantId) {
                    userData = response.data.filter(x => x.TenantId == self.ApplicationUser.TenantId);
                } else {
                    userData = response.data;
                }
                return userData;
            },
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            columns: [
                { title: "Name", field: "FullName" },
                { title: "Email", field: "Email" },
                { title: "Phone", field: "Phone" },
                { title: "Tenant", field: "TenantName" },
                { title: "Dealer", field: "DealerName" },
                { title: "Role", field: "RoleName" },
                { title: "IsBlocked", field: "IsBlocked" },
                { title: "IsActive", field: "IsActive" },
            ]
        });


        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 2) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function (...responses) {
            self.coreDBRoles = responses[0][0]?.data || [];
            self.coreDBTenants = responses[1][0]?.data || [];
            if (self.ApplicationUser.TenantId && !self.ApplicationUser.DealerId)
                self.coreDBDealers = responses[2][0]?.data || [];

            var loggedInUserRole = self.coreDBRoles.find(role => role.Id === self.ApplicationUser.RoleId);
            if (loggedInUserRole) {
                var userRoles = self.filterRoles(loggedInUserRole.Name, self.coreDBRoles);
                genarateDropdown("RoleId", userRoles, "Id", "Name");
            }
            handleDealerAndTenantDropdown();
            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });

        self.filterRoles = function (loggedInRole, roles) {
            if (loggedInRole === 'Administrator') {
                return roles;
            }
            if (loggedInRole === 'System Administrator') {
                return roles.filter(role => !['Administrator', 'System Administrator', 'Tenant'].includes(role.Name));
            }
            if (loggedInRole === 'Dealer') {
                return roles.filter(role => !['Administrator', 'System Administrator', 'Tenant', 'Flagship Dealer'].includes(role.Name));
            }
            return [];
        };
        function handleDealerAndTenantDropdown() {
            const { TenantId, DealerId } = self.ApplicationUser;

            if (TenantId && !DealerId) {
                var dealers = self.coreDBDealers.filter(x => x.TenantId === TenantId);
                $("#dealerSection").removeClass("tenantOrDealerSection");
                genarateDropdown("DealerId", dealers, "DealerId", "DealerName");
                $('#DealerId').attr('required', true);
            } else if (TenantId && DealerId) {
                genarateDropdown("roledropdown", self.coreDBRoles, "Id", "Name");
            } else {
                $("#dealerSection,#tenantSection").removeClass("tenantOrDealerSection");
                genarateDropdown("TenantId", self.coreDBTenants, "TenantId", "TenantName");
                genarateDropdown("DealerId", self.coreDBDealers, "DealerId", "DealerName");
                $('#TenantId').attr('required', true);
            }
        }


        makeFormGeneric('#AddEditUserForm', '#btnsubmit');


        $(document).on("click", "#addBtn", function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });
        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditUserForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditUserForm').on('submit', function (e) {
            e.preventDefault();
            showLoader();
            var formData = getFormData('#AddEditUserForm');
            var userRegistration = addCommonProperties(formData);
            if (self.ApplicationUser.TenantId && self.ApplicationUser.DealerId) {
                userRegistration.TenantId = self.ApplicationUser.TenantId;
                userRegistration.DealerId = self.ApplicationUser.DealerId;
            } else if (self.ApplicationUser.TenantId) {
                userRegistration.TenantId = self.ApplicationUser.TenantId;
            }
            userRegistration.LastPasswordChangedOn = new Date();
            userRegistration.IsBlocked = false;
            userRegistration.Id = null;
            console.log(userRegistration);
            self.addeditUser(userRegistration);
        });
        self.addeditUser = function (userRegistration) {
            makeAjaxRequest({
                url: '/User/InsertOrUpdateUser',
                data: userRegistration,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        $('#AddEditUserForm')[0].reset();
                        $('#sidebar').removeClass('show');
                        $('.modal-backdrop').remove();
                        self.usersgrid.setData();
                    }
                    console.info(response);
                    hideLoader();
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in saving user data: " + error);
                }
            });
        };

    };

}