// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var docAttr = document.documentElement;
var toggleBtn = document.getElementById("toggleMode");
const guiColor = getCookie('gui-color');


if (guiColor != null) {
    if (guiColor == "dark") {
        docAttr.setAttribute('data-theme', 'dark');
    } else if (guiColor == "light") {
        docAttr.setAttribute('data-theme', 'light');
    }
}


toggleBtn.onclick = function () {
    if (toggleBtn.innerHTML == "🌒") {
        toggleBtn.innerHTML = "🌖";
        docAttr.setAttribute('data-theme', 'dark');
        setCookie("gui-color", "dark", 9999);
    } else {
        toggleBtn.innerHTML = "🌒";
        docAttr.setAttribute('data-theme', 'light');
        setCookie("gui-color", "light", 9999);
    }
}


var wavesurfer01 = WaveSurfer.create({
    container: '#waveform01',
    waveColor: '#00a6ed',
    progressColor: '#31708f',
    height: 50,
    barWidth: 3
});

var wavesurfer02 = WaveSurfer.create({
    container: '#waveform02',
    waveColor: '#00a6ed',
    progressColor: '#31708f',
    height: 50,
    barWidth: 3
});

document.getElementById("playpause1").onclick = function () {
    wavesurfer01.playPause();
}

document.getElementById("playpause2").onclick = function () {
    wavesurfer02.playPause();
}

wavesurfer01.load('../musics/test01.mp3');
wavesurfer02.load('../musics/test01.mp3');


////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////


function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}



