'use strict'

$("#btnAllSale").click(function () {
    Sala.caricasale();
});

class Sala {
    ID: number
    numeroPosti: number;
    nome: string;
    prenotabile: boolean;
    constructor(id: number, nposti: number, nome: string, prenotabile: boolean) {
        this.ID = id;
        this.nome = nome;
        this.numeroPosti = nposti;
        this.prenotabile = prenotabile;
    }

    static UpdateSala(id: number) {
        var updatePrenotabile = $("#inputGroupSelectPrenotabile option:selected").val();
        if (updatePrenotabile == 1) {
            var newPrenotabile = "true";
        }
        else {
            var newPrenotabile = "false";
        }
        $.ajax({
            url: 'http://localhost:53935/api/sala/UpdateSala/' + id,
            type: 'PUT',
            dataType: 'json',
            data: {
                ID: id,
                Prenotabile: newPrenotabile
            }
        })
            .done(function (xhr) {
               // window.location.href = "Sale.html";
                window.location.reload();
            })
            .fail(function (xhr) {
                window.location.href = "Sale.html";
            })
    }


    static caricasale() {
        $.ajax({
            url: "http://localhost:53935/api/sala/getsale",
            type: 'GET',
            dataType: "json",
        })
            .done(function (json) {
                console.log("done");
                $('#DatiSale').empty()
                $.each(json, function (index, jsonObject) {
                    if (Object.keys(jsonObject).length > 0) {
                        var tableRow = '<tr>';
                        var id = jsonObject["ID"];
                        $.each(Object.keys(jsonObject), function (i, key) {
                            if (i == 0) {
                                tableRow += '<td>' + jsonObject[key] + '</td>';

                            }
                            else if (i == 3) {
                                if (jsonObject[key] == true) {
                                    var prenotabile = '<select class="" id="inputGroupSelectPrenotabile" onchange="Sala.UpdateSala(' + id + ')"><option value ="1" selected>Si</option><option  value ="0">No</option></select>'
                                }
                                else {
                                    var prenotabile = '<select class="" id="inputGroupSelectPrenotabile" onchange="Sala.UpdateSala(' + id + ')"><option value ="0" selected>No</option><option  value ="1">Si</option></select>'

                                }
                                tableRow += '<td>' + prenotabile + '</td>';
                            }
                            else {
                                tableRow += '<td>' + jsonObject[key] + '</td>';
                            }
                        });
                        tableRow += "</tr>";
                        $('#DatiSale').append(tableRow);
                    }

                });
            })
            .fail((xhr) => {
                console.log("error");
            });

    }


}
