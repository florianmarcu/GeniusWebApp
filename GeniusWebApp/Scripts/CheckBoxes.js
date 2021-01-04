window.onload = function () {

    var checkboxes = document.getElementsByName("Visibility");
    var checkbox1 = checkboxes[0];
    var checkbox2 = checkboxes[1];

    console.log(checkbox1, checkbox2);

    checkbox1.onclick = function () {

        checkbox1.checked = true;
        checkbox2.checked = false;
    }

    checkbox2.onclick = function () {

        checkbox1.checked = false;
        checkbox2.checked = true;
    }
}