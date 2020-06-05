$("#DatiDettaglioEvento").ready(function () {
    var param = new URLSearchParams(window.location.search)
    var id = param.get('id')
    var tableRow = '<tr>';
    $.ajax({
        url: "http://localhost:53935/api/evento/GetEvento/" + id,
        type: 'GET',
        dataType: "json",
     })
        .done(function (json) {
            console.log("done");
            $('#DatiDettaglioEvento').empty()
            $.each(json, function (index, jsonObject) {

                tableRow += '<td>' + jsonObject + '</td>';
            });
            tableRow += "</tr>";
            $('#DatiDettaglioEvento').append(tableRow);
        })

        .fail((xhr) => {
            console.log("error");
        });

});