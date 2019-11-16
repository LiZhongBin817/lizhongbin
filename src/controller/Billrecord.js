// JavaScript source code
//LYSд
layui.define(['table', 'view', 'form', 'admin', 'laydate'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin
        , laydate = layui.laydate

    table.render({
        elem: '#Billrecordtable',
        method: 'HttpGet',
        url: layui.setter.requesturl + '/api/Billrecord/billread',
        cols: [[

            { title: '���', width: 70, type: 'numbers', field: 'Sum' },
            { filed: 'payseq', title: '�˵���ˮ��', width: 180 },
            { filed: 'bmttype', title: '��ˮ����', width: 180 },
            { filed: 'taskperiodname', title: '�˵��·�', width: 180 },
            { filed: 'lastwaternum', title: '��������', width: 180 },
            { filed: 'carrywatercount', title: '��������', width: 180 },
            { filed: 'startnum', title: '����', width: 180 },
            { filed: 'endnum', title: 'ֹ��', width: 180 },
            { filed: 'starttime', title: '������ֹ����', width: 180 },
            { filed: 'waterfee', title: '�˵����', width: 180 },
            { filed: 'cbalance', title: '�˻����', width: 180 },


        ]],
        page: true,
        limit: 5,
        limits: [5, 10, 15],

    });
    form.on('submit(Button002)', function (obj) {

        var field = obj.field;
        admin.req({
            url: layui.setter.requesturl + '/api/Recordpaid/billread',
            type: 'HttpGet',
            data: {
                "startime": field.startime001,
                "endtime": field.endtime001
            },


        });
    });

    exports('Billrecord', {})
});