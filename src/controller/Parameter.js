/*Title:��������
 * Creator:���ұ�
 * Date:2019.8.19
 */
layui.define(['table', 'form', 'view'], function (exports) {
    var $ = layui.$
        , table = layui.table
        , form = layui.form
        , view = layui.view
        , load = layer.load(3)
        , admin = layui.admin;
    //�����Ⱦ
    table.render({
        elem: '#parameterdata'
        , method: 'Get'
        , url: 'http://localhost:8081/ParameterShow'
        , cols: [[
            { field: 'ID', title: 'ID', width: 50, fixed: 'left' },
            { field: 'parameternumber', title: '�������', width: 150 },
            { field: 'parametername', title: '��������', width: 150 },
            { field: 'parametertype', title: '��������', width: 150 },
            { field: 'parametertypename', title: '����������', width: 150 },
            { field: 'parameterkey', title: 'Code', width: 150 },
            { field: 'parametervalue', title: '����ֵ', width: 150 },
            { field: 'Remark', title: '����', width: 150 },
            { title: '����', width: 150, toolbar: '#barDemo', align: 'center', fixed: 'right' }
        ]]
        , page: true
        , limit: 20
        , toolbar: '#toolbarDemo1'
        , limits: [20, 30, 40]
        , done: function () {
            layer.close(load);
        }
    });

    //��������ѯ
    form.on('submit(search)', function (obj) {
        var field = obj.field;
        table.reload('parameterdata', {
            where: {
                "parameterNumber": field.parameterNumber,
                "parameterName": field.parameterName,
                "parameterType": field.parameterType
            }
        });
    });

    //������������Ӱ�ť
    table.on('toolbar(Parameter)', function (obj) {
        var LayEvent = obj.event;
        if (LayEvent == "AddParameter") {
            admin.popup({
                id: 'AddParameter'
                , title: '���'
                , area: ['500px', '540px']
                , success: function (layero, index) {
                    view(this.id).render('SysManage/ParameterSetting/AddParameter', null).done(function () {
                        form.render(null, 'addparameter');
                        //���õ�����ı���
                        layer.style(index, {
                            "background": '#ECF5FF'
                        });
                        //�����ύ��ť
                        form.on('submit(Submit)', function (obj) {
                            var field = obj.field;
                            var load = layer.load(3);
                            admin.req({
                                url: 'http://localhost:8081/AddParameter'
                                , method: 'Get'
                                , data: {
                                    "JsonData": JSON.stringify(field)
                                }
                                , success: function (data) {
                                    if (data.msg == "OK") {
                                        table.reload('parameterdata');
                                        layer.msg("��ӳɹ�");
                                        layer.close(index);
                                        layer.close(load);
                                    }
                                    else {
                                        layer.msg("��������Ѵ���");
                                        layer.close(load);
                                    }
                                }
                            });
                        });
                    })
                }
            });
        }
    });

    //�����������Ĳ���
    table.on('tool(Parameter)', function (obj) {
        var LayEvent = obj.event
            , data = obj.data;
        if (LayEvent == "EditParameter") {
            admin.popup({
                id: 'EditParameter'
                , title: '�༭'
                , area: ['500px', '540px']
                , success: function (layero, index) {
                    view(this.id).render('SysManage/ParameterSetting/EditParameter', data).done(function () {
                        form.render(null, 'editparameter');

                        //���õ�����ı���
                        layer.style(index, {
                            "background": '#ECF5FF'
                        });

                        //�����ύ��ť
                        form.on('submit(Edit-Submit)', function (Data) {
                            var field = Data.field;
                            var load = layer.load(3);
                            var SendData = {
                                "parameternumber": field.parameterNumber,
                                "parametername": field.parameterName,
                                "parametertype": field.parameterType,
                                "parametertypename": field.parameterTypeName,
                                "parameterkey": field.parameterKey,
                                "parametervalue": field.parameterValue,
                                "Remark": field.Remark,
                            };
                            console.log(data.ID);
                            admin.req({
                                url: 'http://localhost:8081/EditParameter'
                                , method: 'Get'
                                , data: {
                                    "JsonData": JSON.stringify(SendData),
                                    "ID": data.ID
                                }
                                , success: function (data) {
                                    if (data.msg == "OK") {
                                        table.reload('parameterdata');
                                        layer.msg("�޸ĳɹ�");
                                        layer.close(index);
                                        layer.close(load);
                                    }
                                    else {
                                        layer.msg("�޸�ʧ��");
                                        layer.close(load);
                                    }
                                }
                            });
                        });
                    })
                }
            });
        }
    });

    exports('Parameter', {})
});