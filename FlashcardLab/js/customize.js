function showhideAdd() {
    var el = $("#addInfo")
    if (el.css("display") === "none") {
        el.css("display", "block");
        el.addClass("slide");
    }
    else {
        el.css("display", "none");
    }
}

function showhideAlter() {
    var el = $("#alterInfo")
    if (el.css("display") === "none") {
        el.css("display", "block");
        el.addClass("slide");
    }
    else {
        el.css("display", "none");
    }
}