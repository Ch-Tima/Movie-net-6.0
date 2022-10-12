const img = document.getElementById('ShowImg');


$(function () {
    GetAll();
    $("#listType").on("change", GetAll);

    //Add new
    $("#AddNew").on("click", AddNew);
    $("#SaveBtn").on("click", Save);
    $("#CancelBtn").on("click", Cancel);
});

//Get
function GetAll() {
    $.ajax({
        url: "../../api/Movie",
        type: "GET",
        dataType: "JSON",
        data: {
            type: $("#listType option:selected").text()
        },
        success: function (data) {
            $("#ListElements").html("");
            for (var i = 0; i < data.length; i++) {
                var temp = "<tr id='ID_" + data[i].id + "'>" +
                    "<td style='text-align: center;'><p>" + data[i].id + "</p></td>" +
                    "<td><p>" + data[i].name + "</p></td>" +
                    "<td style='text-align: center;'><p>" + data[i].evaluation + "</p></td>" +
                    "<td><input type='image' style='width:25px;' onmouseover='MouseOver(this)' onmouseout='MouseOut(this)' src='../../img/image.png'" +
                    "value='../../" + data[i].posterPath + "'></td>" +
                    "<td><button class=Delete onclick='Delete(" + data[i].id + ")'>Delete</button></td>" +
                    "</tr>";

                $("#ListElements").append(temp);
            }
        },
        error: function (err) {
            OpenMessage(err.responseText);// Display error message
        }
    });
}
function Get(id = -1) {
    $.ajax({
        url: "../../api/Movie/" + id,
        type: "GET",
        contentType: 'application/json',
        data: {
            type: $("#listType option:selected").text()
        },
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            OpenMessage(err.responseText);// Display error message
        }
    });
}

//Delete item
function Delete(id = -1) {

    $.ajax({
        url: "../../api/Movie/" + $("#listType option:selected").text() + "/" + id,
        type: "DELETE",
        contentType: 'application/json',
        success: function (res) {
            $("#ID_" + id).remove();
            OpenMessage(res);
        },
        /*        complete: function () {
                    //do something on complete
                },*/
        error: function (err) {
            OpenMessage(err.responseText);// Display error message
        }
    });
}

//AddNew
function AddNew() {

    if ($("#listType option:selected").text() == "Serial") {
        $("#Additional").append('<input type="number" step="1" id="eSeason" placeholder="Season" required />' +
            '<input type="number" step="1" id="eEpisode" placeholder="Episode" required />' +
            '<input type="checkbox" id="eCompleted" placeholder="Completed" required />');
    }
    $("#WinEditObj").css("display", "block");
}
function Save() {
    var obj = new FormData();
    obj.append('Name', $("#eName").val());
    obj.append('Evaluation', $("#eEvaluation").val());
    obj.append('Description', $("#eDescription").val());
    obj.append('file', $("#ePosterPath")[0].files[0]);

    if ($("#listType option:selected").text() == "Serial") {
        obj.append('Season', $("#eSeason").val());
        obj.append('Episode', $("#eEpisode").val());
        obj.append('Completed', $("#eCompleted").is(":checked"));
    }

    $.ajax({
        url: "../../api/Movie/" + $("#listType option:selected").text(),
        type: "POST",
        contentType: false,
        processData: false,
        data: obj,
        success: function (data) {
            console.log('Ok');
            GetAll();
        },
        error: function (err) {
            OpenMessage(err.responseText);
        }
    });

    Cancel();
}
function Cancel() {
    $("#Additional").html("");
    $("div[class=def]").find("input").val("");
    $("div[class=def]").find("textarea").val("")
    $("#WinEditObj").css("display", "none");
}


//see image
function MouseOver(argument) {

    var left = argument.offsetParent.offsetLeft;
    var top = argument.offsetParent.offsetTop + 35;
    img.style.cssText = 'display: block; left:' + left + 'px; top:' + top + 'px;';
    img.src = argument.value;

}
function MouseOut(argument) {
    img.style.cssText = 'display: none;'
    img.src = '';
}

//MessageBox
function OpenMessage(text) {
    var msgBox = document.getElementById('MessageBox');//div MessageBox
    var msg = document.getElementById('msg');//p msg

    msgBox.style = "display: block;";
    msg.textContent = text;
}
function CloseMessage() {
    var msgBox = document.getElementById('MessageBox');//div MessageBox
    msgBox.style = "display: none;";
}