
function CaricaRisorse() {

    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorse",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiRisorse').empty()
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            tableRow += '<td> <a href="DettaglioRisorsa.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += "</tr>";
                    $('#DatiRisorse').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });
};

$("#btnCercaRisorsaNome").click(function () {

    var nome = $("#inputCercaRisorsaNome").val();
    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorsenome/" + nome,
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiRisorse').empty()
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            tableRow += '<td> <a href="DettaglioRisorsa.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += "</tr>";
                    $('#DatiRisorse').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });

});

$("#btnCercaRisorsaCognome").click(function () {

    var cognome = $("#inputCercaRisorsaCognome").val();
    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorsecognome/" + cognome,
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
            console.log("done");
            $('#DatiRisorse').empty()
            $.each(json, function (index, jsonObject) {
                if (Object.keys(jsonObject).length > 0) {
                    var tableRow = '<tr>';
                    $.each(Object.keys(jsonObject), function (i, key) {
                        if (i == 0) {
                            tableRow += '<td> <a href="DettaglioRisorsa.html?id=' + jsonObject[key] + '"> ' + jsonObject[key] + ' (Dettagli)</a></td>';
                        }
                        else {
                            tableRow += '<td>' + jsonObject[key] + '</td>';
                        }
                    });
                    tableRow += "</tr>";
                    $('#DatiRisorse').append(tableRow);
                }

            });
        })
        .fail((xhr) => {
            console.log("error");
        });

});




class Risorsa {
    ID: Int16Array
    Nome: string
    Cognome: string
    Username: string
    email: string
    constructor(initialData) {
        if (initialData) {
            this.ID = initialData.Id;
            this.Nome = initialData.Nome;
            this.Cognome = initialData.Cognome;
            this.Username = initialData.Username;
            this.email = initialData.Mail;
        }
    }

}

