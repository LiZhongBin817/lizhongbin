/*Title:抄表数据管理
 * Creator:彭跃彪
 * Date:2019.9.21
 */
layui.define(['table', 'view', 'form', 'admin', 'laydate'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin
        , laydate = layui.laydate 

    //表格渲染
    table.render({
        elem: '#dataSearchInfo_Table',
        method: 'post',
        url: layui.setter.requesturl + '/api/DataSearch/ShowDataSearchInfo',
        cols: [[
            { field: 'id', title: '序号', width: 110 },
            { field: 'account', title: '户号', width: 110 },
            { field: 'username', title: '户名', width: 110 },
            { field: 'meternum', title: '表号', width: 110 },
            { field: 'regionname', title: '区域', width: 110 },
            { field: 'areaname', title: '小区', width: 110 },
            { field: 'telephone', title: '联系电话', width: 110 },
            { field: 'meterbooknumber', title: '抄表册', width: 110 },
            { field: 'address', title: '地址', width: 110 },
            { field: 'mrreadername', title: '抄表员', width: 110 },
            { field: 'readDateTime', title: '抄表月份', width: 110 },
            { field: 'lastmonthdata', title: '上月读数', width: 110 },
            { field: 'nowmonthdata', title: '本月读数', width: 110 },
            { field: 'usewaternum', title: '本月用量', width: 110 },
            { field: 'omrdatetime', title: '抄表时间', width: 110 },
            {
                field: 'readtype', title: '抄表状态', width: 110 ,
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
                    else {
                        intValue = "异常";
                    }
                    return intValue;
                }
            },
            { field: 'meterstatus', title: '表况', width: 110 },
            { field: ' ', title: '图片', width: 110 }, 
        ]]
        , page: true
        , height: $(document).height() - $('#dataSearchInfo_Table').offset().top - 330
        , toolbar: true
        , limit: 10
        , limits: [5, 10, 15]
        , done: function () {
            layer.close(load);
        }
    });

    //下拉框渲染 
    admin.req({
        url: layui.setter.requesturl + '/api/DataSearch/render_regionInfo',
        type: "post",
        success: function (result) {
            workData = result.data;
            var strs = "";
            strs += '<option value = "">请选择</option>'
            for (var x in workData) {
                strs += '<option value = "' + workData[x] + '">' + workData[x] + '</option>'
            }
            $("#regionname").html(strs);
            form.render();
        }
    }); 
    //监听区域下拉框
    form.on('select(regionnames)', function (d) {
        var nos = $("#regionname").val();
        console.log(nos);
        admin.req({
            url: layui.setter.requesturl + '/api/DataSearch/renderdataInfo',
            type: 'post',
            data: {
                'JsonData': nos
            },
            success: function (d) {
                data = d.data;
                var strs = "";
                for (var x in data) {
                    strs += '<option value = "' + data[x] + '">' + data[x] + '</option>'
                }
                $("#areaname").html(strs);
                form.render();

            }


        });
    });
    //监听查询
    form.on('submit(numSearch)', function (obj) {
        var field = obj.field;
        table.reload('dataSearchInfo_Table', {
            where: {
                'account': field.account,
                'username': field.username,
                'meternum': field.meternum,
                'telephone': field.telephone,
                'meterbooknumber': field.meterbooknumber,
                'mrreadername': field.mrreadername,
                'ordatatime01': field.ordatatime01,
                'ordatatime02': field.ordatatime02,
                'regionname': field.regionname,
                'areaname': field.areaname,
                'page': 1
            }
        });
    });
    // 导出

    $('#btn_export01').on('click', function () {
        window.location.href = layui.setter.requesturl + '/api/DataSearch/OutExcelDataSearch';
    });

    exports('DataSearch', {});
});
