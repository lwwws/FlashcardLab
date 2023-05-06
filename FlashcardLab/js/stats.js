function deckStats() {
    var weekdays = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    var counts = $('input[id$=dataWeek]').val().split(",");
    var barColors = ["#DB5375", "#FC9E4F", "#DFBE99", "#B5BD89", "#9FBBCC", "#729EA1", "#6A5D7B"];

    const d = new Date();
    var deck = $('input[id$=deckName]').val();

    barColors[d.getDay()] = "#FFFFFF";
    weekdays[d.getDay()] += " (TODAY)";

    new Chart("deckWeek", {
        type: "bar",
        data: {
            labels: weekdays,
            datasets: [{
                backgroundColor: barColors,
                data: counts
            }]
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: "Review cards for the week (" + deck + ")."
            }
        }
    });

    var titles = ["Still learning", "In Review", "Finished"];
    var progress = $('input[id$=dataLRF]').val().split(",");
    barColors = ["#FFD275", "#7189FF", "#72B01D"];

    new Chart("lrfDeck", {
        type: "doughnut",
        data: {
            labels: titles,
            datasets: [{
                backgroundColor: barColors,
                data: progress
            }]
        },
        options: {
            title: {
                display: true,
                text: deck + "'s review/learn progress"
            }
        }
    });

    var titlesPie = ["Correct", "Incorrect"];
    var corIncor = $('input[id$=dataCorIncor]').val().split(",");
    barColors = ["#6DA84D", "#FB3523"];


    new Chart("iccDeck", {
        type: "pie",
        data: {
            labels: titlesPie,
            datasets: [{
                backgroundColor: barColors,
                data: corIncor
            }]
        },
        options: {
            title: {
                display: true,
                text: "Total Correct/Incorrect in the " + deck + " deck."
            }
        }
    });

    var dataProg = $('input[id$=dataProgress]').val();
    var el = document.getElementById("pbar");
    el.setAttribute("aria-valuenow", dataProg.slice(0, 5));
    el.setAttribute("style", "width:" + dataProg.slice(0, 5) + "%");
    el.innerHTML = dataProg.slice(0, 5) + "%";
}