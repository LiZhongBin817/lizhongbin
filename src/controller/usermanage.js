/*
 * 用户管理
 * 李芊
 * 2019年8月7
*/
layui.define(['table', 'form', 'view'], function (exports) {
    var table = layui.table;
    var admin = layui.admin;
    var form = layui.form;
    var view = layui.view;
    var $ = layui.$;
    //显示数据
    table.render({
        elem: '#Sys_User',//渲染指定元素(表格ID)
        method: 'post',
        url: layui.setter.requesturl +'/api/SysManange/ShowUserInfoDate',
        cols: [[
            { type: 'checkbox', fixed: 'left' },
            { title: '#', width: 70, type: 'numbers' },
            { field: 'FUserNumber', title: '用户编号', width: 180 },
            { field: 'FUserName', title: '用户名称', width: 150 },
            { field: 'LoginName', title: '登录账号', width: 150 },
            {
                field: 'UserType', title: '用户类型', width: 150,
                templet: function (d) {
                    var intvalue = "";
                    if (d.UserType == "0") {
                        intvalue = "超级管理员"
                    }
                    if (d.UserType == "1") {
                        intvalue = "管理员"
                    }
                    if (d.UserType == "2") {
                        intvalue = "普通用户"
                    }
                    return '<div class="">' + intvalue + '</div>';
                }
            },
            { field: 'Sex', title: '性别', width: 80 },
            { title: '操作', toolbar: '#barDemo', minWidth: 300, align: 'center' }
        ]],
        page: true,
        limit: 10,
        height: $(document).height() - $('#Sys_User').offset().top - 330,
        limits: [5, 10, 15]
    });
    //监听编辑
    table.on('tool(test)', function (obj) {
        var data = obj.data;
        var layEvent = obj.event;
        var sendID = null; //定义一个数组,用来存储选择的角色ID  
        //编辑按钮
        if (layEvent === 'edit') {
            admin.req({
                url: layui.setter.requesturl +'/api/SysManange/ModifyData',
                type: 'get',
                data: {
                    "ID": data.ID
                },
                success: function (obj) {
                    sendID = obj.data.role_mapper;
                    admin.popup({
                        title: '用户管理编辑弹窗',
                        id: 'UserManageEdit',
                        area: ['650px', '650px'],
                        success: function (layero, index) {
                            //渲染指定视图
                            view(this.id).render('SysManage/UserManange/UserEdit', {//注意写法：切勿写成SysManage/UserManage/UserEdit.html
                                //传给动态模板的数据
                                modifydata: obj.data.modifydate,
                                roles: obj.data.roles,
                                role_mapper: obj.data.role_mapper
                            }).done(function () {//视图文件请求完毕，视图渲染完毕
                                form.render(null, 'useredit');//渲染表单
                                form('useredit').on('checkbox', function (obj) {     //form.on代表监听表单，checkbox(filter)则是监听表单中的复选框，此处没有写filter则是监听所有复选框                                               
                                    console.log(obj);
                                    if (obj.elem.checked == true) {//判断触发监听事件的是选中还是取消
                                        for (var i = 0; i < sendID.length; i++) {//判断存储选中数组中是否有该ID
                                            if (obj.elem.value == sendID[i]) { }
                                            else {
                                                if (i == sendID.length - 1)
                                                    sendID.push(Number(obj.elem.value));//push()方法将选中的角色ID存入数组中
                                            }
                                        }
                                    }
                                    else {
                                        for (var i = 0; i < sendID.length; i++) {
                                            if (obj.elem.value == sendID[i]) {
                                                sendID.splice(i, 1);//删除数组中取消的角色ID
                                            }
                                        }
                                    }
                                });

                            });
                            //监听提交
                            form.on('submit(LAY-user-front-submit)', function (Data) {
                                var field = Data.field; //获取提交的字段,如果前台没有写name属性，field将吧包括这个表单元素的值
                                field.ID = data.ID;
                                //var load = layer.load(3);让有一个转转哈哈哈
                                console.log(field);
                                admin.req({
                                    url: layui.setter.requesturl +'/api/SysManange/ModifyUserInfo',
                                    type: 'post',
                                    data: {
                                        "JsonDate": JSON.stringify(field),
                                        "roleid": sendID
                                    },
                                    success: function (message) {
                                        if (message.msg == "ok") {
                                            layui.table.reload('Sys_User'); //重载表格
                                            layer.close(index); //执行关闭 
                                            layer.msg("编辑成功");
                                        }
                                        else {
                                            layer.msg("编辑失败");
                                        }
                                    }
                                })
                            });
                        }
                    });

                }
            });
        }
        //删除
        else if (layEvent === 'delete') {
            admin.popup({
                type: 0,
                title: "删除",
                content: "确定删除该数据吗！！！",
                maxmin: true,
                btn: ['提交', '取消'],
                yes: function (layero, index) {
                    admin.req({
                        url: layui.setter.requesturl +'/api/SysManange/DeleteUser',
                        type: "Get",
                        data: {
                            "ID": Number(data.ID),
                        },
                        success: function (msg) {
                            if (msg.msg == "ok") {
                                console.log(msg);
                                table.reload('Sys_User');
                                layer.msg("删除成功");
                            }
                            else {
                                layer.msg("删除失败");
                            }
                        }
                    });
                },
                success: function (layero, index) {
                }
            });
        }
    });
    //监听表格中的复选框
    table.on('checkbox(test)', function (obj) {
        console.log(obj);

    }); 
    //监听查询
    form.on('submit(polling)', function (data) {
        var field = data.field;
        console.log(data.field);
        //执行重载
        table.reload('Sys_User', {
            where: {
                "FUserName": data.field.FUserName,//传Name属性
                "LoginName": data.field.LoginName,
                "page": 1,
                "limit": 5
            }
        });
    });
    //监听添加
    form.on('submit(Add)', function (obj) {
        admin.req({
            url: layui.setter.requesturl +'/api/SysManange/roleDate',
            type: 'get',
            success: function (obj) {
                console.log(obj);
                admin.popup({
                    title: '用户管理添加弹窗',
                    id: 'UserManageAdd',
                    area: ['600px', '639px'],
                    success: function (layero, index) {
                        sendID = obj.data;
                        //渲染指定视图
                        view('UserManageAdd').render('SysManage/UserManange/UserAdd', obj.data).done(function () {//视图文件请求完毕，视图渲染完毕
                            form.render(null, 'useradd');//渲染表单
                            sendID.splice(0, sendID.length);
                            form('useradd').on('checkbox', function (obj) {     //form.on代表监听表单，checkbox(filter)则是监听表单中的复选框，此处没有写filter则是监听所有复选框                                               
                                if (obj.elem.checked == true) {//判断触发监听事件的是选中还是取消
                                    if (sendID.length != 0) {
                                        for (var i = 0; i < sendID.length; i++) {//判断存储选中数组中是否有该ID
                                            if (obj.elem.value == sendID[i]) { }
                                            else {
                                                if (i == sendID.length - 1)
                                                    sendID.push(Number(obj.elem.value));//push()方法将选中的角色ID存入数组中
                                            }
                                        }
                                    }
                                    else {
                                        sendID.push(Number(obj.elem.value));
                                    }
                                }
                                else {
                                    for (var i = 0; i < sendID.length; i++) {
                                        if (obj.elem.value == sendID[i]) {
                                            sendID.splice(i, 1);//删除数组中取消的角色ID
                                        }
                                    }
                                }
                            });
                            //监听提交
                            form.on('submit(LAY-user-front-submit)', function (Data) {
                                console.log(sendID);
                                var field = Data.field; //获取提交的字段,如果前台没有写name属性，field将吧包括这个表单元素的值                              
                                var load = layer.load(3);
                                admin.req({
                                    url: layui.setter.requesturl +'/api/SysManange/AddUser',
                                    type: 'post',
                                    data: {
                                        "JsonDate": JSON.stringify(field),
                                        "roleid": sendID
                                    },
                                    success: function (message) {
                                        if (message.msg == "ok") {
                                            layui.table.reload('Sys_User'); //重载表格
                                            layer.close(index); //执行关闭 
                                            layer.close(load);//关闭加载层
                                            layer.msg("添加成功");
                                        }
                                        else {
                                            layer.msg("添加失败");
                                        }
                                    }
                                })
                            });
                        });
                    }
                });

            }
        });
    });
    //监听批量删除
    form.on('submit(deletes)', function (obj) {
        var deletedata = layui.table.checkStatus('Sys_User').data;//获取表格中选中的信息
        console.log(deletedata);
        var IDs = "";
        for (var i = 0; i < deletedata.length; i++) {
            IDs += deletedata[i].ID + ",";
        }
        console.log(IDs);
        admin.popup({
            type: 0,
            title: "批量删除",
            content: "确定删除这些数据吗！！！",
            maxmin: true,
            btn: ['提交', '取消'],
            yes: function (index, layero) {
                admin.req({
                    url: layui.setter.requesturl +'/api/SysManange/DeleteUsers',
                    type: "Get",
                    data: {
                        "ids": IDs,
                    },
                    success: function (msg) {
                        if (msg.msg == "ok") {
                            table.reload('Sys_User');
                            layer.msg("删除成功");
                        }
                        else {
                            layer.msg("删除失败");
                        }
                    }
                });
            },
            success: function (layero, index) {
            }
        });
    });
    exports('usermanage', {})
})