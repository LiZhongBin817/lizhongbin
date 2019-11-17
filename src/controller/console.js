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
    //监听用户信息按钮
    form.on('submit(userinfo_button)', function () {
        admin.req({
            url: layui.setter.request + '',
            method: '',
            data: {
                "account": account,
            },
            success: function (d) {
                view('UserSel_Home_Conterior').render('userinfoshow/userinfo', d).done(function () {

                });
            }
        });
        modelinfo = "用户信息";
        $("#modelinfo").html(modelinfo);
    });

    //监听换表记录按钮
    form.on('submit(change_meter_record_button)', function () {
        admin.req({
            url: layui.setter.request + '',
            method: '',
            data: {
                "account": account,
            },
            success: function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('', d).done(function () {

                });
            }
        });
        modelinfo = "换表记录";
        $("#modelinfo").html(modelinfo);
    });

    //监听抄表记录按钮
    form.on('submit(meter_reading_record_button)', function () {
        admin.req({
            url: layui.setter.request + '',
            method: '',
            data: {
                "account": account,
            },
            success: function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('', d).done(function () {

                });
            }
        });
        modelinfo = "抄表记录";
        $("#modelinfo").html(modelinfo);
    });
    //监听待缴记录按钮
    form.on('submit(paid_record_button)', function () {
        admin.req({
            url: layui.setter.request + '',
            method: '',
            data: {
                "account": account,
            },
            success: function (d) {
                //页面渲染，地址自己填
                view('UserSel_Home_Conterior').render('', d).done(function () {
                    form.render();
                });
            }
        });
        modelinfo = "待缴记录";
        $("#modelinfo").html(modelinfo);
    });
    //监听照片记录按钮
    form.on('submit(photo_record_button)', function () {
        admin.req({
            url: layui.setter.requesturl + '/api/Troubleshooting/GetAutoaccountinfo',
            method: 'post',
            data: {
                "autoaccount": "100000000001",
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
    });
    //监听故障维修按钮
    form.on('submit(troubleshooting_button)', function () {
        admin.req({
            url: layui.setter.requesturl + '/api/Troubleshooting/Getinfo',
            method: 'post',
            data: {
                "autoaccount": "100000000001",
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
    });


    exports('console', {});
});