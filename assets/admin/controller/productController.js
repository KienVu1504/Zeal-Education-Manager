var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('.editable').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var detailedit = btn.data('detailedit');
            CKEDITOR.instances['m_detailedit'].setData(detailedit);
        });
        $('.badgebtnlink').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var valueImageURL = document.getElementById('e_image');
            valueImageURL.value = "";
            CKEDITOR.instances['m_detailedit'].setData("");
        });
        $('.unique').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var listid = btn.data('listid');
            var name = btn.data('name');

            var code = btn.data('code');
            var metatitle = btn.data('metaTitle');
            var description = btn.data('description');
            var detail = CKEDITOR.instances['m_detailedit'].getData();
            var image = btn.data('image');
            var listtype = btn.data('listType');
            var listfile = btn.data('listFile');

            var valuename = document.getElementById(name);
            var valuecode = document.getElementById(code);
            var valuemetatitle = document.getElementById(metatitle);
            var valuedescription = document.getElementById(description);
            var valueimage = document.getElementById(image);
            var valuelisttype = document.getElementById(listtype);
            var valuelistfile = document.getElementById(listfile);


            $.ajax({
                url: "/User/UpdateUserAjax",
                data:
                {
                    userId: listid,
                    name: valuename.value,
                    address: valueaddress.value,
                    email: valueemail.value,
                    phone: valuephone.value
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        bootbox.alert({
                            message: "Cập nhật thành công!",
                            size: 'medium',
                            closeButton: false
                        });

                        window.location.href = "/Admin/User";
                    }
                    else {
                        bootbox.alert(
                            {
                                message: "Cập nhật tài khoản lỗi",
                                closeButton: false
                            }
                        );
                    }
                }
            });
        });
        $('#btnAddNew').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var name = document.getElementById('m_name');
            var code = document.getElementById('m_code');
            var metaTitle = document.getElementById('m_metaTitle');
            var description = document.getElementById('m_description');
            var image = document.getElementById('m_image');
            var categoryId = document.getElementById('ddlCategory');
            var detail = CKEDITOR.instances['m_detail'].getData();
            var listType = document.getElementById('m_listType');
            var listFile = document.getElementById('m_listFile');

            var ContributeModel = {
                name: name.value,
                code: code.value,
                metaTitle: metaTitle.value,
                description: description.value,
                image: image.value,
                categoryId: categoryId.value,
                detail: detail,
                listType: listType.value,
                listFile: listFile.value
            }

            if (name.value == "" || metaTitle.value == "" || image.value == "") {
                bootbox.alert("Chưa nhập thông tin cần thiết")
                return;
            }
            $.ajax({
                url: "/Product/AddProductAjax",
                data: JSON.stringify(ContributeModel),
                contentType: 'application/json; charset=utf-8',
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        bootbox.alert({
                            message: "Thêm khoá học thành công!",
                            size: 'medium',
                            closeButton: false
                        });
                        name.value = "";
                        code.value = "";
                        metaTitle = "";
                        description = "";
                        image = "";
                        listType = "";
                        listFile = "";

                        window.location.href = "/Admin/Product";
                    }
                    else {
                        bootbox.alert(
                            {
                                message: "Thêm khoá học lỗi",
                                closeButton: false
                            }
                        );
                    }
                }
            });
        });
    }
}
product.init();