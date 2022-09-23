const img = document.getElementById('ShowImg');


function MouseOver(argument) {

    var left = argument.offsetParent.offsetLeft;
    var top = argument.offsetParent.offsetTop + 45;
    img.style.cssText = 'display: block; left:' + left + 'px; top:' + top + 'px;';
    img.src = argument.value;

}
function MouseOut(argument) {
    img.style.cssText = 'display: none;'
    img.src = '';
}

function Delete(id, type) {

    $.ajax({
        url: "/Admin/Panel/Delete",
        data: {
            id: id,
            type: type
        },
        type: 'POST',
        success: function (res) {
            console.log(res);
            var t = document.getElementById("ID_" + id);
            if (t != null)
            {
                t.remove();
                OpenMessage(res);
            }
        },
        complete: function () {
            //do something on complete
        },
        error: function (err) {
            OpenMessage(err.responseText);// Display error message
        }
    });
}

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