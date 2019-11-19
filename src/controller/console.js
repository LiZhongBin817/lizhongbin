/**
 @Name：layuiAdmin 主页控制台
 @Author：李忠斌
 @Date：2019.11.15
 */


layui.define(['admin', 'view', 'table', 'jquery', 'form'], function (exports) {
    var admin = layui.admin,
        view = layui.view,
        table = layui.table,
        $ = layui.jquery,
        form = layui.form;
    var modelinfo="";//用来存放点击按钮后显示的模块号
    var account=0;//存放用户编号
    //监听用户信息按钮
    form.on('submit(userinfo_button)',function () {
        admin.req({
            url:layui.setter.request+'',
            method:'',
            data:{
               "account": account,
            },
            success:function (d) {
                view('UserSel_Home_Conterior').render('userinfoshow/userinfo',d).done(function () {

                });
            }
        });
        modelinfo="用户信息";
        $("#modelinfo").html(modelinfo);
    });

    //监听换表记录按钮
    form.on('submit(change_meter_record_button)', function () {
        view('UserSel_Home_Conterior').render('OneUserManagement/ChangeWater', null).done(function () {
            console.log(document.getElementById('acconut').value);
            table.render({
                elem: '#ChangeWater_Table',
                method: 'post',  
                url: layui.setter.requesturl + '/api/OneUserManagement/changewater', 
                where: {
                    "autoaccount": "100000000001",
                    "page": 1,
                    "limit": 10
                },
                cols: [[
                    { title: '序号', type: 'numbers' },
                    { field: 'autoaccount', title: '用户编号' },
                    { field: 'meternum', title: '水表编号' },
                    { field: 'caliber', title: '口径' },
                    { field: 'bwcode', title: '初始底数' },
                    { field: 'posname', title: '安装位置' },
                    { field: 'lastwaternum', title: '截止底数' },
                    { field: 'meterstate', title: '状态' },
                    { field: 'installtime', title: '安装时间' },
                    { field: 'readername', title: '安装人' },
                    { field: 'remark', title: '换表原因' },
                    { field: 'updatemetertime', title: '更换时间' },
                    { field: 'GISPlace', title: 'Gis位置' },
                    { field: 'processpreson', title: '换表人' },
                    { title: 'maxrange', title:'最大量程'},
                ]]
                , page: true
                , limit: 10
            });
        });
        modelinfo = "换表记录";
        console.log("key");
        $("#modelinfo").html(modelinfo);
    });

    //监听抄表记录按钮
    form.on('submit(meter_reading_record_button)',function () {
        admin.req({
            url:layui.setter.request+'',
            method:'',
            data:{
                "account": account,
            },
            success:function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('',d).done(function () {

                });
            }
        });
        modelinfo="抄表记录";
        $("#modelinfo").html(modelinfo);
    });
    //监听待缴记录按钮
    form.on('submit(paid_record_button)',function () {
        admin.req({
            url:layui.setter.request+'',
            method:'',
            data:{
                "account": account,
            },
            success:function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('',d).done(function () {

                });
            }
        });
        modelinfo="待缴记录";
        $("#modelinfo").html(modelinfo);
    });
    //监听照片记录按钮
    form.on('submit(photo_record_button)',function () {
        admin.req({
            url:layui.setter.request+'',
            method:'',
            data:{
                "account": account,
            },
            success:function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('',d).done(function () {

                });
            }
        });
        modelinfo="照片记录";
        $("#modelinfo").html(modelinfo);
    });
    //监听故障维修按钮
    form.on('submit(troubleshooting_button)',function () {
        admin.req({
            url:layui.setter.request+'',
            method:'',
            data:{
                "account": account,
            },
            success:function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('',d).done(function () {

                });
            }
        });
        modelinfo="故障维修";
        $("#modelinfo").html(modelinfo);
    });


    exports('console', {});
});