/*Title:派工单管理
 *Creator:丁俊杰
 * Date:2019.09.20
 */

layui.define(['form', 'util', 'table', 'laydate', 'admin', 'view', 'layer', 'layedit', 'upload', 'jquery', 'bMap'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        laydate = layui.laydate,
        view = layui.view,
        util = layui.util,
        upload = layui.upload,
        bMap = layui.bMap,
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
    var FaultArray = new Array();//传给FaultHanding
    table.render({
        elem: '#DSMShowtable',
        url: layui.setter.requesturl + '/api/DispatchSheet/ShowFaultTable',
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
        console.log(field);
        table.reload('DSMShowtable', {
            where: {
                "DSMNumber": field.DSMNumber,
                "DSMType": field.DSMType,
                "DSMStatus": field.DSMStatus,
                "StartTime": field.StarTime,
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
        Resdata.splice(0, Resdata.length);//将数据清零
        Resdata.push(data);
        Resdata.push(util.toDateString(data.DSMTime));//将时间格式规范化
        ID = obj.data.DSMID;
        Number = obj.data.DSMNumber;
        console.log(ID);
        console.log(Number);
        admin.req({
            url: layui.setter.requesturl + '/api/DispatchSheet/ShowDispatchedWorker',
            type: "post",
            data: {
            },
            success: function (resdata) {
                //监听附件
                if (event == "Enclosure") {
                    admin.req({
                        url: layui.setter.requesturl + '/api/DispatchSheet/ShowPhoto',//后台获取图片
                        type: "post",
                        data: {
                            "id": ID
                        },
                        success: function (resdata) {
                            admin.popup({
                                title: "图片弹出层",
                                area: ['700px', '500px'],
                                maxmin: true,
                                id: 'showphoto',
                                shadeClose: true, //点击遮罩关闭
                                // content: '<div id="photoForm1"></div>',
                                success: function (layero, index) {
                                    view('showphoto').render('DispatchSheetManagement/DSMShowPhoto', Number).done(function () {
                                        var rhtml = "";
                                        for (var i = 0; i < resdata.data.length; i++) {
                                            if (resdata.data[i].phototype == 1) {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[i].url}" title="表盘抄表图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--表盘抄表图片</div></div>`;
                                            }
                                            else if (resdata.data[i].phototype == 2) {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[i].url}" title="现场图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--现场图片</div></div>`;
                                            }
                                            else if (resdata.data[i].phototype == 3) {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[i].url}" title="故障处理后图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障处理后图片</div></div>`;
                                            }
                                            else if (resdata.data[i].phototype == 4) {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[i].url}" title="故障图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障图片</div></div>`;
                                            }
                                            else {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[i].url}" title="其他类型图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--其他类型图片</div></div>`;
                                            }
                                        }
                                        $("#DSMShowShowPhoto").html(rhtml);
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
                        url: layui.setter.requesturl + '/api/DispatchSheet/ShowAddress',//后台地址图片
                        type: "post",
                        data: {
                            "id": ID
                        },
                        success: function (data) {
                            admin.popup({
                                title: "地址信息",
                                area: ['1000px', '700px'],
                                maxmin: true,
                                id: 'showaddress',
                                success: function (layero, index) {
                                    view('showaddress').render('DispatchSheetManagement/DSMShowAddress', Number).done(function () {
                                        GPS(data.data);
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
                                        form.on('submit(DSMShowAcceptance_submit)', function (Data) {
                                            console.log(DispatchedWorker);
                                            LatestTime = Data.field.ShowOperation_operationtime;//最迟处理时间
                                            admin.req({
                                                url: layui.setter.requesturl + '/api/DispatchSheet/AcceptanceOperation',
                                                type: "post",
                                                data: {
                                                    "id": ID,
                                                    "Dispatchedworker": DispatchedWorker,
                                                    "Latesttime": LatestTime
                                                },
                                                success: function (RESdata) {
                                                    if (RESdata.code == 0) {
                                                        layer.msg("提交成功");
                                                        table.reload('DSMShowtable');
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

                }
                //监听处理操作
                else if (event == "DSMHandle") {
                    ID = obj.data.DSMID;
                    admin.req({
                        url: layui.setter.requesturl + '/api/DispatchSheet/ShowProcessingoperationdateinfo',
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
                                                laydate.render({
                                                    elem: '#DSMShowProcessingOperationShow_operationtime',
                                                    value: new Date(),
                                                });
                                                form.render();
                                                DealResdata.splice(0, DealResdata.length);
                                                $('#DSMShowPO_submit').click(function () {
                                                    // var str = Data.field.DSMShowProcessingAccessories;//图片文件名
                                                    var isAutoSend = document.getElementsByName('Processed');
                                                    for (var i = 0; i < isAutoSend.length; i++) {
                                                        if (isAutoSend[i].checked == true) {
                                                            var res = isAutoSend[i].value;
                                                        }
                                                    }
                                                    var files = document.getElementById('DSMShowOpdemo1');
                                                    console.log(files);
                                                    var remark = document.getElementById('Handlingsituation');
                                                    console.log(sresdata.data);
                                                    var senddata = {
                                                        "taskperiodname": sresdata.data[0].taskperiodname,
                                                        "faultid": sresdata.data[0].faultid,
                                                        "faulttype": 1,
                                                        "processpreson": DispatchedWorker,
                                                        "processdatetime": sresdata.data[0].processdatetime,
                                                        "processmark": remark.value,
                                                        "processresult": 0,
                                                        "processsource": "后台管理系统",
                                                        "meternum": sresdata.data[0].meternum
                                                    };
                                                    console.log(JSON.stringify(senddata));
                                                    FaultArray.push(senddata);
                                                    console.log(FaultArray);
                                                    admin.req({
                                                        //dataType:'json',
                                                        url: layui.setter.requesturl + '/api/DispatchSheet/Processingoperations',
                                                        data: {
                                                            "FaultHandlinglist": JSON.stringify(FaultArray)
                                                        },
                                                        type: "post",
                                                        success: function (resdata) {
                                                            if (resdata.code == 0) {
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
                //监听处理信息
                else if (event == "DSMHandleinformation") {
                    admin.req({
                        url: layui.setter.requesturl + '/api/DispatchSheet/FaultInformationDisplay',
                        data: {
                            "id": ID
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
                                            // for (var i = 0; i < resdata.data[3].length; i++) {
                                            //     console.log(resdata.data[3][0]);
                                            //$("#DSMSHOWPIphotoshow").append('<img src="' + resdata.data[3][i] + '" height="200px"/>');
                                            var rhtml = "";
                                            for (var i = 0; i < resdata.data[3].length; i++) {
                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[3][i]}" title="现场图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--现场图片</div></div>`;
                                            }
                                            $("#DSMSHOWPIphotoshow").html(rhtml);
                                            //}
                                            //for (var j = 0; j < resdata.data[4].length; j++) {
                                            //    console.log(resdata.data[4][j]);
                                                //$("#DSMShowPIpictureshow").append('<img src="' + resdata.data[4][j] + '" height="200px"/>');
                                                var rhtml2 = "";
                                                for (var j = 0; j < resdata.data[4].length; j++) {
                                                    rhtml2 += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${resdata.data[4][j]}" title="故障处理后图片"><div style="font-size:20px;color:#FF2D2D">图片${i + 1}--故障处理后图片</div></div>`;
                                                }
                                                $("#DSMShowPIpictureshow").html(rhtml2);
                                           // }
                                            form.render();

                                        });
                                    }
                                });
                            }
                        }
                    });
                }
                //监听编辑  
                else if (event == "DSMEdit") {
                    admin.popup({
                        title: "编辑页面",
                        area: ['700px', '500px'],
                        maxmin: true,
                        id: 'Editpage',
                        success: function (layero, index) {
                            view('Editpage').render('DispatchSheetManagement/DSMShowEditPage', obj.data).done(function () {
                                laydate.render({
                                    elem: '#EditReporttime',
                                    max: maxDate()
                                });
                                form.render();
                                form.on('submit(Edit_submit)', function (Data) {
                                    var senddata = {
                                        "readdataid": Data.EditNumber,
                                        "faulttype": Data.EditType,
                                        "faultcontent": Data.EditContent,
                                        "reporttime": Data.EditReporttime,
                                        "reportpeople": Data.EditReporter,
                                        "faultstatus": Data.EditStatus
                                    };
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/DispatchSheet/DSEdits',
                                        type: "post",
                                        data: {
                                            "id": ID,
                                            "data": JSON.stringify(senddata)
                                        },
                                        success: function (resdata) {
                                            if (resdata.code == 0) {
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
                //监听删除
                else if (event == "DSMDelte") {
                    admin.popup({
                        title: "删除",
                        content: "确定删除该数据吗！！！",
                        maxmin: true,
                        btn: ['提交', '取消'],
                        yes: function (layero, index) {
                            admin.req({
                                url: layui.setter.requesturl + '/api/DispatchSheet/DSDelete',
                                data: {
                                    "id": ID
                                },
                                type: "post",
                                success: function (resdata) {
                                    if (resdata.code == 0) {
                                        layer.msg("删除成功");
                                    }
                                    else {
                                        layer.msg("删除失败");
                                    }
                                }
                            });
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
    });
    laydate.render({
        elem: '#EndTime',
    });

    // 设置最小可选的日期
    function minDate() {
        var now = new Date();
        return now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate();
    }
    //设置最大可选时间
    function maxDate() {
        var now = new Date();
        return now.getFullYear() + "-" + (now.getMonth() + 1) + "-" + now.getDate();
    }
    //普通图片上传

    //PGS轨迹
    function GPS(uploadGPS) {
        //百度地图渲染
        bMap.render({
            ak: 'D2b4558ebed15e52558c6a766c35ee73'//一旦设置了全局，此（发起请求的相关）参数就会失效。
            , https: true
            , done: function () {//这种方式回调函数叫做
                var points = [];
                var data = [];
                console.log(uploadGPS.length);
                for (var i = 0; i < uploadGPS.length; i++) {
                    var obj = uploadGPS[i];
                    var ll = obj.split(",");
                    var arr = [ll[0], ll[1]];
                    data[data.length] = arr;
                }
                for (var i = 0; i < data.length; i++) {
                    points.push(new BMap.Point(data[i][0], data[i][1]));
                }
                var options = {
                    size: BMAP_POINT_SIZE_SMALL,
                    shape: BMAP_POINT_SHAPE_STAR,
                    color: '#0f0'
                }
                map = new BMap.Map("icontainer");
                map.centerAndZoom("常德", 12);
                map.enableScrollWheelZoom(true);//开启鼠标滚轮缩放
                map.addControl(new BMap.NavigationControl());
                map.addControl(new BMap.ScaleControl());
                map.addControl(new BMap.OverviewMapControl({ isOpen: true }));
                var polyline = new BMap.Polyline(points, options);
                map.addOverlay(polyline);  // 添加Overlay
                console.log(points.length);
                //添加海量点
                for (var i = 0, pointslen = points.length; i < pointslen; i++) {
                    var point = new BMap.Point(points[i].lng, points[i].lat); //将标注点转化成地图上的点
                    var marker = new BMap.Marker(point); //将点转化成标注点
                    map.addOverlay(marker);  //将标注点添加到地图上

                    //通过drivingroute获取一条路线的point
                }
            }

        });
    }

    exports('DSMSHOW', {});
});
