/*Title:派工单管理
 *Creator:丁俊杰
 * Date:2019.09.17
 */

layui.define(['form', 'table', 'admin', 'view', 'laydate', 'layer', 'laypage', 'index'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        view = layui.view,
        $=layui.$,
        laydate = layui.laydate;
    table.render({
        elem: '#DSMShow',
        col:[[
            { field: 'DSMID', title: '序号', width: 100, sort: true, fixed: 'left' },
            { field: 'DSMNumber', title: '故障编号', width: 150 },
            { field: 'DSMType', title: '故障类型', width: 150 },
            { field: 'DSMContent', title: '故障内容', width: 150 },
            { field: 'DSMTime', title: '上报时间', width: 200 },
            { field: 'DSMReportPerson', title: '上报人', width: 150 },
            { field: 'DSMEnclosure', title: '附件', width: 100, event:'Enclosure' },
            { field: 'DSMStatus', title: '状态', width: 100 },
            { field: 'DSMAddress', title: '位置信息', width: 100, event:'Address' },
            { field: 'DSMOperation', title: '操作', width: 150, fixed: 'right', align: 'center', toolbar: '#DSMbarDemo'}
        ]],
        page: true,
        limit: 10,
        limits: [10, 15, 20],
        
    });

    $(function () {
        admin.req({
            type: "post",
            url: '',//后台地址
            data: {},
            success: function (data) {
                //获取表格id
                var table = document.getElementById("DSMShow");
                //通过循环将获取的数据插入表中
                for (var i = 0; i < data.length; i++) {
                    var row = table.insertRow(table.rows.length);
                    var c1 = row.insertCell(0);
                    c1.innerHTML = data[i].DSMID;
                    var c2 = row.insertCell(1);
                    c2.innerHTML = data[i].DSMNumber;
                    var c3 = row.insertCell(2);
                    c3.innerHTML = data[i].DSMType;
                    var c4 = row.insertCell(3);
                    c4.innerHTML = data[i].DSMContent;
                    var c5 = row.insertCell(4);
                    c5.innerHTML = data[i].DSMTime;
                    var c6 = row.insertCell(5);
                    c6.innerHTML = data[i].DSMReportPerson;
                    var c7 = row.insertCell(6);
                    c7.innerHTML = "<button class='btn btn-link' event:'Enclosure'></button>";
                    var c8 = row.insertCell(7);
                    c8.innerHTML = data[i].DSMStatus;
                    var c9 = row.insertCell(8);
                    c9.innerHTML = "<button class='btn btn-link' event:'Address'>查看</button>";
                    var c10 = row.insertCell(9);
                    c10.innerHTML = data[i].DSMOperation;
                    //图片之前是10列，所以从11列开始插入图片路径，for循环从11开始
                    //循环停止的条件是获取的图片数量加上固定列数，通过变量j将获取的图片插入到每一列中
                    var j = 0;
                    for (var x = 11; x < data[i].img.length+10; x++) {
                        var c11 = row.insertCell(x);
                        c11.innerHTML = data[i].img[j++];
                        c11.style.display = "none";
                    }
                }
            }
        });
    });

    //监听查询
    form.on('submit(DSM_polling)', function (obj) {
        var field = obj.field;
        table.reload('DSMShow', {
            where: {
                "DSMNumber": field.DSMNumber,
                "DSMType": field.DSMType,
                "DSMStatus": field.DSMStatus,
                "DSMTime": field.DSMTime
            }
            , page: {
                "curr": 1,
                "nums": 10
            }
        })
    });

    //监听操作
    table.on('tool(DSM)', function (obj) {
        var data = obj.data,
            event = obj.event,
            tr = obj.tr;
        //监听附件
        if (event =="Enclosure") {
            admin.req({
                url: '',//后台传图片地址
                type: "post",
                data: {

                },
                success: function (data) {
                
                }
            });
            admin.popup({
                title: '附件页面',
                area: ['700px', '500px'],
                maxmin: true,
                id: 'enclosure',
                success: function (layero,index) {
                    //获取表格中的列
                    
                }
            });
        }
    })
        
    exports('DSMShow', {})
})