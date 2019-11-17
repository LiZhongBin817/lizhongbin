/*Title:故障维修页面
 *Creator:丁俊杰
 * Date:2019.11.14
 */

layui.define(['form', 'util', 'table', 'laydate', 'admin', 'view', 'layer', 'layedit', 'upload', 'jquery'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        laydate = layui.laydate,
        view = layui.view,
        upload = layui.upload,
        $ = layui.$;
    table.render({
        elem: '#Troubletable',
        url: layui.setter.requesturl + '/api/',
        method: 'post',
        clos: [[
            { field: 'TroubleID', title: '序号', width: 100, sort: true, fixed: 'left' },
            { field: 'TroubleStatus', title: '状态', width: 150},
            { field: 'TroubleType', title: '故障种类', width: 150 },
            { field: 'TroubleReportTime', title: '上报时间', width: 150 },
            { field: 'TroubleReporter', title: '上报人', width: 150 },
            { field: 'ToubleReason', title: '故障原因', width: 200 },
            { field: 'TroubleSearch', toolbar: '#TroubleshootingShowbarDemo', width: 100, fixed: 'right', align: 'center'}
        ]],
        page: true,
        limit: 10,
        limits:[5,10,15]
    });

    //监听查询
    form.on('submit(Trouble_Seek)', function (obj) {
        var field = obj.field;
        table.reload('Troubletable', {
            where: {
                "TroubleStarTime": field.TroubleStarTime,
                "TroubleEndTime": field.TroubleEndTime,
                "TroubleType": field.TroubleType_Select,
                "autoaccount": field.Trouble_autoaccount
            },
             page: {
                "curr": 1,
                "nums": 10
            }
        });
    });

    //监听表格操作
    table.on('tool(Trouble_tab)', function (obj) {
        var data = obj.data;
        event = obj.event;
        if (event == TroubleshootingShow_Search) {
            admin.req({
                url: layui.setter.requesturl + '/api/DispatchSheet/FaultInformationDisplay',
                data: {
                    "id": data.TroubleID,
                    "DSMStatus": obj.data.DSMStatus
                },
                type: "post",
                success: function (resdata) {
                    if (resdata.code == 0) {
                        admin.popup({
                            title: "处理信息",
                            area: ['700px', '500px'],
                            maxmin: true,
                            id: 'Processinformation',
                            success: function (layero, index) {
                                console.log(resdata.data);
                                view('Processinformation').render('DispatchSheetManagement/DSMShowProcessinformation', resdata.data).done(function () {
                                    //隐藏提交按钮
                                        $("#DSMShowProcessinformation_submit").attr("style", "display:none;");
                                    //单选框的值
                                    if (resdata.data[5].length != 0) {
                                        if (resdata.data[5][0].processresult == 0) {
                                            var result = document.getElementById('ProcessResult_pass');
                                            result.checked = true;
                                        }
                                        else {
                                            var result = document.getElementById('ProcessResult_unpass');
                                            result.checked = true;
                                        }
                                    }
                                    var rhtml = "";
                                    for (var i = 0; i < resdata.data[3].length; i++) {
                                        if (resdata.data[3][i].phototype == 1) {
                                            rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i].url}" title="表盘抄表图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--表盘抄表图片</div></div>`;
                                        }
                                        else if (resdata.data[3][i].phototype == 2) {
                                            rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i].url}" title="现场图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--现场图片</div></div>`;
                                        }
                                        else if (resdata.data[3][i].phototype == 3) {
                                            rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i].url}" title="故障处理后图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障处理后图片</div></div>`;
                                        }
                                        else if (resdata.data[3][i].phototype == 4) {
                                            rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i].url}" title="故障图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障图片</div></div>`;
                                        }
                                        else {
                                            rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i].url}" title="其他类型图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--其他类型图片</div></div>`;
                                        }
                                    }
                                    $("#DSMSHOWPIphotoshow").html(rhtml);
                                    var rhtml2 = "";
                                    for (var j = 0; j < resdata.data[4].length; j++) {
                                        rhtml2 += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[4][j]}" title="故障处理后图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障处理后图片</div></div>`;
                                    }
                                    $("#DSMShowPIpictureshow").html(rhtml2);
                                    form.render();
                                });
                            }
                        });
                    }
                }
            });

        }
    });

    exports('TroubleshootingShow', {});
});