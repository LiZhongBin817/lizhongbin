
/*Title:抄表数据管理
 * Creator:彭跃彪
 * Date:2019.10.14
 */
layui.define(['table', 'view', 'form', 'admin', 'laydate', 'echarts', 'carousel'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin
        , echarts = layui.echarts
        , laydate = layui.laydate;

    //表格渲染
    table.render({
        elem: '#dataAnalysis_SumInfo_Table',
        method: 'post',
        toolbar: true,
        url: layui.setter.requesturl + '/api/DataSearch/ReadAnalysis_Sum',
        cols: [[ 
            { title: '序号', type: 'numbers', totalRowText: '合计', align: 'center' },
            { field: 'mrreadername', title: '抄表员', align: 'center'  },
            { field: 'mindatatime', title: '开始时间'  },
            { field: 'maxdatetime', title: '结束时间'  },
            { field: 'readmetertime', title: '抄表时长', totalRow: true, align: 'center' },
            { field: 'meternum', title: '抄表个数', totalRow: true, align: 'center' },
            { field: 'readmonth', title: '抄表月份', align: 'center'  }, 
        ]], totalRow: true
        , page: true
        , height: $(document).height() - $('#dataAnalysis_SumInfo_Table').offset().top - 200
        , limit: 10
        , limits: [5, 10, 15]
        , done: function (result) {
            layer.close(load); 
            //  抄表员的抄表个数
            $.ajax({
                type: 'post',
                url: layui.setter.requesturl + '/api/DataSearch/ReadAnalysis_key', //web.xml中注册的Servlet的url-pattern
                success: function (result) {
                    if (result) {
                        var temp1 = result.data.datalist01, temp2 = result.data.datalist02;
                        var num = temp1.length;
                        var data2 = [];
                        for (var i = 0; i < num; i++) {
                            var addobject = {
                                value: temp2[i],
                                name: temp1[i]
                            }
                            data2.push(addobject);
                        } var res = "container";
                        var o4 = "抄表员与抄表个数";
                        showadc(temp1, data2, res, o4);
                    }
                },
                error: function (errorMsg) {
                    alert("加载数据失败");
                }
            });
            //抄表员的抄表时间
            $.ajax({
                type: 'post',
                url: layui.setter.requesturl + '/api/DataSearch/ReadAnalysis_val', //web.xml中注册的Servlet的url-pattern
                success: function (result) {
                    if (result) {
                        var temp1 = result.data.datalist01, temp2 = result.data.datalist02;
                        var num = temp1.length;
                        var data2 = [];
                        for (var i = 0; i < num; i++) {
                            var addobject = {
                                value: temp2[i],
                                name: temp1[i]
                            }
                            data2.push(addobject);
                        } var res = "container01";
                        var o4 = "抄表员和抄表时间";
                        showadc(temp1, data2, res, o4);
                    }
                },
                error: function (errorMsg) {
                    alert("加载数据失败");
                }
            });   
        }
    });

    //监听查询
    form.on('submit(number_Search01)', function (obj) {
        var field = obj.field;
        table.reload('dataAnalysis_SumInfo_Table', {
            where: {
                'mrreadername': field.mrreadername,
                'readDatetime01': field.readDatetime01,
                'page': 1
            }
        });
    });

    // 导出 
    $('#btn_export02').on('click', function () {
        window.location.href = layui.setter.requesturl + '/api/DataSearch/OutExcelReadAnalysisSum';
    }); 
    //下拉框渲染
    admin.req({
        url: layui.setter.requesturl + '/api/DataSearch/render_ReaderAnalysis',
        type: "post",
        success: function (result) {
            workData = result.data;
            var strs = "";
            strs += '<option value = "">请选择</option>'
            for (var x in workData) {
                strs += '<option value = "' + workData[x] + '">' + workData[x] + '</option>'
            }
            $("#mrreadername").html(strs);
            form.render();
        }
    });

    //饼图js
    function showadc(o1, o2, o3, o4) {
        var dom = document.getElementById(o3);
        var myChart = echarts.init(dom);
        var app = {};
        option = null;
        option = {
            title: {
                text: '常德牌水表厂',
                subtext: o4,
                x: 'left'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: 'left',
                data: o1
            },
            series: [
                {
                    name: '访问来源',
                    type: 'pie',
                    radius: '55%',
                    center: ['30%', '40%'],
                    data: o2,
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };
        ;
        if (option && typeof option === "object") {
            myChart.setOption(option, true);
        }
    };
    exports('ReadAnalysis_Sum', {});
});