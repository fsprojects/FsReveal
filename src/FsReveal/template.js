//import 'mathjax'; // currently pretty broken
import '../../paket-files/fsprojects/reveal.js/css/reveal.css';
import '../../paket-files/fsprojects/reveal.js/lib/css/zenburn.css';
import '../../paket-files/fsprojects/reveal.js/css/theme/night.css';
import './fonts.css';
import './style.css';
import './deedle.css';
import { ToolTips } from './tips.js';
//import 'file?name=[name].[ext]!./favicon.ico';
import '!file?name=[name].[ext]!./manifest.json';

// Reveal stuff
import Reveal from 'imports?define=>null!../../paket-files/fsprojects/reveal.js/js/reveal.js';
import '!file?name=plugin/markdown/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/markdown/marked.js';
import '!file?name=plugin/markdown/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/markdown/markdown.js';
import '!file?name=plugin/zoom-js/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/zoom-js/zoom.js';
import '!file?name=plugin/notes/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/notes/notes.js';
import hljs from 'highlight.js';

// If the query includes 'print-pdf', include the PDF print sheet
if (window.location.search.match( /print-pdf/gi)) {
  require('../../paket-files/fsprojects/reveal.js/css/reveal.css');
}

// legacy tips.js file:
var currentTip = null;
var currentTipElement = null;

function hideTip(evt, name, unique) {
    var el = document.getElementById(name);
    el.style.display = "none";
    currentTip = null;
}

function hideUsingEsc(e) {
    if (!e) { e = event; }
    hideTip(e, currentTipElement, currentTip);
}

function showTip(evt, name, unique, owner) {
    document.onkeydown = hideUsingEsc;
    if (currentTip == unique) return;
    currentTip = unique;
    currentTipElement = name;

    var target = owner ? owner : (evt.srcElement ? evt.srcElement : evt.target);
    var posx = target.offsetLeft;
    var posy = target.offsetTop + target.offsetHeight;

    var el = document.getElementById(name);
    var parent = target.offsetParent;
    el.style.position = "absolute";
    el.style.left = posx + "px";
    el.style.top = posy + "px";
    parent.appendChild(el);
    el.style.display = "block";
}
// end legacy file tips.js

function init() {
  const websocket = new WebSocket(`ws://${window.location.host}/websocket`);
  websocket.onmessage = function(evt) { location.reload(); };
}
window.addEventListener("load", init, false);

// Add the nohighlight class and data-noescape attribute to code elements that have already been formatted by FSharp.Formatting
[].forEach.call(document.querySelectorAll('pre.highlighted code'), el => {
  el.classList.add('nohighlight');
  el.setAttribute('data-noescape', '');
});

function reveal() {
  hljs.initHighlightingOnLoad();

  // Full list of configuration options available here:
  // https://github.com/hakimel/reveal.js#configuration
  Reveal.initialize({
    controls: true,
    progress: true,
    history: true,
    center: true,
    transition: 'default', // default/cube/page/concave/zoom/linear/fade/none
    // Parallax scrolling
    // parallaxBackgroundImage: 'https://s3.amazonaws.com/hakim-static/reveal-js/reveal-parallax-1.jpg',
    // parallaxBackgroundSize: '2100px 900px',
  });
}
window.addEventListener("load", reveal, false);

if (process.env.NODE_ENV === 'production') {
  require('offline-plugin/runtime').install(); // eslint-disable-line global-require
}