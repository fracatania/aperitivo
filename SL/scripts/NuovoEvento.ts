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
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            // set value property of opt
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

$("#inputGroupIDEvento").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/evento/GetMaxIDEvento",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (index == "ID") {
                    jsonObject++;
                    var sel = document.getElementById('inputGroupIDEvento');
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
    $("#btnNuovoEvento").click(function () {
        var id = $("#inputGroupIDEvento option:selected").val();
        var descrizione = $("#inputDescrizioneEvento").val();
        var data = $('#inputDataEvento').val();
        var RisorsaEvento = $("#inputGroupSelectRisorsa option:selected").val();

        $.ajax({
            url: 'http://localhost:53935/api/evento/AddEvento',
            type: 'POST',
            dataType: 'json',
            data: {
                ID: id,
                Descrizione: descrizione,
                Data: data,
                Risorse: RisorsaEvento
            },
        })
            .done(function (xhr) {
                window.location.href = "Eventi.html";
            })
            .fail((xhr) => {
                window.location.href = "Eventi.html";
            })
    });
});