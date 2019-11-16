// JavaScript source code
layui.define(['table', 'view', 'form', 'admin', 'laydate'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin
        , laydate = layui.laydate

    table.render({
        elem: '#RecordPaidtable',
        method: 'HttpGet',
        url: layui.setter.requesturl + '/api/Recordpaid/Waittopay',
        cols: [[

            { title: '序号', width: 70, type: 'numbers', field: 'Sum' },
            { filed: 'payseq', title: '账单流水号', width: 180 },
            { filed: 'bmttype', title: '用水类型', width: 180 },
            { filed: 'taskperiodname', title: '账单月份', width: 180 },
            { filed: 'lastwaternum', title: '上期用量', width: 180 },
            { filed: 'carrywatercount', title: '本期用量', width: 180 },
            { filed: 'startnum', title: '起码', width: 180 },
            { filed: 'endnum', title: '止码', width: 180 },
            { filed: 'starttime', title: '本期起止日期', width: 180 },
            { filed: 'waterfee', title: '账单金额', width: 180 },
            { filed: 'cbalance', title: '账户余额', width: 180 },


        ]],
        page: true,
        limit: 5,
        limits: [5, 10, 15],

    });
    form.on('submit(Button001)', function (obj) {

        var field = obj.field;
        admin.req({
            url: layui.setter.requesturl + '/api/RecordPaid/Waittopay',
            type: 'HttpGet',
            data: {
                "startime": field.startime,
                "endtime": field.endtime
            },


        });
    });


    exports('RecordPaid', {})
});