/**
@Name：layuiAdmin 主页控制台
@Author：李忠斌
@Date：2019.11.15
*/


layui.define(['admin', 'view', 'table', 'jquery', 'form','bMap',], function (exports) {
    var admin = layui.admin,
        view = layui.view,
        table = layui.table,
        $ = layui.jquery,
        bMap = layui.bMap,
        form = layui.form;
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


    //监听模糊查询按钮
    form.on('submit(user_info_show_like_search)', function (d) {
        var field = d.field;
        admin.req({
            url: layui.setter.requesturl + '',
            method: '',
            data: {

            },
            success: function (data) {

            }

        });
    });

    //监听条件查询按钮
    form.on('submit(user_info_show_search)', function (d) {
        var field = d.field;
        console.log(field);
        admin.req({
            url: layui.setter.requesturl + '/api/HomePageUserInfo/UserInfoSearch',
            method: 'post',
            data: {
                "account": field.account,
                "username": field.username,
                "meternum": field.meternum,
                "address": field.address,
                "telephone": field.telephone,
                "region": field.region,
                "area": field.area,
                "bookno": field.bookno,
                "mrreadername": field.mrreadername,
                page: 1,
            },
            success: function (data) {
                var tabledata = data.data;//存放表格数据
                if (data.msg == "ok") {
                    UserTableRender(tabledata);
                }
                else {
                    var initdata = new Array();
                    UserTableRender(initdata);
                }
            }

        });
    });

    //监听重置按钮
    form.on('submit(reset)', function () {
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
    function UserTableRender(tabledata) {
        table.render({
            elem: '#userinfoshow',
            data: tabledata,
            size: 'sm', //小尺寸的表格
            cols: [[
                { title: '序号', type: 'numbers', fixed: 'left' },
                { field: 'account', title: '用户编号' },
                { field: 'username', title: '用户姓名' },
                { field: 'autoaccount', title: '用户自动编号' },
            ]]
            , page: true
            , limit: 5
            , limits: [5, 10, 15]
        })
    }

    //监听行单击事件
    table.on('row(userinfoshow)', function (obj) {
        $(".layui-table-body.layui-table-main tr").css("background-color", "");
        console.log(obj.data) //得到当前行数据
        autoaccount = obj.data.autoaccount;
        //点击单行
        $(this).attr('style', "background:#f1dddd;color:#000");
    });

    //监听用户信息按钮
    form.on('submit(userinfo_button)', function () {
        console.log(autoaccount);
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.requesturl + '/api/HomePageUserInfo/UserInfoShow',
                method: 'post',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    var data = d.data;
                    view('UserSel_Home_Conterior').render('homeuserinfoshow/homeuserinfo', data).done(function () {
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
                        if (data[0].meterstate == 0) {
                            $("#status").val("未使用");
                        }
                        else if (data[0].meterstate == 1) {
                            $("#status").val("正常");
                        }
                        else if (data[0].meterstate == 2) {
                            $("#status").val("暂停用水");
                        }
                        else if (data[0].meterstate == 3) {
                            $("#status").val("注销");
                        }

                        $("#Bookno").val(data[0].bookno);
                        $("#bookName").val(data[0].bookname);
                        $("#merterReadernum").val(data[0].mrreadernumber);
                        $("#mrreaderName").val(data[0].mrreadername);
                    });
                }
            });
            modelinfo = "用户信息";
            $("#modelinfo").html(modelinfo);
        }
    });

    //监听换表记录按钮
    form.on('submit(change_meter_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            layui.use(['form', 'laydate'], function (){ 
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('OneUserManagement/ChangeWater', null).done(function () {
                        console.log(document.getElementById('acconut').value);
                        table.render({
                            where: {
                                "autoaccount": autoaccount,
                            },
                            elem: '#ChangeWater_Table',
                            method: 'get', 
                            url: layui.setter.requesturl + '/api/OneUserManagement/changewater', 
                            cols: [[
                                { title: '序号', type: 'numbers',width:110 },
                                { field: 'autoaccount', title: '用户编号', width: 110 },
                                { field: 'meternum', title: '水表编号', width: 110 },
                                { field: 'caliber', title: '口径', width: 110 },
                                { field: 'bwcode', title: '初始底数', width: 110},
                                { field: 'posname', title: '安装位置', width: 110 },
                                { field: 'lastwaternum', title: '截止底数', width: 110 },
                                { field: 'meterstate', title: '状态', width: 110 },
                                { field: 'installtime', title: '安装时间', width: 110 },
                                { field: 'readername', title: '安装人', width: 110 },
                                { field: 'remark', title: '换表原因', width: 110 },
                                { field: 'updatemetertime', title: '更换时间', width: 110 },
                                { field: 'GISPlace', title: 'Gis位置', width: 110},
                                { field: 'processpreson', title: '换表人', width: 110},
                                { title: 'maxrange', title: '最大量程', width: 110 },
                            ]]
                            , page: true
                            , limit: 10
                        });
                    });
                
                console.log("123");
                console.log(autoaccount);
                
            });
            modelinfo = "换表记录";
            $("#modelinfo").html(modelinfo);
        }
    });

    //监听抄表记录按钮
    form.on('submit(meter_reading_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "抄表记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听待缴记录按钮
    form.on('submit(paid_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "待缴记录";
            $("#modelinfo").html(modelinfo);
        }

    });
    //监听账单记录按钮
    form.on('submit(bill_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "账单记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听交易记录按钮
    form.on('submit(transaction_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "交易记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听发票记录按钮
    form.on('submit(invoice_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "发票记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听照片记录按钮
    form.on('submit(photo_record_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {
                    });
                }
            });
            modelinfo = "照片记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听地理位置按钮
    form.on('submit(geographical_position_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.requesturl + '/api/OneUserManagement/geograpposition',
                method: 'get',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    view('UserSel_Home_Conterior').render('OneUserManagement/GeograpPosition', d).done(function () {
                                GPS(d.data);
                                console.log(d.data);
                                form.render();
                     });
                }
            });
            modelinfo = "地理位置1";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听故障维修按钮
    form.on('submit(troubleshooting_button)', function () {
        if (autoaccount == "") {
            layer.msg("请选择用户！！");
        }
        else {
            admin.req({
                url: layui.setter.request + '',
                method: '',
                data: {
                    "autoaccount": autoaccount,
                },
                success: function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('', d).done(function () {

                    });
                }
            });
            modelinfo = "故障维修";
            $("#modelinfo").html(modelinfo);
        }
    }); 

    //显示地图的方法
    function GPS(uploadGPS) {
        //百度地图渲染
        bMap.render({
            ak: 'D2b4558ebed15e52558c6a766c35ee73'//一旦设置了全局，此（发起请求的相关）参数就会失效。
            , https: true
            , done: function () {
                var points = [];
                var data = [];
                for (var i = 0; i < uploadGPS.length; i++) {
                    var obj = uploadGPS[i];
                    var ll = obj.split(",");
                    var arr = [ll[0], ll[1]];
                    data[data.length] = arr;
                }
                console.log(data);
                for (var i = 0; i < data.length; i++) {

                    points.push(new BMap.Point(data[i][0], data[i][1]));
                }
                var options = {
                    size: BMAP_POINT_SIZE_SMALL,
                    shape: BMAP_POINT_SHAPE_STAR,
                    color: '#0f0'
                }
                var map = new BMap.Map("BaiDuPagecontainer2");
                map.centerAndZoom("常德", 12);
                map.enableScrollWheelZoom(true);//开启鼠标滚轮缩放
                map.addControl(new BMap.NavigationControl());
                map.addControl(new BMap.ScaleControl());
                map.addControl(new BMap.OverviewMapControl({ isOpen: true }));
                var polyline = new BMap.Polyline(points, options);
                console.log(map);
                map.addOverlay(polyline);  // 添加Overlay
                //添加海量点
                for (var i = 0, pointslen = points.length; i < pointslen; i++) {
                    var point = new BMap.Point(points[i].lng, points[i].lat); //将标注点转化成地图上的点
                    var marker = new BMap.Marker(point); //将点转化成标注点
                    map.addOverlay(marker);  //将标注点添加到地图上
                    //通过drivingroute获取一条路线的point
                }
            }

        });
    }
    exports('console', {});
});