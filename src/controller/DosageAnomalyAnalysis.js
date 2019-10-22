//抄表率分析

//2019.10.22李育松写完


layui.define(['table', 'admin', 'laydate', 'form', 'view', 'bMap', 'jquery'], function (exports) {
    var table = layui.table;
    var admin = layui.admin;
    var form = layui.form;
    var view = layui.view
    var laydate = layui.laydate;
    var bMap = layui.bMap;
    var $ = layui.jquery;

    table.render({
        elem: '#DosageAnomalyAnalysis',
        method: 'post',
        url: layui.setter.requesturl +'/ShowDosageAnomalyAnalysis',
        cols: [[
            { title: '序号', type: 'numbers', width: 60 },
            { field: 'autoaccount', title: '用户编号', width: 100 },
            { field: 'username', title: '用户姓名', width: 100 },
            { field: 'meternum', title: '水表编号', width: 100 },
            { field: 'address', title: '用水地址', width: 100, totalRowText: '合计' },
            { field: 'lastwaternum', title: '上月用量', width: 100 },
            { field: 'carrywatercount', title: '本月用量', width: 100 },
            { field: 'taskperiodname', title: '抄表月份', width: 100 },
            { field: 'startnum', title: '本期起码', width: 100 },
            { field: 'endnum', title: '本期止码', width: 100 },
            { field: 'readname', title: '抄表员', width: 100 },
            { field: 'waterdifference', title: '水量差量', width: 100 },
            { field: 'waterdifferencerate', title: '水量异常比', width: 100},
            { title:'操作',fixed: 'right', width: 165, align: 'center', toolbar: '#BarDemo' }

        ]]
        , page: true
        , toolbar: true
        , limit: 10
        , limits: [10, 20, 30, 40]
        , done: function (res, curr, count) {
            currPage = curr;
            var that = this.elem.next();
          
            $.each(res.data, function (index, obj) {
               // console.log(obj.waterdifference);
                if (obj.waterdifference >= $('#waterdifference').val()) {
                    that.find(".layui-table-box tbody tr[data-index='" + index + "']").css("background-color", "#f3715c");
                   
                }
                if (obj.waterdifferencerate >= $('#waterdifferencerate').val()) {             
                    that.find(".layui-table-box tbody tr[data-index='" + index + "']").css("background-color", "#f3715c");

                }

            })
        }
        
       
    });

   


    //监听查询按钮
    form.on('submit(Button)', function (obj) {
        var field = obj.field;
        table.reload('DosageAnomalyAnalysis', {
            where: {
                "taskperiodname": field.taskperiodname,
                "readname": field.mrreaderman,
                "bookno": field.bookno,
                "autoaccount": field.autoaccount
             
            }
        });
    });
    


    //给抄表员下拉框赋值
     admin.req({
         url: layui.setter.requesturl +'/Serchmrreader',
        type: 'post',
        data: {
        },
         success: function (obj) {         
             for (var i = 0; i < obj.data.length; i++) {
                 $('#mrreaderman').append('<option value="' + obj.data[i] + '">' + obj.data[i] + '</option>');
            }
            form.render();
        }
     });

   //给抄表册传值
    admin.req({
        url: layui.setter.requesturl + '/Serchbookno',
        type: 'post',
        data: {
        },
        success: function (obj) {
            for (var i = 0; i < obj.data.length; i++) {
                $('#bookno').append('<option value="' + obj.data[i] + '">' + obj.data[i] + '</option>');
            }
            form.render();
        }
    });


    //给抄表月份导入日期
    laydate.render({
        elem: '#taskperiodname'
        , type: 'month'
        , format: 'yyyyMM'
    });


    //显示百度地图
    table.on('tool(DosageAnomalyAnalysis)', function (obj) { 
        var data = obj.data.uploadgisplace//获得当前行数据
            , layEvent = obj.event; //获得 lay-event 对应的值 
        var GSPData = new Array();
        GSPData.push(data);
        console.log(GSPData);
        if (layEvent == 'detail') {
            layer.msg('查看操作');
            admin.popup({
                id:"GPSShow_Page",
                title: "地址信息",
                area: ['800px', '700px'],
                success: function (layero, index) {
                    view(this.id).render('DosageAnomalyAnalysis/GPSShow', GSPData).done(function () {
                        GPS(GSPData);
                    });
                }
            });
        }
    });

    function testGps() {
        bMap.render(function () {//这里直接写回调函数即可
            var map = new BMap.Map("BaiDuPagecontainer1"); //js严格区分大小写，因此BMap和bMap是两个不同变量。
            var point = new BMap.Point(116.404, 39.915);  // 创建点坐标  
            map.centerAndZoom(point, 15);                 // 初始化地图，设置中心点坐标和地图级别  
            console.log(map);
        });
        console.log(bMap);
    }

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
                var map = new BMap.Map("BaiDuPagecontainer1");
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

        console.log(bMap);
    }
   

    
    exports('DosageAnomalyAnalysis', {})
})


