//根據DOM元素的id構造出一個編輯器
const editor = CodeMirror.fromTextArea(document.getElementById("code"), {
    mode: {
        name: "python",
        version: 3,
        singleLineStringErrors: false
    },
    lineNumbers: true,
    theme: "seti",
    fullScreen: true,    //全屏模式
    //keyMap: "vim",    //綁定Vim
});

let online = $.connection.codeServiceHub;
let cursor = { line: 0, ch: 0 };
let cid = document.getElementById("cid").value;
let mid = document.getElementById("mid").value;
let gid = document.getElementById("gid").value;

$.connection.hub.start().done(function () {
    editor.setValue("");
    editor.setCursor(cursor);
    online.server.readCode(cid, mid, gid);
});

editor.on('change', (ins, ch) => {

    cursor.line = editor.getCursor().line;
    cursor.ch = editor.getCursor().ch;


    if (ch.origin == "+input" || ch.origin == "+delete") {
        online.server.editCode(document.getElementById("gid").value, editor.getValue(), cursor.line, cursor.ch);
    }

});

online.client.broadcastCode = function (content, line, ch) {

    editor.setValue(content);
    editor.setCursor(line, ch);
}

document.getElementById("saveCode").addEventListener("click", saveCode);

function saveCode() {
    let codeContent = editor.getValue();

    console.log("cid：" + cid + ", \nmid：" + mid + ", \ngid：" + gid + ", \ncode：" + code);

    let model = { "cid": cid, "mid": mid, "gid": gid, "codeContent": codeContent, "IsEdit" : false};
    online.server.saveCode(model);
}
