function DoctorDashboardController() {
    var self = this;
    self.selectedRows = [];
    self.currectSelectedTenant = {};
    self.todayDate = new Date();
    self.fileUploadModal = $("#fileUploadModal");
    self.ImportedTenants = [];
    self.init = function () {
        var table = new Tabulator("#consulationGrid", {
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            data: [
                {
                    "TenantId": 1,
                    "TenantName": "Tenant A",
                    "TenantCode": "TNA001",
                    "Logo": "logoA.png",
                    "TenantWebsite": "https://tenantA.com",
                    "Location": "Location A",
                    "Address": "123 A Street, City A",
                    "ContactName": "John Doe",
                    "ContactPhone": "+1234567890",
                    "Products": "Product A, Product B",
                    "Makes": "Make A, Make B",
                    "DealerCount": 100
                },
                {
                    "TenantId": 2,
                    "TenantName": "Tenant B",
                    "TenantCode": "TNB002",
                    "Logo": "logoB.png",
                    "TenantWebsite": "https://tenantB.com",
                    "Location": "Location B",
                    "Address": "456 B Avenue, City B",
                    "ContactName": "Jane Smith",
                    "ContactPhone": "+0987654321",
                    "Products": "Product C, Product D",
                    "Makes": "Make C, Make D",
                    "DealerCount": 150
                },
                {
                    "TenantId": 3,
                    "TenantName": "Tenant C",
                    "TenantCode": "TNC003",
                    "Logo": "logoC.png",
                    "TenantWebsite": "https://tenantC.com",
                    "Location": "Location C",
                    "Address": "789 C Blvd, City C",
                    "ContactName": "Alice Johnson",
                    "ContactPhone": "+1122334455",
                    "Products": "Product E, Product F",
                    "Makes": "Make E, Make F",
                    "DealerCount": 200
                },
                {
                    "TenantId": 4,
                    "TenantName": "Tenant D",
                    "TenantCode": "TND004",
                    "Logo": "logoD.png",
                    "TenantWebsite": "https://tenantD.com",
                    "Location": "Location D",
                    "Address": "1010 D Road, City D",
                    "ContactName": "Bob Brown",
                    "ContactPhone": "+6677889900",
                    "Products": "Product G, Product H",
                    "Makes": "Make G, Make H",
                    "DealerCount": 250
                },
                {
                    "TenantId": 5,
                    "TenantName": "Tenant E",
                    "TenantCode": "TNE005",
                    "Logo": "logoE.png",
                    "TenantWebsite": "https://tenantE.com",
                    "Location": "Location E",
                    "Address": "2020 E Parkway, City E",
                    "ContactName": "Charlie Green",
                    "ContactPhone": "+2233445566",
                    "Products": "Product I, Product J",
                    "Makes": "Make I, Make J",
                    "DealerCount": 300
                }
            ],
            columns: [
                {
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentTenantChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().TenantId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childTenantChkbox-${rowId}' class='childTenantChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Name", field: "TenantName" },
                { title: "Code", field: "TenantCode" },
                { title: "Logo", field: "Logo" },
                { title: "Website", field: "TenantWebsite" },
                { title: "Location", field: "Location" },
                { title: "Address", field: "Address" },
                { title: "Contact Name", field: "ContactName" },
                { title: "Contact Phone", field: "ContactPhone" },
                { title: "Products", field: "Products" },
                { title: "Makes", field: "Makes" },
                { title: "Dealer Count", field: "DealerCount" },
                {
                    title: "Options",
                    field: "TenantId",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().TenantId;
                            cell.getElement().innerHTML = `<span target="_blank" data-tentnatid='${rowId}' class="genarateTenantAggriment"><i class="fa-solid fa-file-pdf"></i></span>`;
                        });
                        return "";
                    },
                }

            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentTenantChkbox').prop('checked', allSelected);
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
                    var foundRow = rows.find(row => row.getData().TenantId === changedRow.TenantId);

                    if (foundRow) {
                        var rowId = foundRow.getData().TenantId;
                        var checkbox = document.querySelector(`#childTenantChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedTenant = changedRow;
                        }
                        else {
                            self.currectSelectedTenant = {};
                        }
                    }

                }


            }
        });
        $(document).on("change", "#parentTenantChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childTenantChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childTenantChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.id === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentTenantChkbox').prop('checked', allSelected);
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
            var tenant = addCommonProperties(formData);
            tenant.TenantId = self.currectSelectedTenant ? self.currectSelectedTenant.TenantId : null;
            tenant.DealerCount = 0;
            tenant.Makes = "";
            tenant.Products = "";
            self.addedittenant(tenant, false);
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

        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedTenant) {
                $("#TenantName").val(self.currectSelectedTenant.TenantName);
                $("#TenantCode").val(self.currectSelectedTenant.TenantCode);
                $("#Logo").val(self.currectSelectedTenant.Logo);
                $("#TenantWebsite").val(self.currectSelectedTenant.TenantWebsite);
                $("#Address").val(self.currectSelectedTenant.Address);
                $("#Location").val(self.currectSelectedTenant.Location);
                $("#ContactName").val(self.currectSelectedTenant.ContactName);
                $("#ContactPhone").val(self.currectSelectedTenant.ContactPhone);
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }

        });
        $(document).on("click", "#copyBtn", function () {
            if (self.currectSelectedTenant) {
                $('#confirmCopyModal').modal('show');
            }
        });
        $(document).on("click", "#confirmCopyBtn", function () {
            if (self.currectSelectedTenant) {
                var tenant = addCommonProperties(self.currectSelectedTenant);
                tenant.TenantId = null;
                self.addedittenant(tenant, true);
            }
            $('#confirmCopyModal').modal('hide');
        });
        $(document).on("click", "#deleteBtn", function () {
            if (self.currectSelectedTenant) {
                $('#confirmDeleteModal').modal('show');
            }
        });
        $(document).on("click", "#confirmDeleteBtn", function () {
            if (self.currectSelectedTenant) {
                //var tenant = addCommonProperties(self.currectSelectedTenant);
                //tenant.TenantId = null;
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
        self.addedittenant = function (tenant, iscopy) {
            makeAjaxRequest({
                url: API_URLS.InsertTenantAsync,
                data: tenant,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditConsulationForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedTenant = {};
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