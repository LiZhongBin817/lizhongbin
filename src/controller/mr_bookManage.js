//抄表册管理
//李黎东
/*
* 修改人：ZK
* 19.11.17
*
* */
layui.define(['table', 'form', 'view', 'admin'], function (exports) {
    var table = layui.table,
        admin = layui.admin,
        form = layui.form,
        view = layui.view,
        $ = layui.$,
        ReaderID = 0,//选择的抄表员ID
        meternum = [],//选中的用户水表编号
        autoaccount = [],
        table_data = [],//存储当前页面的数据，用于实现页面跳转后复选框勾选保留
        editdata = [];//存储编辑界面的需要传输的数据，[0]存储所有的区域信息，[1]存储当前的区域信息
    //渲染抄表册的表格
    table.render({
        elem: '#Book',
        url: layui.setter.requesturl + '/api/BookManage/t_b_bookinfoShow',
        method: 'Post',
        cols: [[
            { title: '序号', type: 'numbers' },
            { field: 'bookno', title: '表册编号', width: 140 },
            { field: 'bookname', title: '表册名称', width: 140 },
            { field: 'regionname', title: '区域', width: 120 },
            {field:'booktypename',title:'表册种类名称'},
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
            { title: '操作', minWidth: 420, toolbar: '#book_barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 10,
        toolbar: '#book_toolbarDemo',
        limits: [5, 10, 15]
    });
    //监听抄表册查询
    form.on('submit(book_polling)', function (obj) {
        var field = obj.field;
        table.reload('Book', {
            url: layui.setter.requesturl + '/api/BookManage/t_b_bookinfoShow',
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
            url: layui.setter.requesturl + '/api/BookManage/AllUserInfoShow',
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
            url: layui.setter.requesturl + '/api/BookManage/waterrederinfo',
            method: 'post',
            where: {
                "mrreadernumber": obj.field.mrreadernumber,
                "mrreadername": obj.field.mrreadername
            }
        });
    });
    //监听添加抄表册界面中的提交
    form.on('submit(Book-add-submit)', function (obj) {
        admin.req({
            url: layui.setter.requesturl + '/api/BookManage/AddBook',
            type: 'post',
            data: {
                "bookinfo":obj.field
            },
            success: function (msg) {
                if (msg.msg === "ok") {
                    table.reload('Book');
                    layer.msg("操作完成");
                }
                else {
                    layer.msg("操作失败");
                }
            }
        });
    });
    //监听分表格中的操作
    table.on('tool(test)', function (obj) {
        var layEvent = obj.event;
        //监听添加用户至抄表册
        form.on('submit(AddToBook)', function (obj1) {
            if($("#judeallot").val() === "1"){
                layer.msg("请选择未分配用户！");
                return;
            }
            admin.req({
                url: layui.setter.requesturl + '/api/BookManage/SelectUser',
                data: {
                    "autoaccount": autoaccount,
                    "meternum": meternum,
                    "bookid": obj.data.ID,
                    "bookno": obj.data.bookno
                },
                type: 'post',
                success: function (msg) {
                    if (msg.msg === "ok") {
                        layer.msg("操作完成");
                        table.reload('Book');
                        autoaccount.length = 0;
                        meternum.length = 0;
                        /*for (var i = 0; i < autoaccount.length; i++) {//因为数组设置的是全局变量，需要在添加完成之后进行数组清空
                            autoaccount.splice(i, 1);
                            meternum.splice(i, 1);
                        }*/
                    }
                    else {
                        layer.msg("操作失败");
                    }
                }
            });
        });
        //监听添加抄表员到抄表册，需要拿到bookno的值，所以写在里面
        form.on('submit(AddReaderToBook)', function (obj2) {
            if(!ReaderID || ReaderID === 0){
                layer.msg("请选择抄表员");
                return;
            }
            admin.req({
                url: layui.setter.requesturl + '/api/BookManage/SelectReader',
                type: 'post',
                data: {
                    "readerid": ReaderID,
                    "bookid": obj.data.ID
                },
                success: function (msg) {
                    if (msg.msg === "ok") {
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
        if (layEvent === "ShowAssoUser") {
            admin.popup({
                id: 'UserinfoDetail',
                title: '关联用户详情界面',
                area: ['1100px', '500px'],
                success: function (layero, index) {
                    view(this.id).render('mr_bookManage/AssoUserDetail', null).done(function () {
                        form.render(null, 'AssoUserform');
                        table.render({//渲染关联用户界面的表格
                            elem: '#AssoUserTable',
                            url: layui.setter.requesturl + '/api/BookManage/AssoUserShow',
                            where: {
                                "bookid": obj.data.ID
                            },
                            method: 'post',
                            cols: [[
                                { title: '序号', type: 'numbers' },
                                { field: 'useraccount', title: '用户编号', width: 120 },
                                { field: 'username', title: '用户名称', width: 140 },
                                { field: 'address', title: '用户地址', minWidth: 120 },
                                { field: 'watermeternumber', title: '水表编号', width: 120 },
                                { field: 'caliber', title: '口径', width: 50 },
                                { field: 'regionname', title: '所属区域', width: 120 },
                                { field: 'areaname', title: '所属小区', width: 120 },
                                { field: 'telephone', title: '用户电话', width: 120 },
                                { title: '操作', width: 120,templet:function(d){
                                    return '<button class="layui-btn layui-btn-sm" lay-event="deluser">删除</button>';
                                    }
                                }
                            ]],
                            page: true,
                            limit: 10,
                            limits: [5, 10, 15]
                        });
                    });
                }
            });
        }
        //显示关联抄表员信息
        if (layEvent === "ShowAssoReader") {
            admin.req({
                url: layui.setter.requesturl + '/api/BookManage/waterrederinfo',
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
        if (layEvent === "distributeUser") {
            autoaccount.length = 0;
            meternum.length = 0;
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
                            url: layui.setter.requesturl + '/api/BookManage/AllUserInfoShow',
                            method: 'post',
                            cols: [[
                                { title: '#', type: 'checkbox' },
                                { field: 'autoaccount', title: '用户编号', width: 140 },
                                { field: 'username', title: '用户名称', width: 110 },
                                { field: 'address', title: '用户地址', minWidth: 120 },
                                { field: 'meternum', title: '水表编号', width: 120 },
                                { field: 'caliber', title: '口径', width: 50 },
                                { field: 'regionplace', title: '所属区域', width: 120 },
                                { field: 'areaname', title: '所属小区', width: 120 },
                                { field: 'telephone', title: '用户电话', width: 120 }
                            ]],
                            page: true,
                            limit: 10,
                            limits: [5,10,15],
                            done: function (res) {//实现分页后复选框保留功能
                                table_data = res.data;
                                admin.req({
                                    url:layui.setter.requesturl+'/api/BookManage/RenderSelectedUsers',
                                    type:'post',
                                    data:{
                                        "bookid":obj.data.ID
                                    },
                                    success:function(selecteduserdata){
                                        //.假设你的表格指定的 id="DistributeUserTable"，找到框架渲染的表格
                                        var tbl = $('#DistributeUserTable').next('.layui-table-view');
                                        var seldata = selecteduserdata.data;
                                        if(autoaccount.length === 0){
                                            seldata.map(function(item,index){
                                                if(autoaccount.indexOf(item.useraccount) === -1){
                                                    autoaccount.push(item.useraccount);
                                                }
                                                if(meternum.indexOf(item.watermeternumber) === -1){
                                                    meternum.push(item.watermeternumber);
                                                }
                                            });
                                        }
                                        // 渲染选择框
                                        for (var i in table_data) {
                                            for (var j in autoaccount) {
                                                if (table_data[i].autoaccount === autoaccount[j]) {
                                                    tbl.find('table>tbody>tr').eq(i).find('td').eq(0).find('input[type=checkbox]').prop('checked', true);
                                                }
                                            }
                                        }
                                        form.render('checkbox');
                                    }
                                });

                            }
                        });
                    });
                }
            });
        }
        //分配抄表员
        if (layEvent === "distributeReader") {
            admin.popup({
                id: 'distributeReader',
                title: '分配抄表员界面',
                area: ['1100px', '500px'],
                success: function (layero, index) {
                    view('distributeReader').render('mr_bookManage/DistrbuteReader', null).done(function () {
                        //form.render(null, 'DistributeReaderform');
                        ReaderID = obj.data.readmanid;//默认为当前抄表员
                        //渲染所有用户信息的表格
                        table.render({//渲染关联用户界面的表格
                            elem: '#DistributeReaderTable',
                            url: layui.setter.requesturl + '/api/BookManage/waterrederinfo',
                            method: 'post',
                            cols: [[
                                { type:'radio',title:'单选'},
                                { field: 'mrreadernumber', title: '抄表员编号', width: 140 },
                                { field: 'mrreadername', title: '抄表员名称', width: 140 },
                                { field: 'telephone', title: '电话', minWidth: 150 }
                            ]],
                            page: true,
                            limit: 10,
                            limits: [5,10,15],
                            done:function(){
                                //obj.data.readmanid
                            }
                        });
                    });
                }
            });
        }
        //监听编辑按钮
        if (layEvent === "editBook") {
            editdata[0] = obj.data;
            admin.req({
                url: layui.setter.requesturl + '/api/BookManage/RegionShow',
                type: 'Get',
                success: function (DataObj) {
                    editdata[1] = DataObj.data;
                    admin.popup({
                        title: '抄表册编辑按钮',
                        id: 'BookEdit',
                        area: ['500px', '350px'],
                        success: function (layero, index) {
                            view('BookEdit').render('mr_bookManage/BookEdit', editdata).done(function () {
                                form.render(null, 'Bookeditform')
                                form.on('submit(Book-edit-submit)', function (BookeditObj) {
                                    var bookobjdata={
                                        "ID":obj.data.ID,
                                        "bookname": BookeditObj.field.bookname,
                                        "regionno": BookeditObj.field.regionno,
                                    };
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/BookManage/EditBook',
                                        data: {
                                            "ID": obj.data.ID,
                                            "bookname": BookeditObj.field.bookname,
                                            "regionno": BookeditObj.field.regionno,
                                            "booktype": BookeditObj.field.booktype
                                        },
                                        type: 'post',
                                        success: function (msg) {
                                            if (msg.msg === "ok") {
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
        if (layEvent === "deleteBook") {
            layer.confirm('删除后将无法恢复，请确认？', {
                btn: ['确认', '取消'] //按钮
            }, function () {
                admin.req({
                    url: layui.setter.requesturl + '/api/BookManage/DeleteBook',
                    type: 'post',
                    data: {
                        "ID": obj.data.ID,
                        "bookno": obj.data.bookno
                    },
                    success: function (msg) {
                        if (msg.msg === "ok") {
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
        console.log(obj.tr);
        if (obj.checked === true) {
            if($("#judeallot").val() == 1) {
                layer.msg("不能选择已分配用户！");
                //console.log(obj.tr.find('td').eq(0));
                obj.tr.find('td').eq(0).find('input[type=checkbox]').prop('checked', false);
                form.render('checkbox');
                return;
            }
            if (obj.type === 'one') {
                autoaccount.push(obj.data.autoaccount);
                meternum.push(obj.data.meternum);
            }
            else {
                for (var i = 0; i < table_data.length; i++) {
                    for (var j = 0; j < selectuser.length; j++) {
                        if (autoaccount[j] === table_data[i].autoaccount) {//这个不写会有重复数据
                            autoaccount.splice(j, 1);
                            meternum.splice(j, 1);
                        }
                    }
                    autoaccount.push(table_data[i].autoaccount);
                    meternum.push(table_data[i].meternum);
                }
            }
        }
        else {
            //单选去勾
            if (obj.type === 'one') {
                for (var i = 0; i < autoaccount.length; i++) {
                    if (autoaccount[i] === obj.data.autoaccount) {
                        autoaccount.splice(i, 1);
                        meternum.splice(i, 1);
                    }
                }
            }
            //多选去勾
            else {
                for (var i = 0; i < autoaccount.length; i++) {
                    for (var j = 0; j < table_data.length; j++) {
                        if (autoaccount[i] === table_data[j].autoaccount) {
                            autoaccount.splice(i, 1);
                            meternum.splice(i, 1);
                        }
                    }
                }
            }
        }     
    });
    //监听添加抄表册表头的操作
    table.on('toolbar(test)', function (obj) {
        if (obj.event === "AddBook") {
            admin.req({
                url: layui.setter.requesturl + '/api/BookManage/RegionShow',
                type: 'Get',
                success: function (DataObj) {
                    view.popup({
                        id: 'AddBook',
                        title: '生成抄表册',
                        area: ['500px', '500px'],
                        success: function (index, layero) {
                            view('AddBook').render('mr_bookManage/AddBook', DataObj.data).done(function () {
                                form.render('select');
                            });
                        }
                    });
                }
            });
        }
        if (obj.event === "CreatExcel") {
            $.ajax({
                url: layui.setter.requesturl + '/api/BookManage/BuildExcel',
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
    table.on('radio(distrbutereader_radio)', function (obj) {
        ReaderID = obj.data.id;
        //console.log(ReaderID);
    });
    //监听分配用户界面的区域事件
    form.on('select(allotuser_showregion)',function(regionobj){
        console.log(regionobj.value);
        //使用用水户管理的接口
        admin.req({
            url:layui.setter.requesturl+'/api/WatermeterUserManage/RegionSelShow',
            type:'post',
            data:{
                "regionno":regionobj.value
            },
            success:function(resdata){
                var areastr = '<option value="">请选择</option>';
                areastr += resdata.data.map(function(item,index){
                    return `<option value='${item.areano}'>${item.areaname}</option>`;
                }).join('');
                $("#allotuser_areaname").html(areastr);
                form.render('select');
            }
        });
    });
    //监听关联用户时删除按钮事件
    table.on('tool(test2)',function(delobj){
        if(delobj.event === 'deluser'){
            console.log(delobj);
            admin.req({
                url:layui.setter.requesturl+'/api/BookManage/DelBookSingleUser',
                type:'Get',
                data:{
                    "watermeternumber": delobj.data.watermeternumber,
                    "bookid":delobj.data.bookid
                },
                success:function(data){
                    if(data.data === true){
                        layer.msg("删除成功！");
                        delobj.del();
                    }
                }
            });
        }
    });
    //暴露成接口
    exports('mr_bookManage', {})
});
