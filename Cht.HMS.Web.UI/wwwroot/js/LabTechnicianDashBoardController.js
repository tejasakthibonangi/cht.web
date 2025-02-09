function LabTechnicianDashBoardController() {
    var self = this;

    var actions = [];

    self.dbLabTests = [];

    self.ApplicationUser = {};

    self.LabOrders = [];
    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        actions.push('/LabTest/GetLabTests');

        actions.push('/LabTechnicianDashBoard/GetLabOrders');
        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            self.dbLabTests = responses[0][0].data ? responses[0][0].data : [];
            self.LabOrders = responses[1][0].data ? responses[1][0].data : [];
            console.log(self.LabOrders);
            self.LabOrderGrid.replaceData(self.LabOrders);
            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });
        self.LabOrderGrid = new Tabulator("#LabOrdersGrid", {
            height: "800px",
            layout: "fitColumns",
            resizableColumnFit: true,
            data: self.LabOrders,
            columns: [
                {
                    title: "",
                    formatter: function () {
                        return "<i class='icon feather icon-mail'></i>";
                    },
                    width: 40,
                    hozAlign: "center",
                    headerSort: false
                },
                { title: "Patient Name", field: "PatientName" },
                { title: "Phone", field: "PhoneNo" },
                { title: "Doctor Name", field: "DoctorName" },
                { title: "Health Issues", field: "HealthIssues" },
                { title: "Items Qty", field: "patientLabOrder.ItemsQty" },
                { title: "Total Amount", field: "patientLabOrder.TotalAmount" },
                { title: "CurrentStatus", field: "CurrentStatus" },

            ],
            selectable: 1,
            rowClick: function (e, row) {
                if (row.getElement().classList.contains("expanded")) {
                    row.getElement().classList.remove("expanded");
                    row.getElement().querySelector(".expanded-row-content").remove();
                } else {
                    row.getElement().classList.add("expanded");

                    var holderEl = document.createElement("div");
                    holderEl.classList.add("expanded-row-content");

                    var patientData = row.getData();

                    // Assuming patientData.labOrderDetails contains the lab test details
                    var labOrderDetails = patientData.patientLabOrder.patientLabOrderDetails || [];

                    var content = `<div class="table-responsive">
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th>Field</th>
                    <th>Information</th>
                    <th>Field</th>
                    <th>Information</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><strong>Name</strong></td>
                    <td>${patientData.PatientName || 'N/A'}</td>
                    <td><strong>Phone No</strong></td>
                    <td>${patientData.PhoneNo || 'N/A'}</td>
                </tr>
                <tr>
                    <td><strong>Alternate Phone No</strong></td>
                    <td>${patientData.AlternatePhoneNo || 'N/A'}</td>
                    <td><strong>Email</strong></td>
                    <td>${patientData.Email || 'N/A'}</td>
                </tr>
                <tr>
                    <td><strong>Gender</strong></td>
                    <td>${patientData.Gender || 'N/A'}</td>
                    <td><strong>Date of Birth</strong></td>
                    <td>${patientData.DOB ? new Date(patientData.DOB).toLocaleDateString() : 'N/A'}</td>
                </tr>
                <tr>
                    <td><strong>Height</strong></td>
                    <td>${patientData.Height ? patientData.Height + ' cm' : 'N/A'}</td>
                    <td><strong>Weight</strong></td>
                    <td>${patientData.Weight ? patientData.Weight + ' kg' : 'N/A'}</td>
                </tr>
                <tr>
                    <td><strong>Doctor</strong></td>
                    <td>${patientData.DoctorName || 'N/A'}</td>
                    <td><strong>Status</strong></td>
                    <td>${patientData.CurrentStatus || 'N/A'}</td>
                </tr>
                <tr>
                    <td><strong>Date of Visit</strong></td>
                    <td>${patientData.DateOfVisit ? new Date(patientData.DateOfVisit).toLocaleDateString() : 'N/A'}</td>
                    <td><strong>Time of Visit</strong></td>
                    <td>${patientData.TimeOfVisit ? patientData.TimeOfVisit.toString() : 'N/A'}</td>
                </tr>
            </tbody>
        </table>
    </div>
    <br/>
    <h3>Lab Tests</h3>
       <div class="table-responsive">
        <table class="table table-bordered">
            <thead>
                <tr>
                    <th scope="col">Test Name</th>
                    <th scope="col">Test Type</th>
                    <th scope="col">Price/Unit</th>
                    <th scope="col">Qty</th>
                    <th scope="col">Total</th>
                </tr>
            </thead>
            <tbody id="PatientLabTestsGrid">
`;

                    // Loop through the lab order details and create table rows
                    labOrderDetails.forEach(detail => {
                        // Find the test details from self.dbTests using TestId
                        var test = self.dbLabTests.find(t => t.TestId === detail.TestId);

                        // If test is found, use its details; otherwise, use 'N/A'
                        var testName = test ? test.TestName : 'N/A';
                        var testType = test ? test.TestDescription : 'N/A';

                        content += `
        <tr>
            <td>${testName}</td>
            <td>${testType}</td>
            <td>${detail.PricePerUnit ? '$' + detail.PricePerUnit.toFixed(2) : 'N/A'}</td>
            <td>${detail.Quantity || 'N/A'}</td>
            <td>${detail.TotalPrice ? '$' + detail.TotalPrice.toFixed(2) : 'N/A'}</td>
        </tr>
    `;
                    });

                    // Close the table body and table
                    content += `
            </tbody>
        </table>
    </div>
`;

                    holderEl.innerHTML = content;
                    row.getElement().appendChild(holderEl);
                }
            }

        });
    };
}