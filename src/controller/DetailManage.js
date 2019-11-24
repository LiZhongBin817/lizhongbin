/*Title:应抄明细管理
 * Creator:李忠斌
 * Date:2019.9.11
 */

layui.define(['table', 'view', 'form','admin'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin;
    //表格渲染
    table.render({
        elem: '#detailManageInfo_Table',
        method: 'post',
        url: layui.setter.requesturl +'/api/DetailManage/ShowDetailInfo',
        cols: [[
            {title:'序号',width:60,type:'numbers',fixed:'left'},
            { field: 'account', title: '户号', width: 110 },
            { field: 'username', title: '户名', width: 110 },
            { field: 'regionname', title: '区域', width: 110 },
            { field: 'areaname', title: '小区', width: 110 },
            { field: 'address', title: '地址', width: 110 },
            { field: 'mrreadername', title: '抄表员', width: 110 },
            { field: 'bookno', title: '抄表册号', width: 110 },
            { field: 'startnum', title: '上期读数', width: 110 },
            { field: 'inputdata', title: '本期读数', width: 110 },
            {
                field: 'readtype', title: '抄表状态', width: 110, fixed: 'right',
                templet: function (d) {
                    var intValue = "";
                    if (d.readtype == null) {
                        intValue = "未抄";
                    }
                    else if (d.readtype == 1) {
                        intValue = "已抄";
                    }
                    else if (d.readtype == 2) {
                        intValue = "估抄";
                    }
                    else if (d.readtype == 3) {
                        intValue = "异常";
                    }
                    else {

                        intValue = "正常";
                    }
                    return intValue;
                },
            },
            {
                field: 'carrystatus', title: '结转状态', width: 110,fixed:'right',
                templet: function (d) {
                    var Value = "";
                    if (d.carrystatus == null) {
                        Value = "未结转";
                    }
                    else if (d.carrystatus ==1) {
                        Value = "已结转";
                    }
                    return Value;
                }
            },
        ]]
        , page: true
        , limit: 20
        , limits: [20, 30, 40]
        , done: function () {
            layer.close(load);
        }
    });
    //监听查询
    form.on('submit(DetailInfoSearch)', function (obj) {
        var field = obj.field;
        table.reload('detailManageInfo_Table', {
            where: {
                'ReaderName': field.ReaderName,
                'bookno':field.bookno,
                'readtype': field.readtype,
                'page':1,
            }
        })
    });
    exports('DetailManage', {})
});