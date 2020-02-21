
//var inquery = document.getElementById("inquery");
//var theUserId = document.getElementById("userId");

$(document).keyup(function (e) {
    switch (e.which) {
        case 13:
            enterPressed();
            break;
        case 8:
            var textToTruncate = $("#inquery").text();
            if (textToTruncate.length > 0) {
                $("#inquery").text(textToTruncate.substring(0, textToTruncate.length - 1));
            }
            break;
        case 190:
        case 191:
            var whatItMightHaveBeen = $("#inquery").text();
            if (whatItMightHaveBeen != undefined) {
                $("#inquery").text(whatItMightHaveBeen + e.key);
            }
            break;
        default:
            var sk = String.fromCharCode(e.which);
            var s = e.key;
            if (sk.match(/[a-zA-Z0-9~@\.\s]/)) {
                var whatItHasBeen = $("#inquery").text();
                if (whatItHasBeen) {
                    $("#inquery").text(whatItHasBeen + e.key);
                } else {
                    $("#inquery").text(e.key);
                }
            } 
    }

});

function enterPressed() {
    var query = $("#inquery").text();
    var user = $("#userId").text().substring(3);
    var theResultO = document.getElementById("resultO");
    $("#resultO").text("...");
    //$("#inquery").text(""); http://localhost:63070/
    //https://hwmatilda.azurewebsites.net/inquery?user=
    var url = "./inquery?user=" + user + "&query=" + encodeURIComponent(query);
    $.getJSON(url, function (result) {
        $("#resultO").text("");
        $("#inquery").text("");
        var testme = JSON.parse(result);
        if ($("#resultAccumulator p").length > 4)
        {
            $("#resultAccumulator").empty();
        }
        //$("#resultAccumulator").empty();
        $("#startUpStuff").empty();
        $("#resultAccumulator").append("<p>" + query + "</p>");
        $("#resultAccumulator").append("<p style=\"color:orange;\">" + testme.response + "</p>");

    });
}

