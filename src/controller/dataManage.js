/*Title:抄表数据管理
 * Creator:李忠斌
 * Date:2019.9.12
 */

layui.define(['table', 'view', 'admin', 'form', 'element', 'upload'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , element = layui.element
        , upload = layui.upload
        , admin = layui.admin
        , editData = new Array(4);
    table.render({
        elem: '#dataManageInfo_Table',
        method: 'post',
        url: layui.setter.requesturl +'/api/MRManage/Show_CB_DataInfo',
        cols: [[
            { title: '序号', width: 60, type: 'numbers' },
            {
                title: '用户信息', width: 120,
                templet: function (d) {
                    return '<a style="text-decoration:none;">' + d.account + '<br>' + d.username + '<br>' + d.telephone + ' </a>'
                }
            },
            {
                title: '水表信息', width: 120,
                templet: function (d) {
                    return '<a style="text-decoration:none;">' + d.meternum + '<br>' + d.metername + '<br>' + d.address + ' </a>';
                }
            },
            {
                title: '区域地址', width: 100,
                templet: function (d) {
                    return '<a style="text-decoration:none;">' + d.regionname + '<br>' + d.areaname + ' </a>';
                }
            },
            {
                title: '抄表信息', width: 100,
                templet: function (d) {
                    return '<a style="text-decoration:none;">抄表员:<br>' + d.mrreadername + '<br>抄表册:<br>' + d.bookname + '' + d.bookno + ' </a>';
                }
            },
            {
                title: '最近读数', width: 120,
                templet: function (d) {
                    return '<a style="text-decoration:none;">' + d.the_month_before_last + '月份:' + d.lastmonthdata + '<br>' + d.last_month + '月份:' + d.nowmonthdata + ' </a>';
                }
            },
            {
                title: '本期读数', width: 120,
                templet: function (d) {
                    if (d.readtype == 1) {
                        return '<a style="text-decoration:none;">状态:' + '实抄' + '<br>上传读数:' + d.inputdata + '<br>图像识别:' + d.ocrdata + ' </a>';
                    }
                    else if (d.readtype == 2) {
                        return '<a style="text-decoration:none;">状态:' + '估抄' + '<br>上传读数:' + d.inputdata + '<br>图像识别:' + d.ocrdata + ' </a>';
                    }
                    else if (d.readtype == 3) {
                        return '<a style="text-decoration:none;">状态:' + '异常' + '<br>上传读数:' + d.inputdata + '<br>图像识别:' + d.ocrdata + '<br>' + d.recheckresult + ' </a>';
                    }
                    else {
                        return '<a style="text-decoration:none;">状态:' + '正常' + '<br>上传读数:' + d.inputdata + '<br>图像识别:' + d.ocrdata + ' </a>';
                    }
                }
            },
            {
                title: 'GPS位置', width: 160,
                templet: function (d) {
                    return '<a style="text-decoration:none;">' + d.uploadgisplace + ' </a>';
                }
            },
            {
                title: '审核情况', width: 180,
                templet: function (d) {
                    if (d.rtrecheckstatus == null) {
                        return '<a style="text-decoration:none;">未审核</a>';
                    }
                    else if (d.checkor == 0 && d.rtrecheckstatus == 0) {
                        return '<a style="text-decoration:none;">已审核<br>系统自动<br>' + d.checksuccesstime + '</a>';
                    }
                    else if (d.checkor == 1 && d.rtrecheckstatus == 0) {
                        return '<a style="text-decoration:none;">已审核<br>审核人:' + d.FUserName+'<br>' + d.checksuccesstime + '</a>';
                    }
                    else if (d.rtrecheckstatus == 1) {
                        return '<a style="text-decoration:none;">审核未通过<br>审核人:' + d.FUserName+'<br>' + d.recheckresult + '<br>' + d.checktime + '</a>';
                    }
                }
            },
            {
                title: '结转状态', width: 180,
                templet: function (d) {
                    if (d.carrystatus == null) {
                        return '<a style="text-decoration:none;">未结转 </a>';
                    }
                    else if (d.carrystatus == 1 && d.rtrecheckstatus == 0) {
                        return '<a style="text-decoration:none;">已结转<br>' + d.carryime + '</a>';

                    }
                    else if (d.carrystatus == 2 && d.rtrecheckstatus == 0) {
                        return '<a style="text-decoration:none;">异常 </a>';
                    }
                }
            },
            {
                title: '操作', width: 200, align: 'center',
                templet: "#dataManage_btn"
            },
        ]]
        , page: true
        , limit: 10
        , toolbar: '#toolDemo_Carry'
        , height: $(document).height() - $('#dataManageInfo_Table').offset().top - 290
        , limits: [5, 10, 15]
    });
    //监听查询
    form.on('submit(DataInfoSearch)', function (obj) {
        var field = obj.field;
        table.reload('dataManageInfo_Table', {
            where: {
                "account": field.account,
                "meternum": field.meternum,
                "username": field.username,
                "address": field.address,
                "mrreadername": field.mrreadername,
                "recheckstatus": field.recheckstatus,
                "bookno": field.bookno,
                "page": 1,
            }
        });
    });

    //监听结转按钮
    table.on('toolbar(dataManageInfo)', function (obj) {
        var event = obj.event;

        if (event == "Start_Carry") {
            $.ajax({
                url: layui.setter.requesturl +'/api/MRManage/CarryData'
                , method: 'get'
                , beforeSend: function () {
                    //设置2%进度
                    element.progress('demo', '2%')
                },
                complete: function () {
                    // 设置 进度条到100%
                    element.progress('demo', '100%')
                },
                success: function (obj) {
                    // 渲染页面
                    // 进度到100%
                    element.progress('demo', '100%');
                    if (obj.msg == "OK") {
                        layer.msg("结转成功");
                        table.reload('dataManageInfo_Table');
                    }
                    else if (obj.msg == "Over") {
                        layer.msg("审核通过了的已经结转完毕");
                    }
                    else {
                        layer.msg("无符合结转要求的数据");
                    }
                }
            });
        }
    });

    //监听操作中的按钮
    table.on('tool(dataManageInfo)', function (obj) {
        var event = obj.event,
            data = obj.data;
        if (event === "dataManageOpen") {                  
            admin.popup({
                id: 'ReCheckData',
                area: ['845px', '600px'],
                title: '审核',
                success: function (layero, index) {
                    view(this.id).render('DataManage/ReCheckData', data).done(function () {

                        //声明一个变量用来接收表格里面编辑的数据
                        var editData;
                        form.render(null, 'CheckForm');

                        console.log(data.autoaccount);
                        table.render({
                            url: layui.setter.requesturl +'/api/MRManage/Change',
                            type: 'get',
                            where: {
                                'JsonData': data.lastmonthdata+'/'+','+data.nowmonthdata + '/' + ','+data.inputdata + '/',
                                'taskperiodname': data.taskperiodname,
                                'autoaccount': data.autoaccount
                            },
                            elem: '#rechecktable',
                            cols: [[
                                { field: 'month', title: '月份', width: 150, },
                                { field: 'updateData', title: '上传数据', width: 150 },
                                { field: 'ocrData', title: '图像识别', width: 150 },
                                { field: 'recheckdata', title: '复审读数', width: 150, edit: true },
                                {
                                    field: 'pircture', title: '图片', width: 165,
                                    templet: function (d) {
                                        if (d.pircture != "") {
                                            return `<a  lay-event="seephoto">点击查看图片</a>`;
                                        }
                                        else {
                                            return '<a>' + "暂无图片" + '</a>';
                                        }
                                    }
                                   
                                }
                            ]]

                          
                           
                        });
                        //监听单元格编辑
                        table.on('edit(rechecktable)', function (d) {
                            editData = d.value;
                        });
                        //监听确认审核按钮
                        form.on('submit(SubmitChecked)', function (data1) {
                            var Data = data1.field;
                            console.log(data);
                            if (!Data.Pass) {
                                layer.msg("请选中是否通过！！")
                            }
                            else {
                                admin.req({
                                    url: layui.setter.requesturl + '/api/MRManage/SubmitChecked',
                                    method: 'post',
                                    data: {
                                        'JsonData': JSON.stringify(data),
                                        'RecheckData': editData,
                                        'RecheckStatus': Data.Pass,
                                        'result': Data.checked
                                    },
                                    success: function (d) {
                                        layer.close(load);
                                        if (d.msg == "ok") {
                                            layer.msg("审核成功!");
                                            layer.close(index);
                                            table.reload('dataManageInfo_Table');
                                        }
                                        else if (d.msg == "NO") {
                                            layer.alert("初次审核,请检查您是否有填写审核数据!");
                                        }
                                    },
                                });
                            }
                           
                            
                        });                        
                    });

                }
            });
        }
        if (event === "SeeRecheckHistoryData") {
            admin.popup({
                id: 'RecheckHistoryData',
                title: '审核历史记录表',
                area: ['1200px', '500px'],
                success: function (d) {
                    view(this.id).render('DataManage/RecheckHistoryData', data).done(function () {
                        form.render(null, 'RecheckHistoryData');
                        console.log(data.autoaccount);

                        //表格渲染
                        table.render({
                            elem: '#RecheckHistoryDataTable',
                            url: layui.setter.requesturl + '/api/MRManage/ShowHistoryRecheckData',
                            method: 'post',
                            where: {
                                "autoaccount": data.autoaccount
                            },
                            cols: [[
                                { field: 'taskperiodname', title: '月份', width: 120 },
                                { field: 'inputdata', title: '上传数据', width: 120 },
                                { field: 'ocrlogdata', title: '图像识别', width: 120 },
                                { field: 'recheckdata', title: '复审读数', width: 120 },
                                {
                                    field: 'pirctureurl', title: '图片', width: 150,
                                    templet: function (d) {
                                        if (d.pirctureurl != "") {
                                            return `<a  lay-event="seehistoryphoto">点击查看图片</a>`;
                                        }
                                        else {
                                            return '<a>' + "暂无图片" + '</a>';
                                        }
                                    }
                                },
                                {
                                    field: 'recheckstatus', title: '状态', width: 120,
                                    templet: function (d) {
                                        console.log(d.recheckstatus);
                                        var contents = "";
                                        if (d.recheckstatus == 0) {
                                            contents = "已审";
                                        }
                                        else if (d.recheckstatus == 1) {
                                            contents = "不通过";
                                        }
                                        return contents;
                                    }
                                },
                                { field: 'createtime', title: '时间', width: 180 },
                                { field: 'remark', title: '备注', width: 250 },
                            ]]
                            , page: true
                            , limit: 5
                            , limits: [5, 10, 15]
                        });
                    })
                }
            });
        }
    });

    exports('dataManage', {});
});