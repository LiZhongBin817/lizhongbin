/*Title:接口管理
 *Creator:李黎东
 * Date:2019.08.08
 */
layui.define(['table', 'form','view'], function (exports) {
    var $ = layui.$;
    var admin = layui.admin;
    var view = layui.view;
    var table = layui.table;
    var form = layui.form;
    //渲染表格
    table.render({
        elem: '#Interface',
        method: 'Get',
        url: 'http://localhost:8081/api/InterFace/ShowInterface',
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
        done: function () { }
    });
    //监听表格中的操作
    table.on('tool(test)', function (obj) {
        var data = obj.data;       
        var layEvent = obj.event;    
        //编辑按钮
        if (layEvent === 'edit') {
            admin.popup({              
                title: '接口编辑弹窗',               
                id: 'InterFaceEdit',
                area: ['800px', '700px'],
                success: function (layero, index) {
                    view(this.id).render('SysManage/Interface/interfacedit',data).done(function () {
                        form.render(null, 'interfaceeditform');//渲染表单
                        //监听提交
                        form.on('submit(interface-edit-submit)', function (data) {
                            var field = data.field; //获取提交的字段                          
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
                                url: 'http://localhost:8081/api/InterFace/EditInterface',
                                type: 'Get',
                                data: { "interfaceObj": JSON.stringify(sendData) },
                                success: function (message) {
                                    if (message.msg == "ok") {
                                        layui.table.reload('Interface'); //重载表格
                                        layer.close(index); //执行关闭 
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
                            admin.req({
                                url: '',
                                data: {
                                    "interfaceObj": JSON.stringify(field)
                                },
                                success: function (message) {
                                    if (message.msg == "ok") {
                                        table.reload('demo');
                                        layer.close(index); //执行关闭 
                                        layer.msg("成功添加一条数据");
                                    }
                                    else {
                                        layer.msg("添加异常");
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