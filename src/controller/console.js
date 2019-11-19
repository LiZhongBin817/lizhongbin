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
                }
                $("#region").html(str);
                form.render();
            }
        }
    });
    //监听区域下拉框
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
                    }
                    $("#area").html(str1);
                    form.render();
                }
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
                if(data.msg=="ok"){
                    UserTableRender(tabledata);
                }
                else{
                    var initdata=new Array();
                    UserTableRender(initdata);
                }
            }

        });
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
        console.log(obj.data) //得到当前行数据
        autoaccount=obj.data.autoaccount;
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
                url:layui.setter.requesturl+"",
                method:'post',
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
                url:layui.setter.requesturl+'/api/HomePageMeterReadingRecord/MeterReadingRecordInfo',
                method:'post',
                data:{
                    "autoaccount": autoaccount,
                },
                success:function (d) {
                    //页面渲染，地址自己填
                    view('UserSel_Home_Conterior').render('homemeterreadingrecord/meterreadingrecord',d.data).done(function () {

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
                                                return `<a  style="text-decoration: unset" lay-submit lay-filter="see_checkedpircure">点击查看图片</a>`
                                            }
                                        }
                                    },
                                ]]
                                , page: true
                                , limit: 10
                                , limits: [5, 10, 15]
                            });
                        }

                        //监听查看按钮
                        table.on('tool(mr_recordinfo)',function (d) {
                           var data=d.data;
                           var event=d.event;
                            if(event=='see_recheckedrecord'){
                                console.log(data);

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
    //监听照片记录按钮
    form.on('submit(photo_record_button)',function () {
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
            modelinfo="照片记录";
            $("#modelinfo").html(modelinfo);
        }
    });
    //监听故障维修按钮
    form.on('submit(troubleshooting_button)',function () {
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
            modelinfo="故障维修";
            $("#modelinfo").html(modelinfo);
        }
    });
    exports('console', {});
});