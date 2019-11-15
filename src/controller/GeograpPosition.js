layui.define(['table', 'admin', 'laydate', 'form', 'view', 'bMap', 'jquery'], function (exports) {
    var table = layui.table;
    var admin = layui.admin;
    var form = layui.form;
    var view = layui.view
    var laydate = layui.laydate;
    var bMap = layui.bMap;
    var $ = layui.jquery;


//��ʾ�ٶȵ�ͼ
table.on('tool(DosageAnomalyAnalysis)', function (obj) {
    var data = obj.data.uploadgisplace//��õ�ǰ������
        , layEvent = obj.event; //��� lay-event ��Ӧ��ֵ 
    var GSPData = new Array();
    GSPData.push(data);
    console.log(GSPData);
    if (layEvent == 'detail') {
        layer.msg('�鿴����');
        admin.popup({
            id: "GPSShow_Page",
            title: "��ַ��Ϣ",
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
    bMap.render(function () {//����ֱ��д�ص���������
        var map = new BMap.Map("BaiDuPagecontainer1"); //js�ϸ����ִ�Сд�����BMap��bMap��������ͬ������
        var point = new BMap.Point(116.404, 39.915);  // ����������  
        map.centerAndZoom(point, 15);                 // ��ʼ����ͼ���������ĵ�����͵�ͼ����  
        console.log(map);
    });
    console.log(bMap);
}

//��ʾ��ͼ�ķ���
function GPS(uploadGPS) {
    //�ٶȵ�ͼ��Ⱦ
    bMap.render({
        ak: 'D2b4558ebed15e52558c6a766c35ee73'//һ��������ȫ�֣��ˣ������������أ������ͻ�ʧЧ��
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
            map.centerAndZoom("����", 12);
            map.enableScrollWheelZoom(true);//��������������
            map.addControl(new BMap.NavigationControl());
            map.addControl(new BMap.ScaleControl());
            map.addControl(new BMap.OverviewMapControl({ isOpen: true }));
            var polyline = new BMap.Polyline(points, options);
            console.log(map);
            map.addOverlay(polyline);  // ���Overlay
            //��Ӻ�����
            for (var i = 0, pointslen = points.length; i < pointslen; i++) {
                var point = new BMap.Point(points[i].lng, points[i].lat); //����ע��ת���ɵ�ͼ�ϵĵ�
                var marker = new BMap.Marker(point); //����ת���ɱ�ע��
                map.addOverlay(marker);  //����ע����ӵ���ͼ��
                //ͨ��drivingroute��ȡһ��·�ߵ�point
            }
        }

    });

    console.log(bMap);
    }
    exports('DosageAnomalyAnalysis', {})
})
