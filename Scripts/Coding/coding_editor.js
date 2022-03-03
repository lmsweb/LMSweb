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
    keyMap: "vim",    //綁定Vim
});

let online = $.connection.codeServiceHub;
//online.client.hello = function () {
//    editor.setValue("print(Hello);");
//}
console.log(document.getElementById("gid").value);


let oldContent = "";
let newContent = "";
let cursor = { line: 0, ch: 0 };

$.connection.hub.start().done(function () {
    editor.setValue("");
    editor.setCursor(cursor);
});

editor.on('change', (ins, ch) => {

    cursor.line = editor.getCursor().line;
    cursor.ch = editor.getCursor().ch;


    if (ch.origin == "+input" || ch.origin == "+delete") {
        online.server.editCode(editor.getValue(), cursor.line, cursor.ch);
    }

});


online.client.broadcastCode = function (content, line , ch) {

    editor.setValue(content);
    editor.setCursor(line, ch);
}