
//李黎东
//2019/10/14
//用户管理界面
layui.define(['table', 'form', 'view', 'admin', 'laydate', 'upload'], function (exports) {
    var table = layui.table,
        admin = layui.admin,
        view = layui.view,
        $ = layui.$,
        bookinfo = new Array(),
        readerinfo = new Array(),
        regionlist = new Array(),
        arealist = new Array(),
        factorylist = new Array(),
        installposlist = new Array(),
        watermetertypelist = new Array(),
        metermodel = new Array(),//存储水表型号，来显示最大量程
        laydate = layui.laydate,
        upload = layui.upload,
        meternumber = "";
    form = layui.form;
    //执行一个laydate实例
    laydate.render({
        elem: '#Oldupdatemetertime', //指定元素
        format: 'yyyy-MM-dd HH:mm:ss'
    });
    laydate.render({
        elem: '#Oldupdatemetertime', //指定元素
        format: 'yyyy-MM-dd HH:mm:ss'
    });
    upload.render({
        elem: '#import'
        , url: layui.setter.requesturl + '/api/WatermeterUserManage/PutExcel'
        , accept: 'file' //普通文件
        , done: function (data) {
            console.log(data);
        }
    });
    //渲染主界面表格
    table.render({
        url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowWaterUserinfo',
        method: 'get',
        elem: '#watermeteruser',
        cols: [[
            { field: 'autoaccount', title: '用户编号', width: '130' },
            { field: 'account', title: '编号', width: '130', hide: 'true' },
            { field: 'username', title: '用户名称', width: '130' },
            { field: 'address', title: '用户地址', width: '130' },
            { field: 'meternum', title: '水表编号', width: '120' },
            { field: 'caliber', title: '口径', width: '80' },
            { field: 'optname', title: '抄表员', width: '120' },
            { field: 'regionplace', title: '所属区域', width: '120' },
            { field: 'areaname', title: '所属小区', width: '120' },
            { field: 'telephone', title: '电话', width: '140' },
            { field: 'GISPlace', title: 'GIS位置', width: '120' },
            { title: '操作', minWidth: 200, toolbar: '#watermeteruser_barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 5,
        limits: [5, 10, 15]
    });
    //监听新增用户的提交按钮
    form.on('submit(AddUser_Sub)', function (obj) {
        console.log(obj.field);
        var sendData = {
            "account": obj.field.account,
            "address": obj.field.address,
            "areano": obj.field.areano,
            "sex": obj.field.sex,
            "telephone": obj.field.telephone,
            "usemetertype": obj.field.usemetertype,
            "username": obj.field.username,
            "accstate": 1
        };
        $.ajax({
            url: layui.setter.requesturl + '/api/WatermeterUserManage/Adduser',
            type: 'get',
            data: {
                "JsonDate": JSON.stringify(sendData)
            },
            success: function (msg) {
                if (msg.msg == "ok") {
                    layer.msg("操作成功");
                    table.reload('watermeteruser');
                }
            }
        });
    });
    //监听主界面的新增按钮
    form.on('submit(adduserinfo)', function (obj) {
        admin.req({
            url: layui.setter.requesturl + '/api/WatermeterUserManage/AdduserDataShow',
            type: 'post',
            data: {

            },
            success: function (dataobj) {
                console.log(dataobj.data[0]);
                var areanohtml = "";
                var naturehtml = "";
                admin.popup({
                    title: '新增用户界面',
                    id: 'AddUser',
                    area: ['1000px', '500px'],
                    success: function (layero, index) {

                        view('AddUser').render('watermeterManage/AddUser', null).done(function () {
                            for (var i = 0; i < dataobj.data[1].length; i++) {
                                areanohtml += '<option value=' + dataobj.data[1][i].areano + '>' + dataobj.data[1][i].areaname + '</option>';
                            }
                            for (var i = 0; i < dataobj.data[0].length; i++) {
                                naturehtml += '<option value=' + dataobj.data[0][i].bntid + '>' + dataobj.data[0][i].naturename + '</option>';
                            }
                            console.log(areanohtml);
                            $('#areano').append(areanohtml);
                            $('#usemetertype').append(naturehtml);
                            form.render();
                        });
                    }
                });
            }
        });
    });
    //渲染主界面下拉框
    admin.req({
        url: layui.setter.requesturl + '/api/WatermeterUserManage/SelectValue',
        type: 'post',
        data: {

        },
        success: function (obj) {
            var bookhtml = '';
            var readerhtml = '';
            var regionhtml = '';
            var areahtml = '';
            bookinfo = obj.data[0];
            readerinfo = obj.data[1];
            regionlist = obj.data[2];
            arealist = obj.data[3];
            for (var i = 0; i < bookinfo.length; i++) {

                var bookno = bookinfo[i].bookno;
                bookhtml += '<option value=' + bookno + '>' + bookno + '</option>';

            }
            for (var i = 0; i < readerinfo.length; i++) {
                var mrreadername = readerinfo[i].mrreadername;
                readerhtml += '<option value=' + mrreadername + '>' + mrreadername + '</option>';
            }
            for (var i = 0; i < regionlist.length; i++) {
                var regionname = regionlist[i].regionname;
                regionhtml += '<option value=' + regionname + '>' + regionname + '</option>'
            }
            for (var i = 0; i < arealist.length; i++) {
                var areaname = arealist[i].areaname;
                areahtml += '<option value=' + areaname + '>' + areaname + '</option>'
            }
            $('#optname').append(readerhtml);
            $('#regionplace').append(regionhtml);
            $('#bookno').append(bookhtml);
            $('#areaname').append(areahtml);
            form.render('select', 'selectform');
        }
    });
    //用户查询
    form.on('submit(P_User)', function (obj) {
        table.reload('watermeteruser', {
            url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowWaterUserinfo',
            method: 'get',
            where: {
                "account": obj.field.account,
                "username": obj.field.username,
                "meternum": obj.field.meternum,
                "optname": obj.field.optname,
                "bookno": obj.field.bookno,
                "regionplace": obj.field.regionplace,
                "areaname": obj.field.areaname
            }
        })
    });
    //监听换表操作的提交按钮
    form.on('submit(Change_Sub)', function (obj) {
        admin.req({
            url: 'post',
            type: layui.setter.requesturl + '/api/WatermeterUserManage/Changemeter',
            data: {
                "Oldmeternum": obj.field.Oldmeternum,
                "Newmeternum": obj.field.meternum
            },
            success: function (msg) {
                if (msg.msg == "ok") {
                    layer.msg("操作成功");
                }
            }
        });
    });
    //监听主界面表格中的操作
    table.on('tool(watermeteruserManage)', function (obj) {
        console.log(obj);
        //监听编辑界面的提交按钮
        form.on('submit(Edit_Sub)', function (form_obj) {
            var sendData = {
                "username": form_obj.field.username,
                "telephone": form_obj.field.telephone,
                "areano": form_obj.field.areaname,
                "address": form_obj.field.address,
                "autoaccount": form_obj.field.autoaccount,
                "account": obj.data.account,
                "sex": obj.data.sex
            };
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/EditUserInfo',
                type: 'post',
                data: {
                    "JsonDate": JSON.stringify(sendData)
                },
                success: function (msg) {
                    if (msg.msg == "ok") {
                        layer.msg("操作成功");
                        table.reload('watermeteruser');
                    }
                }
            });
        });
        //监听主界面的编辑按钮
        if (obj.event == "User_edit") {
            var load = layer.load(3);
            meternumber = obj.data.meternum;
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowEditRegionDate',
                type: 'get',
                data: {
                    "account": obj.data.account
                },
                success: function (obj) {
                    layer.close(load);
                    admin.popup({
                        id: 'watermeteruserEdit',
                        title: '抄表册编辑界面',
                        area: ['1000px', '700px'],
                        success: function (layero, index) {
                            view('watermeteruserEdit').render('watermeterManage/EditUserinfo', obj.data[0]).done(function () {
                                table.render({
                                    data: obj.data[1],
                                    elem: '#editwatermeter',
                                    cols: [[
                                        { title: '编号', width: '80', type: 'numbers' },
                                        { field: 'meternum', title: '水表编号', width: '130' },
                                        { field: 'caliber', title: '口径', width: '130' },
                                        { field: 'bwcode', title: '初始读数', width: '130' },
                                        { field: 'maxrange', title: '最大量程', width: '120' },
                                        { field: 'posname', title: '安装位置', width: '100' },
                                        { field: 'lastwaternum', title: '截止读数', width: '120' },
                                        {
                                            field: 'delflag', align: 'center', title: '状态', width: '120', templet: function (d) {
                                                var watermeterststus = "";
                                                if (d.flag == 1) {
                                                    watermeterstatus = "使用";
                                                }
                                                else {
                                                    watermeterststus = "已换表";
                                                }
                                                return '<a style="text-decoration:underline">' + watermeterststus + '</a > '
                                            }
                                        },
                                        { field: 'updatemetertime', title: '更换时间', width: '120' },
                                        { field: 'GISPlace', title: 'GIS位置', width: '120' },
                                        { title: '操作', minWidth: 200, toolbar: '#barDemo1', align: 'center', fixed: 'right' }
                                    ]],
                                    toolbar: '#barDemo2',
                                    limit: 5,
                                    limits: [5, 10, 15]
                                });
                                var editregionhtml = '';
                                var editareahtml = '';
                                for (var i = 0; i < regionlist.length; i++) {
                                    var regionname = regionlist[i].regionname;
                                    var selected = "";
                                    if (regionname == obj.data[0][0].regionplace) {
                                        selected = "selected";
                                    }
                                    editregionhtml += '<option value=' + regionname + ' ' + selected + '>' + regionname + '</option>'
                                }
                                for (var i = 0; i < arealist.length; i++) {
                                    var areaname = arealist[i].areaname;
                                    var selected = "";
                                    if (areaname == obj.data[0][0].areaname) {
                                        selected = "selected";
                                    }
                                    editareahtml += '<option value=' + arealist[i].areano + ' ' + selected + '>' + areaname + '</option>'
                                }
                                $('#editregionplace').append(editregionhtml);
                                $('#editareaname').append(editareahtml);
                                form.render('select', 'Edituserinfoform');
                                form.render(null, 'watermeteruserEditform');
                            });
                            //监听表格工具栏
                            table.on('toolbar(editwatermeter)', function (obj1) {
                                var load1 = layer.load(3);
                                //监听表格的新增水表
                                if (obj1.event == "ATable") {
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/WatermeterUserManage/showaddmeterinfo',
                                        type: 'post',
                                        data: {
                                            "meternum1": meternumber
                                        },
                                        success: function (obj1) {
                                            layer.close(load1);
                                            factorylist = obj1.data[0];
                                            installposlist = obj1.data[1];
                                            watermetertypelist = obj1.data[2];
                                            metermodel = obj1.data[4];
                                            admin.popup({
                                                id: 'AddWaterMeterTable',
                                                title: '新增水表界面',
                                                area: ['1000px', '700px'],
                                                success: function (layero, index) {
                                                    view('AddWaterMeterTable').render('watermeterManage/AddWatermeter', obj1.data).done(function () {
                                                        form.render();
                                                    });
                                                }
                                            });
                                        }
                                    });
                                }
                                if (obj1.event == "CTable") {
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowChangemeter',
                                        type: 'post',
                                        data: {
                                            "account": obj1.config.data[0].account
                                        },
                                        success: function (CTobj) {
                                            layer.close(load1);
                                            console.log(CTobj.data);
                                            admin.popup({
                                                title: '换表界面',
                                                id: 'ChangeTable',
                                                area: ['800px', '700px'],
                                                success: function (layero, index) {
                                                    for (var i = 0; i < CTobj.data.length; i++) {
                                                        console.log(CTobj.data[i].updatemetertime);

                                                    }
                                                    view('ChangeTable').render('watermeterManage/ChangeWaterMeter', CTobj.data).done(function () {
                                                        var Oldmaxrange = '';//旧表的最大量程
                                                        var maxrange = '';//新表的最大量程
                                                        var Oldselected = '';//判断旧表最大量程是否选中
                                                        var selected = '';
                                                        var Oldinstallselected = '';
                                                        var installselected = '';
                                                        var Oldinstallpos = '';
                                                        var installpos = '';
                                                        for (var i = 0; i < metermodel.length; i++) {
                                                            if (metermodel[i].bmlid == CTobj.data[0].metermodel) {
                                                                Oldselected = "selected";
                                                            }
                                                            if (metermodel[i].bmlid == CTobj.data[1].metermodel) {
                                                                selected = "selected";
                                                            }
                                                            Oldmaxrange += '<option value=' + metermodel[i].bmlid + ' ' + Oldselected + '>' + metermodel[i].maxrange + '</option>'
                                                            maxrange += '<option value=' + metermodel[i].bmlid + ' ' + selected + '>' + metermodel[i].maxrange + '</option>'
                                                        }
                                                        $('#Oldmetermodel').append(Oldmaxrange);
                                                        $('#metermodel').append(maxrange);
                                                        for (var i = 0; i < installposlist.length; i++) {
                                                            if (installposlist.bipid == CTobj.data[0].installpos) {
                                                                Oldinstallselected = "selected";
                                                            }
                                                            if (installposlist.bipid == CTobj.data[1].installpos) {
                                                                installselected = "selected";
                                                            }
                                                            Oldinstallpos += '<option value=' + installposlist[i].bipid + ' ' + Oldinstallselected + '>' + installposlist[i].posname + '</option>'
                                                            installpos += '<option value=' + installposlist[i].bipid + ' ' + installselected + '>' + installposlist[i].posname + '</option>'
                                                        }
                                                        $('#Oldinstallpos').append(Oldinstallpos);
                                                        $('#installpos').append(installpos);
                                                        form.render();
                                                    });
                                                }
                                            });
                                        }
                                    })
                                }

                            });
                        }
                    });
                }
            });
        }
        if (obj.event == "User_delete") {
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/DelUser',
                data: {
                    "autoaccount": obj.data.autoaccount
                },
                type: 'post',
                success: function (msg) {
                    if (msg.msg == "ok") {
                        table.reload('watermeteruser');
                        layer.msg("操作成功");
                    }
                }
            });
        }
        //监听新增水表的提交按钮
        form.on('submit(Add_Sub)', function (addobj) {
            var Jsondate = {
                "meternum": addobj.field.meternum,
                "factory": addobj.field.factory,
                "caliber": addobj.field.caliber,
                "metertype": addobj.field.metertype,
                "bwcode": addobj.field.bwcode,
                "installpos": addobj.field.installpos,
                "GISPlace": addobj.field.GISPlace == "" ? addobj.field.GISPlace1 : addobj.field.GISPlace,
                "metermodel": addobj.field.metermodel
            };
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/AddWaterMeter',
                type: 'post',
                data: {
                    "JsonDate": JSON.stringify(Jsondate),
                    "autoaccount": obj.data.autoaccount
                },
                success: function (msg) {
                    if (msg.msg == "ok") {
                        layer.msg("操作成功");
                        table.reload('editwatermeter');
                    }
                }
            });
        });
    });
    //监听编辑界面的表格中的操作
    table.on('tool(editwatermeter)', function (obj) {
        if (obj.event == "editWatermeter") {
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/ListData',
                type: 'get',
                data: {

                },
                success: function (dataobj) {
                    admin.popup({
                        title: '编辑水表信息界面',
                        id: 'EditmeterInfo',
                        area: ['1000px', '700px'],
                        success: function (layero, index) {
                            view('EditmeterInfo').render('watermeterManage/EditWatermeterInfo', obj.data).done(function () {
                                var installposSelected = "";
                                var metermodelSelected = "";
                                var delflagSelect = "";
                                var delflagSelect1 = "";
                                var installpos = '';
                                var delflag = '';
                                console.log(obj.data);
                                for (var i = 0; i < dataobj.data[1].length; i++) {
                                    if (dataobj.data[1][i].bipid == obj.data.installpos) {
                                        installposSelected = "selected";
                                    }
                                    installpos += '<option value=' + dataobj.data[1][i].bipid + ' ' + installposSelected + '>' + dataobj.data[1][i].posname + '</option>'
                                }
                                for (var i = 0; i < dataobj.data[3].length; i++) {
                                    if (dataobj.data[3][i].maxrange == obj.data.maxrange) {
                                        metermodelSelected = "selected";
                                    }
                                    metermodel += '<option value=' + dataobj.data[3][i].bmlid + ' ' + metermodelSelected + '>' + dataobj.data[3][i].maxrange + '</option>'
                                }
                                if (obj.data.delflag == 0) {
                                    delflagSelect = "selected";
                                }
                                if (obj.data.delflag == 1) {
                                    delflagSelect1 = "selected";
                                }
                                var delflag = '<option value="0" ' + delflagSelect + '>已换表</option>';
                                var delflag1 = '<option value = "1" ' + delflagSelect1 + '> 使用</option>';
                                $('#Edit_deflag').append(delflag);
                                $('#Edit_deflag').append(delflag1);
                                $('#Edit_metermodel').append(metermodel);
                                $('#Edit_installpos').append(installpos);
                                form.render();
                            });
                        }
                    });
                }
            });
        }
    });
    //监听编辑水表信息界面的提交按钮
    form.on('submit(EditWatermeterinfo_Sub)', function (obj) {
        admin.req({
            url: layui.setter.requesturl + '/api/WatermeterUserManage/EditWaterMater',
            type: 'post',
            data: {
                "JsonDate": JSON.stringify(obj.field)
            },
            success: function (msg) {
                if (msg.msg == "ok") {
                    layer.msg("操作成功");
                    table.reload('editwatermeter');
                }
            }
        });
    });
    //暴露成接口
    exports('watermeterManage', {})
})
