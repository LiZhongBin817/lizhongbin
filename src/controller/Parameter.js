/*Title:参数设置
 * Creator:李忠斌
 * Date:2019.8.19
 */
layui.define(['table', 'form', 'view'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , form = layui.form
        , view = layui.view
        , load = layer.load(3)
        , admin = layui.admin;
    //表格渲染
    table.render({
        elem: '#parameterdata'
        , method: 'Get'
        , url: 'http://192.168.1.32:8088/ParameterShow'
        , cols: [[
            { field: 'ID', title: 'ID', width: 50, fixed: 'left' },
            { field: 'parameternumber', title: '参数编号', width: 150 },
            { field: 'parametername', title: '参数名称', width: 150 },
            { field: 'parametertype', title: '参数类型', width: 150 },
            { field: 'parametertypename', title: '参数类型名', width: 150 },
            { field: 'parameterkey', title: 'Code', width: 150 },
            { field: 'parametervalue', title: '参数值', width: 150 },
            { field: 'Remark', title: '描述', width: 150 },
            { title: '操作', width: 125, toolbar: '#barDemo0', align: 'center', fixed: 'right' }
        ]]
        , page: true
        , limit: 20
        , toolbar: '#toolbarDemo1'
        , limits: [20, 30, 40]
        , done: function () {
            layer.close(load);
        }
    });

    //监听表格查询
    form.on('submit(search)', function (obj) {
        var field = obj.field;
        table.reload('parameterdata', {
            where: {
                "parameterNumber": field.parameterNumber,
                "parameterName": field.parameterName,
                "parameterType": field.parameterType,
                "page":1,
            }
        });
    });

    //监听工具栏添加按钮
    table.on('toolbar(Parameter)', function (obj) {
        var LayEvent = obj.event;
        if (LayEvent == "AddParameter") {
            admin.popup({
                id: 'AddParameter'
                , title: '添加'
                , area: ['500px', '540px']
                , success: function (layero, index) {
                    view(this.id).render('SysManage/ParameterSetting/AddParameter', null).done(function () {
                        form.render(null, 'addparameter');
                        //设置弹出层的背景
                        layer.style(index, {
                            "background": '#ECF5FF'
                        });
                        //监听提交按钮
                        form.on('submit(Submit)', function (obj) {
                            var field = obj.field;
                            var load = layer.load(3);
                            admin.req({
                                url: 'http://192.168.1.32:8088/AddParameter'
                                , method: 'Get'
                                , data: {
                                    "JsonData": JSON.stringify(field)
                                }
                                , success: function (data) {
                                    if (data.msg == "OK") {
                                        table.reload('parameterdata');
                                        layer.msg("添加成功");
                                        layer.close(index);
                                        layer.close(load);
                                    }
                                    else {
                                        layer.msg("参数编号已存在");
                                        layer.close(load);
                                    }
                                }
                            });
                        });
                    })
                }
            });
        }
    });

    //监听表格里面的操作
    table.on('tool(Parameter)', function (obj) {
        var LayEvent = obj.event
            , data = obj.data;
        if (LayEvent == "EditParameter") {
            admin.popup({
                id: 'EditParameter'
                , title: '编辑'
                , area: ['500px', '540px']
                , success: function (layero, index) {
                    view(this.id).render('SysManage/ParameterSetting/EditParameter', data).done(function () {
                        form.render(null, 'editparameter');

                        //设置弹出层的背景
                        layer.style(index, {
                            "background": '#ECF5FF'
                        });

                        //监听提交按钮
                        form.on('submit(Edit-Submit)', function (Data) {
                            var field = Data.field;
                            var load = layer.load(3);
                            var SendData = {
                                "parameternumber": field.parameterNumber,
                                "parametername": field.parameterName,
                                "parametertype": field.parameterType,
                                "parametertypename": field.parameterTypeName,
                                "parameterkey": field.parameterKey,
                                "parametervalue": field.parameterValue,
                                "Remark": field.Remark,
                            };
                            console.log(data.ID);
                            admin.req({
                                url: 'http://192.168.1.32:8088/EditParameter'
                                , method: 'Get'
                                , data: {
                                    "JsonData": JSON.stringify(SendData),
                                    "ID": data.ID
                                }
                                , success: function (data) {
                                    if (data.msg == "OK") {
                                        table.reload('parameterdata');
                                        layer.msg("修改成功");
                                        layer.close(index);
                                        layer.close(load);
                                    }
                                    else {
                                        layer.msg("修改失败");
                                        layer.close(load);
                                    }
                                }
                            });
                        });
                    })
                }
            });
        }
    });

    exports('Parameter', {})
});