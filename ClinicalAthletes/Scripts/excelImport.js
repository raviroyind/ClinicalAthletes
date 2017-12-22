$("#excel").kendoUpload({
    multiple: false,
    showFileList: true,
    async: {
        saveUrl: "api/ExcelImport/upload",
        autoUpload: true,
    },
    localization: {
        select: 'Select excel to import',
        remove: '',
        cancel: ''
    },
    validation: {
        allowedExtensions: [".xlsx"]
    },
    success: function (e) {
        var file0Uid = e.files[0].uid;
        var name = $(".k-file[data-uid='" + file0Uid + "']").find(".k-file-name").text();
        $("#ExcelFilePath").val(name);
        $(".importFrm").toggle();
    },
    error: function (e) {
        var resp = JSON.parse(e.XMLHttpRequest.responseText);
        alert(resp.message);
    }
});
 