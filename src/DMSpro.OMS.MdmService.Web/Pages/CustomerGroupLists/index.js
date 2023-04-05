$(function () {
    var l = abp.localization.getResource("MdmService");
	
	var customerGroupListService = window.dMSpro.oMS.mdmService.customerGroupLists.customerGroupLists;
	
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
        viewUrl: abp.appPath + "CustomerGroupLists/CreateModal",
        scriptUrl: "/Pages/CustomerGroupLists/createModal.js",
        modalClass: "customerGroupListCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerGroupLists/EditModal",
        scriptUrl: "/Pages/CustomerGroupLists/editModal.js",
        modalClass: "customerGroupListEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            description: $("#DescriptionFilter").val(),
            active: (function () {
                var value = $("#ActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			customerId: $("#CustomerIdFilter").val(),			customerGroupId: $("#CustomerGroupIdFilter").val()
        };
    };

    var dataTable = $("#CustomerGroupListsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerGroupListService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupLists.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerGroupList.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupLists.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerGroupListService.delete(data.record.customerGroupList.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerGroupList.description" },
            {
                data: "customerGroupList.active",
                render: function (active) {
                    return active ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "customer.code",
                defaultContent : ""
            },
            {
                data: "customerGroup.code",
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

    $("#NewCustomerGroupListButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerGroupListService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/mdm-service/customer-group-lists/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'description', value: input.description }, 
                            { name: 'active', value: input.active }, 
                            { name: 'customerId', value: input.customerId }
, 
                            { name: 'customerGroupId', value: input.customerGroupId }
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
