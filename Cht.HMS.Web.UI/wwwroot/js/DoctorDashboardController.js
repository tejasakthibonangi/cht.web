function DoctorDashboardController() {
    var self = this;
    self.selectedRows = [];
    self.currectSelectedPatient = {};
    self.todayDate = new Date();
    self.fileUploadModal = $("#fileUploadModal");
    self.coreDBDocters = [];
    var actions = [];
    var dataObjects = [];
    self.ApplicationUser = {};

    actions.push('/Doctor/GetDoctors');
    self.init = function () {

        var appUserInfo = storageService.get('ApplicationUser');
        if (appUserInfo) {
            self.ApplicationUser = appUserInfo;
        }

        var table = new Tabulator("#consulationGrid", {
            height: "780px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Patient/GetPatientsByDoctor',
            ajaxParams: { doctorId: self.ApplicationUser.Id },
            ajaxConfig: {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
            },
            ajaxResponse: function (url, params, response) {
                return response.data;
            },
            columns: [
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentPatientChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().PatientId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childPatientChkbox-${rowId}' class='childPatientChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Name", field: "PatientName" },
                { title: "Date of Visit", field: "DateOfVisit" },
                { title: "Time of Visit", field: "TimeOfVisit" },
                { title: "Coming From", field: "ComingFrom" },
                { title: "Phone Number", field: "PhoneNo" },
                { title: "Gender", field: "Gender" },
                {
                    title: "Vitals",
                    field: "Height",
                    formatter: function (cell, formatterParams, onRendered) {
                        var rowData = cell.getRow().getData();

                        var vitals = [
                            `Height: ${rowData.Height || "N/A"}`,
                            `Weight: ${rowData.Weight || "N/A"}`,
                            `BP: ${rowData.BP || "N/A"}`,
                            `Sugar: ${rowData.Sugar || "N/A"}`,
                            `Temperature: ${rowData.Temperature || "N/A"}`
                        ].join("<br>");
                        return vitals;
                    },
                },
                { title: "Doctor", field: "DoctorName" },
                { title: "Fee", field: "Fee" },
                { title: "Current Status", field: "CurrentStatus" },
                {
                    title: "Options",
                    field: "PatientId",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 150, // Adjust width as needed
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().PatientId;
                            cell.getElement().innerHTML = `
                        <button class="consultation-button btn btn-primary" data-patientid='${rowId}'>
                            Consultation
                        </button>`;
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        var patientId = cell.getRow().getData().PatientId;
                        // Handle the consultation button click event
                        console.log("Consultation button clicked for Patient ID:", patientId);
                        // You can add your logic here to handle the consultation action
                    }
                }
            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentPatientChkbox').prop('checked', allSelected);
                disableAllButtons();

                // Enable buttons based on selection
                if (rows.length > 0) {
                    enableButtons();
                }

                // Find the most recently changed row
                let currentSelectedRows = rows.map(row => row.getData());
                let changedRow = null;

                if (self.selectedRows.length > currentSelectedRows.length) {
                    // A row was deselected
                    changedRow = self.selectedRows.find(row => !currentSelectedRows.includes(row));
                } else if (self.selectedRows.length < currentSelectedRows.length) {
                    // A row was selected
                    changedRow = currentSelectedRows.find(row => !self.selectedRows.includes(row));
                }


                // Update the previous selected rows state
                self.selectedRows = currentSelectedRows;
                // Handle the changed row data
                if (changedRow) {
                    var rows = table.getRows();
                    var foundRow = rows.find(row => row.getData().PatientId === changedRow.PatientId);

                    if (foundRow) {
                        var rowId = foundRow.getData().PatientId;
                        var checkbox = document.querySelector(`#childPatientChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedPatient = changedRow;
                        }
                        else {
                            self.currectSelectedPatient = {};
                        }
                    }

                }


            }
        });

        var requests = actions.map((action, index) => {
            var ajaxConfig = {
                url: action,
                method: 'GET'
            };
            return $.ajax(ajaxConfig);
        });

        $.when.apply($, requests).done(function (...responses) {
            self.coreDBDocters = responses[0]?.data || [];

            if (self.coreDBDocters) {
                var doctorPhoneList = self.coreDBDocters.map(function (doctor) {
                    return {
                        DoctorId: doctor.DoctorId,
                        DoctorName: doctor.DoctorName + "-" + doctor.Specialty + "-" + doctor.Phone
                    };
                });
                genarateDropdown("DoctorAssignedId", doctorPhoneList, "DoctorId", "DoctorName");
            }

            hideLoader();
        }).fail(function () {
            console.log('One or more requests failed.');
        });
        $(document).on("change", "#parentPatientChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childPatientChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childPatientChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.id === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentPatientChkbox').prop('checked', allSelected);
        });


        $('#addBtn').on('click', function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditConsulationForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });

        $('#AddEditConsulationForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditConsulationForm');
            var patient = addCommonProperties(formData);
            patient.PatientId = self.currectSelectedPatient ? self.currectSelectedPatient.PatientId : null;
            patient.CurrentStatus = self.currectSelectedPatient.CurrentStatus ? self.currectSelectedPatient.CurrentStatus : "Patiend Registered";
            patient.PreparedBy = patient.ModifiedBy;
            self.addEditPatient(patient, false);

            console.log(patient);
        });

        makeFormGeneric('#AddEditConsulationForm', '#btnsubmit');

        // Function to disable all buttons
        function disableAllButtons() {

            $(".custom-cursor").addClass("disabled");
            $("#addBtn").removeClass("disabled");
            $("#exportBtn").removeClass("disabled");
            $("#importBtn").removeClass("disabled");
        }

        // Function to enable buttons
        function enableButtons() {
            $(".custom-cursor").removeClass("disabled");

            // Highlight buttons based on selection
            var selectedRows = table.getSelectedRows();
            var hasMultipleSelection = selectedRows.length > 1;

            if (hasMultipleSelection) {
                /* $("#addBtn").removeClass("selected");*/
                $("#editBtn").addClass("disabled");
                $("#deleteBtn").addClass("disabled");
                $("#copyBtn").addClass("disabled");
                $("#addBtn").removeClass("disabled");
            } else {
                selectedRows.forEach(function (row) {
                    // Highlight based on specific conditions (e.g., edit and delete)
                    var isSelected = row.isSelected();
                    if (isSelected) {
                        /*  $("#addBtn").addClass("selected");*/
                        $("#editBtn").addClass("selected");
                        $("#deleteBtn").addClass("selected");
                        $("#copyBtn").addClass("selected");
                        $("#addBtn").addClass("disabled");
                    }
                });
            }
        }

        $(document).on("click", ".consultation-button", function () {
            var rowId = $(this).data('patientid');

            var row = $.grep(table.getData(), function (data) {
                return data.PatientId === rowId;
            });

            console.log(row[0]);

            var patient = row[0];

            var managerPatientUrl = "/DoctorDashboard/DoctorConsutation?patientId=" + patient.PatientId;

            window.location.href = managerPatientUrl;
        });

        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedPatient) {
                // Populate the form fields with data from the selected patient
                $("#PatientName").val(self.currectSelectedPatient.PatientName);
                $("#DateOfVisit").val(self.currectSelectedPatient.DateOfVisit ? self.currectSelectedPatient.DateOfVisit.split('T')[0] : '');  // Date formatting for input[type="date"]
                $("#TimeOfVisit").val(self.currectSelectedPatient.TimeOfVisit);
                $("#ComingFrom").val(self.currectSelectedPatient.ComingFrom);
                $("#Reference").val(self.currectSelectedPatient.Reference);
                $("#PhoneNo").val(self.currectSelectedPatient.PhoneNo);
                $("#AlternatePhoneNo").val(self.currectSelectedPatient.AlternatePhoneNo);
                $("#Email").val(self.currectSelectedPatient.Email);
                $("#Gender").val(self.currectSelectedPatient.Gender);
                $("#DOB").val(self.currectSelectedPatient.DOB ? self.currectSelectedPatient.DOB.split('T')[0] : '');  // Date formatting for input[type="date"]
                $("#Height").val(self.currectSelectedPatient.Height);
                $("#Weight").val(self.currectSelectedPatient.Weight);
                $("#BP").val(self.currectSelectedPatient.BP);
                $("#Sugar").val(self.currectSelectedPatient.Sugar);
                $("#Temperature").val(self.currectSelectedPatient.Temperature);
                $("#HealthIssues").val(self.currectSelectedPatient.HealthIssues);
                $("#DoctorAssignedId").val(self.currectSelectedPatient.DoctorAssignedId);
                $("#Fee").val(self.currectSelectedPatient.Fee);
                $("#PreparedBy").val(self.currectSelectedPatient.PreparedBy);

                // Show the sidebar and backdrop
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                // If no patient is selected, hide the sidebar and backdrop
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }
        });

        $(document).on("click", "#copyBtn", function () {
            if (self.currectSelectedPatient) {
                $('#confirmCopyModal').modal('show');
            }
        });
        $(document).on("click", "#confirmCopyBtn", function () {
            if (self.currectSelectedPatient) {
                var tenant = addCommonProperties(self.currectSelectedPatient);
                tenant.PatientId = null;
                self.addedittenant(tenant, true);
            }
            $('#confirmCopyModal').modal('hide');
        });
        $(document).on("click", "#deleteBtn", function () {
            if (self.currectSelectedPatient) {
                $('#confirmDeleteModal').modal('show');
            }
        });
        $(document).on("click", "#confirmDeleteBtn", function () {
            if (self.currectSelectedPatient) {
                //var tenant = addCommonProperties(self.currectSelectedPatient);
                //tenant.PatientId = null;
                //self.addedittenant(tenant, true);
            }
            $('#confirmDeleteModal').modal('hide');
        });
        $(document).on("click", "#exportTemplate", function (e) {
            var _selectedRows = [];

            self.exportExcel(_selectedRows)
        });
        $(document).on("click", "#exportWithGridData", function (e) {
            if (self.selectedRows.length > 0)
                self.exportExcel(self.selectedRows);
        });
        $(document).on("click", "#exportWithOriginalData", function (e) {
            var gridData = table.getData();
            self.exportExcel(gridData);
        });
        $(document).on("click", "#importBtn", function () {
            self.selectedRows = [];
            $(self.fileUploadModal).modal('show');
        });

        self.exportExcel = function (data) {
            var sorters = table.getSorters();
            var sortColumns = sorters.length > 0 ? sorters[0].field : null;
            var sortOrder = sorters.length > 0 ? sorters[0].dir : null;
            exportToExcel(data, gridColumns.TenantGrid, "Tenant", "Tenant_Report", sortColumns, sortOrder);
        }
        self.addEditPatient = function (patient, iscopy) {
            makeAjaxRequest({
                url: "/Patient/InsertOrUpdatePatientRegistration",
                data: patient,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditConsulationForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedPatient = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
        $(document).on('change', '#fileInput', function (e) {
            var files = e.target.files;
            processFiles(files, gridColumns.TenantGrid, function (importedData) {
                self.ImportedTenants = importedData;
                console.log(self.ImportedTenants);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedTenants = [];
                        table.setData();
                        $(self.fileUploadModal).modal('hide');
                        console.info(response);
                    },
                    errorCallback: function (xhr, status, error) {
                        console.error("Error in upserting data to server: " + error);
                    }
                });
            }
        });
    }
}