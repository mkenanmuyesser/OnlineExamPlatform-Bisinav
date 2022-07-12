var tick;

function stop() {
    clearTimeout(tick);
}

function clock() {
    var ut = new Date();
    var d, m, y, h, m, s;
    var date = "        ";
    var time = "        ";

    d = ut.getDate();
    mo = ut.getMonth();
    y = ut.getFullYear();
    h = ut.getHours();
    m = ut.getMinutes();
    s = ut.getSeconds();

    if (d <= 9) d = "0" + d;
    if (mo <= 9) mo = "0" + mo;
    if (s <= 9) s = "0" + s;
    if (m <= 9) m = "0" + m;
    if (h <= 9) h = "0" + h;


    date += d + "." + mo + "." + y;
    time += h + ":" + m + ":" + s;
    document.getElementById('clock').innerHTML = date + " " + time;
    tick = setTimeout("clock()", 1000);
}