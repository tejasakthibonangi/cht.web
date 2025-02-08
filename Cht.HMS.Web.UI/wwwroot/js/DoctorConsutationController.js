function DoctorConsutationController() {
    var self = this;
    self.patientId = null;
    var actions = [];
    var dataObjects = [];
    self.dbMedicines = [];
    self.dbLabTests = [];
    self.PatientInfirmation = {};
    self.PatientMedicines = [];
    self.PatientLabTests = [];

    self.ApplicationUser = {};
    self.init = function () {
        showLoader();
        self.patientId = getQueryStringParameter("patientId");
        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }
        actions.push('/Patient/GetPatientDetails');

        actions.push('/Medicine/GetMedicines');

        actions.push('/LabTest/GetLabTests');

        dataObjects.push({ patientId: self.patientId });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            if (index === 0) {
                ajaxConfig.data = dataObjects[0];
            }
            return $.ajax(ajaxConfig);
        });
        $.when.apply($, requests).done(function () {
            var responses = arguments;
            console.log(responses);
            self.PatientInfirmation = responses && responses[0][0].data ? responses[0][0].data : {};
            self.dbMedicines = responses && responses[1][0].data ? responses[1][0].data : [];
            self.dbLabTests = responses && responses[2][0].data ? responses[2][0].data : [];
            self.preparePatientInfirmation();
            var medicinesList = self.dbMedicines.map(function (medicine) {
                return {
                    MedicineId: medicine.MedicineId,
                    MedicineName: medicine.MedicineName + " - " + medicine.GenericName + " - " + medicine.DosageForm + " - " + medicine.Manufacturer + " - " + medicine.PricePerUnit
                };
            });

            genarateDropdown("medicineDropdown", medicinesList, "MedicineId", "MedicineName");

            genarateDropdown("labTestDropdown", self.dbLabTests, "TestId", "TestName");

            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
            hideLoader();
        });
        self.preparePatientInfirmation = function () {
            if (self.PatientInfirmation) {
                $("#patientName").text(self.PatientInfirmation.PatientName);
                $("#patientPhone").text(self.PatientInfirmation.PhoneNo);
                $("#doctorName").text(self.PatientInfirmation.DoctorName);
                $("#doctorPhone").text("+91-9848022338");
                $("#bp").text(self.PatientInfirmation.BP);
                $("#sugar").text(self.PatientInfirmation.Sugar);
                $("#temperature").text(self.PatientInfirmation.Temperature);
                $("#heightWeight").text(self.PatientInfirmation.Height + "/" + self.PatientInfirmation.Weight);
                $("#healthIssues").text(self.PatientInfirmation.HealthIssues);
            }
        }
        self.prepareMedicinesGrid = function () {
            var patientMedicinesGrid = $("#PatientMedicinesGrid");
            patientMedicinesGrid.html("");

            if (self.PatientMedicines.length > 0) {
                self.PatientMedicines.forEach(function (item) {
                    var trRow = $("<tr>" +
                        "<td>" + item.MedicineName + "</td>" +
                        "<td>" + item.DosageForm + "</td>" +
                        "<td>" + item.PricePerUnit.toFixed(2) + "</td>" +
                        "<td><input type='number' class='form-control qty-input' value='" + item.Qty + "' data-medicineId='" + item.MedicineId + "'></td>" +
                        "<td class='total-price'>" + item.Total.toFixed(2) + "</td>" +
                        "<td data-id='" + item.MedicineId + "'><a href='#' class='link-primary link-delete text-center' data-medicineId='" + item.MedicineId + "'><i class='fa fa-trash-o' style='color: red'></i></a></td>" +
                        "</tr>");
                    patientMedicinesGrid.append(trRow);
                });
            }
        };

        $(document).on("input", ".qty-input", function () {
            var qty = $(this).val();
            var medicineId = $(this).data("medicineId");
            var medicine = self.PatientMedicines.find(x => x.MedicineId == medicineId);

            if (medicine) {
                medicine.Qty = qty;
                medicine.Total = medicine.PricePerUnit * qty;
                $(this).closest("tr").find(".total-price").text(medicine.Total.toFixed(2));
            }
        });
        $(document).on("click", ".link-delete", function (e) {
            e.preventDefault();

            var medicineId = $(this).data("medicineid");

            self.PatientMedicines = self.PatientMedicines.filter(function (item) {
                return item.MedicineId !== medicineId;
            });

            self.prepareMedicinesGrid();
        });
        $(document).on("click", "#addMedicine", function () {
            var currentMedicineId = $("#medicineDropdown").val();
            var medicine = self.dbMedicines.filter(x => x.MedicineId == currentMedicineId)[0];


            var existingMedicine = self.PatientMedicines.find(x => x.MedicineId === medicine.MedicineId);

            if (existingMedicine) {
                existingMedicine.Qty += 1;
                existingMedicine.Total = existingMedicine.PricePerUnit * existingMedicine.Qty;
            } else {
                var patientMedicine = {
                    MedicineId: medicine.MedicineId,
                    MedicineName: medicine.MedicineName,
                    GenericName: medicine.GenericName,
                    DosageForm: medicine.DosageForm,
                    Manufacturer: medicine.Manufacturer,
                    PricePerUnit: medicine.PricePerUnit,
                    Qty: 1,
                    Total: medicine.PricePerUnit * 1
                };

                self.PatientMedicines.push(patientMedicine);
            }

            self.prepareMedicinesGrid();
        });

        $(document).on("click", "#addLabTest", function () {
            var currentTestId = $("#labTestDropdown").val();
            var labTest = self.dbLabTests.filter(x => x.TestId == currentTestId)[0];

            var existingLabTest = self.PatientLabTests.find(x => x.TestId === labTest.TestId);

            if (existingLabTest) {
                existingLabTest.Qty += 1;
                existingLabTest.Total = 250 * existingLabTest.Qty;
            } else {
                var patientLabTest = {
                    TestId: labTest.TestId,
                    TestName: labTest.TestName,
                    Price: 250,
                    Qty: 1,
                    Total: 250 * 1
                };

                self.PatientLabTests.push(patientLabTest);
            }

            self.prepareLabTestsGrid();
        });

        $(document).on("click", ".link-delete-lab-test", function (e) {
            e.preventDefault();

            var testId = $(this).data("testid");

            self.PatientLabTests = self.PatientLabTests.filter(function (item) {
                return item.TestId !== testId;
            });

            self.prepareLabTestsGrid();
        });

        self.prepareLabTestsGrid = function () {
            var grid = $("#PatientLabTestsGrid");
            grid.empty();

            self.PatientLabTests.forEach(function (labTest) {
                var row = `<tr>
                <td>${labTest.TestName}</td>
                <td>Description for ${labTest.TestName}</td>
                <td>${labTest.Price.toFixed(2)}</td>
                <td>
                    <input type="number" class="qty-input" data-testId="${labTest.TestId}" value="${labTest.Qty}" min="1" />
                </td>
                <td class="total-price">${labTest.Total.toFixed(2)}</td>
                <td><a href="#" class="link-delete-lab-test" data-testId="${labTest.TestId}"><i class='fa fa-trash-o' style='color: red'></i></a></td>
            </tr>`;
                grid.append(row);
            });
        };

        $('#AddEditPatientRegistrationForm').on('submit', function (e) {
            e.preventDefault();

            var diagnosis = $("#Diagnosis").val();
            var treatment = $("#Treatment").val();
            var advice = $("#Advice").val();
            var symptoms = $("#Symptoms").val();
            var followUpDate = $("#FollowUpDate").val();

            var medicalConsultationData = {
                ConsultationId: null,
                PatientId: self.patient,
                DoctorId: self.PatientInfirmation.DoctorId,
                ConsultationDate: self.PatientInfirmation.DateOfVisit,
                ConsultationTime: self.PatientInfirmation.TimeOfVisit,
                Symptoms: symptoms,
                Remarks: symptoms
            };
            var medicalConsultation = addCommonProperties(medicalConsultationData);

            var consultationDetailData = {
                DetailId: null,
                ConsultationId: null,
                Diagnosis: diagnosis,
                Treatment: treatment,
                Advice: advice,
                FollowUpDate: new Date(followUpDate)
            };

            var consultationDetails = addCommonProperties(consultationDetailData);

            console.log(patient);
        });
    };
}