var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('#btnAddNew').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);

            var username = document.getElementById('m_username');
            var name = document.getElementById('m_name');
            var password = document.getElementById('m_password');
            var address = document.getElementById('m_address');
            var email = document.getElementById('m_email');
            var phone = document.getElementById('m_phone');

            if (username.value == "" || name.value == "" || password.value == "") {
                bootbox.alert("Chưa nhập thông tin cần thiết")
                return;
            }
            $.ajax({
                url: "/User/AddUserAjax",
                data:
                {
                    username: username.value,
                    name: name.value,
                    password: password.value,
                    address: address.value,
                    email: email.value,
                    phone: phone.value
                },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        bootbox.alert({
                            message: "Thêm tài khoản thành công!",
                            size: 'medium',
                            closeButton: false
                        });
                        username.value = "";
                        name.value = "";
                        password = "";

                        window.location.href = "/Admin/User";
                    }
                    else {
                        bootbox.alert(
                            {
                                message: "Thêm tài khoản lỗi",
                                closeButton: false
                            }
                        );
                    }
                }
            });
        });
    }
}
user.init();