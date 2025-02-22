﻿
//抄表率分析


layui.define(['table', 'admin', 'laydate', 'form', 'view'], function (exports) {
    var table = layui.table;
    var admin = layui.admin;
    var form = layui.form;
    var view = layui.view;
    var laydate = layui.laydate;
    var $ = layui.$;
    var input = "";

    function pagerender(data1, data2) {
        //显示数据---表格一
        table.render({
            elem: '#Copyseetable',//渲染指定元素(表格ID)
            data: data1,
            cols: [[

                {
                    title: '序号', width: 70, type: 'numbers', field: 'Sum', totalRowText: '本页合计'
                },
                { field: 'name', title: '抄表员', width: 180 },
                { field: 'month', title: '抄表月份', width: 160 },
                { field: 'drop', title: '已抄表', width: 160, totalRow: true },
                { field: 'copy', title: '实抄表', width: 160, totalRow: true },
                { field: 'shoudcopy', title: '应抄表', width: 160, totalRow: true },
                {
                    field: 'droprate', title: '抄见率', width: 160, totalRow: true,
                },
                { field: 'copyrate', title: '实抄率', width: 160, totalRow: true },
                // { field: 'benyeheji', title: '本页合计', width: 160,fixed:'left',fixed:''  }
            ]],
            page: true,
            totalRow: true,
            toolbar: true,
            limit: 5,
            limits: [5, 10, 15
            ],
            done: function (res) {
                var onwanceTotal = 0;
                var onwanceTota2 = 0;
                var onwanceTota3 = 0;
                var seecopyrate;
                var realcopyrate;
                layui.each(res.data, function (index, d) {
                    onwanceTotal += Number(d.drop);
                    onwanceTota2 += Number(d.copy);
                    onwanceTota3 += Number(d.shoudcopy);
                })
                seecopyrate = onwanceTotal / onwanceTota3;
                realcopyrate = onwanceTota2 / onwanceTota3;

                $(".layui-table-total [data-field ='droprate'] .layui-table-cell").text(seecopyrate * 100 + '%');
                $(".layui-table-total [data-field ='copyrate'] .layui-table-cell").text(realcopyrate * 100 + '%');
                // $(".layui-table-total [data-field = 'copyrate'] .layui-table-cell").text(onwanceTota2 * 100 + '%');
            }
        });
        //表格
        table.render({
            elem: "#allcopyseetable",
            data: data2,
            cols: [[
                { field: 'allalreadycopy', title: '总已抄', width: 180 },
                { field: 'allreallycopy', title: '总实抄', width: 180 },
                { field: 'allshoudcopy', title: '总应抄', width: 180 },
                { field: 'allcopyrate', title: '总抄见率', width: 180 },
                { field: 'allreallyrate', title: '总实抄率', width: 180 },
            ]]
        });
    }

    //监听提交
    form.on('submit(Buttonone)', function (obj) {
        // console.log("10001");
        var field = obj.field;
        admin.req({
            url: layui.setter.requesturl + '/api/CopySee/ShowCopysee',
            type: 'post',
            data: {
                "taskperiodname": field.taskperiodname,
                "mrreadername": field.bookman
            },
            
            success: function (d) {
            
                if (d.msg == "ok" && d.data.length > 0) {
                    var tabletwo = {
                        // one: obj.data.copy,
                        allalreadycopy: d.data[0].datatwo.allalreadycopy,
                        allreallycopy: d.data[0].datatwo.allreallycopy,
                        allshoudcopy: d.data[0].datatwo.allshoudcopy,
                        allcopyrate: d.data[0].datatwo.allcopyrate,
                        allreallyrate: d.data[0].datatwo.allreallyrate
                    };
                    var arrysum = [];
                    arrysum.push(tabletwo);
                    pagerender(d.data, arrysum);
                }
                else {
                    var a = new Array();
                    var b = new Array();
                    layer.msg("无数据");
                    pagerender(a, b);
                }

            }

        });
    });


    // 导出

    $('#Buttontow').on('click', function () {
        window.location.href = layui.setter.requesturl + "/api/CopySee/OutExcel1?taskperiodname=" + $('#wx').find("#taskperiodname").val() + "&mrreadername=" + $('#wx').find("#bookman").val();
    });



    //给抄表员下拉框给值
    admin.req({
        url: layui.setter.requesturl + '/api/CopySee/Serchname',
        type: 'post',
        data: {
        },
        success: function (obj) {
            for (var i = 0; i < obj.data.length; i++) {
                input += `<option value="${obj.data[i]}">${obj.data[i]}</option>`;
            }
            //console.log(input);
            $('#bookman').append(input);
            form.render();
        }
    });

    //给静态表格值
    admin.req({
        url: layui.setter.requesturl + '/api/CopySee/ShowCopysee',
        type: 'post',
        data: {
        },
        success: function (obj) {
            //var str = "";//定义用于拼接的字符串
            //for (var i = 0; i < 1; i++) {
            //    str = "<tr><td>"+ ' '+ "</td><td>" + obj.data[i].dataone.allyichao + "</td><td>" + obj.data[i].dataone.allshichao + "</td><td>" + obj.data[i].dataone.allyingchao + "</td><td>" + obj.data[i].dataone.allchaojianlv + "</td><td>" + obj.data[i].dataone.allshichaolv + "</td> </tr>"; 
            //}
            ////console.log(input);
            //$('#jingtai').append(str);
            if (obj.data != null) {
                var tabletwo = {
                    // one: obj.data.copy,  
                    allalreadycopy: obj.data[0].datatwo.allalreadycopy,
                    allreallycopy: obj.data[0].datatwo.allreallycopy,
                    allshoudcopy: obj.data[0].datatwo.allshoudcopy,
                    allcopyrate: obj.data[0].datatwo.allcopyrate,
                    allreallyrate: obj.data[0].datatwo.allreallyrate

                };


                console.log(obj.data);
                console.log("111");
                var arrysum = [];
                arrysum.push(tabletwo);
                pagerender(obj.data, arrysum);
                console.log(obj.data);
                //console.log(obj.data[1].datatwo);
                console.log(tabletwo);
                form.render();
            }
            else {
                var a = new Array();
                var b = new Array();
                layer.msg("无数据");
                pagerender(a, b);
                form.render();
            }
        }


    });

    //给抄表月份导入日期
    laydate.render({
        elem: '#taskperiodname'
        , type: 'month'
        , format: 'yyyyMM'
    });


    exports('Copysee', {})
})
