function RoleController() {
    var self = this;
    self.selectedRows = [];
    self.currectSelectedRole = {};
    self.todayDate = new Date();
    self.fileUploadModal = $("#fileUploadModal");
    self.ImportedRoles = [];
    self.init = function () {
        $("#permissionBtn").removeClass("permission-hidden");
        var table = new Tabulator("#rolesGrid", {
            height: "600px",
            layout: "fitColumns",
            resizableColumnFit: true,
            ajaxURL: '/Role/FetchUserRoles',
            ajaxParams: {},
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
                    title: "<div class='centered-checkbox'><input type='checkbox' id='parentRoleChkbox' style='margin-top: 22px;'></div>",
                    field: "select",
                    headerSort: false,
                    hozAlign: "center",
                    headerHozAlign: "center",
                    cssClass: "centered-checkbox",
                    width: 30,
                    formatter: function (cell, formatterParams, onRendered) {
                        onRendered(function () {
                            var row = cell.getRow();
                            var rowId = row.getData().RoleId;
                            cell.getElement().innerHTML = `<div class='centered-checkbox'><input type='checkbox' id='childRoleChkbox-${rowId}' class='childRoleChkbox' data-row-id='${rowId}'/></div>`;
                            cell.getElement().querySelector('input[type="checkbox"]').checked = row.isSelected();
                        });
                        return "";
                    },
                    cellClick: function (e, cell) {
                        cell.getRow().toggleSelect();
                    }
                },
                { title: "Name", field: "Name", headerFilter: "input" },
                { title: "Code", field: "Code", headerFilter: "input" },
                { title: "Created By", field: "CreatedBy", headerFilter: "input" },
                { title: "Created On", field: "CreatedOn", headerFilter: "input" },
                { title: "Modified By", field: "ModifiedBy", headerFilter: "input" },
                { title: "Modified On", field: "ModifiedOn", headerFilter: "input" },
                { title: "IsActive", field: "IsActive", headerFilter: "input" }

            ],
            rowSelectionChanged: function (data, rows) {
                var allSelected = rows.length && rows.every(row => row.isSelected());
                $('#parentRoleChkbox').prop('checked', allSelected);
                disableAllButtons();

                // Enable buttons based on selection
                if (rows.length > 0) {
                    enableButtons(table);
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
                    var foundRow = rows.find(row => row.getData().RoleId === changedRow.RoleId);

                    if (foundRow) {
                        var rowId = foundRow.getData().RoleId;
                        var checkbox = document.querySelector(`#childRoleChkbox-${rowId}`);
                        if (checkbox.checked && currentSelectedRows.length === 1) {
                            self.currectSelectedRole = changedRow;
                        }
                        else {
                            self.currectSelectedRole = {};
                        }
                    }

                }


            }
        });

        $(document).on("change", "#parentRoleChkbox", function () {
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                table.selectRow();
            } else {
                table.deselectRow();
            }
            $('.childRoleChkbox').prop('checked', isChecked);
        });

        $(document).on('change', '.childRoleChkbox', function () {
            var rowId = $(this).data('row-id');
            var row = table.getRow(function (data) {
                return data.id === rowId;
            });
            var rows = table.getRows();
            var allSelected = rows.length && rows.every(row => row.isSelected());
            $('#parentRoleChkbox').prop('checked', allSelected);
        });
        //-----------------Edit functionality-------------------//
        $(document).on("click", "#editBtn", function () {
            if (self.currectSelectedRole) {
                $("#Name").val(self.currectSelectedRole.Name);
                $("#Code").val(self.currectSelectedRole.Code);
                $('#sidebar').addClass('show');
                $('body').append('<div class="modal-backdrop fade show"></div>');
            } else {
                $('#sidebar').removeClass('show');
                $('.modal-backdrop').remove();
            }

        });
        //-----------------Permission icon click--------------------//
        $('#permissionBtn').on('click', function () {
            window.location.href = "/Permission/ManagerPermission?roleId=" + self.currectSelectedRole.RoleId;
        });
        //---------------Permission icon area closed ------------------//

        $('#addBtn').on('click', function () {
            $('#sidebar').addClass('show');
            $('body').append('<div class="modal-backdrop fade show"></div>');
        });

        $('#closeSidebar, .modal-backdrop').on('click', function () {
            $('#AddEditRoleForm')[0].reset();
            $('#sidebar').removeClass('show');
            $('.modal-backdrop').remove();
        });
        $('#AddEditRoleForm').on('submit', function (e) {
            e.preventDefault();
            var formData = getFormData('#AddEditRoleForm');
            var role = addCommonProperties(formData);
            role.RoleId = self.currectSelectedRole ? self.currectSelectedRole.RoleId : null;

            self.addeditrole(role, false);
        });

        makeFormGeneric('#AddEditRoleForm', '#btnsubmit');
        self.addeditrole = function (role, iscopy) {
            makeAjaxRequest({
                url: API_URLS.InsertOrUpdateRoleAsync,
                data: role,
                type: 'POST',
                successCallback: function (response) {
                    if (response) {
                        if (!iscopy) {
                            $('#AddEditRoleForm')[0].reset();
                            $('#sidebar').removeClass('show');
                            $('.modal-backdrop').remove();
                        }
                        table.setData();
                        self.currectSelectedRole = {};
                    }
                    console.info(response);
                },
                errorCallback: function (xhr, status, error) {
                    console.error("Error in upserting data to server: " + error);
                }
            });
        };
        //---------------Import Functionality-------------//
        $(document).on("click", "#importBtn", function () {
            self.selectedRows = [];
            $(self.fileUploadModal).modal('show');
        });
        $(document).on('change', '#fileInput', function (e) {
            var files = e.target.files;
            processFiles(files, gridColumns.RoleGrid, function (importedData) {
                self.ImportedRoles = importedData;
                console.log(self.ImportedRoles);
            });
        });

        $(document).on("click", "#uploadButton", function (e) {
            if (self.ImportedTenants.length > 0) {
                makeAjaxRequest({
                    url: API_URLS.BulkInsertOrUpdateTenant,
                    data: self.ImportedTenants,
                    type: 'POST',
                    successCallback: function (response) {
                        self.ImportedRoles = [];
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

        //-------------------Export ------------------//
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
        self.exportExcel = function (data) {
            var sorters = table.getSorters();
            var sortColumns = sorters.length > 0 ? sorters[0].field : null;
            var sortOrder = sorters.length > 0 ? sorters[0].dir : null;
            exportToExcel(data, gridColumns.RoleGrid, "Role", "Role_Report", sortColumns, sortOrder);
        }

    };
}