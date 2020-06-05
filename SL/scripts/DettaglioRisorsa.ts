$("#DatiDettaglioRisorse").ready(function () {
    var param = new URLSearchParams(window.location.search)
    var id = param.get('id')
    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorse/" + id,
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiDettaglioRisorse').empty()
            var tableRow = '<tr>';
            $.each(json, function (index, jsonObject) {
                   tableRow += '<td>' + jsonObject + '</td>';
             });
            tableRow += "</tr>";
                 $('#DatiDettaglioRisorse').append(tableRow);
            
        })
        .fail((xhr) => {
            console.log("error");
        });

});