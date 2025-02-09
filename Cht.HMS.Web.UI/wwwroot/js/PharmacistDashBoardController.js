function PharmacistDashBoardController() {
    var self = this;

    var actions = [];

    self.dbMedicines = [];

    self.ApplicationUser = {};

    self.PharmacyOrders = [];
    self.init = function () {
        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        actions.push('/Medicine/GetMedicines');

        actions.push('/PharmacistDashBoard/GetPatientPharmacyOrder');

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function () {
            var responses = arguments;
            self.dbMedicines = responses[0][0].data ? responses[0][0].data : [];
            self.PharmacyOrders = responses[1][0].data ? responses[1][0].data : [];
            console.log(self.PharmacyOrders);
            self.PharmacyOrderGrid.replaceData(self.PharmacyOrders);
            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });
        self.PharmacyOrderGrid = new Tabulator("#PharmacyOrdersGrid", {
            height: "800px",
            layout: "fitColumns",
            resizableColumnFit: true,
            data: self.PharmacyOrders,
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
                { title: "Items Qty", field: "patientPharmacyOrder.ItemsQty" },
                { title: "Total Amount", field: "patientPharmacyOrder.TotalAmount" },
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

                    var pharmacyOrderDetails = patientData.patientPharmacyOrder.patientPharmacyOrderDetails || [];

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
                            <td><strong>Blood Pressure</strong></td>
                            <td>${patientData.BP || 'N/A'}</td>
                            <td><strong>Sugar Level</strong></td>
                            <td>${patientData.Sugar || 'N/A'}</td>
                        </tr>
                        <tr>
                            <td><strong>Temperature</strong></td>
                            <td>${patientData.Temperature || 'N/A'}</td>
                            <td><strong>Health Issues</strong></td>
                            <td>${patientData.HealthIssues || 'N/A'}</td>
                        </tr>
                        <tr>
                            <td><strong>Doctor</strong></td>
                            <td>${patientData.DoctorName || 'N/A'}</td>
                            <td><strong>Fee</strong></td>
                            <td>${patientData.Fee ? '$' + patientData.Fee.toFixed(2) : 'N/A'}</td>
                        </tr>
                        <tr>
                            <td><strong>Status</strong></td>
                            <td>${patientData.CurrentStatus || 'N/A'}</td>
                            <td><strong>Date of Visit</strong></td>
                            <td>${patientData.DateOfVisit ? new Date(patientData.DateOfVisit).toLocaleDateString() : 'N/A'}</td>
                        </tr>
                        <tr>
                            <td><strong>Time of Visit</strong></td>
                            <td>${patientData.TimeOfVisit ? patientData.TimeOfVisit.toString() : 'N/A'}</td>
                            <td><strong>Prepared By</strong></td>
                            <td>${patientData.PreparedBy || 'N/A'}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <br/>
            <h3>Medicines</h3>
               <div class="table-responsive">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th scope="col">Medicine Name</th>
                            <th scope="col">Dosage Form</th>
                            <th scope="col">Price/Unit</th>
                            <th scope="col">Qty</th>
                            <th scope="col">Total</th>
                        </tr>
                    </thead>
                    <tbody id="PatientMedicinesGrid">
        `;

                    // Loop through the pharmacy order details and create table rows
                    pharmacyOrderDetails.forEach(detail => {
                        // Find the medicine details from self.dbMedicines using MedicineId
                        var medicine = self.dbMedicines.find(med => med.MedicineId === detail.MedicineId);

                        // If medicine is found, use its details; otherwise, use 'N/A'
                        var medicineName = medicine ? medicine.MedicineName : 'N/A';
                        var dosageForm = medicine ? medicine.DosageForm : 'N/A';

                        content += `
                <tr>
                    <td>${medicineName}</td>
                    <td>${dosageForm}</td>
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