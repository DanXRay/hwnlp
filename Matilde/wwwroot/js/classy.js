
function buttonPressed() {
    var titley = $("#formTitle").val();
    var titleDivName = "#resultTitle";
    $(titleDivName).empty();
    var bodyDivName = "#resultBody";
    $(bodyDivName).empty();
    var bodyey = $("#formBody").val();
    bodyey = bodyey.trim();
    doForDivs(titleDivName, titley, bodyDivName, bodyey);

    //all done now return with focus on title field
    document.getElementById("formTitle").focus();
}

function doForDivs(titleDivName, title, bodyDivName, body) {
    $("#resultStatus").text("   ...   ");

    var url = "./inquery/classify?user=classy&title=" + encodeURIComponent(title) + "&body=" + encodeURIComponent(body);
    $.getJSON(url, function (result) {

        $("#resultStatus").text("  analyze  ");

        //$("#inquery").text("");
        AddToDiv(titleDivName, result.title);

        if (result.theBod) {
            for (var i = 0; i < result.theBod.length; i++) {
                AddToDiv(bodyDivName, result.theBod[i]);
            }
        }

        //if ($("#resultAccumulator p").length > 4) {
        //    $("#resultAccumulator").empty();
        //}
        //$("#resultAccumulator").empty();
        //$("#startUpStuff").empty();
        // $("#resultAccumulator").append("<p>" + query + "</p>");
        // $("#resultAccumulator").append("<p style=\"color:orange;\">" + testme.response + "</p>");

    });

}

function AddToDiv(divName, item) {
    var itemConfidence = item.confidence;
    var conceptHtml = "";
    if (item.concepts) {
        for (var property in item.concepts) {
            if (item.concepts.hasOwnProperty(property)) {
                conceptHtml = conceptHtml + "&nbsp;concept: " + item.concepts[property];
            }
        }
        //conceptHtml = item.concepts;
    }
    //$(divName).prepend("<p>" + item.query + "</p>");
    $(divName).append("<p>" + item.query + "</p>");
    $(divName).append("<p style=\"color:orange;\">confidence:" + itemConfidence + conceptHtml + "</p>");

}

document.getElementById('funForm').addEventListener('submit', function (e) {
    buttonPressed();
    e.preventDefault();
}, false);

//$(document).keydown(function (e) {
//    switch (e.which) {
//        case 13:
//            buttonPressed();
//            break;
//    }

//});
