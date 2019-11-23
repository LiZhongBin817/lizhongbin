/**
 @Name：layuiAdmin 主页控制台
 @Author：李忠斌
 @Date：2019.11.15
 */


layui.define(['admin', 'view', 'table', 'jquery', 'form', 'util'], function (exports) {
    var admin = layui.admin,
        view = layui.view,
        table = layui.table,
        $ = layui.jquery,
        util = layui.util,
        form = layui.form;
    var modelinfo = "";//用来存放点击按钮后显示的模块号
    var autoaccount = 0;//存放用户编号
    
    //监听换表记录按钮
    form.on('submit(change_meter_record_button)', function () {
        console.log("换表记录");
        admin.req({
            url: layui.setter.request + '',
            method: '',
            data: {
                "account": autoaccount,
            },
            success: function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('', d).done(function () {
                    var modelinfo = "";//用来存放点击按钮后显示的模块号
                    var autoaccount = "";
                    //渲染区域下拉框
                    admin.req({
                        url: layui.setter.requesturl + '/api/HomePageUserInfo/RegionSelectRender',
                        method: 'post',
                        success: function (d) {
                            var data = d.data;
                            var str = `<option value="">请选择</option>`;
                            if (d.msg == "ok") {
                                for (var i in data) {
                                    str += `<option value="${data[i].regionno}">${data[i].regionname}</option>`;
                                }
                                $("#region").html(str);
                                form.render();
                            }
                        }
                    });
                    //监听区域下拉框
                    form.on('select(region)', function (d) {
                        var regionno = $("#region").val();
                        //渲染小区下拉框
                        admin.req({
                            url: layui.setter.requesturl + '/api/HomePageUserInfo/AreaSelectRender',
                            method: 'post',
                            data: {
                                "regionno": regionno
                            },
                            success: function (d) {
                                var data = d.data;
                                console.log(data);
                                var str1 = `<option value="">请选择</option>>`;
                                if (d.msg == "ok") {
                                    for (var i in data) {
                                        str1 += `<option value="${data[i].areano}">${data[i].areaname}</option>`;
                                    }
                                    $("#area").html(str1);
                                    form.render();
                                }
                            }
                        });
                    });
                });
            }
        });
    });
    //监听模糊查询按钮
    form.on('submit(user_info_show_like_search)',function (d) {
        var field=d.field;
        admin.req({
            url:layui.setter.requesturl+'',
            method:'',
            data:{

            },
            success:function (data) {

            }

        });
        modelinfo = "换表记录";
        $("#modelinfo").html(modelinfo);
    });

    //监听条件查询按钮
    form.on('submit(user_info_show_search)',function (d) {
        var field=d.field;
        console.log(field);
        admin.req({
            url:layui.setter.requesturl+'/api/HomePageUserInfo/UserInfoSearch',
            method:'post',
            data:{
                "account":field.account,
                "username":field.username,
                "meternum":field.meternum,
                "address":field.address,
                "telephone":field.telephone,
                "region":field.region,
                "area":field.area,
                "bookno":field.bookno,
                "mrreadername":field.mrreadername,
                page:1,
            },
            success:function (data) {
                var tabledata=data.data;//存放表格数据
                if(data.msg==="ok"){
                    UserTableRender(tabledata);
                }
                else{
                    var initdata=new Array();
                    UserTableRender(initdata);
                }
            }

        });
    });

    //监听照片记录按钮
    form.on('submit(photo_record_button)', function () {
        console.log("照片记录");
        if (autoaccount === "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.requesturl + '/api/Troubleshooting/GetAutoaccountinfo',
                method: 'post',
                data: {
                    "autoaccount": autoaccount,
                    "starttime": null,
                    "endtime": null,
                    "type": 0
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('CameraRecord/CameraRecord', d).done(function () {
                        $("#CameraRecord_autoaccount").val(d.data[0].autoaccount);
                        $("#CameraRecord_autoaccountname").val(d.data[0].username);
                        $("#CameraRecord_WaterNumber").val(d.data[0].meternum);
                        $("#CameraRecord_Address").val(d.data[0].address);
                        var rhtml = "";
                        for (var i = 0; i < d.data[1].length; i++) {
                            if (d.data[1][i].phototype == 1) {
                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${d.data[1][i].url}" title="类型:表盘抄表图片"><div >编号:${d.data[1][i].photocode}</div><div>时间:${d.data[1][i].phototime} 拍摄人:${d.data[1][i].createpeople}</div></div>`;
                            }
                            else if (d.data[1][i].phototype == 2) {
                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${d.data[1][i].url}" title="类型:现场图片"><div>编号:${d.data[1][i].photocode}</div><div>时间:${d.data[1][i].phototime} 拍摄人:${d.data[1][i].createpeople}</div></div>`;
                            }
                            else if (d.data[1][i].phototype == 3) {
                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${d.data[1][i].url}" title="类型:故障处理后图片"><div>编号:${d.data[1][i].photocode}</div><div>时间:${d.data[1][i].phototime} 拍摄人:${d.data[1][i].createpeople}</div></div>`;
                            }
                            else if (d.data[1][i].phototype == 4) {
                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${d.data[1][i].url}" title="类型:故障图片"><div>编号:${d.data[1][i].photocode}</div><div>时间:${d.data[1][i].phototime} 拍摄人:${d.data[1][i].createpeople}</div></div>`;
                            }
                            else {
                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:200px;height:200px" src="${d.data[1][i].url}" title="类型:其他类型图片"><div>编号:${d.data[1][i].photocode}</div><div>时间:${d.data[1][i].phototime} 拍摄人:${d.data[1][i].createpeople}</div></div>`;
                            }
                        }
                        $("#CameraRecord_Photo").html(rhtml);
                        form.render(null, 'CameraRecord');
                    });
                }
            });
            modelinfo = "照片记录";
            $("#modelinfo").html(modelinfo);
        }
       
    });
      //监听故障维修按钮
    form.on('submit(troubleshooting_button)', function () {
        console.log("故障");
        if (autoaccount === "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.requesturl + '/api/Troubleshooting/Getinfo',
                method: 'post',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('Troubleshooting/TroubleshootingShow', d).done(function () {
                        form.render();
                        $("#Trouble_autoaccount").val(d.data.autoaccount);
                        $("#Trouble_autoaccountname").val(d.data.username);
                        $("#Trouble_WaterNumber").val(d.data.meternum);
                        $("#Trouble_Address").val(d.data.address);

                    });
                }
            });
            modelinfo = "故障维修";
            $("#modelinfo").html(modelinfo);
        }
       
    });

    //监听重置按钮
    form.on('submit(reset)',function () {
        $("#acconut").val("");
        $("#username").val("");
        $("#meternum").val("");
        $("#address").val("");
        $("#phone").val("");
        $("#area").val("");
        $("#mr_booknum").val("");
        $("#meterreading").val("");
        $("#region").val("");
        $("#userinfo").val("");

        form.render();

    });

    //表格渲染
    function UserTableRender(tabledata){
        table.render({
            elem: '#userinfoshow',
            data: tabledata,
            size: 'sm', //小尺寸的表格
            cols: [[
                {title: '序号', type: 'numbers', fixed: 'left'},
                {field: 'account', title: '用户编号'},
                {field: 'username', title: '用户姓名'},
                {field: 'autoaccount', title: '用户自动编号'},
            ]]
            , page: true
            , limit: 5
            , limits: [5, 10, 15]
        })
    }

    //监听行单击事件
    table.on('row(userinfoshow)', function(obj){
        $(".layui-table-body.layui-table-main tr").css("background-color", "");
        //console.log(obj.data) //得到当前行数据
        autoaccount = obj.data.autoaccount;
        $("#Theautpaccount").val(obj.data.autoaccount);
        //点击单行
        $(this).attr('style',"background:#f1dddd;color:#000");
    });

    //监听用户信息按钮
    form.on('submit(userinfo_button)',function () {
        console.log(autoaccount);
        if(autoaccount==""){
            layer.msg("请选择用户！！");
        }
        else{
            admin.req({
                url:layui.setter.requesturl+'/api/HomePageUserInfo/UserInfoShow',
                method:'post',
                data:{
                    "autoaccount": autoaccount,
                },
                success:function (d) {
                    var data=d.data;
                    view('UserSel_Home_Conterior').render('homeuserinfoshow/homeuserinfo',data).done(function () {
                        console.log(data[0].username);
                        $("#Account").val(data[0].account);
                        $("#userName").val(data[0].username);
                        $("#Telephone").val(data[0].telephone);
                        $("#Region").val(data[0].regionname);
                        $("#Area").val(data[0].areaname);
                        $("#Address").val(data[0].address);
                        $("#Meternum").val(data[0].meternum);
                        $("#caliber").val(data[0].caliber);
                        $("#init_number").val(data[0].bwcode);
                        $("#max_number").val(data[0].maxrange);
                        $("#install_place").val(data[0].posname);
                        $("#GPSplace").val(data[0].GISPlace);
                        $("#end_number").val(data[0].lastwaternum);
                        if(data[0].meterstate==0){
                            $("#status").val("未使用");
                        }
                        else if(data[0].meterstate==1){
                            $("#status").val("正常");
                        }
                        else if(data[0].meterstate==2){
                            $("#status").val("暂停用水");
                        }
                        else if(data[0].meterstate==3){
                            $("#status").val("注销");
                        }

                        $("#Bookno").val(data[0].bookno);
                        $("#bookName").val(data[0].bookname);
                        $("#merterReadernum").val(data[0].mrreadernumber);
                        $("#mrreaderName").val(data[0].mrreadername);
                    });
                }
            });
            modelinfo="用户信息";
            $("#modelinfo").html(modelinfo);
        }
    });

    //监听换表记录按钮
    form.on('submit(change_meter_record_button)',function () {
        if(autoaccount==""){
            layer.msg("请选择用户！！");
        }
        else{
            admin.req({
                url:layui.setter.request+'',
                method:'',
                data:{
                    "autoaccount": autoaccount,
                },
                success:function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('',d).done(function () {

                    });
                }
            });
            modelinfo="换表记录";
            $("#modelinfo").html(modelinfo);
        }
    });

    //监听抄表记录按钮
    form.on('submit(meter_reading_record_button)',function () {
        if(autoaccount==""){
            layer.msg("请选择用户！！");
        }
        else{
            admin.req({
                url:layui.setter.request+'',
                method:'',
                data:{
                    "autoaccount": autoaccount,
                },
                success:function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('',d).done(function () {

                    });
                }
            });
            modelinfo="抄表记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听待缴记录按钮
    form.on('submit(paid_record_button)',function () {
        if(autoaccount==""){
            layer.msg("请选择用户！！");
        }
        else{
            admin.req({
                url:layui.setter.request+'',
                method:'',
                data:{
                    "autoaccount": autoaccount,
                },
                success:function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('',d).done(function () {

                    });
                }
            });
            modelinfo="待缴记录";
            $("#modelinfo").html(modelinfo);
        }

    });
    exports('console', {});
});