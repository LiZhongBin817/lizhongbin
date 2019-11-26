/* title：结转数据管理
  * creator: 李忠斌
  * date: 2019.10.19
  * 
 */

layui.define(['table', 'view', 'form', 'admin', 'jquery'], function (exports) {
    var table = layui.table,
        admin = layui.admin,
        load = layer.load(3),
        form = layui.form,
        view = layui.view,
        $ = layui.jquery;

    var quanju = new Array();//全局
    var huancun = new Array();//缓存
    //表格渲染
    table.render({
        elem: '#CarryDataShowTable',
        url: layui.setter.requesturl + '/api/CarryOverDataManage/ShowCarriedData',
        type: 'get',
        cols: [[
            { title: '序号', type: 'numbers', width: 50, fixed: 'left' },
            { type: 'checkbox', width: 50, fixed: 'left' },
            { field: 'account', title: '用户编号', width: 100 },
            { field: 'username', title: '户名', width: 100 },
            {
                title: '用户地址', width: 250,
                templet: function (d) {
                    var Value = "";
                    Value = d.regionname + d.areaname + d.address;
                    return Value;
                }
            },
            { field: 'meternum', title: '水表编号', width: 100 },
            { field: 'naturename', title: '用水类型', width: 100 },
            { field: 'taskperiodname', title: '结转月份', width: 100 },
            { field: 'bookkeepingcount', title: '记账用量', width: 100 },
            { field: 'adjustwatercount', title: '用量调整', width: 100 },
            { field: 'adjustremark', title: '调整原因', width: 150 },
            { field: 'carrywatercount', title: '结转用量', width: 100 },
            { field: 'startnum', title: '结转起码', width: 100 },
            { field: 'starttime', title: '起码抄表时间', width: 200 },
            { field: 'endnum', title: '结转止码', width: 100 },
            { field: 'endtime', title: '止码抄表时间', width: 200 },
            {
                title: '操作', width: 350, fixed: 'right', align: 'center',
                templet: "#CarrtOverData_btnContainer"
            }
        ]]
        , toolbar: '#headerbutton'
        , page: true
        , height: $(document).height() - $('#CarryDataShowTable').offset().top - 280
        , limit: 10
        , limits: [5, 10, 15]
        , id: 'studentTable'
        , done: function (res, curr, count) {
            layer.close(load);
            currPage = curr;
            var that = this.elem.next();

            $.each(res.data, function (index, obj) {
                // console.log(obj.waterdifference);
                if (obj.carrywatercount <0) {
                    that.find(".layui-table-box tbody tr[data-index='" + index + "']").css("background-color", "#ff7575");

                }

            })

            //数据表格加载完成时调用此函数
            //如果是异步请求数据方式，res即为你接口返回的信息。
            //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度
            //设置全部数据到全局变量
            quanju = res.data;

            console.log(huancun);
            //在缓存中找到id ,然后设置data表格中的选中状态
            //循环所有数据，找出对应关系，设置checkbox选中状态
            for (var i = 0; i < res.data.length; i++) {
                for (var j = 0; j < huancun.length; j++) {
                    //数据id和要勾选的id相同时checkbox选中
                    if (res.data[i].account == huancun[j]) {
                        //这里才是真正的有效勾选
                        res.data[i]["LAY_CHECKED"] = 'true';
                        //找到对应数据改变勾选样式，呈现出选中效果
                        var index = res.data[i]['LAY_TABLE_INDEX'];
                        $('.layui-table tr[data-index=' + index + '] input[type="checkbox"]').prop('checked', true);
                        $('.layui-table tr[data-index=' + index + '] input[type="checkbox"]').next().addClass('layui-form-checked');
                    }
                }
            }
            //设置全选checkbox的选中状态，只有改变LAY_CHECKED的值， table.checkStatus才能抓取到选中的状态
            //设置全选checkbox的选中状态，只有改变LAY_CHECKED的值， table.checkStatus才能抓取到选中的状态
            var checkStatus = table.checkStatus('studentTable');//这里的studentTable是指分页中的id
            if (checkStatus.isAll) {//是否全选
                //layTableAllChoose
                $('.layui-table th[data-field="0"] input[type="checkbox"]').prop('checked', true);//data-field值默认为0，如果在分页部分自定义了属性名，则需要改成对应的属性名
                $('.layui-table th[data-field="0"] input[type="checkbox"]').next().addClass('layui-form-checked');//data-field值默认为0，如果在分页部分自定义了属性名，则需要改成对应的属性名
            }           
        }

    });


    //复选框选中监听,将选中的id 设置到缓存数组,或者删除缓存数组
    table.on('checkbox(CarryDataShow)', function (obj) {
    
        if (obj.checked == true) {
            if (obj.type == 'one') {
                huancun.push(obj.data.account);
            } else {
                for (var i = 0; i < quanju.length; i++) {
                    huancun.push(quanju[i].account);
                }
            }
        } else {
            if (obj.type == 'one') {
                for (var i = 0; i < huancun.length; i++) {
                    if (huancun[i] == obj.data.account) {
                        removeByValue(huancun, huancun[i]);//调用自定义的根据值移除函数
                    }
                }
            } else {
                for (var i = 0; i < huancun.length; i++) {
                    for (var j = 0; j < quanju.length; j++) {
                        if (huancun[i] == quanju[j].account) {
                            removeByValue(huancun, +huancun[i]);//调用自定义的根据值移除函数
                        }
                    }
                }
            }
        }
    });

    //监听表格中的按钮
    table.on('tool(CarryDataShow)', function (obj) {
        var event = obj.event,
            data = obj.data;
        console.log(data);
        if (event === "Change") {
            admin.popup({
                id: 'AdjustCarryCount',
                title: '用量调整',
                area: ['700px', '600px'],
                success: function (layero, index) {
                    view('AdjustCarryCount').render('CarryOverDataManage/AdjustCarryOverCount', data).done(function () {
                        form.render(null, 'adjustForm');
                        console.log(data);

                        //监听提交按钮
                        form.on('submit(adjustDataSubmit)', function (obj) {
                            var field = obj.field;
                            var SendData = {
                                "adjustwatercount": field.adjustcounts,
                                "adjustremark": field.adjustResults + ":" + field.AdjustType,
                                "adjustperson": field.adjustPerson,
                                "adjusttime": field.adjustDate,
                            }

                            if (!field.AdjustType) {
                                layer.msg("请选中调整状态！！");
                            }
                            else {
                                admin.req({
                                    url: layui.setter.requesturl + '/api/CarryOverDataManage/ChangeCarryCounts',
                                    method: 'post',
                                    data: {
                                        "accounts": data.account,
                                        "JsonData": JSON.stringify(SendData)
                                    },
                                    success: function (d) {
                                        if (d.msg == "ok") {
                                            layer.msg("调整成功");
                                            table.reload('CarryDataShowTable');
                                        }
                                        else {

                                            layer.msg("调整失败");
                                        }
                                    }
                                });
                            }
                           
                        });

                        //监听关闭按钮

                        form.on('submit(adjustDataSubmitClose)', function () {
                            layer.close(index);
                        });
                    });
                }
            });
        }
        if (event === "ReCarryOver") {
            admin.popup({
                id: 'ReCarryOver',
                title: '重新结转',
                area: ['700px', '600px'],
                success: function (layero, index) {
                    view('ReCarryOver').render('CarryOverDataManage/ReCarryOver', data).done(function () {
                        form.render(null, 'reCarryForm');
                        console.log(data);

                        //监听提交按钮
                        form.on('submit(ReCarryDataSubmit)', function (obj) {
                            var field = obj.field;
                            var SendData = {
                                "turndatainfo": field.carryInfo,
                                "turndate": field.CarryDate,
                                "finishturnstatus": field.CarryStatus,
                            }
                            if (!field.CarryStatus) {
                                layer.msg("请选中通过状态！！");
                            }
                            else {
                                admin.req({
                                    url: layui.setter.requesturl + '/api/CarryOverDataManage/ReCarryOver',
                                    method: 'post',
                                    data: {
                                        "account": data.account,
                                        "meternum": data.meternum,
                                        "taskperiodname": data.taskperiodname,
                                        "JsonData": JSON.stringify(SendData)
                                    },
                                    success: function (d) {
                                        if (d.msg == "ok") {
                                            layer.msg("重新结转成功");
                                            table.reload('CarryDataShowTable');
                                        }
                                        else {

                                            layer.msg("重新结转失败");
                                        }
                                    }
                                });
                            }
                           
                        });

                        //监听关闭按钮
                        form.on('submit(ReCarryDataSubmitClose)', function () {
                            layer.close(index);
                        });
                    });
                }
            });
        }

    });

    //监听工具栏操作
    table.on('toolbar(CarryDataShow)', function (data) {             
        data.account = huancun.join(',');
        console.log(data.account);

        admin.popup({
            id: 'AdjustCarryCount',
            title: '用量调整',
            area: ['700px', '600px'],
            success: function (layero, index) {
                view('AdjustCarryCount').render('CarryOverDataManage/AdjustCarryOverCount', data).done(function () {
                    form.render(null, 'adjustForm');
                    console.log(data);

                    //监听提交按钮
                    form.on('submit(adjustDataSubmit)', function (obj) {
                        var field = obj.field;
                        console.log(field.AdjustType);
                        var SendData = {
                            "adjustwatercount": field.adjustcounts,
                            "adjustremark": field.adjustResults + ":" + field.AdjustType,
                            "adjustperson": field.adjustPerson,
                            "adjusttime": field.adjustDate,
                        }
                        admin.req({
                            url: layui.setter.requesturl + '/api/CarryOverDataManage/ChangeCarryCounts',
                            method: 'post',
                            data: {
                                "accounts": huancun,
                                "JsonData": JSON.stringify(SendData)
                            },
                            success: function (d) {
                                if (d.msg == "ok") {
                                    layer.msg("调整成功");
                                    table.reload('CarryDataShowTable');
                                }
                                else {

                                    layer.msg("调整失败");
                                }
                            }
                        });
                    });

                    //监听关闭按钮

                    form.on('submit(adjustDataSubmitClose)', function () {
                        layer.close(index);
                    });
                });
            }
        });

    });

    //自定义方法，根据值去移除
    function removeByValue(arr, val) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == val) {
                arr.splice(i, 1);
                break;
            }
        }
    }

    exports('CarryOverDataManage', {});
});