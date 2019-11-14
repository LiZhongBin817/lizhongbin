/*Title:故障维修页面
 *Creator:丁俊杰
 * Date:2019.11.14
 */

layui.define(['form', 'util', 'table', 'laydate', 'admin', 'view', 'layer', 'layedit', 'upload', 'jquery'], function (exports) {
    var form = layui.form,
        table = layui.table,
        admin = layui.admin,
        laydate = layui.laydate,
        view = layui.view,
        upload = layui.upload,
        $ = layui.$;
    table.render({
        elem: '#Troubletable',
        url: layui.setter.requesturl + '/api/',
        method: 'post',
        clos: [[
            { field: 'TroubleID', title: '序号', width: 100, sort: true, fixed: 'left' },
            { field: 'TroubleStatus', title: '状态', width: 150},
            { field: 'TroubleType', title: '故障种类', width: 150 },
            { field: 'TroubleReportTime', title: '上报时间', width: 150 },
            { field: 'TroubleReporter', title: '上报人', width: 150 },
            { field: 'ToubleReason', title: '故障原因', width: 200 },
            { field: 'TroubleSearch', toolbar: '#TroubleshootingShowbarDemo', width: 100, fixed: 'right', align: 'center'}
        ]],
        page: true,
        limit: 10,
        limits:[5,10,15]
    });

    //监听查询
    form.on('submit(Trouble_Seek)', function (obj) {
        var field = obj.field;
        table.reload('Troubletable', {
            where: {
                "TroubleStarTime": field.TroubleStarTime,
                "TroubleEndTime": field.TroubleEndTime,
                "TroubleType": field.TroubleType_Select
            },
             page: {
                "curr": 1,
                "nums": 10
            }
        });
    });

    exports('TroubleshootingShow', {});
});