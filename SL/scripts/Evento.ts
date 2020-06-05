
function DeleteEvento(id) {
    console.log("btndelete");
    $.ajax({
        url: 'http://localhost:53935/api/evento/DeleteEvento/' + id,
        type: 'DELETE',
        dataType: 'json',
    })
        .done( function (xhr) {
            alert("Evento " + id + " Cancellato");
            window.location.href = "Eventi.html";
    })
        .fail( function (xhr) {
            alert("Evento legato a prenotazione");
            window.location.href = "Eventi.html";
        })
    };


function CaricaEventi() {

    $.ajax({
        url: "http://localhost:53935/api/evento/GetEventi",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiEventi').empty()
            var id = '';
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            id = jsonObject[key];
                            tableRow += '<td> <a href="DettaglioEvento.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += '<td> <button onclick="DeleteEvento('+id+')" class="btn btn-primary" id="btnDeleteEvento" value="' + id + '">Cancella</button></td>';
                    tableRow += "</tr>";
                    $('#DatiEventi').append(tableRow);
                }
                
            });
        })
        .fail((xhr) => {
            console.log("error");
        });
};

$("#btnCercaEventoDescrizione").click(function () {

    var descrizione = $("#inputCercaEventoDescrizione").val();
    $.ajax({
        url: "http://localhost:53935/api/evento/geteventodescrizione/" + descrizione,
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiEventi').empty()
            var id = '';
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            id = jsonObject[key];
                            tableRow += '<td> <a href="DettaglioEvento.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += '<td> <button onclick="DeleteEvento(' + id + ')" class="btn btn-primary" id="btnDeleteEvento" value="' + id + '">Cancella</button></td>';
                    tableRow += "</tr>";
                    $('#DatiEventi').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });

});

$("#btnCercaEventoAnno").click(function () {

    var anno = $("#inputCercaEventoAnno").val();
    $.ajax({
        url: "http://localhost:53935/api/evento/geteventoanno/" + anno,
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiEventi').empty()
            var id = '';
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            id = jsonObject[key];
                            tableRow += '<td> <a href="DettaglioEvento.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += '<td> <button onclick="DeleteEvento(' + id + ')" class="btn btn-primary" id="btnDeleteEvento" value="' + id + '">Cancella</button></td>';
                    tableRow += "</tr>";
                    $('#DatiEventi').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });

});

