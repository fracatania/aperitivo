$("#inputGroupIDRisorsa").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/risorsa/GetMaxIDRisorse",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (index == "ID") {
                    jsonObject++;
                var sel = document.getElementById('inputGroupIDRisorsa');
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
    $("#btnNuovaRisorsa").click(function () {
        var id = $("#inputGroupIDRisorsa option:selected").val();
        var cognome = $("#inputCognomeRisorse").val();
        var nome = $("#inputNomeRisorse").val();
        var username = $("#inputUsernameRisorse").val();
        var mail = $("#inputEmailRisorsa").val();
     
        if (mail == '') {
            alert("Formato Mail non valido");
            window.location.href = "NuovaRisorsa.html";
        }
        else {

            $.ajax({
                url: 'http://localhost:53935/api/risorsa/AddRisorsa',
                type: 'POST',
                data: {
                    ID: id,
                    Cognome: cognome,
                    Nome: nome,
                    Username: username,
                    Mail: mail
                },
            })
                .done(function (xhr) {
                   // alert("Risorsa Inserita");
                    window.location.href = "Risorse.html";
                })
                .fail((xhr) => {
                    alert("Risorsa Non Inserita! Mail o username già presenti");
                    window.location.href = "Risorse.html";
                })
        }
        });
    });
