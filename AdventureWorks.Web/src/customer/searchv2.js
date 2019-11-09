console.log("Hello");
document.getElementById("searchButton").addEventListener("click", function (e) {
    // vorige resultaten leegmaken.
    $("#customers tbody tr").remove();

    // keyword achterhalen. Indien leeg: geen query doen.
    let keyword = document.getElementById("keywordInput").value;
    if (keyword) {
        $.get("/customer/searchv2_json?keyword=" + keyword, function (result) {
            for (let i = 0; i < result.length; i++) {
                $("#customers").find('tbody').append(
                    '<tr><td>' + result[i].id +
                    '</td><td>' + result[i].firstName +
                    '</td><td>' + result[i].lastName +
                    '</td><td>' + result[i].email +
                    '</td><td><a href="/customer/details/' + result[i].id + '">Details</a></td></tr>');
            }
        });
    }
    e.preventDefault();
});
