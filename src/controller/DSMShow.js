/*Title:派工单管理
 *Creator:丁俊杰
 * Date:2019.09.20
 */

layui.define(['form', 'util', 'table', 'laydate', 'admin', 'view', 'layer', 'layedit', 'upload', 'jquery'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        laydate = layui.laydate,
        view = layui.view,
        util = layui.util,
        upload = layui.upload,
        $ = layui.$;
    var StartTime;//开始时间
    var EndTime;//结束时间
    var ID;//faultinfo的ID
    var Number;//faultinfo 中故障编号
    var DispatchedWorker;//派工
    var LatestTime;//最迟处理时间
    var MeterReadingClerkID;//抄表员ID
    var Resdata = new Array();//存储给受理操作界面数据
    var DealResdata = new Array();//存储给操作界面数据
    table.render({
        elem: '#DSMShowtable',
        url: 'http://localhost:8081/ShowFaultTable',
        method: 'post',
        cols: [[
            { field: 'DSMID', title: '序号', width: 100, sort: true, fixed: 'left' },
            { field: 'DSMNumber', title: '故障编号', width: 150 },
            {
                field: 'DSMType', title: '故障类型', width: 100,
                templet: function (d) {
                    if (d.DSMType == 6) {
                        return "表埋"
                    }
                    else if (d.DSMType == 7) {
                        return "表坏"
                    }
                    else if (d.DSMType == 8) {
                        return "井盖坏"
                    }
                    else if (d.DSMType == 10) {
                        return "漏水"
                    }
                    else {
                        return "其他"
                    }
                }
            },
            { field: 'DSMContent', title: '故障内容', width: 150 },
            {
                field: 'DSMTime', title: '上报时间', width: 150,
                templet: function (d) {
                    return util.toDateString(d.DSMTime);
                }
            },
            { field: 'DSMReportPerson', title: '上报人', width: 100 },
            {
                field: 'DSMEnclosure', title: '附件', width: 100, event: 'Enclosure',
                templet: function (d) {
                    return '<a  style="color: #c00;text-decoration:underline">' + d.DSMEnclosure + '</a>'
                }
            },
            {
                field: 'DSMStatus', title: '状态', width: 100,
                templet: function (d) {
                    if (d.DSMStatus == 0) {
                        return "未受理"
                    }
                    else if (d.DSMStatus == 1) {
                        return "已受理"
                    }
                    else if (d.DSMStatus == 2) {
                        return "已处理"
                    }
                    else {
                        return "已存档"
                    }
                }
            },
            {
                field: 'DSMAddress', title: '位置信息', width: 100, event: 'Address',
                templet: function (d) {
                    return '<a style="color: #c00;text-decoration:underline">' + d.DSMAddress + '</a>'
                }
            },
            {
                field: 'DSMOperation', title: '操作', width: 250, fixed: 'right', align: 'center',
                templet: function (d) {
                    if (d.DSMStatus == 0) {
                        return '<a class="layui-btn layui-btn-warm " lay-event="DSMAcceptance">受理</a><a class="layui-btn layui-btn-warm " lay-event="DSMEdit">编辑</a><a class="layui-btn layui-btn-warm " lay-event="DSMDelte">删除</a>'
                    }
                    else if (d.DSMStatus == 1) {
                        return '<a class="layui-btn layui-btn-warm " lay-event="DSMHandle">处理</a><a class="layui-btn layui-btn-warm " lay-event="DSMEdit">编辑</a><a class="layui-btn layui-btn-warm " lay-event="DSMDelte">删除</a>'
                    }
                    else if (d.DSMStatus == 2) {
                        return '<a class="layui-btn layui-btn-warm " lay-event="DSMHandleinformation">处理信息</a><a class="layui-btn layui-btn-warm " lay-event="DSMEdit">编辑</a><a class="layui-btn layui-btn-warm " lay-event="DSMDelte">删除</a>'
                    }
                }
            }
        ]],
        page: true,
        limit: 10,
        limits: [10, 15, 20],
    });

    //监听查询
    form.on('submit(DSM_polling)', function (obj) {
        var field = obj.field;
        table.reload('DSMShowtable', {
            where: {
                "DSMNumber": field.DSMNumber,
                "DSMType": field.DSMType,
                "DSMStatus": field.DSMStatus,
                "StartTime": field.StartTime,
                "EndTime": field.EndTime
            }
            , page: {
                "curr": 1,
                "nums": 10
            }
        })
    });

    //监听操作
    table.on('tool(DSM)', function (obj) {
        var data = obj.data,
            event = obj.event,
            tr = obj.tr;
        Resdata.push(data);
        Resdata.push(util.toDateString(data.DSMTime));//将时间格式规范化
        ID = obj.data.DSMID;
        Number = obj.data.DSMNumber;
        console.log(ID);
        console.log(Number);
        admin.req({
            url: 'http://localhost:8081/ShowDispatchedWorker',
            type: "post",
            data: {
            },
            success: function (resdata) {
                //监听附件
                if (event == "Enclosure") {
                    admin.req({
                        url: 'http://localhost:8081/ShowPhoto',//后台获取图片
                        type: "post",
                        data: {
                            "id": ID
                        },
                        success: function (data) {
                            admin.popup({
                                title: "图片弹出层",
                                area: ['700px', '500px'],
                                maxmin: true,
                                id: 'showphoto',
                                shadeClose: true, //点击遮罩关闭
                                content: '<div id="photoForm1"></div>',
                                success: function (layero, index) {
                                    view('showphoto').render('DispatchSheetManagement/DSMShowPhoto', Number).done(function () {
                                        for (var i = 0; i < data.length; i++) {
                                            $("#imgZmList").append("<li><img src=" + data[i] + "></li>");
                                        }
                                        form.render(null, 'DSMShowOperation');
                                    });
                                }
                            });
                        }

                    });
                }
                //监听位置信息
                else if (event == "Address") {
                    admin.req({
                        url: 'http://localhost:8081/ShowAddress',//后台地址图片
                        type: "post",
                        data: {
                            "id": ID
                        },
                        success: function (data) {
                            var src = data;//后台src地址
                            admin.popup({
                                title: "地址信息",
                                area: ['700px', '500px'],
                                maxmin: true,
                                id: 'showaddress',
                                success: function (layero, index) {
                                    view('showaddress').render('DispatchSheetManagement/DSMShowAddress', Number).done(function () {
                                        for (var i = 0; i < data.length; i++) {
                                            $("#AddressimgZmList").append("<li><img src=" + data[i] + "></li>");
                                        }
                                        form.render(null, 'ShowAddress');
                                    });
                                }
                            });
                        }
                    });
                }
                //监听受理操作
                else if (event == "DSMAcceptance") {
                    if (Resdata.push(resdata.data)) {
                        if (resdata.code == 0) {
                            admin.popup({
                                title: "受理操作",
                                area: ['700px', '500px'],
                                maxmin: true,
                                id: 'AcceptanceOperation',
                                success: function (layero, index) {
                                    view('AcceptanceOperation').render('DispatchSheetManagement/DSMShowAcceptanceOperation', Resdata).done(function () {
                                        laydate.render({
                                            elem: '#ShowOperation_endtime',
                                            min: minDate()
                                        });
                                        laydate.render({
                                            elem: '#ShowOperation_operationtime',
                                            value: new Date(),
                                        });
                                        console.log(Resdata);
                                        form.render();
                                        Resdata.splice(0, Resdata.length);//将数据清零
                                        form.on('submit(DSMShowAcceptance_submit)', function (Data) {
                                            console.log(DispatchedWorker);
                                            LatestTime = Data.field.ShowOperation_operationtime;//最迟处理时间
                                            admin.req({
                                                url: 'http://localhost:8081/AcceptanceOperation',
                                                type: "post",
                                                data: {
                                                    "id": ID,
                                                    "Dispatchedworker": DispatchedWorker,
                                                    "Latesttime": LatestTime
                                                },
                                                success: function (RESdata) {
                                                    if (RESdata.code == 0) {
                                                        layer.msg("提交成功");
                                                    }
                                                    else {
                                                        layer.msg("提交失败");
                                                    }
                                                }
                                            });
                                        });

                                    });
                                }
                            });
                        }
                    }
                    //    }//2
                    //});//1
                }
                //监听处理操作
                else if (event == "DSMHandle") {
                    admin.req({
                        url: 'http://localhost:8081/ShowProcessingoperationdateinfo',
                        data: {
                            "id": ID
                        },
                        type: "post",
                        success: function (sresdata) {
                            if (sresdata.code == 0) {
                                DealResdata.push(resdata.data);                               
                                admin.popup({
                                    title: "处理操作",
                                    area: ['700px', '500px'],
                                    maxmin: true,
                                    id: 'Processingoperation',
                                    success: function (layero, index) {
                                        DealResdata.push(sresdata.data);
                                        view('Processingoperation').render('DispatchSheetManagement/DSMShowProcessingOperation', DealResdata).done(function () {
                                            console.log(DealResdata);
                                            laydate.render({
                                                elem: '#DSMShowProcessingOperationShow_operationtime',
                                                value: new Date(),
                                            });
                                            form.render();
                                            DealResdata.splice(0, DealResdata.length);
                                            form.on('submit(DSMShowPO_submit)', function (Data) {
                                                var str = Data.field.DSMShowProcessingAccessories;
                                                var isAutoSend = document.getElementsByName('Processed');
                                                for (var i = 0; i < isAutoSend.length; i++) {
                                                    if (isAutoSend[i].checked == true) {
                                                        var res = isAutoSend[i].value;
                                                    }
                                                }
                                                var remark = document.getElementById('Handlingsituation');
                                                admin.req({
                                                    url: 'http://localhost:8081/Processingoperations',
                                                    data:{
                                                        "s": str,
                                                        "id": ID,
                                                        "worker": DispatchedWorker,
                                                        "result": res,
                                                        "mark": remark.value
                                                    },
                                                    type: "post",
                                                    success: function (resdata) {
                                                        if (resdata.code==0) {
                                                            layer.msg("提交成功");
                                                            table.reload('DSMShowtable');
                                                        } else {
                                                            layer.msg("提交失败");
                                                        }
                                                    }
                                                });
                                            });
                                        });
                                    }
                                });
                            }
                        }
                    });
                }
                else if (event == "DSMHandleinformation") {
                    admin.req({
                        url: 'http://localhost:8081/FaultInformationDisplay',
                        data: {
                            "id":ID
                        },
                        type: "post",
                        success: function (resdata) {
                            if (resdata.code==0) {
                                admin.popup({
                                    title: "处理信息",
                                    area: ['700px', '500px'],
                                    maxmin: true,
                                    id: 'Processinformation',
                                    success: function (layero, index) {
                                        console.log(resdata.data);
                                        view('Processinformation').render('DispatchSheetManagement/DSMShowProcessinformation', resdata.data).done(function () {
                                            form.render();
                                        });
                                    }
                                });
                            }
                        }
                    });
                }
            }
        });
    })

    //监听受理操作中的下拉框
    form.on('select(DSMShowOperation_select)', function (data) {
        DispatchedWorker = data.value;
        console.log(data.value);
        console.log(DispatchedWorker);
    });
    //监听受理操作中的该表抄户员
    form.on('submit(DSMShowReader)', function (obj) {
        DispatchedWorker = 0;
    });
    form.on('select(DSMShowProcessingOperationShow_DealPeople)', function (data) {
        DispatchedWorker = data.value;
    })


    //时间
    laydate.render({
        elem: '#StarTime',
        min: minDate()
    });
    laydate.render({
        elem: '#EndTime',
        min: minDate()
    });
    // 设置最小可选的日期
    function minDate() {
        var now = new Date();
        return now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate();
    }
    //普通图片上传
    
    exports('DSMSHOW', {});
});
