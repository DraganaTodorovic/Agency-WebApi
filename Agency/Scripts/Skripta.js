$(document).ready(function () {

    //>>>>>>>>>>>>>>>>> Global variable initialization on start <<<<<<<<<<<<<<<<<<<<<<<<<
    var host = window.location.host;
    var token = null;
    var headers = {};
    var tabelaEndpoint = "/api/nekretnine";
    var dropDowEndpoint = "/api/agenti";
    var pretragaEndpoint = "/api/pretraga";

    var editingId;

    $("#logoutDiv").css("display", "none");
    $("#loginDiv").css("display", "none");
    $("#btnsPocetak").css("display", "none");
    $("#info_pocetak").css("display", "none");
    $("#search").addClass("hidden");
    $("#create").addClass("hidden");

    var tabelaUrl = "http://" + host + tabelaEndpoint;
    var dropDownUrl = "http://" + host + dropDowEndpoint;
    var pretragaUrl = "http://" + host + pretragaEndpoint;

    $.getJSON(tabelaUrl, setTabela);

    $("body").on("click", "#regBtn", loadLoginForm);
    $("body").on("click", "#pocetakBtn", loadStartPageFormPocetak);
    $("body").on("click", "#logoutBtn", loadStartPageFormOdjava);
    $("body").on("click", "#registrationBtn", registracijaKorisnika);
    $("body").on("click", "#loginBtn", prijavaKorisnika);
    $("body").on("click", "#findBtn", pretraga);
    $("body").on("click", "#btnDelete", deleteItemFromTabela);
    $("body").on("click", "#btnEdit", editItemFromTabela);
    $("body").on("click", "#giveUpBtn", editOdustanje);

    //>>>>>>>>>>> Load main entity(into table) <<<<<<<<<<<<<<<<<
    function setTabela(data, status) {
        console.log("Status: " + status);
        if (token) {
            var container = $("#data").css("width", "800px");
        }
        else {
            var container = $("#data").css("width", "1200px");
        }
        container.empty();

        if (status === "success") {
            console.log(data);

            var div = $("<div></div>");
            var h1 = $("<h2 class=\"text-center flex-center\">Nekretnine</h2>");
            div.append(h1);

            //var table = $("<table border='1' class=\"table table-hover\"></table>");
            var table = $("<table border='1' class=\"table table-bordered\"></table>");
            if (token) {
                var header = $("<tr style=\"background-color : yellow\"><th class=\"text-center\" style=\"width:150px\">Mesto</th><th class=\"text-center\" style=\"width:100px\">Oznaka</th><th class=\"text-center\" style=\"width:100px\">Izgradnja</th><th class=\"text-center\" style=\"width:100px\">Kvadratura</th><th class=\"text-center\" style=\"width:100px\">Cena</th><th class=\"text-center\" style=\"width:200px\">Agent</th><th style='width:100px' class=\"text-center\">Brisanje</th><th style='width:100px' class=\"text-center\">Izmena</th></tr>");
            }
            else {
                var header = $("<tr style=\"background-color : yellow\"><th class=\"text-center\" style=\"width:150px\">Mesto</th><th class=\"text-center\" style=\"width:100px\">Oznaka</th><th class=\"text-center\" style=\"width:100px\">Izgradnja</th><th class=\"text-center\" style=\"width:100px\">Kvadratura</th><th class=\"text-center\" style=\"width:100px\">Cena</th><th class=\"text-center\" style=\"width:200px\">Agent</th></tr>");
            }
            table.append(header);

            for (var i = 0; i < data.length; i++) {
                var row = "<tr>";
                var displayData = "<td>" + data[i].Mesto + "</td><td>" + data[i].AgencijskaOznaka + "</td><td>" + data[i].GodinaIzgradnje + "</td><td>" + data[i].Kvadratura + "</td><td>" + data[i].Cena + "</td><td>" + data[i].AgentImeiPrezime + "</td>";
                if (token) {
                    // prikaz dugmadi za izmenu i brisanje
                    var stringId = data[i].Id.toString();
                    var displayDelete = "<td><a href=\"#\" id=btnDelete name=" + stringId + ">[Obrisi]</a></td>";
                    var displayIzmena = "<td><a href=\"#\" id=btnEdit name=" + stringId + ">[Izmeni]</a></td>";
                    row += displayData + displayDelete + displayIzmena + "</tr>";
                }
                else {
                    row += displayData + "</tr>";
                }
                table.append(row);
            }

            div.append(table);
            container.append(div);
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja nekretnina!</h1>");
            div.append(h1);
            container.append(div);
        }
    }

    //>>>>>>>>>>>>>>>> Load 2nd entity into dropdown menu-create form <<<<<<<<<<<<<<<<<<
    function getDropDown(data, status) {
        var dropDownList = $("#createInput4select");
        dropDownList.empty();

        if (status === "success") {
            for (var i = 0; i < data.length; i++) {
                var option = "<option value=" + data[i].Id + ">" + data[i].ImeiPrezime + "</option>";
                dropDownList.append(option);
            }
            //$("#createInput4select").val($("#createInput4select > option:first").val());
        }
        else {
            var div = $("<div></div>");
            var h3 = $("<h3>Greška prilikom preuzimanja agenata!</h3>");
            div.append(h3);
            dropDownList.append(div);
        }
    }

    //>>>>>>>>>>> After clicking na button Registracija <<<<<<<<<<<
    function registracijaKorisnika() {
        var email = $("#loginEmail").val();
        var loz1 = $("#loginPass").val();
        var loz2 = $("#loginPass").val();

        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function () {
            $("#loginEmail").val('');
            $("#loginPass").val('');
            alert("Uspesna registracija na sistem!");

        }).fail(function () {
            alert("Greška prilikom registracije!");
        });
    }

    //>>>>>>>>>>> After clicking na button Prijava <<<<<<<<<<<
    function prijavaKorisnika() {
        var email = $("#loginEmail").val();
        var loz = $("#loginPass").val();

        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            token = data.access_token;

            $("#loginEmail").val('');
            $("#loginPass").val('');
            $("#loginDiv").css("display", "none");
            $("#loggedInParagraph").html("Prijavljen korisnik: <b>" + email + "</b>");
            $("#logoutDiv").css("display", "block");
            $("#btnsPocetak").css("display", "none");

            $("#data").css("display", "block");
            $("#search").removeClass("hidden");

            $.getJSON(tabelaUrl, setTabela);
            $.getJSON(dropDownUrl, getDropDown);

        }).fail(function () {
            alert("Greška prilikom prijave!");
        });
    }

    //>>>>>>>>>>>>>>>>>>>> Removing entry from table after clicking on button [Obrisi] <<<<<<<<<<<<<<<<<<<<<<<
    function deleteItemFromTabela() {
        var deleteId = this.name;
        httpAction = "DELETE";

        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        var deleteUrl = tabelaUrl + "?id=" + deleteId;
        $.ajax({
            "url": deleteUrl,
            "type": httpAction,
            "headers": headers
        })
            .done(function (data, status) {
                $.getJSON(tabelaUrl, setTabela);
            })
            .fail(function (data, status) {
                alert("Greska prilikom brisanja nekretnine!")
            })
    }

    //>>>>>>>>>>>>>>>>>>>>>> After clicking on button Pretrazi <<<<<<<<<<<<<<<<<<<<<<<<<
    function pretraga() {
        var start = $("#findInput1").val();
        var kraj = $("#findInput2").val();
        httpAction = "POST";

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var pretragaUrlFull = pretragaUrl + "/?mini=" + start + "&maksi=" + kraj;
        $.ajax({
            "url": pretragaUrlFull,
            "type": httpAction,
            "headers": headers
        })
            .done(setTabela)
            .fail(function () {
                alert("Greska prilikom pretrage!");
            });

        $("#findInput2").val('');
        $("#findInput1").val('');
    }

    //>>>>>>>>>>>>>>  After clicking on button Izmeni <<<<<<<<<<<<<<<<<<<<<<<<<
    $("#create").submit(function (e) {
        e.preventDefault();

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var oznaka = $("#createInput1").val();
        var godina = $("#createInput2").val();
        var mesto = $("#createInput3").val();
        var agent = $("#createInput4select").val();
        var kvadratura = $("#createInput5").val();
        var cena = $("#createInput6").val();
        $("#validationMsgInput1").empty();
        $("#validationMsgInput2").empty();
        $("#validationMsgInput3").empty();
        $("#validationMsgInput5").empty();
        $("#validationMsgInput6").empty();

        var dataCreate = {
            "Id": editingId,
            "Mesto": mesto,
            "AgencijskaOznaka": oznaka,
            "GodinaIzgradnje": godina,
            "Kvadratura": kvadratura,
            "Cena": cena,
            "AgentId": agent
        }
        httpAction = "PUT";

        $.ajax({
            "url": tabelaUrl + "/" + editingId.toString(),
            "type": httpAction,
            "data": dataCreate,
            "headers": headers
        })
            .done(function (data, status) {
                $.getJSON(tabelaUrl, setTabela);
                editOdustanje();
            })
            .fail(function (data, status) {
                //validation();
                alert("Greska prilikom editovanja!");
            })

    });

    //>>>>>>>>>>>>>>>>>>>> Editing entry from table after clicking on button [Izmeni] <<<<<<<<<<<<<<<<<<<<<<<
    function editItemFromTabela() {
        // izvlacimo id
        var editId = this.name;
        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        // prikazi formu za editovanje
        $("#create").removeClass("hidden");

        // saljemo zahtev da dobavimo taj proizvod
        $.ajax({
            url: tabelaUrl + "/" + editId.toString(),
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                $("#createInput1").val(data.AgencijskaOznaka);
                $("#createInput2").val(data.GodinaIzgradnje);
                $("#createInput3").val(data.Mesto);

                $("#createInput5").val(data.Kvadratura);
                $("#createInput6").val(data.Cena);
                $("#createInput4select").val(data.AgentId);
                editingId = data.Id;
                formAction = "Update";
            })
            .fail(function (data, status) {
                formAction = "Create";
                alert("Desila se greska prilikom editovanja!");
                //validation();
            });
    }

    //>>>>>>>> Load login form <<<<<<<<<<
    function loadLoginForm() {
        $("#info_begin").css("display", "none");
        $("#info_pocetak").css("display", "block");
        $("#loginDiv").css("display", "block");
        $("#btns").addClass("hidden");
        $("#btnsPocetak").css("display", "block");
    }

    //>>>>>>>> Load Start Page form after clicking on button Pocetak <<<<<<<<<<
    function loadStartPageFormPocetak() {
        $("#info_begin").css("display", "block");
        $("#info_pocetak").css("display", "none");
        $("#loginDiv").css("display", "none");
        $("#btns").removeClass("hidden");
        $("#btnsPocetak").css("display", "none");
        $.getJSON(tabelaUrl, setTabela);
    }

    //>>>>>>>> Load Start Page form after clicking on button Odjava <<<<<<<<<<
    function loadStartPageFormOdjava() {
        token = null;
        headers = {};
        $("#loggedInParagraph").empty();
        $("#logoutDiv").css("display", "none");
        $("#btns").removeClass("hidden");
        $("#search").addClass("hidden");
        $("#create").addClass("hidden");
        $("#info_begin").css("display", "block");
        $("#info_pocetak").css("display", "none");
        $.getJSON(tabelaUrl, setTabela);
    }

    function editOdustanje() {
        $("#createInput1").val('');
        $("#createInput2").val('');
        $("#createInput3").val('');
        $("#createInput4select").val('');
        $("#createInput5").val('');
        $("#createInput6").val('');
        $("#create").addClass("hidden");
    }

});
