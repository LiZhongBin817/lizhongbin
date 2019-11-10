
/*Title:区域管理
 *Creator:李黎东
 * Date:2019.11.07
 */
layui.define(['table', 'form', 'view'], function (exports) {
    var $ = layui.$;
    var admin = layui.admin;
    var view = layui.view;
    var table = layui.table;
    var form = layui.form;
    //渲染表格
    table.render({
        elem: '#region_table',
        method: 'post',
        url: layui.setter.requesturl + '/api/RegionManage/regionsShow',
        cols: [[
            { title: '序号', fixed: 'left', type: 'numbers' },
            { field: 'regionname', title: '区域名称', width: 200 },
            { field: 'regionno', title: '区域编号', width: 200 },
            { field: 'createtime', title: '创建时间', width: 200 },
            {
                align: 'center', title: '下属小区', templet: function (d) {
                    return '<a style="text-decoration:underline" lay-event="ShowArea">查看</a > '
                }
            },
            { title: '操作', width: 220, toolbar: '#region_barDemo', align: 'center', fixed: 'right' }
        ]],
        page: true,
        limit: 5,
        toolbar: "#region_toolbarDemo",
        limits: [5, 10, 15]
    });
    //监听区域表格中的操作
    table.on('tool(region_table)', function (obj) {
        var layEvent = obj.event;
        var data = obj.data;
        //编辑按钮
        if (layEvent === 'region_edit') {
            admin.popup({
                title: '区域编辑弹窗',
                id: 'RegionEdit',
                area: ['500px', '300px'],
                success: function (layero, index) {
                    view('RegionEdit').render('RegionManage/RegionEdit', data).done(function () {
                        form.render(null, 'Regioneditform');//渲染表单
                        //监听提交
                        form.on('submit(region-edit-submit)', function (Data) {
                            var field = Data.field; //获取提交的字段                            
                            var sendData = {
                                "regionno": field.regionno,
                                "regionname": field.regionname,
                                "regionstate": 1
                            };
                            admin.req({
                                url: layui.setter.requesturl + '/api/RegionManage/editRegion',
                                type: 'post',
                                data: {
                                    "JsonData": JSON.stringify(sendData)
                                },
                                success: function (obj) {
                                    if (obj.msg == "ok") {
                                        layer.msg("成功操作一条数据");
                                        table.reload('region_table');
                                    }
                                }
                            });
                        });
                    })
                }
            })
        }
        //显示下属小区
        if (layEvent == 'ShowArea') {
            admin.popup({
                id: 'areaShow',
                title: '小区显示界面',
                area: ['800px', '500px'],
                success: function (index, layero) {
                    view('areaShow').render('RegionManage/AreaShow', null).done(function () {
                        table.render({
                            url: layui.setter.requesturl + '/api/RegionManage/areaShow',
                            elem: '#area_table',
                            method: 'post',
                            where: {
                                "regionno": data.regionno
                            },
                            cols: [[
                                { title: '序号', fixed: 'left', type: 'numbers' },
                                { field: 'areano', title: '小区编号', width: 200 },
                                { field: 'areaname', title: '小区名称', width: 200 },
                                { field: 'createtime', title: '创建时间', width: 200 },
                                { title: '操作', width: 200, toolbar: '#area_barDemo', align: 'center', fixed: 'right' }
                            ]],
                            page: true,
                            limit: 5,
                            toolbar: "#area_toolbarDemo",
                            limits: [5, 10, 15]
                        });
                        form.render(null, 'Areashowform');


                    });
                }
            });
        }
        //删除区域
        if (layEvent == 'region_delete') {
            layer.confirm('确定删除吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                admin.req({
                    url: layui.setter.requesturl + '/api/RegionManage/deleteRegion',
                    type: 'post',
                    data: {
                        "regionno": data.regionno
                    },
                    success: function (msg) {
                        if (msg.msg == 'ok') {
                            layer.msg("成功操作一条是数据");
                            table.reload('region_table');
                        }
                    }
                });
            });
        }
    });
    //监听查询按钮
    form.on('submit(region_query)', function (obj) {
        var field = obj.field;
        table.reload('region_table', {
            url: layui.setter.requesturl + '/api/RegionManage/regionsShow',
            method: 'post',
            where: {
                "regionname": field.regionname
            }
        });
    });
    //监听区域表工具栏按钮
    table.on('toolbar(region_table)', function (obj) {
        var layEvent = obj.event;
        if (layEvent === "region_add") {
            admin.popup({
                id: 'addregion',
                area: ['500px', '300px'],
                success: function (layero, index) {
                    view('addregion').render('RegionManage/RegionAdd', null).done(function () {
                        form.render(null, 'addregionform');//渲染增加接口的表单
                       
                    });
                }
            });
        }
    });
    //监听添加区域界面的提交按钮
    form.on('submit(addregion_submit)', function (obj) {
        var sendData = {
            "regionno": obj.field.regionno,
            "regionname": obj.field.regionname,
            "regionstate":1
        }
        admin.req({
            url: layui.setter.requesturl + '/api/RegionManage/addRegion',
            data: {
                "JsonData": JSON.stringify(sendData)
            },
            type: 'post',
            success: function (msg) {
                if (msg.msg == 'ok') {
                    layer.msg("成功操作一条数据");
                    table.reload('region_table');
                }
                if (msg.msg = "error") {
                    layer.msg("已存在重复区域名称");
                }
            }
        });
    });
    //监听小区表格表中操作
    table.on('tool(area_table)', function (obj) {
        var layEvent = obj.event;
        var Data = obj.data;
        console.log(Data);
        if (layEvent == "area_edit") {
            admin.popup({
                id: 'area_edit',
                area: ['500px', '400px'],
                success: function (index, layero) {
                    view('area_edit').render('RegionManage/AreaEdit', obj.data).done(function () {
                        form.render(null, 'Areaeditform');
                        //监听小区的编辑提交按钮
                        form.on('submit(area-edit-submit)', function (submitobj) {
                            var sendData = {
                                "regionno": submitobj.field.regionno,
                                "areano": submitobj.field.areano,
                                "areaname": submitobj.field.areaname,
                                "areastate":1
                            }
                            admin.req({
                                url: layui.setter.requesturl + '/api/RegionManage/editArea',
                                data: {
                                    "JsonData": JSON.stringify(sendData)
                                },
                                type: 'post',
                                success: function (msg) {
                                    if (msg.msg == "ok") {
                                        layer.msg("成功操作一条数据");
                                        table.reload('area_table');
                                    }
                                }
                            });
                        });
                    });
                }
            });
        }
        if (layEvent == "area_delete") {
            layer.confirm('确定删除吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                admin.req({
                    url: layui.setter.requesturl + '/api/RegionManage/deleteArea',
                    data: {
                        "areano": Data.areano
                    },
                    type:'post',
                    success: function (msg) {
                        if (msg.msg == "ok") {
                            layer.msg("成功操作一条数据");
                            table.reload("area_table");
                        }
                    }
                });
            })
        }
    });
    table.on('toolbar(area_table)', function (obj) {
        if (obj.event == "area_add") {
            admin.popup({
                id: 'area_add',
                title: '小区添加界面',
                area:['500px','400px'],
                success: function (layero, index) {
                    view('area_add').render('RegionManage/AreaAdd', null).done(function () {
                        form.render(null, 'Areaaddform');
                        form.on('submit(area-add-submit)', function (addareaobj) {
                            var sendData = {
                                "areano": addareaobj.field.areano,
                                "areaname": addareaobj.field.areaname,
                                "regionno": addareaobj.field.regionno,
                                "areastate":1
                            }
                            admin.req({
                                url: layui.setter.requesturl + '/api/RegionManage/addArea',
                                type: 'post',
                                data: {
                                    "JsonData": JSON.stringify(sendData)
                                },
                                success: function (msg) {
                                    if (msg.msg == "ok") {
                                        layer.msg("成功操作一条数据");
                                        table.reload('area_table');
                                    }
                                }
                            });
                        });
                    });
                }
            });
        }
    });
    exports('RegionManage', {})
})