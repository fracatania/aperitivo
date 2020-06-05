function CaricaSale() {
    $.ajax({
        url: "http://localhost:53935/api/risorsa/getrisorse",
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
                $('#DatiRisorse').append(tableRow);
            }
        });
    })
        .fail(function (xhr) {
        console.log("error");
    });
}
;
//# sourceMappingURL=Sale.js.map