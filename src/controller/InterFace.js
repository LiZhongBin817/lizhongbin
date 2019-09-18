﻿/*Title:接口管理
 *Creator:李黎东
 * Date:2019.08.08
 */
layui.define(['table', 'form','view'], function (exports) {
    var $ = layui.$;
    var admin = layui.admin;
    var view = layui.view;
    var table = layui.table;
    var form = layui.form;
    var load = layer.load(3);
    //渲染表格
    table.render({
        elem: '#Interface',
        method: 'post',
        url: 'http://localhost:8088/InterfaceInfoShow',
        cols: [[
            { field: 'InterfaceName', title: '接口名称', width: 200 },
            { field: 'InterfaceUrl', title: '接口地址', width: 200 },
            { field: 'OperationVersion', title: '接口版本', width: 200 },
            {
                field: 'ExternalInterface', title: '是否为第三方接口', width: 150, align: 'center',
                templet: function (d) {
                    var intvalue = "";
                    if (d.ExternalInterface == 1) {
                        intvalue = "否"
                    }
                    else {
                        intvalue = "是"
                    }
                    return '<button class="layui-btn layui-btn-fluid layui-btn-normal">' + intvalue + '</button>';
                }
            },
            {
                field: 'Verify', title: '是否需要验证权限', width: 150, align: 'center',
                templet: function (d) {
                    var intvalue = "";
                    if (d.Verify == 1) {
                        intvalue = "否"
                    }
                    else {
                        intvalue = "是"
                    }
                    return '<button class="layui-btn layui-btn-fluid">' + intvalue + '</button>';
                }
            },
            { field: 'Remark', title: '描述', minWidth: 120 },
            { title: '操作', width: 120, toolbar: '#barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 20,
        toolbar: "#toolbarDemo",
        limits: [20, 30, 40],
        done: function () {
            layer.close(load);
        }
    });
    //监听表格中的操作
    table.on('tool(test)', function (obj) {
        var data = obj.data;       
        var layEvent = obj.event;    
        //编辑按钮
        if (layEvent === 'edit') {
            console.log("编辑");
            admin.popup({              
                title: '接口编辑弹窗',               
                id: 'InterFaceEdit',
                area: ['800px', '700px'],
                success: function (layero, index) {
                    view(this.id).render('SysManage/Interface/interfacedit',data).done(function () {
                        form.render(null, 'interfaceeditform');//渲染表单
                        //监听提交
                        form.on('submit(interface-edit-submit)', function (Data) {
                            var field = Data.field; //获取提交的字段  
                            var load = layer.load(3);
                            var sendData = {
                                "OperationVersion:": field.OperationVersion,
                                "InterfaceName": field.InterfaceName,
                                "InterfaceUrl": field.InterfaceName,
                                "ExternalInterface": field.ExternalInterface == "on" ? 0 : 1,
                                "Verify": field.Verify == "on" ? 0 : 1,
                                "Remark": field.Remark
                            };
                            console.log(sendData);
                            admin.req({
                                url: 'http://localhost:8088/ModifyInterface',
                                type: 'post',
                                data: {
                                    "JsonData": JSON.stringify(sendData),
                                    "ID": data.ID
                                },
                                success: function (obj) {
                                    switch (obj.msg) {
                                        case "The same TnterfaceUrl or InterfaceName exists":
                                            layer.msg("存在相同的接口地址或接口名称");
                                            layer.close(load);
                                        case "edit error":
                                            layer.msg("编辑异常");
                                            layer.close(load);
                                        case "ok":
                                            table.reload('Interface'); //重载表格
                                            layer.close(index); //执行关闭 
                                            layer.close(load);
                                            layer.msg("成功修改一条数据");
                                    }                                   
                                }
                            })
                        });
                    });
                }
            });
        }
    });
    //监听查询按钮
    form.on('submit(polling)', function (obj) {
        var field = obj.field;
        table.reload('Interface', {
            where: {
                "interfacename": field.interfacename,
                "interfaceurl": field.interfaceurl
            }         
        });
    });
    //监听工具栏按钮
    table.on('toolbar(test)', function (obj) {
        var layEvent = obj.event;
        if (layEvent === "AddInterface") {
            admin.popup({
                id: 'addinterface',
                area: ['800px', '700px'],
                success: function (layero, index) {
                    view(this.id).render('SysManage/Interface/addinterface', null).done(function () {
                        form.render(null, 'addinterfaceform');//渲染增加接口的表单
                        //监听增加接口界面的提交,不需要再获取iframe
                        form.on('submit(addinterface_submit)', function (obj) {
                            var field = obj.field;
                            var load = layer.load(3);
                            admin.req({
                                url: 'http://localhost:8088/AddInterface',
                                type:'post',
                                data: {
                                    "JsonData": JSON.stringify(field)
                                },
                                success: function (obj) {
                                    if (obj.msg=="ok") {
                                        table.reload("Interface");
                                        layer.close(index); //执行关闭
                                        layer.close(load);
                                        layer.msg("成功添加一条数据");
                                    }
                                    else {
                                        layer.msg("接口地址或接口名称重复");
                                        layer.close(load);
                                    }
                                }
                            });
                        });
                    });                 
                }
            });
        }
    });
    exports('InterFace', {})
})