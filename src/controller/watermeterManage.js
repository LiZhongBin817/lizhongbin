
//李黎东
//2019/10/14
//用户管理界面
/*
ZK 19-11-15 修改
* */
layui.define(['table', 'form', 'view', 'admin', 'laydate', 'upload'], function (exports) {
    var table = layui.table,
        admin = layui.admin,
        view = layui.view,
        $ = layui.$,
        bookinfo = [],
        readerinfo = [],
        regionlist = [],
        laydate = layui.laydate,
        upload = layui.upload,
        //meternumber = "",
    form = layui.form;
    upload.render({
        elem: '#import'
        , url: layui.setter.requesturl + '/api/WatermeterUserManage/PutExcel'
        , accept: 'file' //普通文件
        , done: function (data) {
            layer.msg("导入成功");
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
            { field: 'readername', title: '抄表员', width: '120' },
            { field: 'regionplace', title: '所属区域', width: '120' },
            { field: 'areaname', title: '所属小区', width: '120' },
            { field: 'telephone', title: '电话', width: '140' },
            { field: 'GISPlace', title: 'GIS位置', width: '120' },
            { title: '操作', minWidth: 200, toolbar: '#watermeteruser_barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 5,
        height: $(document).height() - $('#watermeteruser').offset().top - 280,
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
            "identification":obj.field.identification,
            "workplace":obj.field.workplace,
            "telephone": obj.field.telephone,
            "usemetertype": obj.field.usemetertype,
            "username": obj.field.username,
            "accstate": 1
        };
        admin.req({
            url: layui.setter.requesturl + '/api/WatermeterUserManage/Adduser',
            type: 'get',
            data: {
                "JsonDate": JSON.stringify(sendData)
            },
            success: function (msg) {
                if (msg.msg === "ok") {
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
                admin.popup({
                    title: '新增用水用户界面',
                    id: 'AddUser',
                    area: ['1000px', '500px'],
                    success: function (layero, index) {
                        view('AddUser').render('watermeterManage/AddUser', null).done(function () {
                            var areanohtml = '<option value="">请选择</option>';
                            var naturehtml = '<option value="">请选择</option>';
                            areanohtml += dataobj.data[1].map((item,index) => `<option value='${item.areano}'>${item.areaname}</option>`).join('');
                            naturehtml += dataobj.data[0].map((item,index) => `<option value='${item.bntid}'>${item.naturename}</option>`).join('');
                            $('#areano').html(areanohtml);
                            $('#usemetertype').html(naturehtml);
                            form.render('select');
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
            bookhtml = bookinfo.map((item,index) => {
                return `<option value='${item.bookno}'>${item.bookno}</option>`;
            }).join('');
            readerhtml = readerinfo.map((item,index) => {
                return `<option value='${item.mrreadername}'>${item.mrreadername}</option>`;
            }).join('');
            regionhtml = regionlist.map((item,index) => {
                return `<option value='${item.regionno}'>${item.regionname}</option>`;
            }).join('');
            /*areahtml = arealist.map((item,index) => {
                return `<option value='${item.areano}'>${item.areaname}</option>`;
            }).join('');*/
            $('#optname').append(readerhtml);
            $('#regionplace').append(regionhtml);
            $('#bookno').append(bookhtml);
            //$('#areaname').append(areahtml);
            form.render('select', 'selectform');
        }
    });
    //监听下拉框选择触发
    form.on('select(regionseltigger)', function(data){
        admin.req({
            url:layui.setter.requesturl+'/api/WatermeterUserManage/RegionSelShow',
            type:'post',
            data:{
                "regionno":data.value
            },
            success:function(resdata){
                $("#areaname").html('');
                var areastr = resdata.data.map(function(item,index){
                    return `<option value='${item.areano}'>${item.areaname}</option>`;
                }).join('');
                $("#areaname").html(areastr);
                form.render('select');
            }
        });
    });
    //监听编辑用户信息中的区域下拉框
    form.on('select(edit_region)', function(data){
        admin.req({
            url:layui.setter.requesturl+'/api/WatermeterUserManage/RegionSelShow',
            type:'post',
            data:{
                "regionno":data.value
            },
            success:function(resdata){
                $("#editareaname").html('');
                var areastr = resdata.data.map(function(item,index){
                    return `<option value='${item.areano}'>${item.areaname}</option>`;
                }).join('');
                $("#editareaname").html(areastr);
                form.render('select');
            }
        });
    });
    //监听新增水表窗口中的口径下拉框触发事件
    form.on('select(addwatermeter_selcarialtigger)',function(data){
        $("#Add_caliber").val('');
        admin.req({
            url:layui.setter.requesturl+"/api/WatermeterUserManage/CarialSeltigger",
            type:"post",
            data:{
                "bmlid":data.value
            },
            success:function(res){
                $("#Add_caliber").val(res.data);
            }
        });
    });
    //监听换表窗口中的口径下拉框触发事件
    form.on('select(changewatermeter_selcarialtigger)',function(data){
        $("#maxrange").val('');
        admin.req({
            url:layui.setter.requesturl+"/api/WatermeterUserManage/CarialSeltigger",
            type:"post",
            data:{
                "bmlid":data.value
            },
            success:function(res){
                $("#maxrange").val(res.data);
            }
        });
    });
    //监听编辑水表信息中口径下拉框触发事件
    form.on('select(Edit_watermetermodel)',function(data){
        $("#Edit_watermeter_maxrange").val('');
        admin.req({
            url:layui.setter.requesturl+"/api/WatermeterUserManage/CarialSeltigger",
            type:"post",
            data:{
                "bmlid":data.value
            },
            success:function(res){
                $("#Edit_watermeter_maxrange").val(res.data);
            }
        });
    });

    //用户查询
    form.on('submit(P_User)', function (obj) {
        table.reload('watermeteruser', {
            url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowWaterUserinfo',
            method: 'get',
            where: {
                "account": obj.field.account,
                "username":obj.field.username,
                "meternum":obj.field.meternum,
                "readername": obj.field.readername,
                "bookno": obj.field.bookno,
                "regionplace": obj.field.regionplace,
                "areaname": obj.field.areaname
            }
        })
    });

    //监听主界面表格中的操作
    table.on('tool(watermeteruserManage)', function (obj) {
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
                    if (msg.msg === "ok") {
                        layer.msg("操作成功");
                        table.reload('watermeteruser');
                    }
                }
            });
        });
        //监听主界面的编辑按钮
        if (obj.event === "User_edit") {
            //meternumber = obj.data.meternum;
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowEditRegionDate',
                type: 'get',
                data: {
                    "account": obj.data.autoaccount
                },
                success: function (res) {
                    admin.popup({
                        id: 'watermeteruserEdit',
                        title: '用水用户编辑界面',
                        area: ['1000px', '700px'],
                        success: function (layero, index) {
                            view('watermeteruserEdit').render('watermeterManage/EditUserinfo', res.data[0]).done(function () {
                                table.render({
                                    data: res.data[1],
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
                                            field: 'meterstate', align: 'center', title: '状态', width: '120', templet: function (d) {
                                                var watermeterststus = "";
                                                if (d.meterstate === 1) {
                                                    watermeterststus = "使用";
                                                }
                                                else if(d.meterstate === 3){
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
                                if(res.count === 0){
                                    $(".add_watermeter").show();
                                }else{
                                    $(".add_watermeter").hide();
                                }
                                var editregionhtml = '';
                                var editareahtml = '';
                                editregionhtml += regionlist.map(function(item,index){
                                    var selectstr = item.regionname === res.data[0][0].regionplace?"selected":"";
                                    return `<option value='${item.regionno}' ${selectstr}>${item.regionname}</option>`;
                                }).join('');
                                //请求某一个具体区域下的小区
                                admin.req({
                                    url:layui.setter.requesturl+'/api/WatermeterUserManage/RegionSelShow',
                                    type:'post',
                                    data:{
                                        "regionno":res.data[0][0].regionno
                                    },
                                    success:function(resdata){
                                        editareahtml += resdata.data.map(function(item,index){
                                            var selectstr1 = item.areano === res.data[0][0].areano?"selected":"";
                                            return `<option value='${item.areano}' ${selectstr1}>${item.areaname}</option>`;
                                        }).join('');
                                        $('#editregionplace').html(editregionhtml);
                                        $('#editareaname').html(editareahtml);
                                        form.render('select');
                                        form.render(null, 'editareaname');
                                    }
                                });
                            });
                            //监听表格工具栏
                            table.on('toolbar(editwatermeter)', function (obj1) {
                                //监听表格的新增水表
                                if (obj1.event === "ATable") {
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/WatermeterUserManage/showaddmeterinfo',
                                        type: 'post',
                                        success: function (obj1) {
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
                                if (obj1.event === "CTable") {
                                    admin.req({
                                        url: layui.setter.requesturl + '/api/WatermeterUserManage/ShowChangemeter',
                                        type: 'post',
                                        data: {
                                            "account": obj1.config.data[0].autoaccount
                                        },
                                        success: function (CTobj) {
                                            console.log(CTobj.data);
                                            admin.popup({
                                                title: '换表界面',
                                                id: 'ChangeTable',
                                                area: ['1000px', '700px'],
                                                success: function (layero, index) {
                                                    /*for (var i = 0; i < CTobj.data.length; i++) {
                                                        console.log(CTobj.data[i].updatemetertime);
                                                    }*/
                                                    view('ChangeTable').render('watermeterManage/ChangeWaterMeter', CTobj.data).done(function () {
                                                        form.render('select');
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
        if (obj.event === "User_delete") {
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/DelUser',
                data: {
                    "autoaccount": obj.data.autoaccount
                },
                type: 'post',
                success: function (msg) {
                    if (msg.msg === "ok") {
                        table.reload('watermeteruser');
                        layer.msg("操作成功");
                    }
                }
            });
        }

        //监听换表操作的提交按钮
        form.on('submit(Change_Sub)', function (changeobj) {
            var oldmeterobject = {
                "meternum":changeobj.field.Oldmeternum,
                "GISPlace":changeobj.field.OldGISPlace === ""?changeobj.field.OldGISPlace1:changeobj.field.OldGISPlace,
                "lastwaternum":changeobj.field.Oldlastwaternum,
                "updatemetertime":changeobj.field.Oldupdatemetertime,
                "remark":changeobj.field.Oldremark
            };
            var newmeterobject = {
                "meternum":changeobj.field.meternum,
                "autoaccount":obj.data.autoaccount,
                "metermodel":changeobj.field.metermodel,
                "factory":changeobj.field.factory,
                "GISPlace":oldmeterobject.GISPlace,
                "metertype":changeobj.field.metertype,
                "bwcode":changeobj.field.bwcode,
                "installpos":changeobj.field.installpos
            };
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/Changemeter',
                type: 'post',
                data: {
                    "Oldmeterinfo": JSON.stringify(oldmeterobject),
                    "Newmeterinfo": JSON.stringify(newmeterobject)
                },
                success: function (msg) {
                    if (msg.msg === "ok") {
                        layer.msg("操作成功");
                    }else{
                        layer.msg("该表号已存在！");
                    }
                }
            });
        });
        //监听新增水表的提交按钮
        form.on('submit(Add_Sub)', function (addobj) {
            var Jsondate = {
                "factory": addobj.field.factory,
                "metertype": addobj.field.metertype,
                "bwcode": addobj.field.bwcode,
                "installpos": addobj.field.installpos,
                "GISPlace": addobj.field.GISPlace === "" ? addobj.field.GISPlace1 : addobj.field.GISPlace,
                "metermodel": addobj.field.metermodel,
                "autoaccount": obj.data.autoaccount
            };
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/AddWaterMeter',
                type: 'post',
                data: {
                    "JsonDate": JSON.stringify(Jsondate)
                },
                success: function (msg) {
                    if (msg.msg === "ok") {
                        layer.msg("操作成功");
                        table.reload('editwatermeter');
                    }
                }
            });
        });
    });
    //监听用户表中编辑界面的表格中的操作
    table.on('tool(editwatermeter)', function (obj) {
        if (obj.event === "editWatermeter") {
            admin.req({
                url: layui.setter.requesturl + '/api/WatermeterUserManage/ListData',
                type: 'get',
                data: {
                    "meternum":obj.data.meternum
                },
                success: function (dataobj) {
                    admin.popup({
                        title: '编辑水表信息界面',
                        id: 'EditmeterInfo',
                        area: ['1000px', '700px'],
                        success: function (layero, index) {
                            view('EditmeterInfo').render('watermeterManage/EditWatermeterInfo', obj.data).done(function () {
                                var installpos = '';
                                var cailarsel = "";
                                installpos += dataobj.data[1].map(function(item,index){
                                    var selstr = item.bipid === dataobj.data[0][0].installpos?"selected":"";
                                    return `<option value='${item.bipid}' ${selstr}>${item.posname}</option>`;
                                }).join('');
                                cailarsel += dataobj.data[2].map(function(item,index){
                                    var selstr1 = item.bmlid === dataobj.data[0][0].metermodel?"selected":"";
                                    return `<option value='${item.bmlid}' ${selstr1}>${item.dn}</option>`;
                                }).join('');
                                $('#Edit_delflag').val(dataobj.data[0][0].meterstate);
                                $('#Edit_watermetermodel').html(cailarsel);
                                $('#Edit_installpos').html(installpos);
                                //渲染时间
                                laydate.render({
                                    elem: '#Edit_watermeter_updatemetertime', //指定元素
                                    value:dataobj.data[0][0].updatemetertime,
                                    format:'yyyy-MM-dd HH:mm:ss'
                                });
                                form.render('select');
                            });

                            //监听编辑水表信息界面的提交按钮
                            form.on('submit(EditWatermeterinfo_Sub)', function (editmeterobj) {
                                var editwatermeterinfoobject = {
                                    "meternum":obj.data.meternum,
                                    "metermodel":editmeterobj.field.editmetermodel,
                                    "bwcode":editmeterobj.field.bwcode,
                                    "installpos":editmeterobj.field.installpos,
                                    "lastwaternum":editmeterobj.field.lastwaternum,
                                    "meterstate":editmeterobj.field.meterstate,
                                    "updatemetertime":editmeterobj.field.updatemetertime,
                                    "GISPlace":editmeterobj.field.GISPlace
                                };
                                admin.req({
                                    url: layui.setter.requesturl + '/api/WatermeterUserManage/EditWaterMater',
                                    type: 'post',
                                    data: {
                                        "JsonDate": JSON.stringify(editwatermeterinfoobject)
                                    },
                                    success: function (msg) {
                                        if (msg.msg === "ok") {
                                            layer.msg("操作成功");
                                            table.reload('editwatermeter');
                                        }
                                    }
                                });
                            });
                        }
                    });
                }
            });
        }
    });
    //暴露成接口
    exports('watermeterManage', {})
});
