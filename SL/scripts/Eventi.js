function CaricaEventi() {
    $.ajax({
        url: "http://localhost:53935/api/evento/GetEventi",
        type: 'GET',
        dataType: "json",
    })
        .done(function (json) {
        console.log("done");
        $.each(json, function (index, jsonObject) {
            if (Object.keys(jsonObject).length > 0) {
                var tableRow = '<tr>';
                $.each(Object.keys(jsonObject), function (i, key) {
                    tableRow += '<td>' + jsonObject[key] + '</td>';
                });
                tableRow += "</tr>";
                $('#DatiEventi').append(tableRow);
            }
        });
    })
        .fail(function (xhr) {
        console.log("error");
    });
}
;
function nuovoEvento() {
}
//# sourceMappingURL=Eventi.js.map