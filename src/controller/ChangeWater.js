
/*Title:换表记录
 * Creator:彭跃彪
 * Date:2019.11.14
 */
layui.define(['table', 'view', 'form', 'admin', 'laydate' , 'carousel'], function (exports) {
    var table = layui.table
        , view = layui.view
        , form = layui.form
        , $ = layui.$
        , load = layer.load(3)
        , admin = layui.admin 
        , laydate = layui.laydate; 
    //表格渲染
    table.render({
        elem: '#ChangeWater_Table',
        method: 'post',
        toolbar: true,
        url: layui.setter.requesturl + '/api/OneUserManagement/changewater',
        cols: [[
            { title: '序号', type: 'numbers'},
            { field: 'autoaccount', title: '用户编号'},
            { field: 'meternum', title: '水表编号'},
            { field: 'caliber', title: '口径'},
            { field: 'bwcode', title: '初始底数'},
            { field: 'installpos', title: '安装位置'},
            { field: 'lastwaternum', title: '截止底数'},
            { field: 'meterstate', title: '状态'},
            { field: 'installtime', title: '安装时间'},
            { field: 'createby', title: '安装人'},
            { field: 'remark', title: '换表原因'},
            { field: 'updatemetertime', title: '更换时间'},
            { field: 'GISPlace', title: 'Gis位置'},
            { field: 'lastmodifyby', title: '换表人'},
            { title: '最大量程',text:'9999'},
        ]]
        , page: true
        , height: $(document).height() - $('#ChangeWater_Table').offset().top - 200
        , limit: 10
        , limits: [5, 10, 15]
        , done: function (result) {
            layer.close(load); 
        }
    });  
    exports('ChangeWater', {});
});