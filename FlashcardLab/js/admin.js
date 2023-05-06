function countryChart() {
    $('#regions').css("display", "block");
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "Admin.aspx/GetCountries",
        datatype: "json",
        success: function (data) {
            var vals = JSON.parse(data['d'].toString())[0];

            $('#regions').vectorMap({
                map: 'world_mill',
                series: {
                    regions: [{
                        values: vals,
                        scale: ['#e0c8ff', '#8900a4'],
                        normalizeFunction: 'polynomial'
                    }]
                },
                onRegionTipShow: function (e, el, code) {
                    if (typeof vals[code] === 'undefined') {
                        el.html(el.html() + ' (Users - 0)');
                    }
                    else {
                        el.html(el.html() + ' (Users - ' + vals[code] + ')');
                    }
                }
            });
        },
        error: function (xmlhttprequest, textstatus, errorthrown) {
            alert("error: " + errorthrown);
            console.log("error: " + errorthrown);
        }
    });

    document.getElementById('regionsBtn').disabled = true;
}