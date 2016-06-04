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