$("#DatiDettaglioPrenotazione").ready(function () {
    var param = new URLSearchParams(window.location.search)
    var id = param.get('id')
    var tableRow = '<tr>';
    $.ajax({
        url: "http://localhost:53935/api/prenotazione/GetPrenotazione/" + id,
        type: 'GET',
        dataType: "json",
    })
   
        .done(function (json) {
            console.log("done");
            $('#DatiDettaglioPrenotazione').empty()
            $.each(json, function (index, jsonObject) {

                   tableRow += '<td>' + jsonObject + '</td>';
            });
            tableRow += "</tr>";
            $('#DatiDettaglioPrenotazione').append(tableRow);
        })

        .fail((xhr) => {
            console.log("error");
        });
    
});