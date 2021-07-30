
function checkValidation() {

    email = document.getElementById("email").value
    password = document.getElementById("password").value

    $.ajax(
        {
            type: "GET",
            url: "/Home/CheckUserInformation?email=" + email + "&password=" + password,
            //data: {
            //    email: email,
            //    password: password
            //},
            //contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.dataStatus == true)
                    window.location.href = "/Home/HomePage"
                else
                    alert("Email or Password incorrect")
            },
            error: function (data) {
                alert(data)
            }

        });
}

function setReservationIformation(imageUrl, notes, name, creationDate) {
    document.getElementById("image").src = imageUrl;
    document.getElementById("notes").value = notes;
    document.getElementById("creationDate").innerHTML = "The creation Date Is = " + creationDate;
    document.getElementById("name").innerHTML = "The Name Of Reservation Is = " + name;
}

function enableButton(buttonValue) {
    document.getElementById("submitedBTN").disabled = false;
    document.getElementById("dropdownMenuButton1").value = buttonValue;
}

function sendReservationInformation() {

    CategoryName = document.getElementById("dropdownMenuButton1").innerHTML;
    TripName = document.getElementById("PropertDescription").value;

    $.ajax(
        {
            type: "POST",
            page: 1,
            rp: 6,
            data: {
                CategoryName: CategoryName,
                PropertDescription: PropertDescription
            },
            url: "/Home/addNewProperty",
            dataType: "json",
            success: function (result) {
                alert(result);
            }
        });
    window.location.reload(true);
}

