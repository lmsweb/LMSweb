$.fn.Initfileinput = function (uploadurl) {
    $("#file").fileinput({
        //uploadUrl: "../fileinfo/save", // server upload action
        theme: 'fa',
        language: 'zh-TW',
        uploadUrl: uploadurl,
        textEncoding: 'UTF-8',
        required: true,
        showBrowse: true,
        browseOnZoneClick: true,
        dropZoneEnabled: true,
        allowedFileExtensions: ["xls", "xlsx"],//只能選擇xls,和xlsx格式的檔案提交
        //maxFileSize: 0,//單位為kb，如果為0表示不限制檔案大小
        /*不同檔案圖示配置*/
        previewFileIconSettings: {
            'docx': '<i class="fa fa-file-word-o text-primary" ></i>',
            'xlsx': '<i class="fa fa-file-excel-o text-success"></i>',
            'pptx': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
            'jpg': '<i class="fa fa-file-photo-o text-warning"></i>',
            'pdf': '<i class="fa fa-file-pdf-o text-danger"></i>',
            'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
            'doc': '<i class="fa fa-file-word-o text-primary"></i>',
            'xls': '<i class="fa fa-file-excel-o text-success"></i>',
            'ppt': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
            'pdf': '<i class="fa fa-file-pdf-o text-danger"></i>',
            'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
            'htm': '<i class="fa fa-file-code-o text-info"></i>',
            'txt': '<i class="fa fa-file-text-o text-info"></i>',
            'mov': '<i class="fa fa-file-movie-o text-warning"></i>',
            'mp3': '<i class="fa fa-file-audio-o text-warning"></i>',
            'jpg': '<i class="fa fa-file-photo-o text-danger"></i>',
            'gif': '<i class="fa fa-file-photo-o text-muted"></i>',
            'png': '<i class="fa fa-file-photo-o text-primary"></i>'
        },
        layoutTemplates: { actionUpload: '' },
        /*上傳成功之後執行*/
        fileuploaded: $("#file").on("fileuploaded", function (event, data, previewId, index) {
            console.log("Upload success");
        }),

        /*上傳出錯誤處理*/
        fileerror: $('#file').on('fileerror', function (event, data, msg) {
            console.log("Upload failed")
        }),
    });

};