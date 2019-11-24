/**
@Name：layuiAdmin 主页控制台
@Author：李忠斌
@Date：2019.11.15
*/


<<<<<<< HEAD
layui.define(['admin', 'view', 'table', 'jquery', 'form','bMap',], function (exports) {
=======
layui.define(['admin', 'view', 'table', 'jquery', 'form'], function (exports) {
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
    var admin = layui.admin,
        view = layui.view,
        table = layui.table,
        $ = layui.jquery,
<<<<<<< HEAD
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
=======
        form = layui.form;
    var modelinfo="";//用来存放点击按钮后显示的模块号
    var autoaccount="";
    //渲染区域下拉框
    admin.req({
        url:layui.setter.requesturl + '/api/HomePageUserInfo/RegionSelectRender',
        method:'post',
        success:function (d) {
            var data=d.data;
            var str=`<option value="">请选择</option>`;
            if(d.msg=="ok"){
                for(var i in data ){
                    str+=`<option value="${data[i].regionno}">${data[i].regionname}</option>`;
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
                }
                $("#region").html(str);
                form.render();
            }
        }
    });
    //监听区域下拉框
<<<<<<< HEAD
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
=======
    form.on('select(region)',function (d) {
        var regionno=$("#region").val();
        //渲染小区下拉框
        admin.req({
            url:layui.setter.requesturl + '/api/HomePageUserInfo/AreaSelectRender',
            method:'post',
            data:{
                "regionno":regionno
            },
            success:function (d) {
                var data=d.data;
                console.log(data);
                var str1=`<option value="">请选择</option>>`;
                if(d.msg=="ok"){
                    for(var i in data ){
                        str1+=`<option value="${data[i].areano}">${data[i].areaname}</option>`;
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
                    }
                    $("#area").html(str1);
                    form.render();
                }
            }
        });
    });
<<<<<<< HEAD
=======


    //监听模糊查询按钮
    form.on('submit(user_info_show_like_search)',function (d) {
        var field=d.field;
        admin.req({
            url:layui.setter.requesturl+'',
            method:'',
            data:{
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899


<<<<<<< HEAD
    //监听模糊查询按钮
    form.on('submit(user_info_show_like_search)', function (d) {
        var field = d.field;
        admin.req({
            url: layui.setter.requesturl + '',
            method: '',
            data: {

            },
            success: function (data) {

=======
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
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
<<<<<<< HEAD
            success: function (data) {
                var tabledata = data.data;//存放表格数据
                if (data.msg == "ok") {
=======
            success:function (data) {
                var tabledata=data.data;//存放表格数据
                if(data.msg=="ok"){
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
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
<<<<<<< HEAD
        autoaccount = obj.data.autoaccount;
=======
        autoaccount=obj.data.autoaccount;
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
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
<<<<<<< HEAD
                url: layui.setter.request + '',
                method: '',
                data: {
=======
                url:layui.setter.requesturl+"",
                method:'post',
                data:{
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
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
<<<<<<< HEAD
                url: layui.setter.request + '',
                method: '',
                data: {
=======
                url:layui.setter.requesturl+'/api/HomePageMeterReadingRecord/MeterReadingRecordInfo',
                method:'post',
                data:{
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
                    "autoaccount": autoaccount,
                    page:1
                },
                success: function (d) {
                    //页面渲染，地址自己填
<<<<<<< HEAD
                    view('UserSel_Home_Conterior').render('', d).done(function () {
=======
                    view('UserSel_Home_Conterior').render('homemeterreadingrecord/meterreadingrecord',d.data).done(function () {

                        console.log(d.data);
                        homepagetablerender(d.data);
                        //渲染表格
                        function homepagetablerender(data){
                            table.render({
                                elem:'#mr_recordinfo'
                                ,data:data
                                ,cols:[[
                                    {field:'account',title:'用户编号',width:100,fixed:'left',align:'center'},
                                    {field:'taskperiodname',title:'月份',width:100,align:'center'},
                                    {field:'lastmonthdata',title:'起码',width:100,align:'center'},
                                    {field:'inputdata',title:'抄回止码',width:100,align:'center'},
                                    {field:'ocrdata',title:'图像识别',width:100,align:'center'},
                                    {field:'readcheckdata',title:'复审读数',width:100,align:'center'},
                                    {field:'readDateTime',title:'抄表时间',width:200,align:'center'},
                                    {field:'mrreadername',title:'抄表人',width:100,align:'center'},
                                    {field:'checkor',title:'审核人',width:100,align:'center'},
                                    {title:'审核记录',width:100,fixed:'right',align:'center',
                                        templet:function (d) {
                                            return `<a  style="border-radius: 3px" lay-submit  lay-event="see_recheckedrecord">查看</a>`
                                        }
                                    },
                                    {field:'pircture',title:'图片',width:100,fixed:'right',align:'center',
                                        templet:function (d) {
                                            if(d.pircture==""){
                                                return  `<a style="text-decoration: unset">暂无图片</a>`
                                            }
                                            else{
                                                return `<a  style="text-decoration: unset" lay-submit lay-event="see_checkedpircure">点击查看图片</a>`
                                            }
                                        }
                                    },
                                ]]
                                , page: true
                                , limit: 10
                                , limits: [5, 10, 15]
                            });
                        }
                        //监听操作中的按钮
                        table.on('tool(mr_recordinfo)',function (d) {
                           var data=d.data;
                           var event=d.event;
                            //监听查看按钮
                            if(event=='see_recheckedrecord'){
                                console.log(data);
                                admin.popup({
                                    id:'mr_record_recheckrecord',
                                    title:'审核记录',
                                    area: ['1100px','500px'],
                                    success:function (layero, index) {
                                        view('mr_record_recheckrecord').render('homemeterreadingrecord/mr_record_recheckedrecord',data).done(function (){

                                            //表格渲染
                                            table.render({
                                                elem:'#mr_record_allinfo',
                                                url:layui.setter.requesturl+'/api/HomePageMeterReadingRecord/MeterReadingRecordInfo',
                                                method:'post',
                                                where:{
                                                    "autoaccount":data.autoaccount,
                                                    page:1,
                                                    index:1,
                                                },
                                                cols:[[
                                                    {field:'taskperiodname',title:'月份',width:100,align:'center'},
                                                    {field:'inputdata',title:'上传读数',width:100,align:'center'},
                                                    {field:'ocrdata',title:'图像识别',width:100,align:'center'},
                                                    {field:'readcheckdata',title:'复审读数',width:100,align:'center'},
                                                    {field:'pircture',title:'图片',width:100,align:'center',
                                                        templet:function (d) {
                                                            if(d.pircture==""){
                                                                return  `<a style="text-decoration: unset">暂无图片</a>`
                                                            }
                                                            else{
                                                                return `<a  style="text-decoration: unset" lay-submit lay-event="see_recheckedpircure">点击查看图片</a>`
                                                            }
                                                        }
                                                    },
                                                    {field:'recheckstatus',title:'状态',width:100,align:'center',
                                                        templet:function (d) {
                                                            if(d.recheckstatus==0){
                                                                return  `<a style="text-decoration: unset">已审</a>`
                                                            }
                                                            else{
                                                                return  `<a style="text-decoration: unset">不通过</a>`
                                                            }
                                                        }
                                                    },
                                                    {field:'createtime',title:'时间',width:200,align:'center'},
                                                    {field:'recheckresult',title:'备注',width:220,align:'center'},
                                                ]]
                                                , page: true
                                                , limit: 10
                                                , limits: [5, 10, 15]
                                            });

                                            //监听查看图片按钮
                                            table.on('tool(mr_record_allinfo)',function (d) {
                                                var data=d.data;
                                                var event=d.event;
                                                console.log(event)
                                                if(event=="see_recheckedpircure"){
                                                    console.log("111");
                                                    admin.popup({
                                                        id: "rechecked_pircure",
                                                        area: ['800px', '500px'],
                                                        title: '查看图片',
                                                        success: function () {
                                                            view("rechecked_pircure").render('homemeterreadingrecord/rechecked_pircure', data).done(function () {
                                                                admin.req({
                                                                    url:layui.setter.requesturl+'/api/HomePageMeterReadingRecord/ShowRecheckedPircure',
                                                                    method:'post',
                                                                    data:{
                                                                        "autoaccount": autoaccount,
                                                                        "taskperiodname":data.taskperiodname
                                                                    },
                                                                    success:function (d) {
                                                                        var alldata=d.data;
                                                                        var photoobject = alldata.pircture.split(',');
                                                                        var phototype=alldata.phototype.split(',');
                                                                        var rhtml = "";
                                                                        for (var i = 0; i < photoobject.length; i++) {
                                                                            if (phototype[i] == 1) {
                                                                                rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:600px;height:500px" src="${photoobject[i]}" title="表盘抄表图片"><div style="font-size:20px;color:#FF2D2D">图片${i+1}--表盘抄表图片</div></div>`;
                                                                            }
                                                                            else if (phototype[i] == 2) {
                                                                                rhtml += `<div style="text-align:center;;margin-top:20px""><img style="width:600px;height:500px" src="${photoobject[i]}"  title="现场图片"><div style="font-size:20px;color:#FF2D2D">图片${i+1}--现场图片</div></div>`;
                                                                            }
                                                                        }
                                                                        $("#rechecked_pircure").html(rhtml);
                                                                    }
                                                                });

                                                            });
                                                        }
                                                    });

                                                }
                                            })
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899

                                        });
                                    }
                                });

                            }
                            //监听点击查看图片按钮
                            if(event=='see_checkedpircure'){
                                admin.popup({
                                    id: "rechecked_passed_pircure",
                                    area: ['800px', '500px'],
                                    title: '查看图片',
                                    success: function () {
                                        view("rechecked_passed_pircure").render('homemeterreadingrecord/rechecked_passed_pircure', data).done(function () {
                                            var photo = data.pircture.split(',');
                                            var phototype=data.phototype.split(',');
                                            var rhtml = "";
                                            for (var i = 0; i < photo.length; i++) {
                                                if (phototype[i] == 1) {
                                                    rhtml += `<div style="text-align:center;margin-top:20px"><img style="width:600px;height:500px" src="${photo[i]}" title="表盘抄表图片"><div style="font-size:20px;color:#FF2D2D">图片${i+1}--表盘抄表图片</div></div>`;
                                                }
                                                else if (phototype[i] == 2) {
                                                    rhtml += `<div style="text-align:center;;margin-top:20px""><img style="width:600px;height:500px" src="${photo[i]}"  title="现场图片"><div style="font-size:20px;color:#FF2D2D">图片${i+1}--现场图片</div></div>`;
                                                }
                                            }
                                            $("#rechecked_passed_pircure").html(rhtml);
                                        });
                                    }
                                });
                            }
                        })

                        //查询按钮
                        form.on('submit(mr_record_search)',function (d) {
                            var startdate=$("#startdate").val();
                            var enddate=$("#enddate").val();
                            admin.req({
                                url:layui.setter.requesturl+'/api/HomePageMeterReadingRecord/MeterReadingRecordInfo',
                                method:'post',
                                data:{
                                    "autoaccount": autoaccount,
                                    "startdate":startdate,
                                    "enddate":enddate,
                                },
                                success:function (d) {
                                    if(d.msg=="OK"){
                                        homepagetablerender(d.data)
                                    }
                                    else{
                                        layer.msg("出现错误！");
                                    }
                                }
                            });
                        })
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
<<<<<<< HEAD
    form.on('submit(troubleshooting_button)', function () {
        if (autoaccount == "") {
=======
    form.on('submit(troubleshooting_button)',function () {
        if(autoaccount==""){
>>>>>>> 931ea7f02683c1bb07bfca1eadc47055eec25899
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