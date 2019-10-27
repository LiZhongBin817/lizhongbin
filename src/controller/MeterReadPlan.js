/*Title:接口管理
 *Creator:丁俊杰
 * Date:2019.09.07
 */
layui.define(['form', 'util', 'table', 'admin', 'view', 'layer', 'laydate', 'carousel', 'index', 'layedit'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        view = layui.view,
        util = layui.util,
        $ = layui.$,
        laydate = layui.laydate;

    var accounts = new Array(),//选中的用户编号
        table_data = new Array();//存储当前页面的数据，用于实现页面跳转后复选框勾选保留
    var status;//下拉框的值
    var planid;//任务单ID
    var TaskID;//taskid
    var selectreader;//抄表员下拉框的值
    var DATA = new Array();//传给MeterReaderPlanPage的数据


    table.render({
        elem: '#MeterReading',
        method: 'post',
        url: layui.setter.requesturl + '/ShowPlan',//表格渲染后台地址
        page: true,
        cols: [[
            { field: 'ID', title: '序号', width: 100, sort: true, fixed: 'left' },
            { field: 'Company', title: '所属公司', width: 150 },
            { field: 'Year', title: '所属年', width: 100 },
            { field: 'Month', title: '所属月份', width: 200 },
            {
                field: 'StartTime', title: '起抄时间', width: 200,
                templet: function (d) {
                    return util.toDateString(d.StartTime);
                }
            },
            {
                field: 'EndTime', title: '止抄时间', width: 200,
                templet: function (d) {
                    return util.toDateString(d.EndTime);
                }
            },
            { title: '操作', width: 250, fixed: 'right', align: 'center', toolbar: '#Show_barDemo' }
        ]],
        limit: 10,
        toolbar: '#Show_toolbarDemo',
        limits: [10, 20, 30],
    });

    //监听添加计划单
    table.on('toolbar(MR)', function (obj) {
        var Event = obj.event;
        if (Event == 'AddPlan') {
            admin.popup({
                id: 'AddPlanLayer',
                title: '添加抄表计划',
                area: ['700px', '500px'],
                maxmin: true,
                success: function (layero, index) {
                    view('AddPlanLayer').render('MeterReadingManage/MeterReadingPlan_Manage/AddMeter', null).done(function () {
                        form.render(null, 'AddMeter');
                        form.on('submit(AddMeter_submit)', function (Data) {
                            console.log("点击成功");
                            console.log(Data.field);
                            var fields = Data.field;
                            var SendData = {
                                "mplanname": fields.Add_Company,//所属公司
                                "mplanyear": fields.Add_Year,//所属年
                                "mplanmonth": fields.Add_Month,//所属月
                                "planstarttime": fields.Add_StartTime,//起超时间
                                "planendtime": fields.Add_EndTime,//止抄时间
                            };
                            var jsonData = JSON.stringify(SendData);
                            console.log(SendData);
                            console.log(jsonData);
                            admin.req({
                                url: layui.setter.requesturl + '/AddPlan',//后台地址
                                type: "post",
                                data: {
                                    "data": jsonData,
                                },
                                success: function (data) {
                                    //传值成功
                                    if (data.code == 1) {
                                        layer.msg("抄表计划存在");
                                    }
                                    else if (data.code == 0) {
                                        layer.msg("添加成功");
                                        table.reload('MeterReading');
                                    }
                                    else {
                                        layer.msg("请按正确格式输入日期：如 2019-09-09");
                                    }
                                }
                            });
                        });
                    });
                }
            });
        }
    });

    //监听计划单表格中的操作
    table.on('tool(MR)', function (obj) {
        var Event = obj.event;
        planid = obj.data.ID;
        console.log(planid);
        //监听查看
        if (Event === 'ShowMeter') {
            admin.popup({
                title: '查看抄表计划',
                area: ['1200px', '600px'],
                maxmin: true,
                success: function (layero, index) {
                    view(this.id).render('MeterReadingManage/MeterReadingPlan_Manage/ShowMeterPlan', null).done(function () {
                        //渲染任务单表格
                        table.render({
                            elem: '#MaterReaderPlan',
                            method: 'post',
                            url: layui.setter.requesturl + '/ShowMeterReadingBooks',
                            page: true,
                            cols: [[
                                { field: 'MRID', title: '序号', width: 100, sort: true, fixed: 'left' },
                                { field: 'MRNumber', title: '抄表册编号', width: 150 },
                                { field: 'MRName', title: '抄表册名称', width: 150 },
                                { field: 'MRPeople', title: '抄表员', width: 100 },
                                { field: 'MRMonth', title: '所属月份', width: 100 },
                                {
                                    field: 'MRStartTime', title: '起抄时间', width: 150,
                                    templet: function (d) {
                                        return util.toDateString(d.MRStartTime);
                                    }
                                },
                                {
                                    field: 'MREndTime', title: '止抄时间', width: 150,
                                    templet: function (d) {
                                        return util.toDateString(d.MREndTime);
                                    }
                                },
                                { field: 'MRTaskStatus', title: '任务状态', width: 100 },
                                { title: '编辑', width: 150, toolbar: '#MRbarDemo', align: 'center', fixed: 'right' }
                            ]],
                            limit: 10,
                            limits: [10, 20, 30],
                            toolbar: '#MRtoolbarDemo',
                        });
                    });
                }
            });
        }
        //监听分配
        else if (Event === 'DistributionMeterReading') {
            admin.popup({
                title: '分配抄表册',
                area: ['900px', '500px'],
                maxmin: true,
                btn: ['确认', '取消'],
                yes: function (layero) {
                    admin.req({
                        url: layui.setter.requesturl + '/AllocationOfData',
                        type: "post",
                        data: {
                            "data": accounts,
                            "planid": planid
                        },
                        success: function (data) {
                            if (data.msg == "ok") {
                                layer.msg("分配成功");
                            }
                            else {
                                layer.msg("分配失败");
                            }
                        }
                    });
                },
                success: function (layero, index) {
                    view(this.id).render('MeterReadingManage/MeterReadingPlan_Manage/DistributionMeterReading', null).done(function () {
                        //渲染分配抄表册表格
                        form.render(null, 'DMR_form');
                        table.render({
                            elem: '#DistributionMeterReading',
                            method: "post",
                            url: layui.setter.requesturl + '/DistributionOfMeterReadingBooks',//后台地址
                            cols: [[
                                { type: 'checkbox' },
                                { field: 'Show_ID', title: 'ID', sort: true },
                                { field: 'Show_Number', title: '抄表册编号' },
                                { field: 'Show_MeterReading', title: '抄表册名称' },
                                { field: 'Show_Status', title: '分配状态' }
                            ]],
                            page: true,
                            limit: 10,
                            limits: [10, 20, 30],
                            toolbar: '#DMRtoolbarDemo',
                            done: function (res) {//实现分页后复选框保留功能
                                table_data = res.data;
                                //监听分配抄表册表格中复选框
                                table.on('checkbox(DMR)', function (obj) {
                                    if (obj.checked == true) {
                                        if (obj.type == 'one') {
                                            accounts.push(obj.data.Show_Number);
                                        }
                                        else {
                                            console.log(table_data);
                                            for (var i = 0; i < table_data.length; i++) {
                                                for (var j = 0; j < accounts.length; j++) {
                                                    if (accounts[j] == table_data[i].Show_Number) {//这个不写会有重复数据
                                                        accounts.splice(j, 1);
                                                    }
                                                }
                                                accounts.push(table_data[i].Show_Number);
                                            }
                                        }
                                    }
                                    else {
                                        //单选去勾
                                        if (obj.type == 'one') {
                                            for (var i = 0; i < accounts.length; i++) {
                                                if (accounts[i] == obj.data.Show_Number) {
                                                    accounts.splice(i, 1);
                                                }
                                            }
                                        }
                                        //多选去勾
                                        else {
                                            for (var i = 0; i < accounts.length; i++) {
                                                for (var j = 0; j < table_data.length; j++) {
                                                    if (accounts[i] == table_data[j].Show_Number) {
                                                        accounts.splice(i, 1);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                });
                                //.假设你的表格指定的 id="DistributionMeterReading"，找到框架渲染的表格
                                var tbl = $('#DistributionMeterReading').next('.layui-table-view');
                                // 渲染选择框
                                for (var i in table_data) {
                                    for (var j in accounts) {
                                        if (table_data[i].Show_Number == accounts[j]) {
                                            tbl.find('table>tbody>tr').eq(i).find('td').eq(0).find('input[type=checkbox]').prop('checked', true);
                                        }
                                    }
                                }

                            }
                        });
                    });
                }
            });
        }
    });

    //监听分配选拉框值
    form.on('select(DMR_DistributionStatus)', function (data) {
        status = data.value;
    });
    //监听查看抄表计划中的编辑
    table.on('tool(MRP)', function (obj) {
        var data = obj.data,
            $ = layui.$,
            Event = obj.event;
        TaskID = data.MRID;
        if (Event === 'MRedit') {
            //抄表人员下拉框
            DATA.splice(0, DATA.length);
            DATA.push(data);
            admin.req({
                url: layui.setter.requesturl + '/ShowSelect',
                type: "post",
                data: {
                },
                success: function (data) {
                    if (data.code == 0) {
                        reader = data.data;
                        DATA.push(reader);
                    }
                    admin.popup({
                        title: '编辑抄表册',
                        area: ['700px', '500px'],
                        maxmin: true,
                        id: 'edit1',
                        success: function (layero, index) {
                            view('edit1').render('MeterReadingManage/MeterReadingPlan_Manage/MeterReadPlanPage', DATA).done(function () {
                                console.log(DATA);
                                //时间
                                laydate.render({
                                    elem: '#MRPPStartTime' //指定元素
                                });
                                laydate.render({
                                    elem: '#MRPPEndTime',
                                });
                                laydate.render({
                                    elem: '#StartDownLoadTime',
                                });
                                laydate.render({
                                    elem: '#EndDownLoadTime',
                                });
                                form.render(null, 'MeterReadPlanPage');
                                //监听提交
                                form.on('submit(MRPP_submit)', function (Data) {
                                    var fields = Data.field;
                                    var SendData = {
                                        "mrreadername": selectreader,
                                        "bookname": fields.MeterReadBook,
                                        "mplanmonth": fields.MonthMonth,
                                        "taskstarttime": fields.MRPPStartTime,
                                        "taskendtime": fields.MRPPEndTime,
                                        "downloadstarttime": fields.StartDownLoadTime,
                                        "downloadendtime": fields.EndDownLoadTime
                                    };
                                    var jsonData = JSON.stringify(SendData);
                                    admin.req({
                                        url: layui.setter.requesturl + '/ShowTaskEdit',
                                        type: "post",
                                        data: {
                                            "senddata": jsonData,
                                            "ID": TaskID
                                        },
                                        success: function (data) {
                                            if (data.code == 0) {
                                                layer.msg("编辑成功");
                                                table.reload('MaterReaderPlan', {});
                                            }
                                            else {
                                                layer.msg("编辑失败");
                                            }
                                        }
                                    });
                                });
                            });
                        },
                    });
                }
            });
        }
    });

    //监听查询分配状态
    form.on('submit(DMR_polling)', function (Data) {
        var fields = Data.field;
        table.reload('DistributionMeterReading', {
            where: {
                "status": status,
                //"planid":planid
            }
            , page: {
                "curr": 1,
                "nums": 10
            }
        });
    });

    //监听任务单表格中查询
    form.on('submit(ShowMeterPlan_polling)', function (obj) {
        var field = obj.field;
        table.reload('MaterReaderPlan', {
            where: {
                "bookno": field.MeterNumber,
                "bookname": field.MeterName,
                "reader": field.MeterReader,
            }
            , page: {
                "curr": 1,
                "nums": 10
            }
        })
    });

    //监听抄表员下拉框的值
    form.on('select(MRPMeterReader)', function (data) {
        selectreader = data.value;
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

    exports('MeterReadPlan', {})
});