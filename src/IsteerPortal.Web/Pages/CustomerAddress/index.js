$(function () {
    var l = abp.localization.getResource("IsteerPortal");
	
	var customerAddresService = window.isteerPortal.customerAddress.customerAddress;
	
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
        viewUrl: abp.appPath + "CustomerAddress/CreateModal",
        scriptUrl: "/Pages/CustomerAddress/createModal.js",
        modalClass: "customerAddresCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "CustomerAddress/EditModal",
        scriptUrl: "/Pages/CustomerAddress/editModal.js",
        modalClass: "customerAddresEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            address1: $("#Address1Filter").val(),
			address2: $("#Address2Filter").val(),
			zIPCODE: $("#ZIPCODEFilter").val(),
			customerId: $("#CustomerIdFilter").val()
        };
    };

    var dataTable = $("#CustomerAddressTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(customerAddresService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('IsteerPortal.CustomerAddress.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.customerAddres.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('IsteerPortal.CustomerAddress.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    customerAddresService.delete(data.record.customerAddres.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "customerAddres.address1" },
			{ data: "customerAddres.address2" },
			{ data: "customerAddres.zipcode" },
            {
                data: "customer.name",
                defaultContent : ""
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewCustomerAddresButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        customerAddresService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/customer-address/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'address1', value: input.address1 }, 
                            { name: 'address2', value: input.address2 }, 
                            { name: 'zIPCODE', value: input.zIPCODE }, 
                            { name: 'customerId', value: input.customerId }
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
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    
});
