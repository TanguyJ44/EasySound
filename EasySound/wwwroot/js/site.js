// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var docAttr = document.documentElement;
var toggleBtn = document.getElementById("toggleMode");


toggleBtn.onclick = function () {
    if (toggleBtn.innerHTML == "🌒") {
        toggleBtn.innerHTML = "🌖";
        docAttr.setAttribute('data-theme', 'dark');
    } else {
        toggleBtn.innerHTML = "🌒";
        docAttr.setAttribute('data-theme', 'light');
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