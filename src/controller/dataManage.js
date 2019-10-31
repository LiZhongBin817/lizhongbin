/*Title:抄表数据管理
 * Creator:李忠斌
 * Date:2019.9.12
 */

layui.define(['table', 'view', 'admin', 'form', 'element', 'upload'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , element = layui.element
        , upload = layui.upload
        , admin = layui.admin
        , editData = new Array(4);
    table.render({
        elem: '#dataManageInfo_Table',
        method: 'post',
        url: layui.setter.requesturl+'/Show_CB_DataInfo',
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
                title: '操作', width: 100, align: 'center',
                templet: function (d) {
                    if (d.rtrecheckstatus == 0) {
                        return '<a style="text-decoration:none;">无 </a>';
                    }
                    else if (d.recheckstatus == 2 && d.carrystatus == null && d.rtrecheckstatus != 1) {
                        return '<button  class="layui-btn layui-btn-radius layui-btn-sm" lay-event="dataManageOpen">审核</button>';
                    }
                    else if (d.recheckstatus == 2 && d.carrystatus == null && d.rtrecheckstatus == 1) {
                        return '<button  class="layui-btn layui-btn-radius layui-btn-sm" lay-event="dataManageOpen">再次审核</button>'; '<a style="text-decoration:none;">无 </a>';
                    }
                    else {
                        return '<a style="text-decoration:none;">无 </a>';

                    }
                }
            },
        ]]
        , page: true
        , limit: 20
        , toolbar: '#toolDemo_Carry'
        , limits: [20, 30, 40]
        , done: function () {
            layer.close(load);
        }
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
                "rtrecheckstatus": field.rtrecheckstatus,
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
                url: layui.setter.requesturl+'/CarryData'
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
                        layer.msg("无符号结转要求的数据");
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
                area: ['845px', '550px'],
                title: '审核',
                success: function (layero, index) {
                    view(this.id).render('DataManage/ReCheckData', data).done(function () {

                        //声明一个变量用来接收表格里面编辑的数据
                        var editData;
                        form.render(null, 'CheckForm');

                        console.log(data.autoaccount);
                        table.render({
                            url: layui.setter.requesturl+'/Change',
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
                                        if (true) {
                                            return '<a href="http://www.baidu.com">' + d.pircture+'</a>';
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
                            load = layer.load(3);
                            admin.req({
                                url: layui.setter.requesturl+'/SubmitChecked',
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
                                    else {
                                        layer.msg("审核失败!");
                                    }
                                },
                            });
                            
                        });                        
                    });

                }
            });
        }
    });
    exports('dataManage', {});
});