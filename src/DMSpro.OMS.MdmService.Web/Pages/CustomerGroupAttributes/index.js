$(function () {
    var l = abp.localization.getResource("MdmService");
	
	var customerGroupAttributeService = window.dMSpro.oMS.mdmService.customerGroupAttributes.customerGroupAttributes;
	
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
        viewUrl: abp.appPath + "CustomerGroupAttributes/CreateModal",
        scriptUrl: "/Pages/CustomerGroupAttributes/createModal.js",
        modalClass: "customerGroupAttributeCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerGroupAttributes/EditModal",
        scriptUrl: "/Pages/CustomerGroupAttributes/editModal.js",
        modalClass: "customerGroupAttributeEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            description: $("#DescriptionFilter").val(),
			customerGroupId: $("#CustomerGroupIdFilter").val(),			attr0Id: $("#Attr0IdFilter").val(),			attr1Id: $("#Attr1IdFilter").val(),			attr2Id: $("#Attr2IdFilter").val(),			attr3Id: $("#Attr3IdFilter").val(),			attr4Id: $("#Attr4IdFilter").val(),			attr5Id: $("#Attr5IdFilter").val(),			attr6Id: $("#Attr6IdFilter").val(),			attr7Id: $("#Attr7IdFilter").val(),			attr8Id: $("#Attr8IdFilter").val(),			attr9Id: $("#Attr9IdFilter").val(),			attr10Id: $("#Attr10IdFilter").val(),			attr11Id: $("#Attr11IdFilter").val(),			attr12Id: $("#Attr12IdFilter").val(),			attr13Id: $("#Attr13IdFilter").val(),			attr14Id: $("#Attr14IdFilter").val(),			attr15Id: $("#Attr15IdFilter").val(),			attr16Id: $("#Attr16IdFilter").val(),			attr17Id: $("#Attr17IdFilter").val(),			attr18Id: $("#Attr18IdFilter").val(),			attr19Id: $("#Attr19IdFilter").val()
        };
    };

    var dataTable = $("#CustomerGroupAttributesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerGroupAttributeService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupAttributes.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerGroupAttribute.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupAttributes.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerGroupAttributeService.delete(data.record.customerGroupAttribute.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerGroupAttribute.description" },
            {
                data: "customerGroup.code",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue1.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue2.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue3.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue4.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue5.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue6.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue7.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue8.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue9.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue10.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue11.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue12.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue13.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue14.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue15.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue16.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue17.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue18.attrValName",
                defaultContent : ""
            },
            {
                data: "customerAttributeValue19.attrValName",
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

    $("#NewCustomerGroupAttributeButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerGroupAttributeService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/mdm-service/customer-group-attributes/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'description', value: input.description }, 
                            { name: 'customerGroupId', value: input.customerGroupId }
, 
                            { name: 'attr0Id', value: input.attr0Id }
, 
                            { name: 'attr1Id', value: input.attr1Id }
, 
                            { name: 'attr2Id', value: input.attr2Id }
, 
                            { name: 'attr3Id', value: input.attr3Id }
, 
                            { name: 'attr4Id', value: input.attr4Id }
, 
                            { name: 'attr5Id', value: input.attr5Id }
, 
                            { name: 'attr6Id', value: input.attr6Id }
, 
                            { name: 'attr7Id', value: input.attr7Id }
, 
                            { name: 'attr8Id', value: input.attr8Id }
, 
                            { name: 'attr9Id', value: input.attr9Id }
, 
                            { name: 'attr10Id', value: input.attr10Id }
, 
                            { name: 'attr11Id', value: input.attr11Id }
, 
                            { name: 'attr12Id', value: input.attr12Id }
, 
                            { name: 'attr13Id', value: input.attr13Id }
, 
                            { name: 'attr14Id', value: input.attr14Id }
, 
                            { name: 'attr15Id', value: input.attr15Id }
, 
                            { name: 'attr16Id', value: input.attr16Id }
, 
                            { name: 'attr17Id', value: input.attr17Id }
, 
                            { name: 'attr18Id', value: input.attr18Id }
, 
                            { name: 'attr19Id', value: input.attr19Id }
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
