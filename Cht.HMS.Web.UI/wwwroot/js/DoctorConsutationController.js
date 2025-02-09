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


            if (self.ApplicationUser.RoleName != "Doctor") {
                $('.card-body').find('input, textarea, select, button').prop('disabled', true);
                $('#btnBack').prop('disabled', false);
            }

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

            var medicinesList = self.dbMedicines.map(function (medicine) {
                return {
                    MedicineId: medicine.MedicineId,
                    MedicineName: medicine.MedicineName + " - " + medicine.GenericName + " - " + medicine.DosageForm + " - " + medicine.Manufacturer + " - " + medicine.PricePerUnit
                };
            });

            genarateDropdown("medicineDropdown", medicinesList, "MedicineId", "MedicineName");

            genarateDropdown("labTestDropdown", self.dbLabTests, "TestId", "TestName");
            self.preparePatientInfirmation();
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

                //Consulation Infirmation
                $("#Diagnosis").val(self.PatientInfirmation.patientCunsultation.Diagnosis);
                $("#Treatment").val(self.PatientInfirmation.patientCunsultation.patientConsultationDetails.Treatment);
                $("#Advice").val(self.PatientInfirmation.patientCunsultation.patientConsultationDetails.Advice);
                $("#Symptoms").val(self.PatientInfirmation.patientCunsultation.Symptoms);
                $("#FollowUpDate").val(self.PatientInfirmation.patientCunsultation.patientConsultationDetails.FollowUpDate);

                if (self.PatientInfirmation.patientLabOrder) {
                    if (self.PatientInfirmation.patientLabOrder.patientLabOrderDetails.length > 0) {
                        self.PatientLabTests = self.PatientInfirmation.patientLabOrder.patientLabOrderDetails.map(function (detail) {
                            var labTest = self.dbLabTests.find(function (test) {
                                return test.TestId === detail.TestId;
                            });

                            return {
                                TestId: detail.TestId,
                                TestName: labTest ? labTest.TestName : "Unknown",
                                Price: detail.PricePerUnit,
                                Qty: detail.Quantity,
                                Total: detail.TotalPrice
                            };
                        });
                        console.log(self.PatientLabTests);
                        self.prepareLabTestsGrid();
                    }
                }

                if (self.PatientInfirmation.patientPharmacyOrder) {
                    if (self.PatientInfirmation.patientPharmacyOrder.patientPharmacyOrderDetails.length > 0) {
                        self.PatientMedicines = self.PatientInfirmation.patientPharmacyOrder.patientPharmacyOrderDetails.map(function (detail) {
                            var medicine = self.dbMedicines.find(function (med) {
                                return med.MedicineId === detail.MedicineId;
                            });

                            return {
                                OrderDetailId: detail.OrderDetailId || null,
                                OrderId: detail.OrderId || null,
                                MedicineId: detail.MedicineId,
                                MedicineName: medicine ? medicine.MedicineName : "Unknown",
                                GenericName: medicine ? medicine.GenericName : "Unknown",
                                DosageForm: medicine ? medicine.DosageForm : "Unknown",
                                Manufacturer: medicine ? medicine.Manufacturer : "Unknown",
                                PricePerUnit: detail.PricePerUnit,
                                Qty: detail.Quantity,
                                Total: detail.TotalPrice
                            };
                        });
                        self.prepareMedicinesGrid();
                    }
                }
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
                    OrderDetailId: null,
                    OrderId: null,
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

        $(document).on("click", "#btnBack", function (e) {
            e.preventDefault();
            if (self.ApplicationUser.RoleName != "Doctor") {
                window.location.href = "/ExecutiveDashboard/Index";
            } else {
                window.location.href = "/DoctorDashboard/Index";
            }

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

        $('#AddEditDoctorConsutationForm').on('submit', function (e) {
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
                Diagnosis: diagnosis,
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

            medicalConsultation.patientConsultationDetails = consultationDetails;

            var patientPharmacyOrder = {};

            var totalQty = 0;
            var totalAmount = 0;

            self.PatientMedicines.forEach(function (medicine) {
                totalQty += medicine.Qty;
                totalAmount += medicine.Total;
            });

            patientPharmacyOrder.OrderId = null;
            patientPharmacyOrder.PatientId = self.patientId;
            patientPharmacyOrder.ConsultationId = self.consultationId;
            patientPharmacyOrder.OrderDate = new Date();
            patientPharmacyOrder.ItemsQty = totalQty;
            patientPharmacyOrder.TotalAmount = totalAmount;
            patientPharmacyOrder.CreatedBy = self.ApplicationUser.Id;
            patientPharmacyOrder.CreatedOn = new Date();
            patientPharmacyOrder.ModifiedBy = self.ApplicationUser.Id;
            patientPharmacyOrder.ModifiedOn = null;
            patientPharmacyOrder.IsActive = true;
            patientPharmacyOrder.patientPharmacyOrderDetails = self.PatientMedicines.map(function (medicine) {
                return {
                    OrderDetailId: null,
                    OrderId: patientPharmacyOrder.OrderId,
                    MedicineId: medicine.MedicineId,
                    MedicineName: medicine.MedicineName,
                    GenericName: medicine.GenericName,
                    DosageForm: medicine.DosageForm,
                    Manufacturer: medicine.Manufacturer,
                    PricePerUnit: medicine.PricePerUnit,
                    Qty: medicine.Qty,
                    Total: medicine.Total,
                    CreatedBy: self.ApplicationUser.Id,
                    CreatedOn: new Date(),
                    ModifiedBy: self.ApplicationUser.Id,
                    ModifiedOn: new Date(),
                    IsActive: true
                };
            });

            var patientLabOrder = {};

            var totalAmount = 0;

            self.PatientLabTests.forEach(function (test) {
                totalAmount += test.Total;
            });

            patientLabOrder.LabOrderId = null;
            patientLabOrder.PatientId = self.patientId;
            patientLabOrder.ConsultationId = self.consultationId;
            patientLabOrder.OrderDate = new Date();
            patientLabOrder.TotalAmount = totalAmount;
            patientLabOrder.CreatedBy = self.ApplicationUser.Id;
            patientLabOrder.CreatedOn = new Date();
            patientLabOrder.ModifiedBy = self.ApplicationUser.Id;
            patientLabOrder.ModifiedOn = new Date();
            patientLabOrder.IsActive = true;
            patientLabOrder.patientLabOrderDetails = self.PatientLabTests.map(function (test) {
                return {
                    LabOrderDetailId: null,
                    LabOrderId: patientLabOrder.LabOrderId,
                    TestId: test.TestId,
                    Quantity: test.Qty,
                    PricePerUnit: test.Price,
                    TotalPrice: test.Total,
                    CreatedBy: self.ApplicationUser.Id,
                    CreatedOn: new Date(),
                    ModifiedBy: self.ApplicationUser.Id,
                    ModifiedOn: new Date(),
                    IsActive: true
                };
            });

            console.log(patientLabOrder);

            var _patientInformation = self.PatientInfirmation;
            _patientInformation.CurrentStatus = "Doctor Consulted";
            _patientInformation.ModifiedBy = self.ApplicationUser.Id;
            _patientInformation.ModifiedOn = new Date();
            _patientInformation.patientCunsultation = medicalConsultation;
            _patientInformation.patientPharmacyOrder = patientPharmacyOrder;
            _patientInformation.patientLabOrder = patientLabOrder;

            makeAjaxRequest({
                url: "/Patient/UpsertPatientConsultationDetails",
                data: _patientInformation,
                type: 'POST',
                successCallback: function (response) {
                    console.info(response);
                    hideLoader();
                    window.location.href = "/DoctorDashboard/Index";
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });

            console.log(_patientInformation);

        });
    };
}