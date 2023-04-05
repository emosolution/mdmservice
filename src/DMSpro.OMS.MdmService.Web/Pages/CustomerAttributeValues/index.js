$(function () {
    var l = abp.localization.getResource("MdmService");
	
	var customerAttributeValueService = window.dMSpro.oMS.mdmService.customerAttributeValues.customerAttributeValues;
	
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
        viewUrl: abp.appPath + "CustomerAttributeValues/CreateModal",
        scriptUrl: "/Pages/CustomerAttributeValues/createModal.js",
        modalClass: "customerAttributeValueCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerAttributeValues/EditModal",
        scriptUrl: "/Pages/CustomerAttributeValues/editModal.js",
        modalClass: "customerAttributeValueEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            code: $("#CodeFilter").val(),
			attrValName: $("#AttrValNameFilter").val(),
			customerAttributeId: $("#CustomerAttributeIdFilter").val(),			parentId: $("#ParentIdFilter").val()
        };
    };

    var dataTable = $("#CustomerAttributeValuesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerAttributeValueService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('MdmService.CustomerAttributeValues.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerAttributeValue.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('MdmService.CustomerAttributeValues.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerAttributeValueService.delete(data.record.customerAttributeValue.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerAttributeValue.code" },
			{ data: "customerAttributeValue.attrValName" },
            {
                data: "customerAttribute.attrName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue1.attrValName",
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

    $("#NewCustomerAttributeValueButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerAttributeValueService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/mdm-service/customer-attribute-values/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'code', value: input.code }, 
                            { name: 'attrValName', value: input.attrValName }, 
                            { name: 'customerAttributeId', value: input.customerAttributeId }
, 
                            { name: 'parentId', value: input.parentId }
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
