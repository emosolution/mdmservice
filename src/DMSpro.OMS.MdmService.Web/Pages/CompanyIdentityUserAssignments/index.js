$(function () {
    var l = abp.localization.getResource("MdmService");
	
	var companyIdentityUserAssignmentService = window.dMSpro.oMS.mdmService.controllers.companyIdentityUserAssignments.companyIdentityUserAssignment;
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: "/Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompanyIdentityUserAssignments/CreateModal",
        scriptUrl: "/Pages/CompanyIdentityUserAssignments/createModal.js",
        modalClass: "companyIdentityUserAssignmentCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CompanyIdentityUserAssignments/EditModal",
        scriptUrl: "/Pages/CompanyIdentityUserAssignments/editModal.js",
        modalClass: "companyIdentityUserAssignmentEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            identityUserId: $("#IdentityUserIdFilter").val(),
			companyId: $("#CompanyIdFilter").val()
        };
    };

    var dataTable = $("#CompanyIdentityUserAssignmentsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(companyIdentityUserAssignmentService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('MdmService.CompanyIdentityUserAssignments.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.companyIdentityUserAssignment.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('MdmService.CompanyIdentityUserAssignments.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    companyIdentityUserAssignmentService.delete(data.record.companyIdentityUserAssignment.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "companyIdentityUserAssignment.identityUserId" },
            {
                data: "company.code",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewCompanyIdentityUserAssignmentButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        companyIdentityUserAssignmentService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/mdm-service/company-identity-user-assignments/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'identityUserId', value: input.identityUserId }, 
                            { name: 'companyId', value: input.companyId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
