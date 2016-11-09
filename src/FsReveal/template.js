import 'mathjax';
import '../../paket-files/fsprojects/reveal.js/css/reveal.css';
import '../../paket-files/fsprojects/reveal.js/lib/css/zenburn.css';
import './style.css';
import './deedle.css';
import './tips.js';
//import 'file?name=[name].[ext]!./favicon.ico';
import '!file?name=[name].[ext]!./manifest.json';

// Reveal stuff
import 'expose?Reveal!imports?define=>null!../../paket-files/fsprojects/reveal.js/js/reveal.js';
import '!file?name=plugin/markdown/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/markdown/marked.js';
import '!file?name=plugin/markdown/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/markdown/markdown.js';
import '!file?name=plugin/zoom-js/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/zoom-js/zoom.js';
import '!file?name=plugin/notes/[name].[ext]!../../paket-files/fsprojects/reveal.js/plugin/notes/notes.js';
import hljs from 'highlight.js';

// If the query includes 'print-pdf', include the PDF print sheet
if (window.location.search.match( /print-pdf/gi)) {
  require('../../paket-files/fsprojects/reveal.js/css/reveal.css');
}

function init() {
  const websocket = new WebSocket(`ws://${window.location.host}/websocket`);
  websocket.onmessage = function(evt) { location.reload(); };
}
window.addEventListener("load", init, false);

// Add the nohighlight class and data-noescape attribute to code elements that have already been formatted by FSharp.Formatting
document.querySelectorAll('pre.highlighted code').forEach(el => {
  el.classList.add('nohighlight');
  el.setAttribute('data-noescape', '');
});

function reveal() {
  hljs.initHighlightingOnLoad(); // from expose?hljs

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