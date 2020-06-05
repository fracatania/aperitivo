function DeletePrenotazione(id) {
    console.log("btndelete");
    $.ajax({
        url: 'http://localhost:53935/api/prenotazione/DeletePrenotazione/' + id,
        type: 'DELETE',
        dataType: 'json',

        complete: function (response) {
            alert("Prenotazione " + id + " Cancellata");
            window.location.href = "Prenotazioni.html";
        }
    })
        .fail(function () {
            console.log("errore");
        })
};


function CaricaPrenotazioni() {

    $.ajax({
        url: "http://localhost:53935/api/prenotazione/GetPrenotazioni",
        type: 'GET',
        dataType: "json",
     })
        .done(function (json) {
            console.log("done");
            $('#DatiPrenotazioni').empty()
            var id = '';
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            id = jsonObject[key];
                            tableRow += '<td> <a href="DettaglioPrenotazione.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += '<td> <button onclick="DeletePrenotazione(' + id + ')" class="btn btn-primary" id="btnDeletePrenotazione" value="' + id + '">Cancella</button></td>';
                    tableRow += "</tr>";
                    $('#DatiPrenotazioni').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });
};
