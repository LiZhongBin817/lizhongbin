//抄表册管理
//李黎东
layui.define(['table', 'form', 'view', 'admin'], function (exports) {
    var table = layui.table,
        admin = layui.admin,
        form = layui.form,
        load = layer.load(3),//页面加载，3代表样式
        view = layui.view,
        $ = layui.$,
        ReaderID = "",//选择的抄表员ID
        meternum = new Array(),//选中的用户水表编号
        account = new Array(),
        table_data = new Array(),//存储当前页面的数据，用于实现页面跳转后复选框勾选保留
        editdata = new Array();//存储编辑界面的需要传输的数据，[0]存储所有的区域信息，[1]存储当前的区域信息
    //渲染抄表册的表格
    table.render({
        elem: '#Book',
        url: 'http://localhost:8081/t_b_bookinfoShow',
        method: 'Post',
        cols: [[
            { title: '序号', type: 'numbers' },
            { field: 'bookno', title: '表册编号', width: 140 },
            { field: 'bookname', title: '表册名称', width: 140 },
            { field: 'regionname', title: '区域', width: 120 },
            {
                field: 'contectusernum', align: 'center', title: '关联用户', templet: function (d) {
                    return '<a style="text-decoration:underline" lay-event="ShowAssoUser">' + d.contectusernum + '户</a > '
                }
            },
            { field: 'ReaderName', title: '抄表员名称', width: 150, hide: 'true' },
            { field: 'ReaderNumber', title: '抄表员编号', width: 150, hide: 'true' },
            {
                title: '抄表员', width: 150, templet: function (d) {
                    if (d.mrreadername == null) {
                        return '<a style="text-decoration:underline" lay-event="ShowAssoReader">未分配</a > '
                    }
                    else {
                        return '<a style="text-decoration:underline" lay-event="ShowAssoReader">' + d.mrreadername + ' ' + d.mrreadernumber + '</a > '
                    }
                }
            },
            { title: '操作', minWidth: 400, toolbar: '#barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 10,
        toolbar: '#toolbarDemo',
        limits: [10, 20, 30],
        done: function () {
            layer.close(load);
        }
    });
    //监听抄表册查询
    form.on('submit(polling)', function (obj) {
        var field = obj.field;
        table.reload('Book', {
            url: 'http://localhost:8081/t_b_bookinfoShow',
            method: 'post',
            where: {
                "bookno": field.bookno,
                "bookname": field.bookname
            }
        });
    });
    //监听用户信息查询
    form.on('submit(DiPolling)', function (obj) {
        table.reload('DistributeUserTable', {
            url: 'http://localhost:8081/AllUserInfoShow',
            method: 'post',
            where: {
                "areaname": obj.field.areaname,
                "regionplace": obj.field.regionplace,
                "disstatus": obj.field.status
            }
        })
    });
    //监听抄表员查询
    form.on('submit(DiReaderPolling)', function (obj) {
        table.reload('DistributeReaderTable', {
            url: 'http://localhost:8081/waterrederinfo',
            method: 'post',
            where: {
                "mrreadernumber": obj.field.mrreadernumber,
                "mrreadername": obj.field.mrreadername
            }
        })
    });
    //监听添加抄表册界面中的提交
    form.on('submit(Book-add-submit)', function (obj) {
        admin.req({
            url: 'http://localhost:8081/AddBook',
            type: 'post',
            data: {
                "bookno": obj.field.bookno,
                "bookname": obj.field.bookname,
                "regionno": obj.field.regionno,
                "readperiod": obj.field.readperiod
            },
            success: function (msg) {
                if (msg.msg == "ok") {
                    table.reload('Book');
                    layer.msg("操作完成");
                }
                else {
                    layer.msg("操作失败");
                }
            }
        });
    });
    //监听表格中的操作
    table.on('tool(test)', function (obj) {
        var layEvent = obj.event;
        //监听添加用户至抄表册
        form.on('submit(AddToBook)', function (obj1) {
            admin.req({
                url: 'http://localhost:8081/SelectUser',
                data: {
                    "account": account,
                    "meternum": meternum,
                    "bookid": obj.data.ID,
                    "bookno": obj.data.bookno
                },
                type: 'post',
                success: function (msg) {
                    if (msg.msg == "ok") {
                        layer.msg("操作完成");
                        table.reload('Book');
                        for (var i = 0; i < account.length; i++) {//因为数组设置的是全局变量，需要在添加完成之后进行数组清空
                            account.splice(i, 1);
                            meternum.splice(i, 1);
                        }
                    }
                    else {
                        layer.msg("操作失败");
                    }
                }
            });
        });
        //监听添加抄表员到抄表册，需要拿到bookno的值，所以写在里面
        form.on('submit(AddReaderToBook)', function (obj2) {
            admin.req({
                url: 'http://localhost:8081/SelectReader',
                type: 'post',
                data: {
                    "readerid": ReaderID,
                    "bookid": obj.data.ID
                },
                success: function (msg) {
                    if (msg.msg == "ok") {
                        layer.msg("操作完成");
                        table.reload('Book');
                    }
                    else {
                        layer.msg("操作失败");
                    }
                }
            });
        });
        //显示关联用户信息
        if (layEvent == "ShowAssoUser") {
            admin.popup({
                id: 'UserinfoDetail',
                title: '关联用户详情界面',
                area: ['1100px', '500px'],
                success: function (layero, index) {
                    view(this.id).render('mr_bookManage/AssoUserDetail', null).done(function () {
                        form.render(null, 'AssoUserform');
                        table.render({//渲染关联用户界面的表格
                            elem: '#AssoUserTable',
                            url: 'http://localhost:8081/AssoUserShow',
                            where: {
                                "bookid": obj.data.ID
                            },
                            method: 'post',
                            cols: [[
                                { title: '序号', type: 'numbers' },
                                { field: 'useraccount', title: '用户编号', width: 140 },
                                { field: 'username', title: '用户名称', width: 140 },
                                { field: 'address', title: '用户地址', minWidth: 120 },
                                { field: 'watermeternumber', title: '水表编号', width: 120 },
                                { field: 'caliber', title: '口径', width: 120 },
                                { field: 'regionname', title: '所属区域', width: 120 },
                                { field: 'areaname', title: '所属小区', width: 120 },
                                { field: 'telephone', title: '用户电话', width: 120 },
                            ]],
                            page: true,
                            limit: 10,
                            limits: [10, 20, 30]
                        });
                    });
                }
            });
        }
        //显示关联抄表员信息
        if (layEvent == "ShowAssoReader") {
            admin.req({
                url: 'http://localhost:8081/waterrederinfo',
                type: 'Post',
                data: {
                    "readmanid": obj.data.readmanid
                },
                success: function (ReaderObj) {
                    admin.popup({
                        id: 'AssoReader',
                        title: '关联抄表员弹窗',
                        area: ['700px', '300px'],
                        success: function (layero, index) {
                            view('AssoReader').render('mr_bookManage/AssoReaderDetail', null).done(function () {
                                table.render({//渲染关联抄表员的表格
                                    elem: '#AssoReaderTable',
                                    data: ReaderObj.data,
                                    cols: [[
                                        { field: 'mrreadernumber', title: '抄表员编号', width: 150 },
                                        { field: 'mrreadername', title: '抄表员姓名', width: 130 },
                                        { field: 'telephone', title: '电话', minWidth: 150 }
                                    ]]
                                });
                            });
                        }
                    });
                }
            });
        }
        //分配用户
        if (layEvent == "distributeUser") {
            admin.popup({
                id: 'distributeUser',
                title: '分配用户界面',
                area: ['1100px', '500px'],
                success: function (layero, index) {
                    view('distributeUser').render('mr_bookManage/DistributeUser', null).done(function () {
                        form.render(null, 'DistributeUserform');
                        //渲染所有用户信息的表格
                        table.render({//渲染关联用户界面的表格
                            elem: '#DistributeUserTable',
                            url: 'http://localhost:8081/AllUserInfoShow',
                            method: 'post',
                            cols: [[
                                { title: '#', type: 'checkbox' },
                                { field: 'account', title: '用户编号', width: 140 },
                                { field: 'username', title: '用户名称', width: 140 },
                                { field: 'address', title: '用户地址', minWidth: 120 },
                                { field: 'meternum', title: '水表编号', width: 120 },
                                { field: 'caliber', title: '口径', width: 120 },
                                { field: 'regionplace', title: '所属区域', width: 120 },
                                { field: 'areaname', title: '所属小区', width: 120 },
                                { field: 'telephone', title: '用户电话', width: 120 },
                            ]],
                            page: true,
                            limit: 10,
                            limits: [10, 20, 30],
                            done: function (res) {//实现分页后复选框保留功能
                                table_data = res.data;
                                //.假设你的表格指定的 id="DistributeUserTable"，找到框架渲染的表格
                                var tbl = $('#DistributeUserTable').next('.layui-table-view');
                                // 渲染选择框
                                for (var i in table_data) {
                                    for (var j in account) {
                                        if (table_data[i].account == account[j]) {
                                            tbl.find('table>tbody>tr').eq(i).find('td').eq(0).find('input[type=checkbox]').prop('checked', true);
                                        }
                                    }
                                }
                                form.render('checkbox');
                            }
                        });
                    });
                }
            });
        }
        //分配抄表员
        if (layEvent == "distributeReader") {
            admin.popup({
                id: 'distributeReader',
                title: '分配抄表员界面',
                area: ['1100px', '500px'],
                success: function (layero, index) {
                    view('distributeReader').render('mr_bookManage/DistrbuteReader', null).done(function () {
                        form.render(null, 'DistributeReaderform');
                        //渲染所有用户信息的表格
                        table.render({//渲染关联用户界面的表格
                            elem: '#DistributeReaderTable',
                            url: 'http://localhost:8081/waterrederinfo',
                            method: 'post',
                            cols: [[
                                { title: '#', type: 'radio' },
                                { field: 'mrreadernumber', title: '抄表员编号', width: 140 },
                                { field: 'mrreadername', title: '抄表员名称', width: 140 },
                                { field: 'telephone', title: '电话', minWidth: 150 }
                            ]],
                            page: true,
                            limit: 10,
                            limits: [10, 20, 30],
                        });
                    });
                }
            });
        }
        //监听编辑按钮
        if (layEvent == "edit") {
            editdata[0] = obj.data;
            var load4 = layer.load(3);
            admin.req({
                url: 'http://localhost:8081/RegionShow',
                type: 'post',
                success: function (DataObj) {
                    editdata[1] = DataObj.data;
                    admin.popup({
                        title: '抄表册编辑按钮',
                        id: 'BookEdit',
                        area: ['500px', '350px'],
                        success: function (layero, index) {
                            layer.close(load4);
                            view('BookEdit').render('mr_bookManage/BookEdit', editdata).done(function () {
                                form.render(null, 'Bookeditform')
                                form.on('submit(Book-edit-submit)', function (BookeditObj) {
                                    admin.req({
                                        url: 'http://localhost:8081/EditBook',
                                        data: {
                                            "ID": obj.data.ID,
                                            "bookname": BookeditObj.field.bookname,
                                            "regionno": BookeditObj.field.regionno
                                        },
                                        type: 'post',
                                        success: function (msg) {
                                            if (msg.msg == "ok") {
                                                layer.msg("操作成功");
                                                table.reload('Book');
                                            }
                                            else {
                                                layer.msg("操作失败");
                                            }
                                        }
                                    });
                                });
                            });
                        }
                    });
                }
            });
        }
        //监听删除按钮
        if (layEvent == "delete") {
            layer.confirm('删除后将无法恢复，请确认？', {
                btn: ['确认', '取消'] //按钮
            }, function () {
                admin.req({
                    url: 'http://localhost:8081/DeleteBook',
                    type: 'post',
                    data: {
                        "ID": obj.data.ID,
                        "bookno": obj.data.bookno
                    },
                    success: function (msg) {
                        if (msg.msg == "ok") {
                            layer.msg("操作成功");
                            table.reload('Book');
                        }
                        else {
                            layer.msg("操作失败");
                        }
                    }
                });
            }, function () {

            });

        }
    });

    //监听关联用户信息左边的复选框
    table.on('checkbox(test1)', function (obj) {
        if (obj.checked == true) {
            if (obj.type == 'one') {
                account.push(obj.data.account);
                meternum.push(obj.data.meternum);
            }
            else {
                for (var i = 0; i < table_data.length; i++) {
                    for (var j = 0; j < selectuser.length; j++) {
                        if (account[j] == table_data[i].account) {//这个不写会有重复数据
                            account.splice(j, 1);
                            meternum.splice(j, 1);
                        }
                    }
                    account.push(table_data[i].account);
                    meternum.push(table_data[i].meternum);
                }
            }
        }
        else {
            //单选去勾
            if (obj.type == 'one') {
                for (var i = 0; i < account.length; i++) {
                    if (account[i] == obj.data.account) {
                        account.splice(i, 1);
                        meternum.splice(i, 1);
                    }
                }
            }
            //多选去勾
            else {
                for (var i = 0; i < account.length; i++) {
                    for (var j = 0; j < table_data.length; j++) {
                        if (account[i] == table_data[j].account) {
                            account.splice(i, 1);
                            meternum.splice(i, 1);
                        }
                    }
                }
            }
        }
        console.log(account);
        console.log(meternum);
    });
    //监听表头的操作
    table.on('toolbar(test)', function (obj) {
        if (obj.event == "AddBook") {
            admin.req({
                url: 'http://localhost:8081/RegionShow',
                type: 'post',
                success: function (DataObj) {
                    view.popup({
                        id: 'AddBook',
                        title: '生成抄表册',
                        area: ['500px', '500px'],
                        success: function (index, layero) {
                            view('AddBook').render('mr_bookManage/AddBook', DataObj.data).done(function () {
                                form.render(null, 'Bookaddform');
                            });
                        }
                    });
                }
            });
        }
        if (obj.event == "CreatExcel") {
            $.ajax({
                url: 'http://localhost:8081/BuildExcel',
                type: 'Get',
                data: {

                },
                success: function (obj) {
                    console.log(obj);
                }
            });
        }
    });
    //监听关联抄表员信息的左边的单选框
    table.on('radio(test2)', function (obj) {
        ReaderID = obj.data.id;
    });
    //暴露成接口
    exports('mr_bookManage', {})
})
