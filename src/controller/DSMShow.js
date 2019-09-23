/*Title:�ɹ�������
 *Creator:������
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
            { field: 'DSMID', title: '���', width: 100, sort: true, fixed: 'left' },
            { field: 'DSMNumber', title: '���ϱ��', width: 150 },
            { field: 'DSMType', title: '��������', width: 150 },
            { field: 'DSMContent', title: '��������', width: 150 },
            { field: 'DSMTime', title: '�ϱ�ʱ��', width: 200 },
            { field: 'DSMReportPerson', title: '�ϱ���', width: 150 },
            { field: 'DSMEnclosure', title: '����', width: 100, event:'Enclosure' },
            { field: 'DSMStatus', title: '״̬', width: 100 },
            { field: 'DSMAddress', title: 'λ����Ϣ', width: 100, event:'Address' },
            { field: 'DSMOperation', title: '����', width: 150, fixed: 'right', align: 'center', toolbar: '#DSMbarDemo'}
        ]],
        page: true,
        limit: 10,
        limits: [10, 15, 20],
        
    });

    $(function () {
        admin.req({
            type: "post",
            url: '',//��̨��ַ
            data: {},
            success: function (data) {
                //��ȡ���id
                var table = document.getElementById("DSMShow");
                //ͨ��ѭ������ȡ�����ݲ������
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
                    c9.innerHTML = "<button class='btn btn-link' event:'Address'>�鿴</button>";
                    var c10 = row.insertCell(9);
                    c10.innerHTML = data[i].DSMOperation;
                    //ͼƬ֮ǰ��10�У����Դ�11�п�ʼ����ͼƬ·����forѭ����11��ʼ
                    //ѭ��ֹͣ�������ǻ�ȡ��ͼƬ�������Ϲ̶�������ͨ������j����ȡ��ͼƬ���뵽ÿһ����
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

    //������ѯ
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

    //��������
    table.on('tool(DSM)', function (obj) {
        var data = obj.data,
            event = obj.event,
            tr = obj.tr;
        //��������
        if (event =="Enclosure") {
            admin.req({
                url: '',//��̨��ͼƬ��ַ
                type: "post",
                data: {

                },
                success: function (data) {
                
                }
            });
            admin.popup({
                title: '����ҳ��',
                area: ['700px', '500px'],
                maxmin: true,
                id: 'enclosure',
                success: function (layero,index) {
                    //��ȡ����е���
                    
                }
            });
        }
    })
        
    exports('DSMShow', {})
})