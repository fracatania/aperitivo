$("#inputGroupSelectRisorsa").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorse",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var sel = document.getElementById('inputGroupSelectRisorsa');
                    // create new option element
                    var opt = document.createElement('option');

                    // create text node to add to option element (opt)
                    var textNode = '';

                     // set value property of opt
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            opt.value = jsonObject[key];
                        }
                        else {
                            textNode += jsonObject[key] + ' '
                        }
                    });
                    opt.appendChild(document.createTextNode(textNode));
                    // add opt to end of select box (sel)
                    sel.appendChild(opt);
                }
            });
        })
        .fail((xhr) => {
            console.log("error");
        });
});


$("#inputGroupSelectEvento").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/evento/geteventi",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var sel = document.getElementById('inputGroupSelectEvento');
                    // create new option element
                    var opt = document.createElement('option');

                    // create text node to add to option element (opt)
                    var textNode = '';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                              // set value property of opt
                            opt.value = jsonObject[key];
                        }
                        else {
                              // set text property of opt
                            textNode += jsonObject[key] + ' '
                        }
                    });
                    opt.appendChild(document.createTextNode(textNode));
                   // add opt to end of select box (sel)
                    sel.appendChild(opt);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });
});

$("#inputGroupSelectSala").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/sala/getsale",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    if (jsonObject['Prenotabile']) {
                        //   $.each(Object.keys(jsonObject), function (i, key) {
                        var sel = document.getElementById('inputGroupSelectSala');
                        // create new option element
                        var opt = document.createElement('option');

                        // create text node to add to option element (opt)
                        var textNode = '';
                        $.each(Object.keys(jsonObject), function (i, key) {
                            if (key != "Prenotabile") {
                                if (i == 0) {
                                 // set value property of opt
                                    opt.value = jsonObject[key];
                                 }
                                 else {
                                     if (key == "Nome") {
                                         textNode += jsonObject[key] + ' '
                                     }
                                     if (key == "NumeroPosti") {
                                         textNode += 'Numero Posti: ' + jsonObject[key] + ' '
                                     }
                                }
                            }
                        });
                        opt.appendChild(document.createTextNode(textNode));
                        // add opt to end of select box (sel)
                        sel.appendChild(opt);
                    }
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });
});



$("#inputGroupIDPrenotazione").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/prenotazione/GetMaxIDPrenotazione",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (index == "ID") {
                    jsonObject++;
                    var sel = document.getElementById('inputGroupIDPrenotazione');
                    // create new option element
                    var opt = document.createElement('option');
                    // create text node to add to option element (opt)
                    opt.appendChild(document.createTextNode(jsonObject));
                    // set value property of opt
                    opt.value = jsonObject;
                    // add opt to end of select box (sel)
                    sel.appendChild(opt);
                }
            });
        })
        .fail((xhr) => {
            console.log("error");
        });
});


$(function () {
    $("#btnNuovaPrenotazione").click(function () {
        var id = $("#inputGroupIDPrenotazione option:selected").val();
        var descrizione = $("#inputDescrizionePrenotazione").val();
        var dataInizio = $('#inputDataPrenotazioneInizio').val();
        var dataFine = $('#inputDataPrenotazioneFine').val();
        var RisorsaPrenotazione = $("#inputGroupSelectRisorsa option:selected").val();
        var EventoPrenotazione = $("#inputGroupSelectEvento option:selected").val();
        var SalaPrenotazione = $("#inputGroupSelectSala option:selected").val();

        $.ajax({
            url: 'http://localhost:53935/api/prenotazione/AddPrenotazione',
            type: 'POST',
            data: {
                ID: id,
                Descrizione: descrizione,
                PrenotazioneRisorsa: RisorsaPrenotazione,
                DataInizio: dataInizio,
                DataFine: dataFine,
                PrenotazioneEvento: EventoPrenotazione,
                PrenotazioneSala: SalaPrenotazione
            }
        })          
          .done(function(xhr) {
                alert("Prenotazione Inserita");
                window.location.href = "Prenotazioni.html";
            })
            .fail(function(xhr) {
                var a = xhr.statusCode.toString();
                alert("Prenotazione Non Inserita");
                window.location.href = "Prenotazioni.html";
            })
       
    });
});