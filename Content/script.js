$(document).ready(function () {
    $(".siderbar_menu li").click(function () {
        $(".siderbar_menu li").removeClass("active");
        $(this).addClass("active");
    });

    $(".hamburger").click(function () {
        $(".wrapper").addClass("active");
    });

    $(".close, .bg_shadow").click(function () {
        $(".wrapper").removeClass("active");
    });
});

$(function () {
    var url = window.location.href;
    $("#sub-header a").each(function () {
        if (url == (this.href)) {
            $(this).closest("li").addClass("active");
        }
    })
})

var x = document.getElementById("user-location");

$(function () {
    var options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };

    function success(pos) {
        var crd = pos.coords;
        var lat = crd.latitude.toString();
        var lng = crd.longitude.toString();
        var coordinates = [lat, lng];

        console.log(x);
        getCity(coordinates);
        return;
    }

    function error(err) {
        x.innerHTML = err.message;
    }

    navigator.geolocation.getCurrentPosition(success, error, options);
})

function getCity(coordinates) {
    var xhr = new XMLHttpRequest();
    var lat = coordinates[0];
    var lng = coordinates[1];

    xhr.open('GET', "https://us1.locationiq.com/v1/reverse.php?key=pk.686fc1c555414bc569b4d3a02be52e2b&lat=" + lat + "&lon=" + lng + "&format=json", true);
    xhr.send();
    xhr.onreadystatechange = processRequest;
    xhr.addEventListener("readystatechange", processRequest, false);

    function processRequest(e) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var response = JSON.parse(xhr.responseText);
            var city = response.address.city;
            var country = response.address.country;

            x.innerHTML = city + ", " + country;
            return;
        }
    }
}