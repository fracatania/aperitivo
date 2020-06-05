$("#inputGroupIDSala").ready(function () {

    $.ajax({
        url: "http://localhost:53935/api/sala/GetMaxIDSale",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $.each(json, function (index, jsonObject) {
                if (index == "ID") {
                    jsonObject++;
                    var sel = document.getElementById('inputGroupIDSala');
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
            console.log("Errore Recupero ID Sala");
        });
});

$(function () {
    $("#btnNuovaSala").click(function () {
        var id = $("#inputGroupIDSala option:selected").val();
        var nome = $("#inputNomeSala").val();
        var posti = $("#inputPostiSala").val();
        var prenotabile = $("#inpuPrenotabileSala option:selected").val();
       
        $.ajax({
            url: 'http://localhost:53935/api/sala/AddSala',
            type: 'POST',
            dataType: 'json',
            data: {
                ID: id,
                Nome: nome,
                NumeroPosti: posti,
                Prenotabile: prenotabile
            },
        })
            .done(function (xhr) {
               // alert("Sala Inserita");
                window.location.href = "Sale.html";
            })
            .fail((xhr) => {
              //  alert("Risorsa Non Inserita! Mail o username già presenti");
                window.location.href = "Sale.html";
            })
        });
    });