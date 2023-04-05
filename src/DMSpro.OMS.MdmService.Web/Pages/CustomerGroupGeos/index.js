$(function () {
    var l = abp.localization.getResource("MdmService");
	
	var customerGroupGeoService = window.dMSpro.oMS.mdmService.customerGroupGeos.customerGroupGeos;
	
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
        viewUrl: abp.appPath + "CustomerGroupGeos/CreateModal",
        scriptUrl: "/Pages/CustomerGroupGeos/createModal.js",
        modalClass: "customerGroupGeoCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerGroupGeos/EditModal",
        scriptUrl: "/Pages/CustomerGroupGeos/editModal.js",
        modalClass: "customerGroupGeoEdit"
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
			customerGroupId: $("#CustomerGroupIdFilter").val(),			geoMaster0Id: $("#GeoMaster0IdFilter").val(),			geoMaster1Id: $("#GeoMaster1IdFilter").val(),			geoMaster2Id: $("#GeoMaster2IdFilter").val(),			geoMaster3Id: $("#GeoMaster3IdFilter").val(),			geoMaster4Id: $("#GeoMaster4IdFilter").val()
        };
    };

    var dataTable = $("#CustomerGroupGeosTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerGroupGeoService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupGeos.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerGroupGeo.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('MdmService.CustomerGroupGeos.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerGroupGeoService.delete(data.record.customerGroupGeo.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerGroupGeo.description" },
            {
                data: "customerGroupGeo.active",
                render: function (active) {
                    return active ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
            {
                data: "customerGroup.code",
                defaultContent : ""
            },
            {
                data: "geoMaster.code",
                defaultContent : ""
            },
            {
                data: "geoMaster1.code",
                defaultContent : ""
            },
            {
                data: "geoMaster2.code",
                defaultContent : ""
            },
            {
                data: "geoMaster3.code",
                defaultContent : ""
            },
            {
                data: "geoMaster4.code",
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

    $("#NewCustomerGroupGeoButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerGroupGeoService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/mdm-service/customer-group-geos/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'description', value: input.description }, 
                            { name: 'active', value: input.active }, 
                            { name: 'customerGroupId', value: input.customerGroupId }
, 
                            { name: 'geoMaster0Id', value: input.geoMaster0Id }
, 
                            { name: 'geoMaster1Id', value: input.geoMaster1Id }
, 
                            { name: 'geoMaster2Id', value: input.geoMaster2Id }
, 
                            { name: 'geoMaster3Id', value: input.geoMaster3Id }
, 
                            { name: 'geoMaster4Id', value: input.geoMaster4Id }
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
