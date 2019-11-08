
/*Title:抄表数据管理
 * Creator:彭跃彪
 * Date:2019.10.08
 */
layui.define(['table', 'view', 'form', 'admin', 'laydate', 'echarts', 'carousel'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin
        , laydate = layui.laydate
        , carousel = layui.carousel
        , device = layui.device(); 
    //单个柱状图渲染
    function singlerender(o) {
        //标准柱状图
        var echnormcol = [], normcol = [
            {
                title: {
                    text: '抄表个数与时间量化统计',
                    subtext: '常德水表厂'
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: ['抄表个数', '抄表时长']
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        data: o.readtime
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: [
                    {
                        name: '抄表个数',
                        type: 'bar',
                        data: o.readnum,
                        markPoint: {
                            data: [
                                { type: 'max', name: '最大值' },
                                { type: 'min', name: '最小值' }
                            ]
                        },
                        markLine: {
                            data: [{ type: 'average', name: '平均值' }]
                        }
                    },
                    {
                        name: '抄表时长',
                        type: 'bar',
                        data: o.metertime,
                        markPoint: {
                            data: [

                            ]
                        },
                        markLine: {
                            data: [
                                { type: 'average', name: '平均值' }
                            ]
                        }
                    }
                ]
            }
        ]
            , elemNormcol = $('#LAY-index-normcol' + o.readerid).children('div')
            , renderNormcol = function (index) {
                echnormcol[index] = echarts.init(elemNormcol[index], layui.echartsTheme);
                echnormcol[index].setOption(normcol[index]);
                window.onresize = echnormcol[index].resize;
            };
        if (!elemNormcol[0]) return;
        renderNormcol(0);
        //console.log(renderNormcol);
    }
    //初始化
    function ShowPage(data, num) {
        var alist = {
            readerid: 0,
            readmonth: [],
            readnum: [],
            readname: "",
            readtime: [],
            metertime: []
        };
        var sumlist = [];
        for (var i = 0; i < num; i++) {
            var temp = data[i], temp2 = i === data.length - 1 ? { id: null } : data[i + 1]; 
                alist.readmonth.push(temp.metermonth ? temp.metermonth : "undefind");
                alist.readnum.push(temp.meternum);
                alist.readtime.push(temp.readtime ? temp.readtime : "undefind");
                alist.metertime.push(temp.readmetertime);
                if (temp.id !== temp2.id) {
                    alist.readerid = temp.id;
                    alist.readname = temp.mrreadername;
                    sumlist.push(alist);
                    alist = {
                        readerid: 0,
                        readname: "",
                        readmonth: [],
                        readnum: [],
                        readtime: [],
                        metertime: []
                    }
                } 
        }
        return sumlist;
    } 
    function renderhtml(d) {
        var sumhtml = ``;
        for (var i = 0; i < d.length; i++) {
            var htm = `<div class="layui-card">
                    <div class="layui-card-header">${d[i].readname}</div>
                    <div class="layui-card-body">
                        <div class="layui-carousel layadmin-carousel layadmin-dataview" data-anim="fade" lay-filter="LAY-index-normcol">
                            <div carousel-item id="LAY-index-normcol${d[i].readerid}">
                                <div><i class="layui-icon layui-icon-loading1 layadmin-loading"></i></div>
                            </div>
                        </div>
                    </div>
                </div>`;
            sumhtml += htm;
        }
        $("#Histogramcontainer").html(sumhtml);
    } 
    //表格渲染
    table.render({
        elem: '#dataAnalysisInfo_Table',
        method: 'post',
        toolbar: true, 
        url: layui.setter.requesturl + '/api/DataSearch/ReadAnalysis', 
        cols: [[
            { title: '序号', type: 'numbers',   totalRowText: '合计' },
            { field: 'mrreadername', title: '抄表员', align:'center' },
            { field: 'readtime', title: '抄表日期'  },
            { field: 'mindatatime', title: '开始时间' },
            { field: 'maxdatetime', title: '结束时间'  },
            { field: 'readmetertime', title: '抄表时长', totalRow: true, align: 'center'},
            { field: 'meternum', title: '抄表个数', totalRow: true, align: 'center' },
            { field: 'metermonth', title: '抄表月份', align: 'center'  }, 
        ]], totalRow: true
        , page: true
        , limit: 10
        , limits: [5, 10, 15]
        , done: function (data) {
            layer.close(load);
            var num = data.count;
            var renderdata = ShowPage(data.data, num);//渲染所有抄表员的柱状图
            renderhtml(renderdata);
            //轮播切换
            $('.layadmin-carousel').each(function () {
                var othis = $(this);
                carousel.render({
                    elem: this
                    , width: '100%'
                    , arrow: 'none'
                    , interval: othis.data('interval')
                    , autoplay: othis.data('autoplay') === true
                    , trigger: (device.ios || device.android) ? 'click' : 'hover'
                    , anim: othis.data('anim')
                });
            }); 
            for (var i = 0; i < renderdata.length; i++) {
                if (renderdata[i].readtime != null) {
                    singlerender(renderdata[i]);

                } 
            } 
        }
    });

    //监听查询
    form.on('submit(numberSearch)', function (obj) {
        var field = obj.field;
        table.reload('dataAnalysisInfo_Table', {
            where: {
                'mrreadername': field.mrreadername,
                'readDatetime01': field.readDatetime01,
                'page': 1
            }
        });
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
    //监听行数据
  /*  table.on('row(dataAnalysisInfo)', function (obj) {  
        var num = obj.data.meternum;
        var time = obj.data.readmetertime;
         var readtime=obj.data.readtime;
       // ChangeNum(months, num, time);
        console.log(obj.data);
         
    });*/
    // 导出 
    $('#button_export').on('click', function () {
        window.location.href = layui.setter.requesturl + '/api/DataSearch/OutExcelReadAnalysis';
    });

    exports('ReadAnalysis', {});
});