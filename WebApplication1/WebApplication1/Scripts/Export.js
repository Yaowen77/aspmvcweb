;
(function (windows) {
    if (typeof (jQuery) === 'undefined') { alert('jQuery Library NotFound.'); return; }

    var HasData = 'False';

    /*$(function () {

        $('#ButtonExport').click(function (a) {
            console.log(a);
            ExportData();
        });

    });*/

    ExportClick = function (ConttollerName) {
        ExportData(ConttollerName);
    }

    function AlertErrorMessage(title, message) {
        /// <summary>
        /// 使用 Bootbox.js 的錯誤訊息顯示
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>

        bootbox.dialog({
            title: title,
            message: message,
            buttons: {
                danger: {
                    label: "確認",
                    className: "btn-danger"
                }
            }
        });
    }

    function ExportData(ConttollerName) {
        /// <summary>
        /// 資料匯出
        /// </summary>

        $.ajax({
            type: 'post',
            url: Router.action(ConttollerName, 'HasData'),
            dataType: 'json',
            cache: false,
            async: false,
            success: function (data) {
                if (data.Msg) {
                    HasData = data.Msg;
                    if (HasData === 'False') {
                        AlertErrorMessage("錯誤", "尚未建立任何資料, 無法匯出資料.");
                    }
                    else {
                        window.location = Router.action(ConttollerName, 'Export');
                    }
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                AlertErrorMessage("錯誤", "資料匯出錯誤");
            }
        });
    }
})
    (window);