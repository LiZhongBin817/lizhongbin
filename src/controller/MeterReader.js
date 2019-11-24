/*
 *title:抄表员管理 
 * creator:李忠斌
 * date:2019.9.1
*/

layui.define(['table', 'view', 'form'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin;

    //表格渲染
    table.render({
        elem: '#MR-Info',
        method: 'post',
        url: layui.setter.requesturl +'/api/Mr_b_Reader/ShowMassage',
        cols:
            [[
                { field: 'ID', title: '序号', width: 80, fixed: 'left' },
                { field: 'mrreadernumber', title: '抄表员编号', width: 150 },
                { field: 'mrreadername', title: '抄表员名字', width: 150 },
                { field: 'telephone', title: '电话号码', width: 150 },
                { field: 'appcount', title: '账号', width: 120 },
                { field: 'apppassword', title: '密码', width: 120 },
                { field: 'nearnrtime', title: '最近抄表时间', width: 150 },
                { field: 'address', title: '住址', width: 150 },
                {
                    field: 'sex', title: '性别', width: 80, align: 'center',
                    templet: function (d) {
                        var intValue = "";
                        if (d.sex == 0) {
                            intValue = "男";
                        }
                        else {
                            intValue = "女";
                        }
                        return intValue;
                    }
                },
                { field: 'idcard', title: '身份证号码', width: 150 },
                { field: 'roles', title: '角色身份', width: 120 },
                { field: 'lastlogintime', title: '最后登录时间', width: 150 },
                { field: 'Remark', title: '备注', width: 120 },
                { title: '操作', width: 250, toolbar: '#barDemo1', align: 'center', fixed: 'right' }
            ]]
        , page: true
        , limit: 20
        , toolbar: '#toolbarDemo2'
        , limits: [20, 30, 40]
        , done: function () {
            layer.close(load);
        }

    });

    //监听查询
    form.on('submit(Search)', function (obj) {
        var field = obj.field;
        table.reload('MR-Info', {
            where: {
                "Number": field.MR_Number,
                "Name": field.MR_Name,
                "Telephone": field.MR_Telephone,
                "Appcount": field.MR_AppCount,
                "page":1,
            }
        });

    });

    //监听单元格操作
    table.on('tool(MR_Data)', function (obj) {
        var event = obj.event,
            data = obj.data;
        //修改
        if (event == 'edit') {
            admin.popup({
                id: 'EditMrInfo'
                , title: '编辑'
                , area: ['520px', '600px']
                , success: function (layero, index) {
                    view(this.id).render('MR_B_BookManage/mr_b_readerManage/EditMrInfo', data).done(function () {

                        //表单渲染
                        form.render(null, 'Mr_EditInfo');

                        //监听提交按钮
                        form.on('submit(MR_submit)', function (Data) {
                            var field = Data.field
                                , load = layer.load(3);
                            var SendData = {
                                "mrreadername": field.Mr_Name,
                                "telephone": field.Mr_Telephone,
                                "address": field.Mr_Address,
                                "idcard": field.Mr_idcard,
                                "sex": field.Mr_Sex == '男' ? 0 : 1,
                                "roles": field.Mr_Roles,
                                "Remark": field.Remark
                            };
                            admin.req({
                                url: layui.setter.requesturl +'/api/Mr_b_Reader/Edit_Mr_B_ReaderData'
                                , method: 'post'
                                , data: {
                                    "JsonData": JSON.stringify(SendData),
                                    "ID": data.ID
                                }
                                , success: function (obj) {
                                    if (obj.msg == "OK") {
                                        layer.msg("修改成功");
                                        layer.close(load);
                                        layer.close(index);
                                        table.reload('MR-Info')
                                    }
                                    else {
                                        layer.msg("修改失败")
                                    }

                                }
                            });
                        });


                    });
                }

            });
        }
        //删除
        if (event == 'delete') {
            layer.confirm('您确定要删除这条数据吗？', {
                btn: ['确定', '取消']
            }, function () {
                admin.req({
                    url: layui.setter.requesturl+'/Del_Mr_B_ReaderData'
                    , method: 'post'
                    , data: {
                        "ID": data.ID
                    }
                    , success: function (obj) {
                        if (obj.msg == "OK") {
                            layer.msg("删除成功");
                            table.reload('MR-Info');
                        }
                        else {
                            layer.msg("删除失败");
                        }
                    }
                });
            });
        }
        //重置密码
        if (event == 'reset') {
            admin.req({
                url: layui.setter.requesturl +'/api/Mr_b_Reader/ReSetPwd'
                , method: 'post'
                , data: {
                    "ID": data.ID
                }
                , success: function (obj) {
                    if (obj.msg == "OK") {
                        layer.msg("修改成功");
                        table.reload('MR-Info');
                    }
                    else {
                        layer.msg("修改失败");
                    }
                }
            });
        }

    });

    //监听添加按钮
    table.on('toolbar(MR_Data)', function (obj) {
        var event = obj.event
            , data = obj.data;
        if (event == 'AddMrInfo') {
            admin.popup({
                id: 'AddMrInfo'
                , title: '添加'
                , area: ['520px', '720px']
                , success: function (layero, index) {
                    view(this.id).render('MR_B_BookManage/mr_b_readerManage/AddMrInfo', null).done(function () {

                        //渲染表单
                        form.render(null, 'Mr_AddInfo');

                        //监听确认按钮
                        form.on('submit(AddMR_submit)', function (data) {
                            var field = data.field;
                            var SendData = {
                                "mrreadername": field.Mr_Name,
                                "telephone": field.Mr_Telephone,
                                "address": field.Mr_Address,
                                "idcard": field.Mr_idcard,
                                "sex": field.Mr_Sex == '男' ? 0 : 1,
                                "roles": field.Mr_Roles,
                                "Remark": field.Remark
                            }
                            admin.req({
                                url: layui.setter.requesturl +'/api/Mr_b_Reader/Add_Mr_B_ReaderData'
                                , method: 'post'
                                , data: {
                                    "JsonData": JSON.stringify(SendData)
                                }
                                , success: function (obj) {
                                    layer.close(load);
                                    if (obj.msg == "OK") {
                                        layer.msg("添加成功");
                                        layer.close(index);
                                        table.reload('MR-Info');
                                    }
                                    else {
                                        layer.msg("添加失败");
                                    }
                                }
                            });
                        });
                    });
                }
            });
        }
    });
    exports('MeterReader', {})
});
