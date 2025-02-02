function DynamicSideBarController() {
    var self = this;

    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        // Construct the navigation HTML
        var navHTML = `
            <li class="nav-item pcoded-menu-caption">
                <label>navigation</label>
            </li>
            <li class="nav-item">
                <a href="/Home/Index" class="nav-link">
                    <span class="pcoded-micon"><i class="feather icon-home"></i></span>
                    <span class="pcoded-mtext">Dashboard</span>
                </a>
            </li>
        `;

        // Check the user's role and append corresponding menu items
        if (appUserInfo) {
            switch (appUserInfo.RoleName) {
                case "Doctor":
                    navHTML += `
                        <li class="nav-item">
                            <a href="/DoctorDashboard/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-user-md"></i></span>
                                <span class="pcoded-mtext">Doctor Dashboard</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/DoctorAppointments/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-calendar"></i></span>
                                <span class="pcoded-mtext">Appointments</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/DoctorPrescriptions/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-file-text"></i></span>
                                <span class="pcoded-mtext">Prescriptions</span>
                            </a>
                        </li>
                    `;
                    break;

                case "Executive":
                    navHTML += `
                        <li class="nav-item">
                            <a href="/ExecutiveDashboard/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-briefcase"></i></span>
                                <span class="pcoded-mtext">Executive Dashboard</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/ExecutiveReports/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-file-text"></i></span>
                                <span class="pcoded-mtext">Reports</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/ExecutiveAnalytics/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-pie-chart"></i></span>
                                <span class="pcoded-mtext">Analytics</span>
                            </a>
                        </li>
                    `;
                    break;

                case "Pharmacist":
                    navHTML += `
                        <li class="nav-item">
                            <a href="/PharmacistDashBoard/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-capsule"></i></span>
                                <span class="pcoded-mtext">Pharmacist Dashboard</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/PharmacistInventory/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-box"></i></span>
                                <span class="pcoded-mtext">Inventory</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/PharmacistOrders/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-truck"></i></span>
                                <span class="pcoded-mtext">Orders</span>
                            </a>
                        </li>
                    `;
                    break;

                case "Lab technicians":
                    navHTML += `
                        <li class="nav-item">
                            <a href="/LabTechnicianDashBoard/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-flask"></i></span>
                                <span class="pcoded-mtext">Lab Technician Dashboard</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/LabTechnicianTests/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-test-tube"></i></span>
                                <span class="pcoded-mtext">Tests</span>
                            </a>
                        </li>
                        <li class="nav-item">
                            <a href="/LabTechnicianReports/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-file-text"></i></span>
                                <span class="pcoded-mtext">Reports</span>
                            </a>
                        </li>
                    `;
                    break;

                default:
                    navHTML += `
                        <li class="nav-item">
                            <a href="/Home/Index" class="nav-link">
                                <span class="pcoded-micon"><i class="feather icon-home"></i></span>
                                <span class="pcoded-mtext">Home</span>
                            </a>
                        </li>
                    `;
            }
        }

        // Insert the constructed HTML into the #dynamic-nav container
        $('#dynamic-nav').html(navHTML);
    };
}
